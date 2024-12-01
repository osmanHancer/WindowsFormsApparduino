using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApparduino
{
    internal class Mysql
    {
        static readonly string server = "127.0.0.1";
        static readonly string user = "root";
        static readonly string password = "";
        static readonly string database = "ıha";
        public static string connection_string = "server='" + server + "'; user='" + user + "'; database = '" + database + "'; password='" + password + "'";
        public MySqlConnection MySqlConnection = new MySqlConnection(connection_string);
        public bool connect_db()
        {
            try
            {
                MySqlConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool close_db()
        {
            try
            {
                MySqlConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}
