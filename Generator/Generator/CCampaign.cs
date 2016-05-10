using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Generator
{
  partial class Analysis
  {

    string MinePath = @"..\..\Data\mine\twitter";
    bool CancelMining = false;

    void Analysis_Campaign()
    {
      GetCampaigns();
    }

    private void btn_Campaign_New_Click(object sender, EventArgs e)
    {
      CampaignList.Items.Clear();
      CampaignChart.Series.Clear();
      CampaignResults.Items.Clear();
      CampaignSummary.Items.Clear();
      CampaignName.Text = "";
      GetCampaigns();
    }

    public void SetupCampaignGraph()
    {

      CampaignChart.Series.Clear();
      foreach( string s in CampaignList.Items)
      {
        Series item = CampaignChart.Series.Add(s);
        item.ChartType = SeriesChartType.Line;        
      }      
    }

    public string StripAuthorFromTweet(string s)
    {
      string ss = System.Text.RegularExpressions.Regex.Replace(s, @"@\w+", "");
      ss = System.Text.RegularExpressions.Regex.Replace(s, @"http://\w+", "");
      ss = System.Text.RegularExpressions.Regex.Replace(s, @"https://\w+", "");
      return ss;
    }

    public void Campaign_Search(string s)
    {
      IEnumerable < Tweetinvi.Core.Interfaces.ITweet > result = _Twitter.DoSearchParams(s);
      if (result == null) return;

      //string[] row = { text, author, Popularity, id };  

      float TotalSentiment = 0;    
      
      foreach (Tweetinvi.Core.Interfaces.ITweet item in result) {        
        string sPre = StripAuthorFromTweet(item.Text);        
        
        float Sentiment = AnalyseSentence(sPre);
        AddToList(item.Text, item.CreatedBy.ScreenName, item.RetweetCount.ToString(), item.Id.ToString(), Sentiment.ToString(), CampaignResults);
        TotalSentiment += Sentiment;
      }

      TotalSentiment /= (float)result.Count();

      CampaignSummary.Items.Add("" + s + "=" + TotalSentiment);

      CampaignChart.Series[s].Points.Add(TotalSentiment);

    }

    float ClassifyText(string key, string s)
    {
      float Sentiment = AnalyseSentence(s);      
      return Sentiment;
    }

    public void AddGraphPoint(string key, float val)
    {
      CampaignChart.Series[key].Points.Add(val);
    }

    public void AddGraphPoints( Dictionary<string,float> data)
    {
      foreach( KeyValuePair<string,float> kv in data)
      {
        AddGraphPoint(kv.Key, kv.Value);
      }
    }

    public void AddToList(string text, string author, string Popularity, string id, string sentiment, ListView list)
    {
      string[] row = { text, author, Popularity, id, sentiment };
      ListViewItem item = new ListViewItem(row);
      list.Items.Add(item);
    }

    void Campaign_SaveSettings()
    {

    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      Campaign_DoNext();
    }

    void Campaign_DoNext()
    {
      if (CampaignList.Items.Count == 0) return;
      if ( CampaignList.SelectedIndex <0 )
      {       
        CampaignList.SelectedIndex = 0;
      }
      string sItem = (String)CampaignList.Items[CampaignList.SelectedIndex];
      CampaignResults.Items.Clear();
      Campaign_Search(sItem);      
      if (CampaignList.SelectedIndex < CampaignList.Items.Count-1)
      {
        CampaignList.SelectedIndex++;
      } 
      else
      {
        CampaignList.SelectedIndex=0;
      }      
    }

    private void AddToCampaign_Click(object sender, EventArgs e)
    {
      string s = CampaignQuery1.Text;
      CampaignList.Items.Add(s);
      Campaign_SaveSettings();
      SetupCampaignGraph();
    }

    private void RunCampaignNow_Click(object sender, EventArgs e)
    {
      Campaign_DoNext();
    }

    private void CampaignGraphLine_Click(object sender, EventArgs e)
    {
      foreach( Series item in CampaignChart.Series)
      {
        item.ChartType = SeriesChartType.Line;
      }      
    }

    private void CampaginGraphBar_Click(object sender, EventArgs e)
    {
      foreach (Series item in CampaignChart.Series)
      {
        item.ChartType = SeriesChartType.Column;
      }
    }

    void GetCampaigns()
    {
      if ( !Directory.Exists(DataPath + "\\Campaigns\\"))
      {
        Directory.CreateDirectory(DataPath + "\\Campaigns\\");
      }
      DirectoryInfo d = new DirectoryInfo(DataPath + "\\Campaigns\\");
      DirectoryInfo[] infos = d.GetDirectories();
      foreach( DirectoryInfo di in infos)
      {
        CampaignName.Items.Add(di.Name);
      }
    }

    void LoadCampaign(string s)
    {      
      string[] items = File.ReadAllLines(CampaignPath() + "\\keywords.txt");
      CampaignList.Items.Clear();
      CampaignList.Items.AddRange(items);
      SetupCampaignGraph();
    }

    void SaveCampaign()
    {
      string sItems = "";

      foreach (string s in CampaignList.Items)
      {
        sItems += s + "\r\n";
      }
      
      Directory.CreateDirectory(CampaignPath());
      File.WriteAllText(CampaignPath() + "\\keywords.txt", sItems);
      GetCampaigns();
    }

    private void CampaignName_SelectedIndexChanged(object sender, EventArgs e)
    {
      string s = CampaignName.Text;
      LoadCampaign(s);
    }

    private void SaveCampaign_Click(object sender, EventArgs e)
    {
      SaveCampaign();
    }

    async Task MineData()
    {
      DirectoryInfo di = new DirectoryInfo(MinePath);
      FileInfo[] fi = di.GetFiles();


      string Summary = "";

      float count = 0;
      int TotalCount = 0;
      foreach (string s in CampaignList.Items)
      {
        TotalCount++;
        float totalSentiment = 0;
        foreach (FileInfo f in fi)
        {          
          String data = File.ReadAllText(f.FullName);
          data = data.ToLower();
          data = StripAuthorFromTweet(data);

          if (data.Contains(s))
          {
            count++;
            //CampaignSummary.Items.Add(sFile);
            float sentiment = ClassifyText(s, data);
            totalSentiment += sentiment;
            AddGraphPoint(s, sentiment);
          }

          toolStripStatusLabel1.Text = "Mining " + TotalCount + "/" + fi.Length;
          await Task.Delay(10);

          if (CancelMining == true) break;
        }


        totalSentiment /= count;
        double corrected = CorrectSentiment(totalSentiment);
        CampaignSummary.Items.Add(s + "=" + corrected);
        Summary += s + "=" + corrected + "\r\n";
        
        File.WriteAllText(CampaignPath() + "\\summary.txt", Summary);
      }
    }

    //CancellationToken token
    async Task MineAverage()    
    {
      DirectoryInfo di = new DirectoryInfo(MinePath);
      FileInfo[] fi = di.GetFiles();

      Dictionary<string, float> Averages = new Dictionary<string, float>();
      string Summary = "";      

      float count = 0;
      int TotalCount = 0;
      int AverageEveryNItems = 10;

      foreach (string s in CampaignList.Items)
      {
        Averages.Add(s, 0);
      }

        foreach (FileInfo f in fi)
        {
        TotalCount++;
          String data = File.ReadAllText(f.FullName);
          data = data.ToLower();
          data = StripAuthorFromTweet(data);
        if (CancelMining == true) break;
        count++;
        bool gotdata = false;
        foreach (string s in CampaignList.Items)
          {        
            if (data.Contains(s))
              {
            gotdata = true;
            //CampaignSummary.Items.Add(sFile);
            float sentiment = ClassifyText(s, data);
              double corrected = CorrectSentiment(sentiment);
            
              if (Averages.ContainsKey(s)) { 
                Averages[s] = (Averages[s] + sentiment)/2.0f;
              }              
            }
          } //foreach

        if ((count > AverageEveryNItems) )
        {
          AddGraphPoints(Averages);
          count = 0;
        }

        if (TotalCount % 5 == 1)
        {
          toolStripStatusLabel1.Text = "Mining " + TotalCount + "/" + fi.Length;
          await Task.Delay(2);
        }



        //CampaignSummary.Items.Add(s + "=" + corrected);
        //Summary += s + "=" + sentiment + "\r\n";


      }


        foreach( KeyValuePair<string,float> k in Averages)
      {
        Summary += k.Key + "=" + CorrectSentiment(k.Value) + "\n";
        CampaignSummary.Items.Add(k.Key + "=" + CorrectSentiment(k.Value));
      }
      
      File.WriteAllText(CampaignPath() + "\\summary_Average.txt", Summary);
      CancelMining = false;
    }

    private void CampaignMineStop_Click(object sender, EventArgs e)
    {
      CancelMining = true;
    }

    double CorrectSentiment(double s)
    {
      return Math.Round(s * 1000, 2);
    }

    private void CampaignMineButton_Click(object sender, EventArgs e)
    {
      MineData();
    }

    private void CampaignMineAverage_Click(object sender, EventArgs e)
    {
      MineAverage();
    }

    private void CampaignClearGraph_Click(object sender, EventArgs e)
    {
      CampaignChart.Series.Clear();
      SetupCampaignGraph();
    }

    private void CampaignActive_CheckedChanged(object sender, EventArgs e)
    {
      timer1.Enabled = CampaignActive.Checked;
    }

    private void SaveGraph_Click(object sender, EventArgs e)
    {
      CampaignChart.SaveImage(CampaignPath() + "\\chart.png", ChartImageFormat.Png);
    }

    string CampaignPath()
    {
      string CPath = DataPath + "\\Campaigns\\" + CampaignName.Text;
      return CPath;
    }

  }
}
