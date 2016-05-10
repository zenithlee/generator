using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
  partial class Analysis
  {
    string SentimentFile = "../../../../Data/sentiments2.csv";
    Dictionary<string, float> Sentiments = new Dictionary<string, float>();

    void LoadSentiments()
    {

      if (!File.Exists(SentimentFile))
      {
        FileInfo fi = new FileInfo(SentimentFile);
        toolStripStatusLabel1.Text = "Sentiments not found:" + fi.FullName;
        return;
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
    
      var SentiData = from row in Sentiments select new { word = row.Key, sentiment = row.Value };
      SentimentGrid.DataSource = SentiData.ToArray();
    }

    float AnalyseWord(string s)
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
        if (!missing.Contains(slower))
        {
          //slower = slower.Replace(" ", "_");
          missing.Add(slower);
          report.Text += slower + "\r\n";
        }
      }
      return sentiment;
    }

    float AnalyseSentence(string s)
    {
      string slower = s.ToLower();
      float sentiment = 0;

      string[] words = s.Split(' ');

      float total = 0;
      foreach( string word in words)
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
