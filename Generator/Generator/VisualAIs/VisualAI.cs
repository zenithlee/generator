using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;

namespace Generator.VisualAIs
{
  public partial class VisualAI : UserControl
  {
    string TokenURL = "https://api.clarifai.com/v1/token/";
    string ClientID = "s_Cw-z1pOGSQOq2yBowj0kwOIAS3BwBs_laBToCF";
    string ClientSecret = "OQWb6URIHDwYN1NoGB7fBv4YPWuCb_TdCz0Vovgg";
    string AccessToken = "LO8jyZikbEtS0P4VwJWVruu2I8RiYr";

    public VisualAI()
    {
      InitializeComponent();
    }

    public string CheckImage(string s)
    {
      string modelURL = "http://api.clarifai.com/v1/tag/?model=general-v1.3&url=" + s + "&access_token=LO8jyZikbEtS0P4VwJWVruu2I8RiYr";
      WebClient myWebClient = new WebClient();
      // download & save in a databuffer)
      byte[] myDataBuffer = myWebClient.DownloadData(modelURL);
      // full data buffer -> string
      string download = Encoding.ASCII.GetString(myDataBuffer);
      return download;

    }

    public void CheckNSWF()
    {
      string vikingDude = "https://i.imgur.com/lnzsgv9.jpg";
      string nudeWoman = "https://i.imgur.com/aJXq544.jpg";
      string modelURL = "http://api.clarifai.com/v1/tag/?model=nsfw-v0.1&url=" + nudeWoman + "&access_token=LO8jyZikbEtS0P4VwJWVruu2I8RiYr";
      // make a webclient 
      WebClient myWebClient = new WebClient();
      // download & save in a databuffer)
      byte[] myDataBuffer = myWebClient.DownloadData(modelURL);
      // full data buffer -> string
      string download = Encoding.ASCII.GetString(myDataBuffer);

      ClarNSFW(download);
    }

    public void ClarNSFW(string download)
    {

      int iop = download.IndexOf("classes");
      int iof = download.IndexOf("docid_str");
      string strn = download.Substring(iop, iof - iop);

      //Enable for dbg 
      Console.WriteLine("Parsed Text: ");
      Console.WriteLine(" ");
      Console.WriteLine(strn);


      int iof2 = download.IndexOf("nsfw");
      int iop2 = download.IndexOf("sfw");
      // dbg 
      // Console.WriteLine("{0}, {1}", iof2, iop2);


      string[] numbers = Regex.Split(download, @"\D+");

      int beforeDeciOne = int.Parse(numbers[1]);
      int beforeDeciTwo = int.Parse(numbers[3]);
      double afterDeciOne = double.Parse(numbers[2]);
      double afterDeciTwo = double.Parse(numbers[4]);

      if (iof2 > iop2)
      {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Image has been deemed SAFE FOR WORK");
        Console.WriteLine("     probability: 0 . {0} %", afterDeciTwo);
      }
      else if (iop2 > iof2)
      {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Image has been deemed NOT SAFE FOR WORK");
        Console.WriteLine("     probability: 0 . {0} %", afterDeciOne);
      }
      else
      {
        Console.WriteLine("Couldnt tell...");
      }
    }

    private void VisualSimulateInteraction_Click(object sender, EventArgs e)
    {
      //KBSim.Test(this);
      string sImage = VisualPath.Text;

      string result = CheckImage(sImage);

      Clarifai o = JsonConvert.DeserializeObject<Clarifai>(result);


      for (int i = 0; i < o.Results[0].Resulted.Tag.Classes.Length; i++)
      {
        string sClass = o.Results[0].Resulted.Tag.Classes[i];
        double sProbability = o.Results[0].Resulted.Tag.Probs[i];
        VisualResultsList.Items.Add(sClass + "(" + sProbability + ")");
      }
    }

    private void VisualPath_TextChanged(object sender, EventArgs e)
    {    
      VisualResultsList.Items.Clear();
      VisualAIPicture.ImageLocation = VisualPath.Text;
    }

    private void VisualTest_Click(object sender, EventArgs e)
    {     
     CheckNSWF();    
    }
  }

}
