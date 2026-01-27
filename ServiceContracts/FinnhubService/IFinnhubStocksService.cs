using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.Arm;

namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubStocksService
    {
        /// <summary>
        /// Returns list of all stocks supported by an exchange (default: US)
        /// </summary>
        /// <returns>List of stocks</returns>
        Task<List<Dictionary<string, string>>?> GetStocks();
    }
}






// Quote Response Attributes
//c
//Current price

//d
//Change

//dp
//Percent change

//h
//High price of the day

//l
//Low price of the day

//o
//Open price of the day

//pc
//Previous close price



//Company Profile Response Attributes 

//country

//Country of company's headquarter.


//currency

//Currency used in company filings.


//exchange

//Listed exchange.


//finnhubIndustry

//Finnhub industry classification.


//ipo

//IPO date.


//logo

//Logo image.


//marketCapitalization

//Market Capitalization.


//name

//Company name.


//phone

//Company phone number.


//shareOutstanding

//Number of oustanding shares.


//ticker

//Company symbol/ticker as used on the listed exchange.


//weburl

//Company website.


//Reference: https://finnhub.io/docs/api/company-profile2



