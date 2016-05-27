using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Parameters;
using Quantifai.Database;
using MySql.Data.MySqlClient;

namespace Quantifai
{
  public partial class SXE : UserControl
  {
    MySQL db = new MySQL();
    DataTable Values = new DataTable("Quantifai"); 
    Mkt Market = new Mkt("../../../Data/Stocks/GoogleAPI/Requests/");

    public SXE()
    {
      InitializeComponent();
      Values.Columns.Add("Date");
      Values.Columns.Add("Text");
      Values.Columns.Add("Q Score");      
    }

    public void Setup()
    {
      Auth.SetUserCredentials("Cjb5edejG9GaW2k6JlyfgYyPx", "m1aZ9GpbbRuwEjFxLjWwFg9eLd2tzf4STK9mWZYSvl4GdUyPH6", "721031630807265280-S5TisYo9MPII6VMiJ2F5pSC56aysWcH", "iF1jHQuUOPMAToVn2sXX8dnUDqJ3yJjk5u9fvYVe0PtPB");
    }

    void GetStocks(string sStock)
    {
      Series series = null;
      StockChart.Series.Clear();

      Series y4 = StockChart.Series.Add("Forecast2");
      y4.ChartType = SeriesChartType.Range;

      Series y2 = StockChart.Series.Add("Average");
      y2.ChartType = SeriesChartType.Line;

      Series y3 = StockChart.Series.Add("Forecast");
      y3.ChartType = SeriesChartType.Line;      

      if ( StockChart.Series.IndexOf(sStock)<0) {
        series = StockChart.Series.Add(sStock);
        series.XValueType = ChartValueType.DateTime;
        series.ChartType = SeriesChartType.Column;
      }

      //string Query = "SELECT * FROM sentijvbwg_db2.sl_twitter WHERE text LIKE '%" + sStock + "%' ORDER BY datecreated LIMIT 0,100";
      MySqlDataReader Reader = db.GetLatestFor(sStock, 200);

      Dictionary<string,string[]> data = new Dictionary<string, string[]>();
      Values.BeginLoadData();
      while (Reader.Read())
      {         
        string text = db.GetDBString("text", Reader);
        string myDate = db.GetDBString("datecreated", Reader);
        DateTime dateValue = DateTime.Parse(myDate);        
        double sentiment = db.GetDBDouble("sentiment", Reader);
        StockChart.Series[sStock].Points.AddXY(dateValue.ToOADate(), sentiment);

        object[] newRow = new object[3];
        newRow[0] = myDate;
        newRow[1] = text;
        newRow[2] = sentiment.ToString();
        Values.LoadDataRow(newRow,true);
      }
      Values.EndLoadData();

      Reader.Close();
      db.Close();

      int count = StockChart.Series[sStock].Points.Count ;
      if (count > 1)
      {
        StockChart.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, count.ToString(), sStock + ":Y", "Average:Y");
        //StockChart.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "5", sStock + ":Y", "Forecast:Y");
        StockChart.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "2,40,true,true", sStock + ":Y", "Forecast:Y,Forecast2:Y,Forecast2:Y2");
      }

      StockLister.DataSource = Values;
      StockLister.Columns["text"].Width = 300;
    }

    private void btnTest_Click(object sender, EventArgs e)
    {
      //call the service to get the thing
      string stock = StockText.Text;
      GetStocks(stock);
    }

    private void btn_Test_Click(object sender, EventArgs e)
    {
      string stock = StockText.Text;
      Market.GetHistoricData(stock);
      Market.PlotTo(StockChart);
    }

    private void StockChart_AxisViewChanged(object sender, ViewEventArgs e)
    {
      if (e.Axis.AxisName == AxisName.X)
      {
       
      }
    }  
  }
}
