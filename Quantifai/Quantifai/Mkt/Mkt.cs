using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

//planet money
//tim ferris 
//thinking fast and slow

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
    string DataStorePath = "../../../Data/Stocks/GoogleAPI/Requests/";
    string Portfolio = "SXE";
    List<Stk> Stks = new List<Stk>();
    string Period = "60d"; //or "1Y";
    //string URL = "http://www.google.com/finance/getprices?q=GOOG&x=NASD&i=86400&p=40Y&f=d,c,v,k,o,h,l&df=cpct&auto=0&ei=Ef6XUYDfCqSTiAKEMg";
    string FullURL = "http://www.google.com/finance/getprices?q={stock}&x=NASD&i=86400&p={period}&f=d,c,v,k,o,h,l&df=cpct&auto=0&ei=Ef6XUYDfCqSTiAKEMg";
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
     // CheckPath();
    }

    public void CheckPath()
    {
      string FullPath = DataStorePath;
      if (!Directory.Exists(FullPath))
      {
        Directory.CreateDirectory(FullPath);
      }
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
      // Unix timestamp is seconds past epoch
      System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
      dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
      return dtDateTime;
    }

    //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    public void GetHistoricData(string Stock)
    {
      string URL = FullURL.Replace("{stock}", Stock);
      URL = URL.Replace("{period}", Period);

      string Result = MakeWebRequest(URL, Stock);
      Result = Result.Replace("//", "");
      Result = Result.Trim();
      
      string[] lines = Result.Split('\n');
      CheckPath();
      File.WriteAllLines(DataStorePath + Stock + ".txt", lines);
      //JsonSerializerSettings set = new JsonSerializerSettings();
      //StockClassObject[] o = JsonConvert.DeserializeObject<StockClassObject[]>(Result);
      //return o[0];

      Stk std = new Stk();
      std.sName = Stock;
      Stks.Add(std);
      DateTime CurrentTime = new DateTime();
      DateTime at = new DateTime();      
      foreach ( string line in lines)
      {
        string[] items = line.Split(',');

        if (items[0].StartsWith("EXCHANGE")) continue;
        if (items[0].StartsWith("MARKET_OPEN_MINUTE")) continue;
        if (items[0].StartsWith("MARKET_CLOSE_MINUTE")) continue;
        if (items[0].StartsWith("INTERVAL")) continue;
        if (items[0].StartsWith("COLUMNS")) continue;
        if (items[0].StartsWith("DATA")) continue;
        if (items[0].StartsWith("TIMEZONE_OFFSET")) continue;
        
        if (items[0].StartsWith("a")){
          string unix = items[0].Replace("a", "");
          double db = Double.Parse(unix);
          CurrentTime = UnixTimeStampToDateTime(db);
          at = CurrentTime;
        }
        else {
          int n = int.Parse(items[0]);
          at = CurrentTime.AddDays(n);
        }
       
        double c = Double.Parse(items[1]);
        double h = Double.Parse(items[2]);
        double l = Double.Parse(items[3]);
        double o = Double.Parse(items[4]);
        std.AddPoint(at, o,c,h,l);
      }
    }

    public void Clear(Chart c)
    {
      foreach( Series s in c.Series)
      {
        s.Points.Clear();
      }

      c.Series.Clear();
     // c.ChartAreas.Clear();
      
    }

    public void PlotTo( Chart c)
    {
      c.Series.Clear();

      foreach( Stk s in Stks )
      {
        s.PlotTo(c);
      }
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
