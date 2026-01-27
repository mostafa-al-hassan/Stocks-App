using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.Arm;

namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubCompanyProfileService
    {
        /// <summary>
        /// creates a connection and then gets a Company Profile Response Dictionary from Finnhub.io
        /// </summary>
        /// <param name="stockSymbol">the Company stockSymbol you want stock info about </param>
        /// <returns></returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);        
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



