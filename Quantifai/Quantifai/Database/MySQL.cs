using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Quantifai.Database
{
  class MySQL
  {
    private string databaseName = string.Empty;
    private string serverName = string.Empty;
    public string userName { get; set; }
    public string passWord { get; set; }
    private MySqlConnection connection = null;
    
    //get last 100 containing the word $AAPL
    //(SELECT * FROM sl_twitter WHERE text LIKE '%$AAPL%' ORDER BY datecreated DESC LIMIT 1000) ORDER BY datecreated ASC;

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

    public string GetDBString(string SqlFieldName, MySqlDataReader Reader)
    {
      return Reader[SqlFieldName].Equals(DBNull.Value) ? String.Empty : Reader.GetString(SqlFieldName);
    }

    public double GetDBDouble(string SqlFieldName, MySqlDataReader Reader)
    {
      return Reader[SqlFieldName].Equals(DBNull.Value) ? Double.MinValue : Reader.GetDouble(SqlFieldName);
    }

    public MySqlDataReader GetLatestFor(string sKeyword, int limit = 100)
    {
      string query = "(SELECT * FROM sl_twitter WHERE text LIKE '%" + sKeyword + "%' ORDER BY datecreated DESC LIMIT " + limit + ") ORDER BY datecreated ASC;";
      Open();
      var cmd = new MySqlCommand(query, connection);
      var reader = cmd.ExecuteReader();
      return reader;
    }

    public MySqlDataReader Query(string query)
    {
      Open();
      var cmd = new MySqlCommand(query, connection);      
      var reader = cmd.ExecuteReader();     
      return reader;
    }

  }


}
