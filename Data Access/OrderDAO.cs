using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using pi_store.Services;
using pi_store.Models;

namespace pi_store.DataAccess
{
    public class OrderDAO
    {
        private DBConnection conn;

        public OrderDAO()
        {
            this.conn = new DBConnection();
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            string query = @"SELECT o.ID, o.ClientID, o.EmployeeID, c.Name AS ClientName, 
                            e.Name AS EmployeeName, o.OrderDate, o.TotalPrice
                     FROM [Order] o
                     JOIN Client c ON o.ClientID = c.ID
                     JOIN Employee e ON o.EmployeeID = e.ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order
                    {
                        ID = reader["ID"].ToString(),
                        ClientID = reader["ClientID"].ToString(),
                        EmployeeID = reader["EmployeeID"].ToString(),
                        ClientName = reader["ClientName"].ToString(),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                    };
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }


        public Order GetOrderByID(string id)
        {
            string query = @"SELECT o.ID, c.Name AS ClientName, e.Name AS EmployeeName, 
                        o.OrderDate, o.TotalPrice
                 FROM [Order] o
                 JOIN Client c ON o.ClientID = c.ID
                 JOIN Employee e ON o.EmployeeID = e.ID
                 WHERE o.ID = @ID";


            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Order order = new Order
                    {
                        ID = reader["ID"].ToString(),
                        ClientID = reader["ClientID"].ToString(),
                        EmployeeID = reader["EmployeeID"].ToString(),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalPrice = Convert.ToDecimal(reader["Address"]),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        ClientName = reader["ClientName"].ToString()
                    };
                    reader.Close();
                    return order;
                }
                reader.Close();
                return null;
            }
        }

        public void AddOrder(Order order)
        {
            string query = @"INSERT INTO [Order] (ID, ClientID, EmployeeID, OrderDate, TotalPrice) 
                             VALUES (@ID, @ClientID, @EmployeeID, @OrderDate, @TotalPrice)";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", order.ID);
                command.Parameters.AddWithValue("@ClientID", order.ClientID);
                command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Order order)
        {
            string query = @"UPDATE [Order] 
                             SET ClientID = @ClientID, EmployeeID = @EmployeeID, OrderDate = @OrderDate, TotalPrice = @TotalPrice 
                             WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", order.ID);
                command.Parameters.AddWithValue("@ClientID", order.ClientID);
                command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(string id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn.GetConnection();

                command.CommandText = "DELETE FROM [Bill] WHERE OrderID = @OrderID";
                command.Parameters.AddWithValue("@OrderID", id);
                command.ExecuteNonQuery();

              
                command.CommandText = "DELETE FROM [OrderItem] WHERE OrderID = @OrderID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@OrderID", id);
                command.ExecuteNonQuery();

            
                command.CommandText = "DELETE FROM [Order] WHERE ID = @ID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }



        public List<Order> SearchOrders(string searchTerm)
        {
            List<Order> orders = new List<Order>();
            string query = @"SELECT o.ID, c.Name AS ClientName, e.Name AS EmployeeName, 
                                    o.OrderDate, o.TotalPrice
                             FROM [Order] o
                             JOIN Client c ON o.ClientID = c.ID
                             JOIN Employee e ON o.EmployeeID = e.ID
                             WHERE o.ID LIKE @SearchTerm OR c.Name LIKE @SearchTerm";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order
                    {
                        ID = reader["ID"].ToString(),
                        ClientName = reader["ClientName"].ToString(), // Thay đổi
                        EmployeeName = reader["EmployeeName"].ToString(), // Thay đổi
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                    };
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }
    }
}
