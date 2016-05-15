using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Generator;

namespace Generator.Options
{
  public partial class Settings : UserControl
  {

    //Form ParentForm = null;

    public Settings()
    {
      InitializeComponent();
      //Properties.Settings.Default.AlwaysOnTop = Properties.Settings.Default.AlwaysOnTop;
      AlwaysOnTop.Checked = Properties.Settings.Default.AlwaysOnTop;
    }

    public void SetForm(Form f)
    {
      //ParentForm = f;
    }

    private void AlwaysOnTop_CheckedChanged(object sender, EventArgs e)
    {
      if ( ParentForm != null ) {
        Properties.Settings.Default.AlwaysOnTop = AlwaysOnTop.Checked;
        Properties.Settings.Default.Save();
        ParentForm.TopMost = AlwaysOnTop.Checked;
        
      }
    }
  }
}
