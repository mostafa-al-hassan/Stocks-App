using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using Newtonsoft.Json.Linq;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.FinnhubService
{
    public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
    {
        private readonly IFinnhubRepository _finnhubRepository;


        public FinnhubStockPriceQuoteService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            //invoke repository
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);

            //return response dictionary back to the caller
            return responseDictionary;
        }

    }
}
