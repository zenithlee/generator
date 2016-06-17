using System;
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
    public TextSynthesizer()
    {
      InitializeComponent();
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
      MarkovChain chain = new MarkovChain();
      chain.Load(txtInput.Text);
      string result = "";

      while ( result.Length < 1000 )
      {
        result += chain.Output();
      }
      txtOutput.Text = result;
    }
  }
}
