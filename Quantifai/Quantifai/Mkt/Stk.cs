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
    string sName = "Demo";
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

    public void PlotTo(Chart c)
    {
      Series s = c.Series.Add(sName);
      s.XValueType = ChartValueType.DateTime;
      s.ChartType = SeriesChartType.Stock;      
      foreach ( Tick t in TickList) {
        s.Points.AddXY(t.time.ToOADate(), new { t.H, t.L, t.O, t.C });        
      }
    }
  }
}
