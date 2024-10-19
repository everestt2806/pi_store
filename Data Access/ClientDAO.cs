using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using pi_store.Services;
using pi_store.Models;

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
                        Address = reader["Address"].ToString()
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
        public void AddClient(Client client)
        {
            using (SqlCommand command = new SqlCommand("sp_AddClient", conn.GetConnection()))
            {
                // Đặt kiểu command là StoredProcedure
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Thêm các tham số cho stored procedure
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Address", client.Address);

                // Thực thi stored procedure
                command.ExecuteNonQuery();
            }
        }


        public void UpdateClient(Client client)
        {
            string query = @"UPDATE Client 
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

        public void DeleteClient(string id)
        {
            string query = "DELETE FROM Client WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }

        public List<Client> SearchClients(string searchTerm)
        {
            List<Client> clients = new List<Client>();
            string query = "SELECT * FROM Client WHERE Name LIKE @SearchTerm OR ID LIKE @SearchTerm";

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
                        Address = reader["Address"].ToString()
                    };
                    clients.Add(client);
                }
                reader.Close();
            }
            return clients;
        }
    }
}
