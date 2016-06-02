using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Quantifai
{
  class Stk
  {
    public string sName = "Demo";
    List<Tick> TickList = new List<Tick>();

    public void CreateDummData()
    {
      Random r = new Random();
      for (int i=0; i<100; i++)
      {
        Tick t = new Tick();
        t.time = DateTime.UtcNow;
        t.H = r.NextDouble()* 100;
        t.L = t.H - r.NextDouble() * 20;
        t.O = r.NextDouble() * 100;
        t.C = r.NextDouble() * 100;
      }
    }

    public void AddPoint( DateTime time, double O,double C, double H, double L)
    {
      Tick t =new Tick();
      t.time = time;
      t.H = H;
      t.L = L;
      t.O = O;
      t.C = C;
      TickList.Add(t);
    }

    /*
    foreach (ChartArea area in chart1.ChartAreas)
    {
      List<double> allNumbers = new List<double>();

      foreach (Series item in chart1.Series)
        if (item.ChartArea == area.Name)
          allNumbers.AddRange(item.Points.Where((x, i) => i >= start && i <= end).Select(x => x.YValues[0]).ToList());

      double ymin = allNumbers.Min();
    double ymax = allNumbers.Max();

      if (ymax > ymin)
      {
        double offset = 0.02 * (ymax - ymin);
    area.AxisY.Maximum = ymax + offset;
        area.AxisY.Minimum = ymin - offset;
      }
}
*/

//TODO: Rescale chart
public void ReScale(Chart c)
    {
      double miny = double.MaxValue;
      double maxy = double.MinValue;

      foreach( Series s in c.Series) { 
      DataPointCollection p = s.Points;
      
      foreach( DataPoint dp in p)
      {
        miny = Math.Min(miny, dp.YValues[0]);
        maxy = Math.Max(maxy, dp.YValues[0]);
      }
      }

      c.ChartAreas[0].AxisY.Minimum = miny;
      c.ChartAreas[0].AxisY.Maximum = maxy;
      
    }

    public void PlotTo(Chart c)
    {      
      Series s;
      if (c.Series.IndexOf(sName)>-1)
      {
        s = c.Series[sName];
      }
      else
      {
        s = c.Series.Add(sName);
      }
      
      s.ChartType = SeriesChartType.Candlestick;
      s.XValueType = ChartValueType.DateTime;
      s.XAxisType = AxisType.Primary;
      //DateTime minDate = new DateTime(2016, 01, 01);
      //DateTime maxDate = DateTime.Now;
      //c.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
      //c.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();      

      foreach (Tick t in TickList) {
        object[] f = new object[] { t.H, t.L, t.O, t.C };
        object date = t.time.ToOADate();
        s.Points.AddXY(t.time, f);
      }

      ReScale(c);
    }
  }
}
