using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Generator.VisualAIs
{
  class CVisualAI
  {
    string TokenURL = "https://api.clarifai.com/v1/token/";
    string ClientID = "s_Cw-z1pOGSQOq2yBowj0kwOIAS3BwBs_laBToCF";
    string ClientSecret = "OQWb6URIHDwYN1NoGB7fBv4YPWuCb_TdCz0Vovgg";
    string AccessToken = "LO8jyZikbEtS0P4VwJWVruu2I8RiYr";

    public void GenerateToken()
    {
      WebRequest request = WebRequest.Create(TokenURL);
      request.Method = "POST";
      object op = new
      {
        client_id = ClientID,
        client_secret = ClientSecret,
        grant_type = "client_credentials"
      };
      
      string postData = JsonConvert.SerializeObject(op);
      byte[] byteArray = Encoding.UTF8.GetBytes(postData);
      request.ContentType = "application/x-www-form-urlencoded";
      request.ContentLength = byteArray.Length;
      Stream dataStream = request.GetRequestStream();
      // Write the data to the request stream.
      dataStream.Write(byteArray, 0, byteArray.Length);
      // Close the Stream object.
      dataStream.Close();
      // Get the response.
      WebResponse response = request.GetResponse();
      // Display the status.
      Console.WriteLine(((HttpWebResponse)response).StatusDescription);
      // Get the stream containing content returned by the server.
      dataStream = response.GetResponseStream();
      // Open the stream using a StreamReader for easy access.
      StreamReader reader = new StreamReader(dataStream);
      // Read the content.
      string responseFromServer = reader.ReadToEnd();
      // Display the content.
      Console.WriteLine(responseFromServer);
      // Clean up the streams.
      reader.Close();
      dataStream.Close();
      response.Close();
      //make request to TokenURL
      //pass 
      //"client_id={client_id}" \
      //"client_secret={client_secret}" \
      //"grant_type=client_credentials"
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
  }

}
