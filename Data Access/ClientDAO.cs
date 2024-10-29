using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using pi_store.Models;
using pi_store.Services;

namespace pi_store.DataAccess
{
    public class ClientDAO
    {
        private DBConnection conn;

        public ClientDAO()
        {
            this.conn = new DBConnection();
        }

        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            string query = "SELECT * FROM Client";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Client client = new Client
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                    };
                    clients.Add(client);
                }
                reader.Close();
            }
            return clients;
        }

        public Client GetClientById(string id)
        {
            string query = "SELECT * FROM Client WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Client client = new Client
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                    };
                    reader.Close();
                    return client;
                }
                reader.Close();
                return null;
            }
        }

        public string getClientNamebyID(string clientID)
        {
            string clientName = string.Empty;
            string query = "SELECT Name FROM Client WHERE ID = @ClientID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ClientID", clientID);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    clientName = result.ToString();
                }
            }
            return clientName;
        }

        public List<CustomerInfo> GetTopCustomers()
        {
            List<CustomerInfo> topCustomers = new List<CustomerInfo>();
            string query =
                @"
SELECT TOP 3 Client.Name, Client.Phone, SUM(TotalPrice) AS TotalSpent
FROM [Order]
JOIN Client ON [Order].ClientID = Client.ID
GROUP BY Client.Name, Client.Phone
ORDER BY TotalSpent DESC";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        topCustomers.Add(
                            new CustomerInfo
                            {
                                Name = result["Name"].ToString(),
                                Phone = result["Phone"].ToString(),
                                TotalSpent =
                                    result["TotalSpent"] != DBNull.Value
                                        ? Convert.ToInt64(result["TotalSpent"])
                                        : 0,
                            }
                        );
                    }
                }
            }
            return topCustomers;
        }

        public void AddClient(Client client)
        {
            using (SqlCommand command = new SqlCommand("sp_AddClient", conn.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Address", client.Address);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateClient(Client client)
        {
            string query =
                @"UPDATE Client 
                             SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address 
                             WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", client.ID);
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Address", client.Address);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteClient(string clientId)
        {
            using (var connection = conn.GetConnection())
            {

              
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        
                        string deleteOrderItemsQuery = @"
                    DELETE FROM OrderItem 
                    WHERE OrderID IN (
                        SELECT ID FROM [Order] WHERE ClientID = @ClientID
                    )";

                        using (var orderItemsCommand = new SqlCommand(deleteOrderItemsQuery, connection, transaction))
                        {
                            orderItemsCommand.Parameters.AddWithValue("@ClientID", clientId);
                            orderItemsCommand.ExecuteNonQuery();
                        }

                        
                        string deleteOrdersQuery = "DELETE FROM [Order] WHERE ClientID = @ClientID";

                        using (var ordersCommand = new SqlCommand(deleteOrdersQuery, connection, transaction))
                        {
                            ordersCommand.Parameters.AddWithValue("@ClientID", clientId);
                            ordersCommand.ExecuteNonQuery();
                        }

                        
                        string deleteClientQuery = "DELETE FROM Client WHERE ID = @ClientID";

                        using (var clientCommand = new SqlCommand(deleteClientQuery, connection, transaction))
                        {
                            clientCommand.Parameters.AddWithValue("@ClientID", clientId);
                            clientCommand.ExecuteNonQuery();
                        }

                       
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        
                        transaction.Rollback();
                        throw new Exception("Có lỗi xảy ra khi xóa Client: " + ex.Message);
                    }
                }
            }
        }


        public List<Client> SearchClients(string searchTerm)
        {
            List<Client> clients = new List<Client>();
            string query =
                "SELECT * FROM Client WHERE Name LIKE @SearchTerm OR ID LIKE @SearchTerm";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                    };
                    clients.Add(client);
                }
                reader.Close();
            }
            return clients;
        }
    }
}
