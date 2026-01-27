using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.FinnhubService;

namespace StocksApp.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;

        public SelectedStockViewComponent( IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService)
        {
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? companyProfileDict = null;

            if (stockSymbol != null)
            {
                companyProfileDict = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
                var stockPriceDict = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);
                if (stockPriceDict != null && companyProfileDict != null)
                {
                    companyProfileDict.Add("price", stockPriceDict["c"]);
                }
            }

            if (companyProfileDict != null && companyProfileDict.ContainsKey("logo"))
                return View(companyProfileDict);
            else
                return Content($"No data for {stockSymbol}");
        }
    }
}
