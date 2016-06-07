using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantifai
{
  class SentimentAnalyser
  {
    string SentimentFile = @"..\..\Data\sentiments2.csv";
    Dictionary<string, float> Sentiments = new Dictionary<string, float>();
    List<string> Missing = new List<string>();

    public SentimentAnalyser()
    {
      LoadSentiments();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>string indicating pass or fail, to show on status line</returns>
    public string LoadSentiments()
    {
      string sFile = Directory.GetCurrentDirectory() + SentimentFile;
      FileInfo fi = new FileInfo(sFile);
      sFile = fi.FullName;
      if (!File.Exists(SentimentFile))
      { 
        return "Sentiments not found:" + fi.FullName;
      }
      string[] items = File.ReadAllLines(SentimentFile);
      foreach (string item in items)
      {
        string[] pair = item.Split(',');
        string sword = pair[0];
        if (pair[1] == "") pair[1] = "0";
        float value = (float)Convert.ToDouble(pair[1]);
        if (!Sentiments.ContainsKey(sword))
        {
          Sentiments.Add(sword, value);
        }
      }

      //var SentiData = from row in Sentiments select new { word = row.Key, sentiment = row.Value };
      //SentimentGrid.DataSource = SentiData.ToArray();
      return "Sentiments Loaded";
    }

    public float AnalyseWord(string s)
    {
      float sentiment = 0;
      string slower = s.ToLower();
      slower = slower.Replace("#", "");
      slower = slower.Replace("!", "");
      slower = slower.Replace("?", "");
      slower = slower.Replace("@", "");

      if (Sentiments.ContainsKey(slower))
      {
        sentiment = Sentiments[slower];
      }
      else
      {
        sentiment = 0;
        if (!Missing.Contains(slower))
        {
          //slower = slower.Replace(" ", "_");
          Missing.Add(slower);
          //report.Text += slower + "\r\n";
        }
      }
      return sentiment;
    }

    public float AnalyseSentence(string s)
    {
      string slower = s.ToLower();
      float sentiment = 0;

      string[] words = s.Split(' ');

      float total = 0;
      foreach (string word in words)
      {

        sentiment += AnalyseWord(word);
        if (sentiment != 0) total++;
      }
      if (total > 0)
      {
        sentiment = sentiment / (float)total;
      }


      return sentiment;
    }
  }
}
