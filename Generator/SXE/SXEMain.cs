using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SXE
{
  public partial class SXEMain : Form
  {
    public SXEMain()
    {
      InitializeComponent();      
    }

    private void button1_Click(object sender, EventArgs e)
    {
      sxe1.Setup();
    }
  }
}
