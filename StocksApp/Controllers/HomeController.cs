using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;
using System.Threading.Tasks;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _FinnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        public HomeController(FinnhubService myService, IOptions<TradingOptions> tradingOptions)
        {
            _FinnhubService = myService;
            _tradingOptions = tradingOptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }
            Dictionary<string, object>? responseDictinary = await _FinnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictinary["c"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictinary["h"].ToString()),
                LowestPrice = Convert.ToDouble(responseDictinary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictinary["o"].ToString())
            };

            return View(stock);
        }
    }
}
