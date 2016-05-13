using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Generator
{
  class StockMarket
  {

    string PortfolioPath = "../../Data/Portfolio/";
    List<String> Portfolio = new List<string>();
    Chart MarketChart;
    string StockFolder = "../../Data/mine/stocks/";

    public string URL = "http://www.google.com/finance/info?";
    //http://finance.google.com/finance/info?client=ig&q=NASDAQ:GOOG
    //http://www.google.com/finance/getprices?q=GOOG&x=NASD&i=86400&p=40Y&f=d,c,v,k,o,h,l&df=cpct&auto=0&ei=Ef6XUYDfCqSTiAKEMg
    //q=NSE:AAPL,AAPL,

//    q - Stock symbol
//    x - Stock exchange symbol on which stock is traded(ex: NASD)
//    i - Interval size in seconds(86400 = 1 day intervals)
//    p - Period. (A number followed by a "d" or "Y", eg.Days or years.Ex: 40Y = 40 years.)
//    f - What data do you want? d(date - timestamp/interval, c - close, v - volume, etc...) Note: Column order may not match what you specify here
//    df - ??
//    auto - ??
//    ei - ??
//    ts - Starting timestamp(Unix format). If blank, it uses today.
//    http://www.google.com/finance/getprices?q=GOOG&x=NASD&i=86400&p=40Y&f=d,c,v,k,o,h,l&df=cpct&auto=0&ei=Ef6XUYDfCqSTiAKEMg

    string Result = "";


    /*   
   // 
   [
   {
   "id": "22144"
   ,"t" : "AAPL"
   ,"e" : "NASDAQ"
   ,"l" : "105.97" //latest
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
   ,"el": "105.20" //after hours price
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

      Starbucks Corp. shares SBUX, -3.27% slid 4.7% in after-hours trade Thursday, after the company posted weaker-than-expected sales for its second fiscal quarter. Starbucks said it had net income of $575.1 million, or 39 cents a share, compared with $494.9 million, or 33 cents a share, in the year-earlier period. Revenue rose to $4.99 billion from $4.56 billion. The FactSet consensus was for EPS of 39 cents and revenue of $5.03 billion. Same-store sales rose 6%, below the FactSet consensus of 6.5%. Looking ahead, the company said it expects third-quarter EPS of 48 cents to 49 cents, compared with the current consensus of 49 cents. The board has agreed to add an additional 100 million shares to the company's share buyback authorization. Shares are up 1% in the year so far, while the S&P 500 has gained about 2%.

   */

    public void SetGraph(Chart inMarketChart)
    {
      MarketChart = inMarketChart;
    }


    public void Analyse_Stocks()
    {
      foreach( string s in Portfolio) { 
        StockClassObject o = GetStockInfo(s.ToUpper());        
        PlotTo(o, MarketChart);
        AddToMine(o.t, o.e, o.lt_dts, o.el, o.l, o.c, o.yld);
      }
    }

    public void AddToPortfolio(string s)
    {
      Portfolio.Add(s);
      SavePortfolio();
    }

    public void RemoveFromPortfolio(string s)
    {
      Portfolio.Remove(s);
      SavePortfolio();
    }

    public void SavePortfolio()
    {
      if (!Directory.Exists(PortfolioPath))
      {
        Directory.CreateDirectory(PortfolioPath);
      }
      File.WriteAllLines(PortfolioPath + "portfolio.txt", Portfolio);
    }

    public List<string> LoadPortfolio()
    {
      string sPort = PortfolioPath + "portfolio.txt";
      if ( !File.Exists(sPort))
      {
        return null;
      }
      Portfolio = new List<string>(File.ReadAllLines(sPort));
      return Portfolio;
    }

    string CompanyFromCode(string s)
    {
      switch(s)
      {
        case "AAPL":
          return "Apple";          
        case "MSFT":
          return "Microsoft";                  
      }
      return "";
    }

    public string CreateReport(StockClassObject o)
    {
      string s = "";
      if (o == null) return "No data";

      s += "~B~H1T~" + o.t;
      s += CompanyFromCode(o.t) + " reported on the " + o.e + " stock exchance today";
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

    public void AddToMine(string stock, string exchange, string time, string low, string high, string open, string close)
    {      
      if ( !Directory.Exists(StockFolder)) { 
        Directory.CreateDirectory(StockFolder);
      }
      File.AppendAllText(StockFolder + stock + ".txt", "" + exchange + "," + time + "," + low + "," + high + "," + open + "," + close + "\r\n");
    }

    public void PlotTo(StockClassObject so, Chart MarketChart)
    {
      if (MarketChart.Series.IndexOf(so.t)<0)
      {
        MarketChart.BorderlineColor = System.Drawing.Color.Gray;

        Series sm = MarketChart.Series.Add(so.t + "_mean");
        sm.ChartType = SeriesChartType.Line;

        Series s = MarketChart.Series.Add(so.t);
        s.ChartType = SeriesChartType.Candlestick;
        s.Label = so.t;
        s.MarkerStyle = MarkerStyle.Circle;
        s.Color = sm.Color;
        s.LabelAngle = 45;
        
        //MarketChart.Titles.Add(so.t);
        //MarketChart.Titles.Add(so.e);
        s.MarkerSize = 1;
        s.MarkerStep = 20;
        s.MarkerStyle = MarkerStyle.Diamond;
        s.IsValueShownAsLabel = true;        
      }

      //low high open close
      double p1 = Convert.ToDouble(so.el);
      double p2 = Convert.ToDouble(so.l);
      double p3 = Convert.ToDouble(so.el) + Convert.ToDouble(so.cp);
      double p4 = Convert.ToDouble(so.l) - Convert.ToDouble(so.cp);
      DataPointCollection col = MarketChart.Series[so.t + "_mean"].Points;
     
      List<float> values = new List<float>();
      foreach( DataPoint d in col)
      {
        values.Add((float)d.YValues[0]);
      }

      double mean = Quantify.Mean(values);
      
      MarketChart.Series[so.t+"_mean"].Points.AddY(new object[] { mean });
      MarketChart.Series[so.t].Points.AddY(new object[] { p1, p2, p3, p4 });
    }
  }

  
}
