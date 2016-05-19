using Newtonsoft.Json;
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

    string MinePath = @"..\..\Data\mine\twitter\";
    string CampaignsPath = @"..\..\Data\Campaigns\";
    bool CancelMining = false;

    void Analysis_Campaign()
    {
      GetCampaigns();
      LoadSettings();
      
    }

    void LoadSettings()
    {
      CampaignActive.Checked = settings1.CampaignActive;
      CampaignTimerRate.Text = settings1.CampaignTickInterval.ToString();
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

      StringBuilder sb = new StringBuilder("INSERT IGNORE INTO `sl_twitter` VALUES");

      foreach (Tweetinvi.Core.Interfaces.ITweet item in result) {        
        string sPre = StripAuthorFromTweet(item.Text);        
        
        float Sentiment = AnalyseSentence(sPre);
        AddToList(item.Text, item.CreatedBy.ScreenName, item.RetweetCount.ToString(), item.Id.ToString(), Sentiment.ToString(), CampaignResults);
        AddToMine(item, Sentiment);

        //id
        //postid
        //author
        //text
        //retweets
        //sentiment
        //datecreated
        //category
        string formatForMySql = item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
        string text = item.Text.Replace("'", "''");
        sb.AppendFormat(" (NULL,{0},'{1}','{2}',{3},{4},'{5}','{6}'),", item.IdStr, item.CreatedBy.ScreenName, text, item.RetweetCount, Sentiment, formatForMySql, s);
        TotalSentiment += Sentiment;
      }

      string q = sb.ToString();
      q = q.TrimEnd(',');
      q += ";";
      AddToDB(q);

      TotalSentiment /= (float)result.Count();

      CampaignSummary.Items.Add("" + s + "=" + TotalSentiment);

      CampaignChart.Series[s].Points.Add(TotalSentiment);

    }

    void AddToDB(string sb)
    {      
      db.Open();
      string s = sb.ToString();
      //s = db.Escape(s);
      db.Query(s);
      db.Close();
    }

    void AddToMine(Tweetinvi.Core.Interfaces.ITweet item, float Sentiment) {

      QTweet q = new QTweet() { id=item.Id, author=item.CreatedBy.ScreenName, retweets=item.RetweetCount, sentiment=Sentiment, time=item.CreatedAt, text=item.Text };
      string sData = JsonConvert.SerializeObject(q);            
      File.WriteAllText(MinePath + item.Id + ".txt", sData);
    }

    float ClassifyText(string key, string s)
    {
      float Sentiment = AnalyseSentence(s);      
      return Sentiment;
    }

    public void AddGraphPoint(string key, float val)
    {
      if (CampaignChart.Series.IndexOf(key) < 0)
      {
        Series s = CampaignChart.Series.Add(key);
        s.ChartType = SeriesChartType.Line;
      }

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
      if (CampaignName.Items.Count == 0) return;
      if (CampaignName.SelectedIndex < 0)
      {
        CampaignName.SelectedIndex = 0;
      }

      if (CampaignList.Items.Count == 0) {
        CampaignName.SelectedIndex++;
        return;
      }
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
        if ( CampaignName.SelectedIndex < CampaignName.Items.Count-1)
        {          
          CampaignName.SelectedIndex++;
          if (CampaignList.Items.Count > 0)
          {
            CampaignList.SelectedIndex = 0;
          }

        } else
        {
          CampaignName.SelectedIndex = 0;
        }
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
      if ( !Directory.Exists(CampaignsPath))
      {
        Directory.CreateDirectory(CampaignsPath);
      }
      DirectoryInfo d = new DirectoryInfo(CampaignsPath);
      DirectoryInfo[] infos = d.GetDirectories();
      foreach( DirectoryInfo di in infos)
      {
        CampaignName.Items.Add(di.Name);
      }
    }

    void LoadCampaign(string s)
    {      
      string[] items = File.ReadAllLines(CampaignsPath + s + "\\keywords.txt");
      CampaignList.Items.Clear();
      CampaignList.Items.AddRange(items);
      SetupCampaignGraph();
    }

    void SaveCampaign(string sc)
    {
      string sItems = "";

      foreach (string s in CampaignList.Items)
      {
        sItems += s + "\r\n";
      }

      if (!Directory.Exists(CampaignsPath + sc))
      {
        Directory.CreateDirectory(CampaignsPath + sc);
      }
      File.WriteAllText(CampaignsPath + sc + "\\keywords.txt", sItems);
      GetCampaigns();
    }

    private void CampaignName_SelectedIndexChanged(object sender, EventArgs e)
    {
      string s = CampaignName.Text;
      LoadCampaign(s);
    }

    private void SaveCampaign_Click(object sender, EventArgs e)
    {
      string s = CampaignName.Text;
      SaveCampaign(s);
    }

    async Task MineData(string sCampaign)
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
        
        File.WriteAllText(CampaignsPath + sCampaign + "\\summary.txt", Summary);
      }
    }

    //CancellationToken token
    async Task MineAverage(string sCampaign)    
    {
      DirectoryInfo di = new DirectoryInfo(MinePath);
      FileInfo[] fi = di.GetFiles();

      Dictionary<string, float> Averages = new Dictionary<string, float>();
      Dictionary<string, List<float>> Values = new Dictionary<string, List<float>>();


      foreach (string s in CampaignList.Items)
      {
        Values.Add(s, new List<float>());
      }

        string Summary = "";      

      float count = 0;
      int TotalCount = 0;
      int AverageEveryNItems = 10;
      
        foreach (FileInfo f in fi)
        {        
          TotalCount++;
        //if (TotalCount < fi.Length-50) continue;
          String data = File.ReadAllText(f.FullName);
          data = data.ToLower();
          data = StripAuthorFromTweet(data);
          if (CancelMining == true) break;
          count++;
          bool gotdata = false;
          foreach (string s in CampaignList.Items)
          {        
            if (data.Contains(s.ToLower()))
            {
              gotdata = true;
              //CampaignSummary.Items.Add(sFile);
              float sentiment = ClassifyText(s, data);
              double corrected = CorrectSentiment(sentiment);
              Values[s].Add((float)corrected);
             // AddGraphPoint(s, (float)corrected);
            }
          } //foreach

        //AddGraphPoints(Values);        

        if (TotalCount % 55 == 1)
        {
          toolStripStatusLabel1.Text = "Mining " + TotalCount + "/" + fi.Length;
          await Task.Delay(1);
        }
        //CampaignSummary.Items.Add(s + "=" + corrected);
        //Summary += s + "=" + sentiment + "\r\n";
      } 
      List<double> avs = new List<double>();
      foreach(string s in Values.Keys)
      {        
        double mean = Quantify.Mean(Values[s]);
        Averages.Add(s, (float)mean);

        foreach( float f in Values[s]) {
          AddGraphPoint(s, f);
          AddGraphPoint(s+"_mean", (float)mean);
        }
      }

      foreach ( KeyValuePair<string,float> k in Averages)
      {
        Summary += k.Key + "=" + CorrectSentiment(k.Value) + "\n";
        CampaignSummary.Items.Add(k.Key + "=" + CorrectSentiment(k.Value));
      }
      
      File.WriteAllText(CampaignsPath + sCampaign + "\\summary_Average.txt", Summary);
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
      string s = CampaignName.Text;
      MineData(s);
    }

    private void CampaignMineAverage_Click(object sender, EventArgs e)
    {
      string s = CampaignName.Text;
      MineAverage(s);
    }

    private void CampaignClearGraph_Click(object sender, EventArgs e)
    {
      CampaignChart.Series.Clear();
      SetupCampaignGraph();
    }

    private void CampaignActive_CheckedChanged(object sender, EventArgs e)
    {
      int interval = (Convert.ToInt32(CampaignTimerRate.Text));
      CampaignTimer.Interval = interval;
      settings1.CampaignTickInterval = interval;
      CampaignTimer.Enabled = CampaignActive.Checked;

      settings1.CampaignActive = CampaignActive.Checked;
    }

    private void SaveGraph_Click(object sender, EventArgs e)
    {
      string s = CampaignName.Text;
      CampaignChart.SaveImage(CampaignsPath + s + "\\chart.png", ChartImageFormat.Png);
    }
  }
}
