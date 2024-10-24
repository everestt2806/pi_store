using System;
using System.Data;
using System.Data.SqlClient;

namespace pi_store.Services
{
    public class DBConnection
    {
        private readonly string connectionString;

        public DBConnection()
        {
            connectionString = "Data Source=SonEverst;Initial Catalog=PiStore;Integrated Security=True;";
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(connectionString);
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Failed to open database connection: {ex.Message}");
                }
            }
            return connection;
        }
    }
}