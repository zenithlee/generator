using System;
using System.Collections;

namespace Quantifai.Generators
{
  /// <summary>
  /// Summary description for Structs.
  /// </summary>
  public class Structs
  {
    public struct RootWord
    {
      public bool Start;
      public bool End;
      public string Word;
      public int ChildCount;
      public Hashtable Childs;
    }
    public struct Child
    {
      public int Occurrence;
      public string Word;
    }
  }
  public class MarkovChain
  {
    int RandCounter = 0; //for consecutive calls
    ArrayList startindex = new ArrayList();
    public Hashtable Words = new Hashtable(2048, .1f);

    public void Clear()
    {
      Words = new Hashtable(2048, .1f);
      startindex = new ArrayList();
    }
    public void Load(string Input)
    {
      if (Input.Length == 0) return;
      Input = Input.Replace("\r", "");
      Input = Input.Replace("\n", "");
     // startindex = new ArrayList();
      //Words = new Hashtable(1024, .1f);
      Structs.RootWord w = new Structs.RootWord();
      Structs.Child c = new Structs.Child();
      string[] s = Input.Replace("\r\n", " ").Replace("\t", " ").Replace("  ", " ").Split(' ');
      string s1 = "";
      bool NextisStart = false;
      for (int i = 0; i < s.Length; i++)
      {
        if (s[i] == "")
          continue;
        s1 = s[i].ToLower();
        w = new Structs.RootWord();
        c = new Structs.Child();
        if (Words.ContainsKey(s1))
        {//Already Exists, add new child word or update count of existing child word
          if (i < s.Length - 1)
          {
            w = (Structs.RootWord)Words[s1];
            if (NextisStart)
            {
              w.Start = true;
              NextisStart = false;
              startindex.Add(s1);
            }
            if (w.Childs.ContainsKey(s[i + 1].ToLower())) //Exists, just update count
            {
              c = (Structs.Child)w.Childs[s[i + 1].ToLower()];
              c.Occurrence++;
              w.Childs.Remove(s[i + 1].ToLower());
            }
            else //Doesn't Exist, add new word
            {
              c.Word = s[i + 1];
              c.Occurrence = 1;

            }
            w.ChildCount++;
            w.Childs.Add(s[i + 1].ToLower(), c);

            Words.Remove(s1);
            Words.Add(s1, w);
          }
        }
        else
        {//New Word
          w = new Structs.RootWord();
          w.Childs = new Hashtable();
          if (i == 0)
          {
            w.Start = true;
            startindex.Add(s1);
          }
          w.Word = s[i];
          if (i < s.Length - 1)
          {
            c.Word = s[i + 1];
            c.Occurrence = 1;
            w.Childs.Add(s[i + 1].ToLower(), c);
            w.ChildCount = 1;
          }
          else
            w.End = true;
          if (s1.Substring(s1.Length - 1, 1) == ".")
          {
            w.End = true;
            NextisStart = true;
          }
          else if (NextisStart)
          {
            w.Start = true;
            NextisStart = false;
            startindex.Add(s1);
          }
          Words.Add(s1, w);
        }
      }
    }
    public string Output()
    {
      string output = "";
      Random r = new Random(++RandCounter + Environment.TickCount + startindex.Count);
      int rIndex = r.Next(startindex.Count);
      Structs.RootWord w = (Structs.RootWord)Words[((string)startindex[rIndex]).ToLower()];
      output = w.Word + " ";
      Structs.Child c = new Structs.Child();
      ArrayList a = new ArrayList();
      int pos = 0;
      int rnd = 0; int min = 0; int max = 0;
      do
      {
        rnd = r.Next(w.ChildCount + 1);
        pos = 0;
        foreach (object x in w.Childs)
        {
          c = (Structs.Child)w.Childs[((System.Collections.DictionaryEntry)x).Key];
          if (c.Word == "")
          {
            w.End = true;
            continue;
          }
          min = pos;
          pos += c.Occurrence; //bigger slice for more occurrences
          max = pos;
          if (min <= rnd & max >= rnd)
          {            
            output += c.Word + " ";
            w = (Structs.RootWord)Words[c.Word.ToLower()];           
            break;
          }
        }
      } while (!w.End);
      return output;
    }
  }
}
