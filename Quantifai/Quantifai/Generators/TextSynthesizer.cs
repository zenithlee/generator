using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantifai.Generators
{
  public partial class TextSynthesizer : UserControl
  {
    MarkovChain chain = new MarkovChain();

    public TextSynthesizer()
    {
      InitializeComponent();
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
      
      //chain.Load(txtInput.Text);
      string result = "";

      while ( result.Length < 1000 )
      {
        result += chain.Output() + "\r\n";
      }
      txtOutput.Text = result;
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      openFileDialog1.ShowDialog();
      string sName = openFileDialog1.FileName;
      if ( sName != "" )
      {
        string sContent = File.ReadAllText(sName);
        chain.Load(sContent);
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      chain.Clear();
    }

    private void btnCopyToModel_Click(object sender, EventArgs e)
    {
      chain.Load(txtInput.Text);
    }
  }
}
