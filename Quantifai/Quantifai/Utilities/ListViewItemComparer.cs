using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

class ListViewItemComparer : IComparer
{
  private int col;
  public bool Ascending = true;
  public ListViewItemComparer()
  {
    col = 0;
  }
  public ListViewItemComparer(int column)
  {
    col = column;
  }
  public bool IsNumeric(object Expression)
  {
    double retNum;

    bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
    return isNum;
  }
  public int Compare(object x, object y)
  {
    int returnVal = -1;

    if (IsNumeric(((ListViewItem)x).SubItems[col].Text) && IsNumeric(((ListViewItem)y).SubItems[col].Text))
    {

      double ix = Convert.ToDouble(((ListViewItem)x).SubItems[col].Text);
      double iy = Convert.ToDouble(((ListViewItem)y).SubItems[col].Text);

      if (Ascending == true)
      {
        returnVal = ix > iy ? -1 : 1;
      }
      else
      {
        returnVal = ix < iy ? -1 : 1;
      }
    }
    else
    {
      returnVal = String.Compare(((ListViewItem)y).SubItems[col].Text.ToString(), ((ListViewItem)y).SubItems[col].Text.ToString());
    }
    return returnVal;

  }
}