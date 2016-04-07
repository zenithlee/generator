﻿using System;
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
    public string URL = "http://api.openweathermap.org/data/2.5/weather?";
    public string URL_Forecast = "http://api.openweathermap.org/data/2.5/forecast?";
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

    public string MakeWebRequest( string sURL, string PlaceCode)
    {
      WebRequest r = WebRequest.Create(sURL + "id=" + PlaceCode + "&APPID=" + APIKey);

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
      return Result;
    }


    public WeatherClass GetWeatherReport(string sURL, string PlaceCode)
    {
      Result = MakeWebRequest(sURL, PlaceCode);            
      WeatherClass o = JsonConvert.DeserializeObject<WeatherClass>(Result);
      return o;
    }

    public ForecastClass GetWeatherForecast(string sURL, string PlaceCode)
    {
      Result = MakeWebRequest(sURL, PlaceCode);
      Result = Result.Replace("3h", "_3h");
      ForecastClass o = JsonConvert.DeserializeObject<ForecastClass>(Result);
      File.WriteAllText("test.txt", Result);
      return o;
    }

    public string ParseForTTS(string s)
    {
      s = s.Replace("winds", "whinds");
      return s;
    }

    public string GetReportName(WeatherClass c)
    {
      string ds = DateTime.Now.ToString("yyyyMMdd_HHmmss");
      return "WEATHER_" + ds + "_" + c.name;
    }

    public string SaveReport(WeatherClass c)
    {
      string s = JsonConvert.SerializeObject(c);
      Directory.CreateDirectory("../data/reports/"+ c.id);
      string ds = DateTime.Now.ToString("yyyyMMdd_HHmmss");
      string ReportPath = "../data/reports/" + c.id + "/";
      File.WriteAllText(ReportPath + ds + ".txt", s);
      string icon = c.weather[0].icon;
      if (!File.Exists(ReportPath + icon + ".png"))
      {
        File.Copy("../data/icons/" + icon + ".png", ReportPath + icon + ".png");
      }
      return s;
    }

    string GetWindDirection(float angle)
    {
      string w = "";

      if (angle >= 0 && (angle < 25))
      {
        w += " in a northerly direction";
      }
      else
        if (angle >= 25 && (angle < 65))
      {
        w += " in a north-easterly direction";
      }
      else
      if ((angle >= 65) && (angle < 110))
      {
        w += " in an easterly direction";
      }
      else if ((angle >= 110) && (angle < 150))
      {
        w += " in a south easterly direction";
      }
      else if ((angle >= 150) && (angle < 215))
      {
        w += " in a southerly direction";
      }
      else if ((angle >= 215) && (angle < 250))
      {
        w += " in a south westerly direction";
      }
      else if ((angle >= 250) && (angle < 290))
      {
        w += " in a westerly direction";
      }
      else if ((angle >= 290) && (angle < 320))
      {
        w += " in a north-westerly direction";
      }
      else
      {
        w += " in a northerly direction";
      }
      return w;

    }

    public string GenerateForecast(ForecastClass fc)
    {
      WeatherClass o = (WeatherClass)fc.list[fc.list.Count-1];
      string w = ". By tomorrow" + o.name;

      DateTime dt = UnixTimeStampToDateTime(o.dt);

      if (dt.Hour < 12)
      {
        w += " morning";
      }
      else
      {
        w += " afternoon";
      }

      w += " we will have " + o.weather[0].description;

      if (o.weather.Count > 1)
      {
        w += " and " + o.weather[1].description;
      }

      w += " with winds reaching " + Math.Ceiling(o.wind.speed) + " meters per second";
      w += GetWindDirection((float)o.wind.deg) + ".";
      w += " The outside temperature will be " + KelvinToC(o.main.temp) + " degrees";
      if (o.main.temp_min != o.main.temp_max)
      {
        w += " and will reach a low of " + KelvinToC(o.main.temp_min) + " and a high of " + KelvinToC(o.main.temp_max);
      }
      else
      {
        w += " likely to stay the same until the day after tomorrow.";
      }

      return w;
    }


    string GetQuirk( double temp, double windspeed)
    {
      string w = "";
      if (temp<10)
      {
        w += "Dress warm";
      }
      if (temp > 50)
      {
        w += "Heat wave";
      }


      if (( windspeed > 10 ) && ( windspeed < 20 ))
      {
        w += "It's windy";
      }

      if ((windspeed >= 20) && (windspeed < 30))
      {
        w += "High wind warning";
      }

      if (windspeed >= 30)
      {
        w += "Hurricane Warning";
      }

      return w;
    }

    /**
     * Generates a report from the JSON created class
     */
    public string GenerateReport(WeatherClass o, ForecastClass f)
    {
      string w = "";

      w+= GetQuirk(KelvinToC(o.main.temp), o.wind.speed) + ". ";

        w += "In " + o.name;
      WeatherClass tomorrow = (WeatherClass)f.list[f.list.Count - 1];

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

      w += " with winds reaching " + Math.Ceiling(o.wind.speed) + " meters per second";

      w += GetWindDirection((float)o.wind.deg) + ".";

      w += " The outside temperature is " + KelvinToC(o.main.temp) + " degrees";

      if ( o.main.temp > tomorrow.main.temp)
      {
        w += " falling to " + KelvinToC(tomorrow.main.temp);
      }
      else if ( o.main.temp < tomorrow.main.temp)
      {
        w += " rising to " + KelvinToC(tomorrow.main.temp);
      }
      else
      {
        w += " staying at " + KelvinToC(tomorrow.main.temp);
      }

      w += " degrees tomorrow";

      /*
      if (o.main.temp_min != o.main.temp_max) {
        w += " and will reach a low of " + KelvinToC(o.main.temp_min) + " and a high of " + KelvinToC(o.main.temp_max);
      } else
      {
        w += " likely to stay the same until tomorrow.";
      }
      */

      //Console.ReadLine();

      return w;
    }

  }
}
