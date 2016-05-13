using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Parameters;
using System.Windows.Forms;

namespace Generator
{

  class QTweet
  {
    public long id;
    public int retweets;
    public string text;
    public string author;
    public float sentiment;
    public DateTime time;
  }

  class TwitterBot
  {

    public bool PopularResults = false; //when true return only popular results
    public bool RecentResults = true; //when true return only recent results
    public bool MixedResults = false; //when true return only mixed results

    public TwitterBot()
    {
      Auth.SetUserCredentials("Cjb5edejG9GaW2k6JlyfgYyPx", "m1aZ9GpbbRuwEjFxLjWwFg9eLd2tzf4STK9mWZYSvl4GdUyPH6", "721031630807265280-S5TisYo9MPII6VMiJ2F5pSC56aysWcH", "iF1jHQuUOPMAToVn2sXX8dnUDqJ3yJjk5u9fvYVe0PtPB");
    }

    public string GetInfo()
    {
      
      var user = User.GetAuthenticatedUser();
      Console.WriteLine(user.ScreenName);
      return user.Name + ":" +user.Description;
    }

    public string MakeTweet(string s)
    {
      var user = User.GetAuthenticatedUser();
      var result = user.PublishTweet(s);
      //Tweet(s);
      return result.Text;      
    }

    public string MakeRetweet(long id)
    {
      Tweet.PublishRetweet(id);
      return "OK";
    }

    public string DoSearch(string sTopic)
    {
      // Simple Search
      var matchingTweets = Search.SearchTweets(sTopic);
      if (matchingTweets == null) return "NULL RESULT";
      string s = "";
            
      foreach( var t in matchingTweets)
      {
        s += t.Text + "\r\n\r\n";
      }
      return s;
    }

    public IEnumerable<Tweetinvi.Core.Interfaces.ITweet> DoSearchParams(string sTopic)
    {
      var searchParameter = new TweetSearchParameters(sTopic)
      {
        //GeoCode = new GeoCode(-122.398720, 37.781157, 1, DistanceMeasure.Miles),
        Lang = Language.English,
        //SearchType = SearchResultType.Popular,
        MaximumNumberOfResults = 50,
        Until = DateTime.Now,
        //SinceId = 399616835892781056,
        //MaxId = 405001488843284480,
        //Filters = TweetSearchFilters.Images
      };

      searchParameter.TweetSearchType = TweetSearchType.OriginalTweetsOnly;

      if ( PopularResults )
      {
        searchParameter.SearchType = SearchResultType.Popular;
      }
      if (RecentResults)
      {
        searchParameter.SearchType = SearchResultType.Recent;
      }
      if (MixedResults)
      {
        searchParameter.SearchType = SearchResultType.Mixed;
      }

      
      var tweets = Search.SearchTweets(searchParameter);
      if (tweets == null) return null;
      foreach (var t in tweets)
      {
        string item = t.Id + "~" + t.CreatedBy.ScreenName + "~" + t.RetweetCount + "~" + t.Text ;                
        SaveToMine(t.Id.ToString(), item);
      }
      return tweets;
    }

   

    public void SaveToMine(string id, string text)
    {
      string MinePath = "../data/mine/twitter";
      if (!Directory.Exists(MinePath)) Directory.CreateDirectory(MinePath);
      File.WriteAllText(MinePath + "/" + id + ".txt", text);

    }

    public void ReplyTo(string user, string s)
    {
      long tweetIdtoReplyTo = 0;
      var tweetToReplyTo = Tweet.GetTweet(tweetIdtoReplyTo);

      // We must add @screenName of the author of the tweet we want to reply to
      var textToPublish = string.Format("@{0} {1}", tweetToReplyTo.CreatedBy.ScreenName, s);
      var tweet = Tweet.PublishTweetInReplyTo(textToPublish, tweetIdtoReplyTo);
      Console.WriteLine("Publish success? {0}", tweet != null);
    }

  }
}
