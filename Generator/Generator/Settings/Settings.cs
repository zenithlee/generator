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

    public int CampaignTickInterval
    {
      get {
        return Properties.Settings.Default.CampaignTickInterval;
      }

      set {    
        Properties.Settings.Default.CampaignTickInterval = value;
        Save();
      }
    }

    public bool CampaignActive {
      get
      {
        return Properties.Settings.Default.CampaignActive;
      }
      set
      {
        Properties.Settings.Default.CampaignActive = value;
        Save();
      }
    }

    public bool AlwaysOnTop {
      get
      {
        return Properties.Settings.Default.AlwaysOnTop;
      }
      set
      {
        Properties.Settings.Default.AlwaysOnTop = value;
        Save();
      }

    }

    //Form ParentForm = null;

    public Settings()
    {
      InitializeComponent();      
    }
    void Save()
    {
      Properties.Settings.Default.Save();
    } 

    private void AlwaysOnTop_CheckedChanged(object sender, EventArgs e)
    {
      if ( ParentForm != null ) {
        AlwaysOnTop = chk_AlwaysOnTop.Checked;
        Save();
        ParentForm.TopMost = chk_AlwaysOnTop.Checked;
      }
    }
  }
}
