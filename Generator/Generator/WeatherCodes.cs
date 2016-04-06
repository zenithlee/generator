using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{

  public class WeatherItem
  {
    public int code;
    public string description;
    public string icon;
  }

  public class WeatherCodes
  {
    WeatherItem[] Items;

    public WeatherCodes()
    {
      AddItem(200, "thunderstorm with light rain", "11d");
      AddItem(201, "thunderstorm with rain", "11d");
      AddItem(202, "thunderstorm with heavy rain", "11d");
      AddItem(210, "light thunderstorm", "11d");
      AddItem(211, "thunderstorm", "11d");
      AddItem(212, "heavy thunderstorm", "11d");
      AddItem(221, "ragged thunderstorm", "11d");
      AddItem(230, "thunderstorm with light drizzle", "11d");
      AddItem(231, "thunderstorm with drizzle", "11d");
      AddItem(232, "thunderstorm with heavy drizzle	", "11d");

      AddItem(300, "light intensity drizze", "09d");
      AddItem(301, "drizzle", "09d");
      AddItem(302, "heavy intensity drizzle", "09d");
      AddItem(310, "light intensity drizzle rain", "09d");
      AddItem(311, "drizzle rain", "09d");
      AddItem(312, "heavy intensity drizzle rain", "09d");
      AddItem(313, "shower rain and drizzle", "09d");
      AddItem(314, "heavy shower rain and drizzle", "09d");
      AddItem(321, "shower drizzle", "09d");

      AddItem(500, "light rain", "10d");
      AddItem(501, "moderate rain", "10d");
      AddItem(502, "heavy intensity rain", "10d");
      AddItem(503, "very heavy rain", "10d");
      AddItem(504, "extreme rain", "10d");
      AddItem(511, "freezing rain", "10d");
      AddItem(520, "light intensity shower rain", "10d");
      AddItem(521, "shower rain", "10d");
      AddItem(522, "heavy intensity shower rain", "10d");
      AddItem(531, "ragged shower rain", "10d");

      AddItem(600, "light snow", "13d");
      AddItem(601, "snow", "13d");
      AddItem(602, "heavy snow", "13d");
      AddItem(611, "sleet", "13d");
      AddItem(612, "shower sleet", "13d");
      AddItem(615, "light rain and snow", "13d");
      AddItem(616, "rain and snow", "13d");
      AddItem(620, "light shower snow", "13d");
      AddItem(621, "shower snow", "13d");
      AddItem(622, "heavy shower snow", "13d");

      AddItem(701, "mist", "50d");
      AddItem(711, "smoke", "50d");
      AddItem(721, "haze", "50d");
      AddItem(731, "sand, dust whirls", "50d");
      AddItem(741, "fog", "50d");
      AddItem(751, "sand", "50d");
      AddItem(761, "dust", "50d");
      AddItem(762, "volcanic ash", "50d");
      AddItem(771, "squalls", "50d");
      AddItem(781, "tornado", "50d");

      AddItem(800, "clear sky", "01d");

      AddItem(801, "few clouds", "02d");
      AddItem(802, "scattered clouds", "02d");
      AddItem(803, "broken clouds", "02d");
      AddItem(804, "overcast clouds", "02d");

      AddItem(900, "tornado", "0d");
      AddItem(901, "tropical storm", "0d");
      AddItem(902, "hurricane", "0d");
      AddItem(903, "cold", "0d");
      AddItem(904, "hot", "0d");
      AddItem(905, "windy", "0d");
      AddItem(906, "hail", "0d");

      AddItem(951, "calm", "0d");
      AddItem(952, "light breeze", "0d");
      AddItem(953, "gentle breeze", "0d");
      AddItem(954, "moderate breeze", "0d");
      AddItem(955, "fresh breeze", "0d");
      AddItem(956, "strong breeze", "0d");
      AddItem(957, "high wind, near gale", "0d");
      AddItem(958, "gale", "0d");
      AddItem(959, "severe gale", "0d");
      AddItem(960, "storm", "0d");
      AddItem(961, "violent storm", "0d");
      AddItem(962, "hurricane", "0d");

    }

    public void AddItem( int Code, string Description, string Icon )
    {
      WeatherItem item = new WeatherItem();
      item.code = Code;
      item.description = Description;
      item.icon = Icon;
    }

    public WeatherItem GetWeatherFromCode(int n) {
      foreach (WeatherItem item in Items)
      {
        if (item.code == n)
        {
          return item;
        }
      }
      return null;
    }
  }
}
