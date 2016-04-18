using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Parameters;

namespace Generator
{
  class TwitterBot
  {

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

    public string DoSearchParams(string sTopic)
    {
      var searchParameter = new TweetSearchParameters(sTopic)
      {
        //GeoCode = new GeoCode(-122.398720, 37.781157, 1, DistanceMeasure.Miles),
        Lang = Language.English,
        SearchType = SearchResultType.Popular,
        MaximumNumberOfResults = 10,
        //Until = new DateTime(2016, 06, 02),
        //SinceId = 399616835892781056,
        //MaxId = 405001488843284480,
        //Filters = TweetSearchFilters.Images
      };
      string s = "";
      var tweets = Search.SearchTweets(searchParameter);
      foreach (var t in tweets)
      {
        s += t.Id + "~" + t.CreatedBy.Name + "~" + t.CreatedBy.ScreenName + "~" + t.RetweetCount + "~" + t.Text + "\r\n\r\n";
      }
      return s;      
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
