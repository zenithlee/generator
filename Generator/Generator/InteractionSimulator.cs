using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator
{
  class InteractionSimulator
  {
    Timer timer = new Timer();
    Form HostForm;

    public void Test(Form f) {
    {
        HostForm = f;
        timer.Tick += Timer_Tick;
        timer.Enabled = true;
        timer.Interval = 1000;
    }
  }
    private void Timer_Tick(object sender, EventArgs e)
    {
      HostForm.Cursor = new Cursor(Cursor.Current.Handle);
      Random r = new Random();
      Cursor.Position = new Point(Cursor.Position.X + r.Next(-20, 20), Cursor.Position.Y + r.Next(-20,20));
      Cursor.Clip = new Rectangle(HostForm.Location, HostForm.Size);
    }
  }
}
