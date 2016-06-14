using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantifai.Utilities
{
  class TextTools
  {

    public Dictionary<string, int> FindPhrases(string text)
    {
      Dictionary<string, int> r = new Dictionary<string, int>();

      List<string> BigList = new List<string>();

      string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', ';', '\r','\n' });

      
     
        int counter = 0;
        string phrase = "";
      for ( int c = 3; c < 5; c++) { 

        for (int i= 0; i<words.Length;i++)
        {
        phrase = "";
          if ( i + c  > words.Length ) break;

        for ( int j=0; j<c; j++)
          {
            phrase = phrase + words[i+j] + " ";
          }
          phrase = phrase.Trim(' ');
          if (phrase.Length > 0)
          {
            BigList.Add(phrase.Trim(' '));
          }
        //Console.WriteLine(phrase);
        }
      }

      string sw = "";
      for (int n = 0; n < BigList.Count; n++)
      {
        sw = BigList[n];
        if (r.ContainsKey(sw))
        {
          r[sw]++;
        }
        else
        {
          r.Add(sw, 0);
        }
      } 

      for (int n = 0; n < BigList.Count; n++)
      {
       if ( n % 100 == 1)  Console.WriteLine(n + "/" + BigList.Count);
        for (int m = n; m < BigList.Count; m++)
        {
          sw = BigList[n];
          if (sw == BigList[m])
          {            
           r[sw]++;           
          }
        }
      }

      IOrderedEnumerable<KeyValuePair<string,int>> rs = r.OrderByDescending(x => x.Value);

      return rs.ToDictionary(x=>x.Key,x=>x.Value);
    }

  }
}
