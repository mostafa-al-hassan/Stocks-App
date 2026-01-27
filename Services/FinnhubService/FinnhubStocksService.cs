using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using Newtonsoft.Json.Linq;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.FinnhubService
{
    public class FinnhubStocksService : IFinnhubStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            //invoke repository
            List<Dictionary<string, string>>? responseDictionary = await _finnhubRepository.GetStocks();

            //return response dictionary back to the caller
            return responseDictionary;
        }

    }
}
