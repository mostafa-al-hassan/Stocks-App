using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using Newtonsoft.Json.Linq;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.FinnhubService
{
    public class FinnhubSearchStocksService : IFinnhubSearchStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;


        public FinnhubSearchStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }


        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            //invoke repository
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);

            //return response dictionary back to the caller
            return responseDictionary;
        }
    }
}
