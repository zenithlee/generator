using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Synthesis.TtsEngine;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO.Pipes;

using Generator;
using System.Collections;
using Generator.Database;
using Generator.VisualAIs;
using System.Reflection;

namespace Generator
  {
  public partial class Analysis : Form
  {    
    NamedPipeClientStream clientStream;

    string path = "../../NewsNet_Bin/Output/";
    string CurrentFile = "test";
    string CurrentProject;
    string DataPath = "../Data/";    

    SpeechSynthesizer reader;
    List<string> items = new List<string>();
    List<string> missing = new List<string>();
    public BindingSource source = new BindingSource();

    Bitmap image1;
    Bitmap image2;
    Bitmap image3;

    string VoiceName = "";

    Weather _weather = new Weather();
    StockMarket _stocks = new StockMarket();

    TwitterBot _Twitter = new TwitterBot();

    MySQL db = new MySQL();    
    InteractionSimulator KBSim = new InteractionSimulator();

    int PreviousVisemeMs = 0; //used to reduce overlap data

  public Analysis()
  {
      CurrentProject = path + CurrentFile;
      source.DataSource = Sentiments;
      
      Directory.CreateDirectory(CurrentProject);
          
      Console.WriteLine(Directory.GetCurrentDirectory());

      InitializeComponent();      

      SetupReader();
           
      ReadOnlyCollection<InstalledVoice> voices = reader.GetInstalledVoices();
      foreach (InstalledVoice voice in voices)
      {
          voicebox.Items.Add(voice.VoiceInfo.Name);
      }

      voicebox.SelectedValueChanged +=voicebox_SelectedValueChanged;
      voicebox.Text = voices[0].VoiceInfo.Name;
      LoadSentiments();
     

      GetExistingProjects();
      Application.EnableVisualStyles();
      Analysis_Campaign();

      SetupStocks();

      SetupTimers();
    }

    void SetupStocks()
    {
      List<string> stocks = _stocks.LoadPortfolio();
      if ( stocks != null ) { 
        StockBox.Items.AddRange(stocks.ToArray());
      }
    }

    void SetupTimers()
    {
      MarketTimer.Tick += MarketTimer_Tick_1;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Directory.CreateDirectory(CurrentProject);
      string sText = MainText.Text;
      items.Clear();
      Visemes.Clear();
      missing.Clear();
      report.Clear();

      items.Add("0~H~" + Headline.Text);
      Visemes.Text += "0~H~" + Headline.Text + "\r\n";
      if (checkBox1.Checked)
      {
        reader.SetOutputToWaveFile(CurrentProject + "/audio.wav");
      }
      else
      {
        reader.SetOutputToDefaultAudioDevice();
      }

      //reader.SpeakAsync(sText);
      ReadWithMarkup(sText);

      textBox2.Text = "SPEAKING";
    }

    void ReadWithMarkup(string s)
    {
      //SpeechSynthesizer synth = new SpeechSynthesizer();
      PromptBuilder pb = new PromptBuilder();
      pb.AppendSsmlMarkup("<voice xml:lang=\"en-US\">");

      pb.StartVoice(VoiceName);
      //pb.AppendText("Hello, how are you today?");
      //s = @"<bookmark mark=""bookmark_start""/> " + s;
      pb.AppendBookmark("bm1");

      string[] lines = s.Split('\n');
      foreach (string line in lines)
      {
        string[] br = line.Split('~');
        if (br[0] == "B") pb.AppendBookmark(br[1] + "~" + br[2]);
        if (br[0] == "S") pb.AppendText(br[1]);
      }

      //pb.AppendSsmlMarkup(s);

      //string high = "This is Normal pitch <prosody pitch=\"+20\"> This is Higher pitch. </prosody>";
      //string low = "<prosody pitch=\"-10\">This is extra low pitch. </prosody>";
      //pb.AppendSsmlMarkup(high);
      //pb.AppendSsmlMarkup(low);
      //string test= "This is extra <prosody pitch=\"-10\">extra</prosody> low pitch. ";
      //pb.AppendSsmlMarkup(test);
      pb.AppendSsmlMarkup("</voice>");
      pb.EndVoice();
      try
      {
        reader.SpeakAsync(pb);
      }
      catch (Exception ee)
      {
        Console.WriteLine("error" + ee.Message);
      }
    }

   

  void GetExistingProjects()
  {
      DirectoryInfo di = new DirectoryInfo(path);
      DirectoryInfo[] dirs = di.GetDirectories();      
      foreach( DirectoryInfo dir in dirs) {
        ProjectNames.Items.Add(dir.Name);
      }

      ProjectNames.TextChanged += ProjectNames_TextChanged;
      ProjectNames.SelectedIndexChanged += ProjectNames_SelectedIndexChanged;
  }

  private void ProjectNames_SelectedIndexChanged(object sender, EventArgs e)
  {
      CurrentProject = path + ProjectNames.Text;
      checkBox1.Checked = false;
      LoadProject();
  }

  private void ProjectNames_TextChanged(object sender, EventArgs e)
  {
      CurrentProject = path + ProjectNames.Text;
      checkBox1.Checked = false;
      LoadProject();
  }

  void LoadProject()
  {
      if (File.Exists(CurrentProject + "/input.txt"))
      {
        MainText.Text = File.ReadAllText(CurrentProject + "/input.txt");
      }

      if (File.Exists(CurrentProject + "/headline.txt"))
      {
        Headline.Text = File.ReadAllText(CurrentProject + "/headline.txt");
      }

      if (File.Exists(CurrentProject + "/strapline.txt"))
      {
        strapline.Text = File.ReadAllText(CurrentProject + "/strapline.txt");
      }
      else
      {
        strapline.Text = "";
      }

      if (File.Exists(CurrentProject + "/image1.png"))
      {
        pictureBox1.ImageLocation = CurrentProject + "/image1.png";
        pictureBox1.Load();
      }
      else {
        pictureBox1.Image = null;
      }
      if (File.Exists(CurrentProject + "/image2.png"))
      {
        pictureBox2.ImageLocation = CurrentProject + "/image2.png";
        pictureBox2.Load();
      }
      else
      {
        pictureBox2.Image = null;
      }

      if (File.Exists(CurrentProject + "/image3.png"))
      {
        pictureBox3.ImageLocation = CurrentProject + "/image3.png";
        pictureBox3.Load();
      }
      else
      {
        pictureBox3.Image = null;
      }
    }

  void SetupReader()
  {
      reader = new SpeechSynthesizer(); //create new object
      //reader.SetOutputToWaveFile(path + CurrentFile + "/audio.wav");                                              
      reader.VoiceChange += Reader_VoiceChange;
      reader.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(reader_SpeakCompleted);
      reader.VisemeReached += new EventHandler<VisemeReachedEventArgs>(reader_VisemeReached);
      reader.BookmarkReached += Reader_BookmarkReached;
      reader.SpeakProgress += reader_SpeakProgress;
      VoiceName = reader.Voice.Name;
      if ( VoiceName != "" ) {
        reader.SelectVoice(VoiceName);
      }      
  }

    void SendToPipe(string s)
    {
      return;

      byte[] buffer = ASCIIEncoding.ASCII.GetBytes(s);
      clientStream = new NamedPipeClientStream("aipipe");
      clientStream.Connect(TimeSpan.MaxValue.Seconds);
      clientStream.WaitForPipeDrain();
      clientStream.Write(buffer, 0, buffer.Length);

      clientStream.Flush();
      clientStream.Dispose();
      clientStream.Close();
    }

    private void Reader_BookmarkReached(object sender, BookmarkReachedEventArgs e)
    {
       int ms = (int)e.AudioPosition.TotalMilliseconds;
      string s = ms +"~B~" +e.Bookmark;
      Visemes.Text += s + "\r\n";
      items.Add(s);
      SendToPipe(s);
    }

    private void Reader_VoiceChange(object sender, VoiceChangeEventArgs e)
  {
      
      string s = "0~S~" + e.Voice.Name + "~" + e.Voice.Gender + "~" + e.Voice.Age + "~" + e.Voice.Culture + "~" + e.Voice.Description;
      Visemes.Text += s + "\r\n";
      items.Add(s);
      SendToPipe(s);
    }

  void reader_SpeakProgress(object sender, SpeakProgressEventArgs e)
  {
      int ms = (int)e.AudioPosition.TotalMilliseconds;

      string word = e.Text;
      string s = ms.ToString()+"~W~" + word.Replace(" ", "_") ;      
      
      float sentiment = AnalyseWord(word);
      Visemes.Text += s + "~" + sentiment + "\r\n";
      items.Add(s + "~" + sentiment);
  }

  private void voicebox_SelectedValueChanged(object sender, EventArgs e)
  {
      VoiceName = voicebox.Text;
      reader.SelectVoice(VoiceName);
  }

  private void reader_VisemeReached(object sender, VisemeReachedEventArgs e)
  {
      int ms = (int)e.AudioPosition.TotalMilliseconds;
      if ( PreviousVisemeMs == ms )
      {        
        ms += 30;
      }
      string s = ms + "~V~" + e.Viseme.ToString();
      Visemes.Text += s + "\r\n";
      items.Add(s);
      PreviousVisemeMs = (int)e.AudioPosition.TotalMilliseconds;
      //Console.WriteLine(e.Viseme);
      SendToPipe(s);
    }

  void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
  {
      textBox2.Text = "IDLE.";
      Visemes.Text += "0~X~X";
      items.Add("0~X~X");
      SaveProject();
      textBox2.Text = "SAVED.";
      
    } 

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    
  }

  private void button2_Click(object sender, EventArgs e)
  {
      reader.Pause();
      reader.Dispose();
      SetupReader();
  }

  void SaveProject()
  {      
      CurrentProject = path + ProjectNames.Text;
      Directory.CreateDirectory(CurrentProject);
      File.WriteAllLines(CurrentProject + "/sequence.txt", items);
      File.WriteAllText(CurrentProject + "/input.txt", MainText.Text);
      File.WriteAllText(CurrentProject + "/headline.txt", Headline.Text);
      File.WriteAllText(CurrentProject + "/strapline.txt", strapline.Text);
      File.WriteAllLines(CurrentProject + "/missing.txt", missing.ToArray());
      textBox2.Text = "IDLE. Wrote to :" + CurrentProject;
  }

    private void button3_Click(object sender, EventArgs e)
    {
        SaveProject();
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      FileInfo fi = new FileInfo(path);
      openFileDialog1.InitialDirectory = fi.FullName;
      DialogResult result = openFileDialog1.ShowDialog();
      
      string fullname = openFileDialog1.FileName;
      //pictureBox1.ImageLocation = fullname;
      if ( File.Exists(fullname)) { 
        image1 = new Bitmap(fullname);
        pictureBox1.Image = image1;      
        image1.Save(CurrentProject + "/image1.png");
      }
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {
      FileInfo fi = new FileInfo(path);
      openFileDialog1.InitialDirectory = fi.FullName;
      DialogResult result = openFileDialog1.ShowDialog();
      string fullname = openFileDialog1.FileName;
      //pictureBox1.ImageLocation = fullname;
      image2 = new Bitmap(fullname);
      pictureBox2.Image = image2;

      image2.Save(CurrentProject + "/image2.png");
    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {
      FileInfo fi = new FileInfo(path);
      openFileDialog1.InitialDirectory = fi.FullName;
      DialogResult result = openFileDialog1.ShowDialog();
      string fullname = openFileDialog1.FileName;
      //pictureBox1.ImageLocation = fullname;
      image3 = new Bitmap(fullname);
      pictureBox3.Image = image3;

      image3.Save(CurrentProject + "/image3.png");
    }


    private void trackBar1_Scroll(object sender, EventArgs e)
    {
        
    }

    private void SentimentGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      SentimentGrid.CurrentCell.ReadOnly = false;
      SentimentGrid.BeginEdit(false); 
    }
    
    private void RefreshButton_Click(object sender, EventArgs e)
    {
      var SentiData = from row in Sentiments select new { word = row.Key, sentiment = row.Value };
      SentimentGrid.DataSource = SentiData.ToArray();
    }

    private void SentimentGrid_Validated(object sender, EventArgs e)
    {
      var SentiData = from row in Sentiments select new { word = row.Key, sentiment = row.Value };
      SentimentGrid.DataSource = SentiData.ToArray();
    }

    void PopulateControl()
    {
      var SentiData = from row in Sentiments select new { word = row.Key, sentiment = row.Value };
      SentimentGrid.DataSource = SentiData.ToArray();
    }

    private void SentimentGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
      if (e.ColumnIndex == 1)
      {
        Sentiments[SentimentGrid[0, SentimentGrid.CurrentRow.Index].Value.ToString()] = (float)Convert.ToDouble(e.FormattedValue);        
        BeginInvoke(new MethodInvoker(PopulateControl));
      }
    }

    private void Test_Click(object sender, EventArgs e)
    {
      string sml = @"hello.";

      //SpeechSynthesizer synth = new SpeechSynthesizer();
      PromptBuilder pb = new PromptBuilder();      
      pb.AppendSsmlMarkup("<voice xml:lang=\"en-US\">"); 

      pb.StartVoice(VoiceName);
      pb.AppendBookmark("test");
      //pb.AppendText("Hello, how are you today?");
      pb.AppendSsmlMarkup(sml);
      
      string high = "This is Normal pitch <prosody pitch=\"+20\"> This is Normal pitch. </prosody>";
      pb.AppendBookmark("low");
      string low = "<prosody pitch=\"-10\">This is extra low pitch. </prosody>";
      pb.AppendSsmlMarkup(high);
      pb.AppendSsmlMarkup(low);
      //string test= "This is extra <prosody pitch=\"-10\">extra</prosody> low pitch. ";
      //pb.AppendSsmlMarkup(test);
      pb.AppendSsmlMarkup("</voice>");
      pb.EndVoice();
      try {
        reader.SpeakAsync(pb);
      }
      catch ( Exception ee)
      {

      }
    }

    void Getfor(string Country)
    {
      WeatherClass o = _weather.GetWeatherReport(_weather.URL, Country);
      ProjectNames.Text = _weather.GetReportName(o);
      RawWeather.Text = _weather.SaveReport(o);
      string sIcon = o.weather[0].icon;
      iconBox.ImageLocation = DataPath + "\\icons\\" + sIcon + ".png";
      iconBox.Load();
      //image2 = new Bitmap(fullname);
      //pictureBox2.Image = image2;

      Headline.Text = _weather.GetHeadline(o);

      ForecastClass Forecast = _weather.GetWeatherForecast(_weather.URL_Forecast, Country);

      RawWeather.Text += JsonConvert.SerializeObject(Forecast);

      string temp = _weather.GenerateReport(o, Forecast);
      Directory.CreateDirectory(CurrentProject);
      File.WriteAllText(CurrentProject + "/weather.txt", temp);
      temp += "B~H4I~" + sIcon + _weather.NL;
      
      temp += _weather.GenerateForecast(Forecast);
      ServiceResult.Text = _weather.ParseForTTS(temp);

      MainText.Text = ServiceResult.Text;
      //ProjectNames.Text = CurrentProject;
    }

    private void GETButton_Click(object sender, EventArgs e)
    {
      Getfor(CountryCodes.LONDON_UK);
    }

    private void button5_Click(object sender, EventArgs e)
    {
      Getfor(CountryCodes.NEWYORK_US);
    }

    private void button4_Click(object sender, EventArgs e)
    {
      Getfor(CountryCodes.CAPETOWN_ZA);
    }

    private void SpeechAdd_Click(object sender, EventArgs e)
    {
      InsertText("S~");      
    }

    private void button6_Click(object sender, EventArgs e)
    {
      InsertText("B~");
    }

    private void button7_Click(object sender, EventArgs e)
    { 
      InsertText("~B~H1T~");
    }

    void InsertText(string s)
    {
      string ss = "\r\n" + s;
      var selectionIndex = MainText.SelectionStart;
      MainText.Text = MainText.Text.Insert(selectionIndex, ss);
      MainText.SelectionStart = selectionIndex + ss.Length+1;
      MainText.Select();
    }

    private void button8_Click(object sender, EventArgs e)
    {
      InsertText("~B~H2T~");
    }

    private void button9_Click(object sender, EventArgs e)
    {
      InsertText("~B~H3T~");
    }

    private void TestTwitterbutton_Click(object sender, EventArgs e)
    {
      TwitterInfo.Text = _Twitter.GetInfo();      
    }

    private void MakeTweetButton_Click(object sender, EventArgs e)
    {
      TwitterInfo.Text = "Tweeting...";
      _Twitter.MakeTweet(TweetBox.Text + " " + HashTagsBox.Text);
      TwitterInfo.Text = "OK";
    }

    private void TestSearchButton_Click(object sender, EventArgs e)
    {
      TweetResults.Clear();
      IEnumerable<Tweetinvi.Core.Interfaces.ITweet> results = _Twitter.DoSearchParams(SearchBox.Text);

      foreach(Tweetinvi.Core.Interfaces.ITweet  tweet in results)
      {
        AddToList(tweet.Text, tweet.CreatedBy.ScreenName, tweet.Retweets.ToString(), tweet.Id.ToString(), TweetResults);
      }
    }

    public void AddToList(string text, string author, string Popularity, string id, ListView list)
    {
      string[] row = { text, author, Popularity, id };
      ListViewItem item = new ListViewItem(row);
      list.Items.Add(item);
    }

    private void button10_Click(object sender, EventArgs e)
    {
      string s = MainText.Text;

      s = s.Replace("\r", "");
      string[] items = s.Split('\n');
      string r = "";
      foreach( string item in items)
      {
        r += "S~" + item + "\r\n\r\n";
      }

      MainText.Text = r;
    }

    private void button11_Click(object sender, EventArgs e)
    {
      InsertText("B~H1I~image1");
    }

    private void button12_Click(object sender, EventArgs e)
    {
      InsertText("B~H1I~image2");
    }

    private void button13_Click(object sender, EventArgs e)
    {
      InsertText("B~H1I~image3");
    }

    private void GetApple_Click(object sender, EventArgs e)
    {
      //string s = _stocks.MakeWebRequest(_stocks.URL, "AAPL");
      //MarketResponse.Text = s;
      StockClassObject o = _stocks.GetStockInfo("AAPL");      
      MarketStory.Text = _stocks.CreateReport(o);
      _stocks.PlotTo(o, MarketChart);
    }

    private void button14_Click(object sender, EventArgs e)
    {

    }

    private void Popularity_CheckedChanged(object sender, EventArgs e)
    {
      _Twitter.PopularResults = Popularity.Checked;
    }

    private void RecentCheck_CheckedChanged(object sender, EventArgs e)
    {
      _Twitter.RecentResults = RecentCheck.Checked;
    }

    private void ReTweet_Click(object sender, EventArgs e)
    {
      ListView.SelectedListViewItemCollection selected = TweetResults.SelectedItems;

      string s = selected[0].SubItems[0].Text;
      string sid = selected[0].SubItems[3].Text;
      long id = Convert.ToInt64(sid);

      TwitterInfo.Text = "Tweeting...";

      //_Twitter.MakeTweet(TweetBox.Text + " " + HashTagsBox.Text);
      _Twitter.MakeRetweet(id);
      TwitterInfo.Text = "OK";

    }

    private void CopyToTweet_Click(object sender, EventArgs e)
    {
      ListView.SelectedListViewItemCollection selected = TweetResults.SelectedItems;
      string s = selected[0].SubItems[0].Text;
      s += "\r\n" + selected[0].SubItems[1].Text;
      s += "\r\n" + selected[0].SubItems[2].Text;
      TwitterInfo.Text = s;
    }

    private void TweetResults_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      this.TweetResults.ListViewItemSorter = new ListViewItemComparer(e.Column);
      // Call the sort method to manually sort.
      TweetResults.Sort();
    }

    private void CampaignResults_ColumnClick(object sender, ColumnClickEventArgs e)
    {
    
      this.CampaignResults.ListViewItemSorter = new ListViewItemComparer(e.Column);
      // Call the sort method to manually sort.
      CampaignResults.Sort();
    
  }

    private void CampaignChart_Click(object sender, EventArgs e)
    {

    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
      

      StringBuilder sq = new StringBuilder("INSERT INTO sl_sentimentwords VALUES");
      string[] items = File.ReadAllLines(SentimentFile);

      int counter = 0;
      foreach (string item in items)
      {
        counter++;
        string[] pair = item.Split(',');
        string sword = pair[0];
        if (pair[1] == "") pair[1] = "0";
        float sentiment= (float)Convert.ToDouble(pair[1]);

        string pos = pair[2];
        if (pos == "") pos = "";

        sword = db.Escape(sword);
        sq.AppendFormat(" (NULL,'{0}',{1},'-','{2}'),", sword, sentiment, pos);

        if ( counter > 100 )
        {
          string q = sq.ToString();
          q = q.TrimEnd(',');
          q += ";";

          db.Open();
          db.Query(q);
          db.Close();

          sq = new StringBuilder("INSERT INTO sl_sentimentwords VALUES");
          counter = 0;
        }
       
      }

      
    }

    #region Stocks

    private void MarketAuto_CheckedChanged(object sender, EventArgs e)
    {
      MarketTimer.Enabled = MarketAuto.Checked;
    }

    private void btnAddStock_Click(object sender, EventArgs e)
    {
      StockBox.Items.Add(txtStockToWatch.Text);
      _stocks.AddToPortfolio(txtStockToWatch.Text);
    }

    private void btnRemoveStock_Click(object sender, EventArgs e)
    {
      string s = StockBox.Items[StockBox.SelectedIndex].ToString();
      StockBox.Items.RemoveAt(StockBox.SelectedIndex);
      _stocks.RemoveFromPortfolio(s);
    }

    private void btnMarketRun_Click(object sender, EventArgs e)
    {
      _stocks.SetGraph(MarketChart);
      _stocks.Analyse_Stocks();
    }

    private void MarketTimer_Tick_1(object sender, EventArgs e)
    {
      _stocks.SetGraph(MarketChart);
      _stocks.Analyse_Stocks();
    }

    private void StockBox_MouseClick(object sender, MouseEventArgs e)
    {
      if (StockBox.SelectedIndex > -1) { 
        string sStock = StockBox.Items[StockBox.SelectedIndex].ToString();

        StockClassObject so = _stocks.GetStockInfo(sStock);
        _stocks.PlotTo(so, MarketChart);
        MarketStory.Text = _stocks.CreateReport(so);
      }

    }


    #endregion

    #region visual


    #endregion
  }

  class ListViewItemComparer : IComparer
  {
    private int col;
    public ListViewItemComparer()
    {
      col = 0;
    }
    public ListViewItemComparer(int column)
    {
      col = column;
    }
    public bool IsNumeric(object Expression)
    {
      double retNum;

      bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
      return isNum;
    }
    public int Compare(object x, object y)
    {
      int returnVal = -1;

      if (IsNumeric(((ListViewItem)x).SubItems[col].Text) && IsNumeric(((ListViewItem)y).SubItems[col].Text))
      {

        double ix = Convert.ToDouble(((ListViewItem)x).SubItems[col].Text);
        double iy = Convert.ToDouble(((ListViewItem)y).SubItems[col].Text);

        returnVal = ix > iy ? -1 : 1;
      }
      else
      {
        returnVal = String.Compare(((ListViewItem)y).SubItems[col].Text.ToString(), ((ListViewItem)y).SubItems[col].Text.ToString());
      }
        return returnVal;
      
    }
  }

}
