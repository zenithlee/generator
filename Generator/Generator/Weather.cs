using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Generator
{
  class Weather
  {
    string APIKey = "796ad2e53acc694482ab556883295fb0";
    string URL = "http://api.openweathermap.org/data/2.5/weather?";
    string Result = "";
    WeatherCodes _weatherCodes = new WeatherCodes();

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
      // Unix timestamp is seconds past epoch
      System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
      dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
      return dtDateTime;
    }

    string GetStormForCode(int n)
    {
      WeatherItem item = _weatherCodes.GetWeatherFromCode(n);
      if ( item != null ) {
        return item.description;
      }
      
      return "";
    }

    int KelvinToC(double n)
    {
      return (int)(n - Math.Ceiling(273.15f));
    }

    public WeatherClass GetWeatherReport(string PlaceCode)
    {
      WebRequest r = WebRequest.Create(URL + "id="+PlaceCode + "&APPID=" + APIKey);

      //WebProxy myProxy = new WebProxy("myproxy", 80);
      //myProxy.BypassProxyOnLocal = true;
      //wrGETURL.Proxy = myProxy;
      //wrGETURL.Proxy = WebProxy.GetDefaultProxy();
      Stream objStream;
      objStream = r.GetResponse().GetResponseStream();

      StreamReader objReader = new StreamReader(objStream);

      string sLine = "";
      int i = 0;
      Result = "";
      
      while (sLine != null)
      {
        i++;

        sLine = objReader.ReadLine();

        if (sLine != null)
        {
          Result += sLine;
        }
      }

      WeatherClass o = JsonConvert.DeserializeObject<WeatherClass>(Result);
      return o;
    }

    /**
     Generates a report from the JSON created class
    */
    public string GenerateReport(WeatherClass o)
    {      

      string w = "In " + o.name;

      DateTime dt = UnixTimeStampToDateTime(o.dt);

      if ( dt.Hour< 12 )
      {
        w += " this morning";
      }
      else
      {
        w += " this afternoon";
      }

      w += " we have " + o.weather[0].description;

      if ( o.weather.Count > 1 ) {
        w += " and " + o.weather[1].description;
      }

      w += " with winds reaching " + o.wind.speed + " meters per second";
      if (( o.wind.deg > 90) && (o.wind.deg < 270))
      {
        w += " in a southerly direction.";
      }
      else
      {
        w += " in a northerly direction.";
      }
      w += " The outside temperature is " + KelvinToC(o.main.temp) + " degrees";
      if (o.main.temp_min != o.main.temp_max) {
        w += " and will reach a low of " + KelvinToC(o.main.temp_min) + " and a high of " + KelvinToC(o.main.temp_max);
      } else
      {
        w += " likely to stay the same until tomorrow.";
      }

      //Console.ReadLine();

      return w;
    }

  }
}
