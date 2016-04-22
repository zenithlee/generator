using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
  class StockMarket
  {
    public string URL = "http://www.google.com/finance/info?";
    //q=NSE:AAPL,AAPL,
    string Result = "";


    /*   
   // 
   [
   {
   "id": "22144"
   ,"t" : "AAPL"
   ,"e" : "NASDAQ"
   ,"l" : "105.97"
   ,"l_fix" : "105.97"
   ,"l_cur" : "105.97"
   ,"s": "2"
   ,"ltt":"4:00PM EDT"
   ,"lt" : "Apr 21, 4:00PM EDT"
   ,"lt_dts" : "2016-04-21T16:00:01Z"
   ,"c" : "-1.16"
   ,"c_fix" : "-1.16"
   ,"cp" : "-1.08"
   ,"cp_fix" : "-1.08"
   ,"ccol" : "chr"
   ,"pcls_fix" : "107.13"
   ,"el": "105.20"
   ,"el_fix": "105.20"
   ,"el_cur": "105.20"
   ,"elt" : "Apr 21, 8:00PM EDT"
   ,"ec" : "-0.77"
   ,"ec_fix" : "-0.77"
   ,"ecp" : "-0.73"
   ,"ecp_fix" : "-0.73"
   ,"eccol" : "chr"
   ,"div" : "0.52"
   ,"yld" : "1.96"
   }
   ]
   */

      string CompanyFromCode(string s)
    {
      switch( s)
      {
        case "AAPL":
          return "Apple";
          break;
      }
      return "";
    }

    public string CreateReport(StockClassObject o)
    {
      string s = "";
      if (o == null) return "No data";
      s += CompanyFromCode(o.t) + " reported ";
      s += " closing at " + o.l;
      return s;
    }

    public StockClassObject GetStockInfo(string Stock)
    {
      Result = MakeWebRequest(URL, Stock);
      Result = Result.Replace("//", "");
      Result = Result.Trim();
      JsonSerializerSettings set = new JsonSerializerSettings();      
      StockClassObject[] o = JsonConvert.DeserializeObject<StockClassObject[]>(Result);      
      return o[0];
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
      Result = "";

      while (sLine != null)
      {
        i++;
        sLine = objReader.ReadLine();
        if (sLine != null)
        {
          Result += sLine;
        }
      }
      return Result;
    }
  }
}
