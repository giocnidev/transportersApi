using BusinessLogic;
using Entities.Dtos;
using Entities.Interfaces.Repositories;
using Moq;
using Repositories;
using Repositories.SQLite;

namespace Tests
{
    [TestClass]
    public class UnitTestContainers
    {
        [TestMethod]
        public void TestSelectContainersSucces1()
        {
            Mock<IContainerRepository> containerRepository = new();
            ContainerBL containerBl = new(containerRepository.Object);

            double budget = 100;
            List<ContainerDto>? containers = new() {
                new ContainerDto(){
                    Name = "C1",
                    ContainerPrice = 1000,
                    TransportCost = 30
                },new ContainerDto(){
                    Name = "C2",
                    ContainerPrice = 500,
                    TransportCost = 90
                },new ContainerDto(){
                    Name = "C3",
                    ContainerPrice = 200,
                    TransportCost = 50
                }
            };

            string?[] expected = new string?[] { "C1", "C3" };

            ResponseDto<string?[]> result = containerBl.SelectContainers(budget, containers);
            Assert.IsTrue(result?.Data?.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestSelectContainersSucces2()
        {
            Mock<IContainerRepository> containerRepository = new();
            ContainerBL containerBl = new(containerRepository.Object);

            double budget = 1508.65;
            List<ContainerDto>? containers = new() {
                new ContainerDto(){
                    Name = "C1",
                    ContainerPrice = 4744.03,
                    TransportCost = 571.40
                },new ContainerDto(){
                    Name = "C2",
                    ContainerPrice = 3579.07,
                    TransportCost = 537.33
                },new ContainerDto(){
                    Name = "C3",
                    ContainerPrice = 1379.26,
                    TransportCost = 434.66
                },new ContainerDto(){
                    Name = "C4",
                    ContainerPrice = 1700.12,
                    TransportCost = 347.28
                },new ContainerDto(){
                    Name = "C5",
                    ContainerPrice = 1434.80,
                    TransportCost = 264.54
                }
            };

            string?[] expected = new string?[] { "C1", "C2", "C4" };

            ResponseDto<string?[]> result = containerBl.SelectContainers(budget, containers);
            Assert.IsTrue(result?.Data?.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestSelectContainersSucces3()
        {
            Mock<IContainerRepository> containerRepository = new();
            ContainerBL containerBl = new(containerRepository.Object);

            double budget = 100;
            List<ContainerDto>? containers = new() {
                new ContainerDto(){
                    Name = "C1",
                    ContainerPrice = 1200,
                    TransportCost = 90
                },new ContainerDto(){
                    Name = "C2",
                    ContainerPrice = 1200,
                    TransportCost = 20
                },new ContainerDto(){
                    Name = "C3",
                    ContainerPrice = 0,
                    TransportCost = 10
                }
            };

            string?[] expected = new string?[] { "C2" };

            ResponseDto<string?[]> result = containerBl.SelectContainers(budget, containers);
            Assert.IsTrue(result?.Data?.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestSelectContainersError()
        {
            Mock<IContainerRepository> containerRepository = new();
            ContainerBL containerBl = new(containerRepository.Object);

            double budget = 100;
            List<ContainerDto>? containers = new() {
                new ContainerDto(){
                    Name = "C1",
                    ContainerPrice = 1200,
                    TransportCost = 90
                },new ContainerDto(){
                    Name = "C2",
                    ContainerPrice = 1200,
                    TransportCost = 20
                },new ContainerDto(){
                    Name = "C3",
                    ContainerPrice = 0,
                    TransportCost = 10
                }
            };

            string?[] expected = new string?[] { "C2", "C3" };

            ResponseDto<string?[]> result = containerBl.SelectContainers(budget, containers);
            Assert.IsFalse(result?.Data?.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestSelectContainersErrorBudget1()
        {
            Mock<IContainerRepository> containerRepository = new();
            ContainerBL containerBl = new(containerRepository.Object);

            double budget = 40;
            List<ContainerDto>? containers = new() {
                new ContainerDto(){
                    Name = "C1",
                    ContainerPrice = 1200,
                    TransportCost = 90
                },new ContainerDto(){
                    Name = "C2",
                    ContainerPrice = 1200,
                    TransportCost = 50
                }
            };

            int expected = 103;

            ResponseDto<string?[]> result = containerBl.SelectContainers(budget, containers);
            Assert.AreEqual(expected,result.Code);
        }

        [TestMethod]
        public void TestSelectContainersErrorBudget2()
        {
            Mock<IContainerRepository> containerRepository = new();
            ContainerBL containerBl = new(containerRepository.Object);

            double budget = 0;
            List<ContainerDto>? containers = new() {
                new ContainerDto(){
                    Name = "C1",
                    ContainerPrice = 1200,
                    TransportCost = 90
                },new ContainerDto(){
                    Name = "C2",
                    ContainerPrice = 1200,
                    TransportCost = 50
                }
            };

            int expected = 101;

            ResponseDto<string?[]> result = containerBl.SelectContainers(budget, containers);
            Assert.AreEqual(expected, result.Code);
        }
    }
}