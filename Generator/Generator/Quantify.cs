using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Generator
{
  //https://en.wikipedia.org/wiki/Boltzmann_machine

  public static class Quantify
  { 

    public static float Variance(this List<float> values)
    {
      return values.PopulationVariance(values.Mean(), 0, values.Count);
    }

    public static float PopulationVariance(this List<float> values, float mean, int start, int end)
    {
      float variance = 0;

      for (int i = start; i < end; i++)
      {
        variance += (float)Math.Pow((values[i] - mean), 2);
      }

      int n = end - start;
      if (start > 0) n -= 1;

      return variance / (n);
    }

    public static float SampleVariance(this List<float> values, float mean, int start, int end)
    {
      float variance = 0;

      for (int i = start; i < end; i++)
      {
        variance += (float)Math.Pow((values[i] - mean), 2);
      }

      int n = end - start;
      if (start > 0) n -= 1;

      return variance / (n - 1);
    }

    public static float Mean(this List<float> values)
    {
      return values.Count == 0 ? 0 : values.Mean(0, values.Count);
    }

    public static float Mean(this List<float> values, int start, int end)
    {
      float s = 0;

      for (int i = start; i < end; i++)
      {
        s += values[i];
      }

      return s / (end - start);
    }

    public static float StandardDeviation(this List<float> values)
    {
      return values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
    }

    public static float StandardDeviation(this List<float> values, int start, int end)
    {
      float mean = values.Mean(start, end);
      float variance = values.PopulationVariance(mean, start, end);

      return (float)Math.Sqrt(variance);
    }

    public static float Average(float[] values)
    {
      return values.Average();
    }

    public static float Sum(float[] values)
    {
      return values.Sum();
    }

    public static long Count(List<float> values)
    {
      return values.LongCount();
    }

  }
}
