using Entities.Database;
using Entities.Dtos;
using Entities.Interfaces.BusinessLogic;
using Entities.Interfaces.Repositories;
using System.Diagnostics;
using System.Net;

namespace BusinessLogic
{
    public class ContainerBL : IContainerBL
    {
        private readonly IContainerRepository _containerRepository;
        private Dictionary<string, double> budgetDictionary = new();
        private Dictionary<string, double> kpiDictionary = new();

        public ContainerBL(IContainerRepository usersRepository) {
            _containerRepository = usersRepository;
        }

        private List<QueueDto> InitQueue(int index, ContainerDto container) {
            QueueDto queue = new(data: new() { container }){
                Index = index
            };

            return new() { queue };
        }

        public ResponseDto<string?[]> SelectContainers(double budget, List<ContainerDto>? data){
            Stopwatch watch = Stopwatch.StartNew();
            ResponseDto<string?[]> response = new ResponseDto<string?[]>();
            try{
                response = ValidateSelectContainers(budget, data);
                if (response.Code != 0 || data == null) { 
                    return response;
                }

                double bestKpi = 0;
                double bestBudget = 0;
                int containerListSize = data.Count;
                List<ContainerDto> selectedContainers = new();

                for (int index = 0; index < containerListSize; index++) {
                    if (data[index].TransportCost > budget) continue;

                    List<QueueDto> queueList = InitQueue(index, data[index]);

                    int indexQueue = 0;
                    while (indexQueue < queueList.Count) {
                        QueueDto queueItem = queueList[indexQueue];
                        int lastIndex = queueItem.Index + 1;

                        for (int indexNext = lastIndex; indexNext < containerListSize; indexNext++){
                            if (data[indexNext].TransportCost > budget) continue;

                            List<ContainerDto> possibleContainers = queueItem.Data.Append(data[indexNext]).ToList();
                            if (GetCalculatedBudget(possibleContainers) <= budget){
                                queueList.Add(
                                    new(data: possibleContainers) { 
                                        Index = indexNext
                                    }
                                );
                            }
                        }
                        indexQueue++;
                    }

                    //Buscar en la cola de posibles despachos, con el mejor KPI y menor uso del presupuesto
                    foreach (QueueDto queueItem in queueList){
                        double currentKpi = GetCalculatedKPI(queueItem.Data);
                        double currentBudget = GetCalculatedBudget(queueItem.Data);
                        if ((currentKpi > bestKpi) || (currentKpi == bestKpi && (currentBudget < bestBudget || bestBudget == 0))){
                            bestKpi = currentKpi;
                            bestBudget = currentBudget;
                            selectedContainers = queueItem.Data;
                        }
                    }
                }

                if (selectedContainers.Count == 0) {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Code = 103;
                    response.Message = "No es posible despachar con el presupuesto indicado.";
                    return response;
                }

                List<ContainerDto> noSelectedContainers = data.Where(s => selectedContainers.All(ns => ns.Name != s.Name)).ToList();
                SaveStats(selectedContainers, noSelectedContainers);

                response.Data = selectedContainers.Select(t => t.Name).ToArray();
            }catch (Exception ex) {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Code = 500;
                response.Message = "Ups!!, ha ocurrido un error: " + ex.Message;
            }
            watch.Stop();
            response.TimeElapsed = String.Format("{0} ms", watch.ElapsedMilliseconds);
            return response;
        }

        private void SaveStats(List<ContainerDto> selectedContainers, List<ContainerDto> noSelectedContainers) {
            Stats? stats = _containerRepository.GetStats();
            if (stats == null){
                stats = new Stats();
            }
            stats.ContainersNotDispatched += noSelectedContainers.Sum(t => t.ContainerPrice);
            stats.ContainersDispatched += selectedContainers.Sum(t => t.ContainerPrice);
            stats.BudgetUsed += selectedContainers.Sum(t => t.TransportCost);
            _containerRepository.UpdateStats(stats);
        }

        private ResponseDto<string?[]> ValidateSelectContainers(double budget, List<ContainerDto>? data) {
            ResponseDto<string?[]> response = new ResponseDto<string?[]>();

            if (budget == 0){
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Code = 101;
                response.Message = "Se debe proporcionar un presupuesto válido.";
                return response;
            }

            if (data == null || data.Count == 0){
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Code = 102;
                response.Message = "Se debe proporcionar un listado de contenedores válido.";
                return response;
            }

            return response;
        }

        private string GenerateKey(List<ContainerDto> data) {
            return String.Join("-", data.Select(t => t.Name));
        }

        private double GetCalculatedBudget(List<ContainerDto> possibleContainers) {
            string key = GenerateKey(possibleContainers);
            if (!budgetDictionary.TryGetValue(key, out double budgetTotal)){
                budgetTotal = possibleContainers.Sum(t => t.TransportCost);
                budgetDictionary.Add(key, budgetTotal);
            }
            return budgetTotal;
        }

        private double GetCalculatedKPI(List<ContainerDto> possibleContainers){
            string key = GenerateKey(possibleContainers);
            if (!kpiDictionary.TryGetValue(key, out double kpiTotal)){
                kpiTotal = possibleContainers.Sum(t => t.ContainerPrice);
                kpiDictionary.Add(key, kpiTotal);
            }
            return kpiTotal;
        }

        public ResponseDto<StatsDto> GetStats(){
            ResponseDto<StatsDto> response = new ResponseDto<StatsDto>();
            try{
                Stats? stats = _containerRepository.GetStats();
                if (stats == null) {
                    //Se inicializa con valores en 0
                    stats = new();
                }

                response.Data = StatsDto.FromEntity(stats);
            } catch (Exception ex){
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Code = 500;
                response.Message = "Ups!!, ha ocurrido un error: " + ex.Message;
            }
            return response;
        }
    }
}