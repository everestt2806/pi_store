using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Services
{
    public class DBConnection
    {
        private SqlConnection connection;

        public DBConnection()
        {
            string connectionString = "Data Source=SONEVERST;Initial Catalog=PiStore;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        public void CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
