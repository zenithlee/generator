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

namespace Generator
  {
  public partial class Analysis : Form
  {
  string path = "../../NewsNet_Bin/Output/";
  string CurrentFile = "test";
  string CurrentProject;
    string DataPath = "../Data/";
  string SentimentFile = "../Data/sentiments2.csv";
  Dictionary<string, float> Sentiments = new Dictionary<string, float>();

  SpeechSynthesizer reader;
  List<string> items = new List<string>();
  List<string> missing = new List<string>();
  public BindingSource source = new BindingSource();

    Bitmap image1;
    Bitmap image2;
    Bitmap image3;

    string VoiceName = "";

    Weather _weather = new Weather();

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
      var SentiData = from row in Sentiments select new { word = row.Key, sentiment = row.Value };
      SentimentGrid.DataSource = SentiData.ToArray();

      GetExistingProjects();
      Application.EnableVisualStyles();

    }

    private void button1_Click(object sender, EventArgs e)
    {
      Directory.CreateDirectory(CurrentProject);
      string sText = MainText.Text;
      items.Clear();
      Visemes.Clear();
      missing.Clear();
      report.Clear();

      items.Add("H " + Headline.Text);
      Visemes.Text += "H " + Headline.Text + "\r\n";
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

    void LoadSentiments()
  {
      string[] items = File.ReadAllLines(SentimentFile);
      foreach( string item in items)
      {
        string[] pair = item.Split(',');
        string sword = pair[0];
        if (pair[1] == "") pair[1] = "0";
        float value = (float)Convert.ToDouble(pair[1]);
        if ( !Sentiments.ContainsKey(sword)) { 
            Sentiments.Add(sword, value);
        }
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

    private void Reader_BookmarkReached(object sender, BookmarkReachedEventArgs e)
    {
      string s = "B " + e.Bookmark;
      Visemes.Text += s + "\r\n";
      items.Add(s);
    }

    private void Reader_VoiceChange(object sender, VoiceChangeEventArgs e)
  {
      string s = "S " + e.Voice.Name + "," + e.Voice.Gender + "," + e.Voice.Age + "," + e.Voice.Culture + "," + e.Voice.Description;
      Visemes.Text += s + "\r\n";
      items.Add(s);
  }

  void reader_SpeakProgress(object sender, SpeakProgressEventArgs e)
  {
      string s = "W " + e.Text.Replace(" ", "_") ;
      string slower = e.Text.ToLower();
      float sentiment = 0f;
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
      Visemes.Text += s + " " + sentiment + "\r\n";
      items.Add(s + " " + sentiment);
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
      string s = "V " + ms + " " + e.Viseme.ToString();
      Visemes.Text += s + "\r\n";
      items.Add(s);
      PreviousVisemeMs = (int)e.AudioPosition.TotalMilliseconds;
      //Console.WriteLine(e.Viseme);
    }

   

  void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
  {
      textBox2.Text = "IDLE.";
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
      image1 = new Bitmap(fullname);
      pictureBox1.Image = image1;
      
      image1.Save(CurrentProject + "/image1.png");
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

      image1.Save(CurrentProject + "/image3.png");
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
      MainText.Text += "\r\nS~";
    }

    private void button6_Click(object sender, EventArgs e)
    {
      MainText.Text += "\r\nB~";
    }

    private void button7_Click(object sender, EventArgs e)
    {
      MainText.Text += "\r\nB~H1T~";
    }

    private void button8_Click(object sender, EventArgs e)
    {
      MainText.Text += "\r\nB~H2T~";
    }

    private void button9_Click(object sender, EventArgs e)
    {
      MainText.Text += "\r\nB~H3T~";
    }
  }
  }
