using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts.DTO;
using StocksApp.Models;
using StocksApp;
using System.Diagnostics;
using System.Text.Json;
using ServiceContracts.FinnhubService;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly TradingOptions _tradingOptions;

        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubSearchStocksService _finnhubSearchStocksService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;
        private readonly IFinnhubStocksService _finnhubStocksService;

        private readonly ILogger<StocksController> _logger;

        public StocksController(IOptions<TradingOptions> tradingOptions, IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubSearchStocksService finnhubSearchStocksService, IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService, IFinnhubStocksService finnhubStocksService, ILogger<StocksController> logger)
        {
            _tradingOptions = tradingOptions.Value;

            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubSearchStocksService = finnhubSearchStocksService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;
            _finnhubStocksService = finnhubStocksService;

            _logger = logger;
        }


        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            //logger
            _logger.LogInformation("In StocksController.Explore() action method");
            _logger.LogDebug("stock: {stock}, showAll: {showAll}", stock, showAll);

            //get company profile from API server
            List<Dictionary<string, string>>? stocksDictionary = await _finnhubStocksService.GetStocks();

            List<Stock> stocks = new List<Stock>();

            if (stocksDictionary is not null)
            {
                //filter stocks
                if (!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(",");
                    if (Top25PopularStocksList is not null)
                    {
                        stocksDictionary = stocksDictionary
                         .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
                         .ToList();
                    }
                }

                //convert dictionary objects into Stock objects
                stocks = stocksDictionary
                 .Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
                .ToList();
            }

            ViewBag.stock = stock;
            return View(stocks);
        }
    }
}