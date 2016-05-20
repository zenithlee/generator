using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Parameters;
using Quantifai.Database;
using MySql.Data.MySqlClient;

namespace Quantifai
{
  public partial class SXE : UserControl
  {
    MySQL db = new MySQL();

    public SXE()
    {
      InitializeComponent();
    }

    public void Setup()
    {
      Auth.SetUserCredentials("Cjb5edejG9GaW2k6JlyfgYyPx", "m1aZ9GpbbRuwEjFxLjWwFg9eLd2tzf4STK9mWZYSvl4GdUyPH6", "721031630807265280-S5TisYo9MPII6VMiJ2F5pSC56aysWcH", "iF1jHQuUOPMAToVn2sXX8dnUDqJ3yJjk5u9fvYVe0PtPB");
    }

    void GetStocks(string sStock)
    { 
      string Query = "SELECT * FROM sentijvbwg_db2.sl_twitter LIMIT 0,1000";
      MySqlDataReader reader = db.Query(Query);
    }

    private void btnTest_Click(object sender, EventArgs e)
    {
      //call the service to get the thing
      GetStocks("$AAPL");
    }
  }
}
