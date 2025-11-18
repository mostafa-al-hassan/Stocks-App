using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Tests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;

        public StocksServiceTest()
        {

            _stocksService = new StocksService(new StockMarketDbContext(new DbContextOptionsBuilder<StockMarketDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options));
        }


        #region GetBuyOrders
        //By default it should return an Empty list 
        [Fact]
        public async Task GetBuyOrders_EmptyListDefaultAsync()
        {
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
            Assert.Empty(buyOrders);
        }

        // if there is requests they should be returened 
        [Fact]
        public async Task GetAllBuyOrders_WithBuyOrders_ToBeSuccessfulAsync()
        {
            //Arrange

            //Create a list of buy orders 
            BuyOrderRequest buyOrder_request_1 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            BuyOrderRequest buyOrder_request_2 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            List<BuyOrderRequest> buyOrder_requests = new List<BuyOrderRequest>() { buyOrder_request_1, buyOrder_request_2 };

            List<BuyOrderResponse> buyOrder_response_list_from_add = new List<BuyOrderResponse>();

            foreach (BuyOrderRequest buyOrder_request in buyOrder_requests)
            {
                BuyOrderResponse buyOrder_response = await _stocksService.CreateBuyOrder(buyOrder_request);
                buyOrder_response_list_from_add.Add(buyOrder_response);
            }

            //Act
            List<BuyOrderResponse> buyOrders_list_from_get = await _stocksService.GetBuyOrders();


            //Assert
            foreach (BuyOrderResponse buyOrder_response_from_add in buyOrder_response_list_from_add)
            {
                Assert.Contains(buyOrder_response_from_add, buyOrders_list_from_get);
            }
        }
        #endregion

        #region CreateBuyOrder 

        //if BuyOrder is null then throw ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
        {
            BuyOrderRequest? buyOrderRequest = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        [Theory] //[Theory] to be able to pass parameters using InlineData to the test method
        [InlineData(100001)] // the data to be passed
        // if Quantity is greater than maximum, throw argument exception
        public async Task CreateBuyOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        [Theory] //[Theory] to be able to pass parameters using InlineData to the test method
        [InlineData(0)] // the data to be passed
        // if Quantity is Less than minimum, throw argument exception
        public async Task CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentExceptionAsync(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        // I can add more validations per property but I made a validation Helper class which has a static method that throws an Argument exception if any of the properties invalidated its condition.

        [Fact]
        public async Task CreateBuyOrder_ValidData_ToBeSuccessfulAsync()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"), Price = 1, Quantity = 1 };

            //Act
            BuyOrderResponse buyOrderResponseFromCreate = await _stocksService.CreateBuyOrder(buyOrderRequest);

            //Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponseFromCreate.BuyOrderID);
        }


        #endregion


        #region CreateSellOrder

        [Fact]
        public async Task CreateSellOrder_NullSellOrder_ToBeArgumentNullExceptionAsync()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = null;

            //Act
            await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateSellOrder(sellOrderRequest));

        }

        // I can add validations per property but I made a validation Helper class which has a static method that throws an Argument exception if any of the properties invalidated its condition.

        [Fact]
        public async Task CreateSellOrder_ValidData_ToBeSuccessfulAsync()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"), Price = 1, Quantity = 1 };

            //Act
            SellOrderResponse sellOrderResponseFromCreate = await _stocksService.CreateSellOrder(sellOrderRequest);

            //Assert
            Assert.NotEqual(Guid.Empty, sellOrderResponseFromCreate.SellOrderID);
        }
        #endregion


        #region GetSellOrders

        //The GetAllSellOrders() should return an empty list by default
        [Fact]
        public async Task GetAllSellOrders_DefaultList_ToBeEmptyAsync()
        {
            //Act
            List<SellOrderResponse> sellOrdersFromGet = await _stocksService.GetSellOrders();

            //Assert
            Assert.Empty(sellOrdersFromGet);
        }

        // if there is requests they should be returened 
        [Fact]
        public async Task GetAllSellOrders_WithSellOrders_ToBeSuccessful()
        {
            //Arrange

            //Create a list of sell orders 
            SellOrderRequest sellOrder_request_1 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            SellOrderRequest sellOrder_request_2 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            List<SellOrderRequest> sellOrder_requests = new List<SellOrderRequest>() { sellOrder_request_1, sellOrder_request_2 };

            List<SellOrderResponse> sellOrder_response_list_from_add = new List<SellOrderResponse>();

            foreach (SellOrderRequest sellOrder_request in sellOrder_requests)
            {
                SellOrderResponse sellOrder_response = await _stocksService.CreateSellOrder(sellOrder_request);
                sellOrder_response_list_from_add.Add(sellOrder_response);
            }

            //Act
            List<SellOrderResponse> sellOrders_list_from_get = await _stocksService.GetSellOrders();


            //Assert
            foreach (SellOrderResponse sellOrder_response_from_add in sellOrder_response_list_from_add)
            {
                Assert.Contains(sellOrder_response_from_add, sellOrders_list_from_get);
            }
        }

        #endregion
    }
}
