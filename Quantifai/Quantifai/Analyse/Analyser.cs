using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Quantifai.Analyse
{
  public partial class Analyser : UserControl
  {
    SentimentAnalyser sentiment = new SentimentAnalyser();
    string[] RegularWords = { "", "~", "~I", "of~the", "on~the", " ", ".", "the", "and", "to", "of", "is", "then", "that", "not", "in", "are", "will", "be", "on", "for", "a", "it" };
    bool Ascending = true;

    public Analyser()
    {
      InitializeComponent();      
    }

    private void btnAnalyseOpen_Click(object sender, EventArgs e)
    {      
      openFileDialog1.ShowDialog();
      AnalyseFile(openFileDialog1.FileName);
    }

    void AnalyseFile(String sFile)
    {
      string[] lines = File.ReadAllLines(sFile);
      List<string> analines = new List<string>() ;

      List<float> items = new List<float>();

      List<float> zero= new List<float>();
      List<float> posi= new List<float>();
      List<float> negi= new List<float>();

      foreach ( string line in lines)
      {
        //Console.WriteLine(line);
        string[] words = line.Split(';');
        float f = sentiment.AnalyseSentence(line);
        if (f == 0) zero.Add(f);
        if (f > 0) posi.Add(f);
        if (f < 0) negi.Add(f);
        items.Add(f);
        analines.Add(words[1]);
        string[] subs = new string[] { words[1], f.ToString() };
        ListViewItem li = new ListViewItem(subs);
        SentimentView.Items.Add(li);
      }

      chart1.Series.Clear();
      Series s = new Series();
      s.ChartType = SeriesChartType.Pie;
      chart1.Series.Add(s);

      float mean = QMath.Mean(items);
      txtMean.Text = (mean*10000).ToString();
      float average = QMath.Average(items.ToArray());
      txtAverage.Text = (average*10000).ToString();

      float total = zero.Count + negi.Count + posi.Count;

      DataPoint dpz = new DataPoint(0, Math.Ceiling(((float)zero.Count / total)*100));
      dpz.AxisLabel = "Zero";
      dpz.LegendText = "Ambivalent #VALY%";
      dpz.Label = "Ambivalent #VALY%";
      s.Points.Add(dpz);

      DataPoint dpn = new DataPoint(0, Math.Ceiling(((float)negi.Count / total)* 100));
      dpn.AxisLabel = "Negative";
      dpn.LegendText = "Negative #VALY%";
      dpn.Label = "Negative #VALY%";
      s.Points.Add(dpn);

      DataPoint dpp = new DataPoint(0, Math.Ceiling(((float)posi.Count / total)*100));
      dpp.AxisLabel = "Positive";
      dpp.LegendText = "Positive #VALY%";
      dpp.Label = "Positive #VALY%";
      s.Points.Add(dpp);

      IEnumerable<KeyValuePair<string, int>> resultspair = QMath.SortPairByPopularity(analines.ToArray());

      foreach (KeyValuePair<string, int> item in resultspair)
      {
        if (IsRegularWord(item.Key)) continue;
        string[] subs = new string[] { item.Key, item.Value.ToString() };
        ListViewItem li = new ListViewItem(subs);
        listPopularity.Items.Add(li);
      }

      IEnumerable<KeyValuePair<string, int>> results = QMath.SortByPopularity(analines.ToArray());

      foreach (KeyValuePair<string, int> item in results)
      {
        if (IsRegularWord(item.Key)) continue;
        string[] subs = new string[] { item.Key, item.Value.ToString() };
        ListViewItem li = new ListViewItem(subs);
        listPopularity.Items.Add(li);
      }

    }

    bool IsRegularWord(string s)
    {      
      if (RegularWords.Contains(s)) return true;
      return false;
    }

    private void SentimentView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      ListViewItemComparer com = new ListViewItemComparer(e.Column);
      SentimentView.ListViewItemSorter = com;
      Ascending = !Ascending;
      com.Ascending = Ascending;
      SentimentView.Sort();    
  }
  }
}
