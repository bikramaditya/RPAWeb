using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using MySql.Data.MySqlClient;

namespace LuceneSearch
{
    public interface ISQLReader
    {
        List<RPAResult> ReadAllRows();
    }

    public class SQLDataReader : ISQLReader
    {
        public List<RPAResult> ReadAllRows()
        {

            MySql.Data.MySqlClient.MySqlConnection conn;
            String DBHost = System.Configuration.ConfigurationManager.AppSettings["DBHost"];
            String DBName = System.Configuration.ConfigurationManager.AppSettings["DBName"];
            String DBUser = System.Configuration.ConfigurationManager.AppSettings["DBUser"];
            String DBPW = System.Configuration.ConfigurationManager.AppSettings["DBPW"];

            string  myConnectionString = "Server="+DBHost+";uid="+DBUser+";" +"pwd="+DBPW+";database="+ DBName + ";";

            List<RPAResult> list = new List<RPAResult>();

            try
            {
                
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                MySqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "select * from ticket_category_script_map";
                MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    RPAResult res = new RPAResult();
                    res.ID = dataReader.GetInt32("id");
                    res.KeyWords = dataReader.GetString("key_words");
                    res.ScriptID = dataReader.GetInt32("script_id");
                    res.UserConfirmationMsg = dataReader.GetString("user_confirmation_msg");                    
                    res.ScriptText = dataReader.GetString("script_text");
                    list.Add(res);
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }

            return list;            
        }
    }
}