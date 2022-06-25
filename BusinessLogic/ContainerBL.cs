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

        public ContainerBL(IContainerRepository usersRepository) {
            _containerRepository = usersRepository;
        }

        public ResponseDto<string?[]> SelectContainers(double budget, List<ContainerDto>? data){
            Stopwatch watch = Stopwatch.StartNew();
            ResponseDto<string?[]> response = new ResponseDto<string?[]>();
            try{
                response = ValidateSelectContainers(budget, data);
                if (response.Code != 0 || data == null) { 
                    return response;
                }

                double kpi = 0;
                int containerListSize = data.Count;
                List<ContainerDto> selectedContainers = new List<ContainerDto>();
                Dictionary<string, double> calculatedBudget = new Dictionary<string, double>();
                Dictionary<string, double> calculatedKPI = new Dictionary<string, double>();

                for (int index = 0; index < containerListSize - 1; index++) {
                    if (data[index].TransportCost > budget) continue;

                    List<ContainerDto> possibleContainers = new List<ContainerDto>() { data[index] };
                    for (int indexNext = (index + 1); indexNext < containerListSize; indexNext++){
                        if (data[indexNext].TransportCost > budget) continue;

                        possibleContainers.Add(data[indexNext]);
                        if (GetCalculatedBudget(possibleContainers, calculatedBudget) > budget) {
                            //se quita el ultimo posible contenedor, ya que se supera el presupuesto
                            possibleContainers.RemoveAt(possibleContainers.Count - 1);

                            double currentKpi = GetCalculatedKPI(possibleContainers, calculatedKPI);
                            if (currentKpi > kpi) {
                                kpi = currentKpi;
                                selectedContainers.Clear();
                                selectedContainers.AddRange(possibleContainers);
                            }
                        }
                    }
                }

                if (selectedContainers.Count == 0) {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Code = 103;
                    response.Message = "No es posible despachar con el presupuesto indicado.";
                    return response;
                }

                var noSelectedContainers = data.Where(s => selectedContainers.All(ns => ns.Name != s.Name));

                Stats? stats = _containerRepository.GetStats();
                if (stats == null) {
                    stats = new Stats();
                }
                stats.ContainersNotDispatched += noSelectedContainers.Sum(t => t.ContainerPrice);
                stats.ContainersDispatched += selectedContainers.Sum(t => t.ContainerPrice);
                stats.BudgetUsed += selectedContainers.Sum(t => t.TransportCost);
                _containerRepository.UpdateStats(stats);

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

        private double GetCalculatedBudget(List<ContainerDto> possibleContainers, Dictionary<string, double> calculatedBudget) {
            string key = GenerateKey(possibleContainers);
            if (!calculatedBudget.TryGetValue(key, out double budgetTotal)){
                budgetTotal = possibleContainers.Sum(t => t.TransportCost);
                calculatedBudget.Add(key, budgetTotal);
            }
            return budgetTotal;
        }

        private double GetCalculatedKPI(List<ContainerDto> possibleContainers, Dictionary<string, double> calculatedKPI){
            string key = GenerateKey(possibleContainers);
            if (!calculatedKPI.TryGetValue(key, out double kpiTotal)){
                kpiTotal = possibleContainers.Sum(t => t.ContainerPrice);
                calculatedKPI.Add(key, kpiTotal);
            }
            return kpiTotal;
        }

        public ResponseDto<Stats> GetStats(){
            ResponseDto<Stats> response = new ResponseDto<Stats>();
            try{
                response.Data = _containerRepository.GetStats(); ;
            }catch (Exception ex){
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Code = 500;
                response.Message = "Ups!!, ha ocurrido un error: " + ex.Message;
            }
            return response;
        }
    }
}