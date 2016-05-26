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

    public void PlotTo(Chart c)
    {
      Series s = c.Series.Add(sName);
      s.ChartType = SeriesChartType.Candlestick;
      s.XValueType = ChartValueType.DateTime;
      s.XAxisType = AxisType.Primary;    
      DateTime minDate = new DateTime(2016, 01, 01);
      DateTime maxDate = DateTime.Now;
      c.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
      c.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();

      double min = 99999;
      double max = 0;

      foreach ( Tick t in TickList) {
        object[] f = new object[] { t.H, t.L, t.O, t.C };
        object date = t.time.ToOADate();
        s.Points.AddXY(t.time, f);

        min = Math.Min(t.H, min);
        max = Math.Max(t.H, max);
      }

      //int min = (int)c.ChartAreas[0].AxisY.Minimum;
      //int max = (int)c.ChartAreas[0].AxisY.Maximum;

     // c.ChartAreas[0].AxisY.Minimum = min;
      //c.ChartAreas[0].AxisY.Maximum = max;      

    }
  }
}
