using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

//planet money
//tim ferris 

/*
The following table shows the suffix codes that can be used with span ID:
Suffix for span ID  Quote returned
_l
Last price traded
_c
Change since close
_cp
Change percentage
_vo
Volume
_e
Stock exchange(NYSE, AMEX, or NASDAQ)
_s
Symbol
_t
Ticker
_op
Open
_hi
Highest price today(intraday high)
_lo
Lowest price today(intraday low)
_lt
Last trade date and time
_el
Last price(extended hours)
_evo
Volume(extended hours)
_ec
Change(extended hours)
_ecp
Change percentage(extended hours)
_elt
Last trade time(extended hours)
*/

namespace Quantifai
{
  class Mkt
  {
    string DataStorePath = "../../Data/Stocks/GoogleAPI/Requests/";
    List<Stk> Stks = new List<Stk>();
    //string URL = "http://www.google.com/finance/getprices?q=GOOG&x=NASD&i=86400&p=40Y&f=d,c,v,k,o,h,l&df=cpct&auto=0&ei=Ef6XUYDfCqSTiAKEMg";
    string FullURL = "http://www.google.com/finance/getprices?q={stock}&x=NASD&i=86400&p=40Y&f=d,c,v,k,o,h,l&df=cpct&auto=0&ei=Ef6XUYDfCqSTiAKEMg";
    public string SingleURL = "http://www.google.com/finance/info?";

    public void GetData(string Stock)
    {
      //public StockClassObject GetStockInfo(string Stock)
      //string URL = FullURL.Replace("{stock}", Stock);
      string Result = MakeWebRequest(SingleURL, Stock);
      Result = Result.Replace("//", "");
      Result = Result.Trim();
      JsonSerializerSettings set = new JsonSerializerSettings();
      StockClassObject[] o = JsonConvert.DeserializeObject<StockClassObject[]>(Result);
      //return o[0];
   }

    public Mkt(string inDataStorePath)
    {
      DataStorePath = inDataStorePath;
      CheckPath();
    }

    public void CheckPath()
    {
      string FullPath = DataStorePath;
      if (!Directory.Exists(FullPath))
      {
        Directory.CreateDirectory(FullPath);
      }
    }

    public void GetHistoricData(string Stock)
    {
      string URL = FullURL.Replace("{stock}", Stock);
      string Result = MakeWebRequest(URL, Stock);
      Result = Result.Replace("//", "");
      Result = Result.Trim();
      
      string[] lines = Result.Split('\n');
      CheckPath();
      File.WriteAllLines(DataStorePath + Stock + ".txt", lines);
      //JsonSerializerSettings set = new JsonSerializerSettings();
      //StockClassObject[] o = JsonConvert.DeserializeObject<StockClassObject[]>(Result);
      //return o[0];
    }

    public string MakeWebRequest(string sURL, string Stock)
    {
      WebRequest r = WebRequest.Create(sURL + "q=" + Stock);

      //WebProxy myProxy = new WebProxy("myproxy", 80);
      //myProxy.BypassProxyOnLocal = true;
      //wrGETURL.Proxy = myProxy;
      //wrGETURL.Proxy = WebProxy.GetDefaultProxy();
      Stream objStream;
      objStream = r.GetResponse().GetResponseStream();
      StreamReader objReader = new StreamReader(objStream);

      string sLine = "";
      int i = 0;
      string Result = "";

      while (sLine != null)
      {
        i++;
        sLine = objReader.ReadLine();
        if (sLine != null)
        {
          Result += sLine + "\n";
        }
      }
      return Result;
    }
  }
}
