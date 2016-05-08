using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Generator.Database
{
  class MySQL
  {
    private string databaseName = string.Empty;
    private string serverName = string.Empty;
    public string userName { get; set; }
    public string passWord { get; set; }
    private MySqlConnection connection = null;

    public void Open()
    {
      LoadSettings();
      string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", serverName, databaseName, userName, passWord);
      connection = new MySqlConnection(connstring);
      connection.Open();
    }

    void LoadSettings()
    {
      databaseName = "sentijvbwg_db2";
      serverName = "sql29.jnb2.host-h.net";
      userName = "sentijvbwg_2";
      passWord = "BM9WJ2k7TH8";
    }

    public string Escape(string s)
    {
      return MySqlHelper.EscapeString(s);
    }

    public void Close()
    {
      connection.Close();
    }

    public MySqlDataReader Query(string query)
    {
      var cmd = new MySqlCommand(query, connection);
      var reader = cmd.ExecuteReader();
      return reader;
    }

  }


}
