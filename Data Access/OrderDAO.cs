using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using pi_store.Services;
using pi_store.Models;
using System.Data;

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
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"])
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
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"]),
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

            using (SqlConnection connection = conn.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", order.ID);
                    command.Parameters.AddWithValue("@ClientID", order.ClientID);
                    command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddOrderItem(OrderItem orderItem)
        {
            string query = @"INSERT INTO OrderItem (ID, OrderID, ProductID, Quantity) 
                     VALUES (@ID, @OrderID, @ProductID, @Quantity)";

            using (SqlConnection connection = conn.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", orderItem.ID);
                    command.Parameters.AddWithValue("@OrderID", orderItem.OrderID);
                    command.Parameters.AddWithValue("@ProductID", orderItem.ProductID);
                    command.Parameters.AddWithValue("@Quantity", orderItem.Quantity);

                    command.ExecuteNonQuery();
                }
            } 
        }



        private static readonly object orderLock = new object();
        private static readonly object orderItemLock = new object();

        public string GenerateNewOrderID()
        {
            lock (orderLock) // Sử dụng lock để đồng bộ truy cập hàm này
            {
                string newOrderId = "OD01";

                using (SqlConnection connection = conn.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Get largest OrderID 
                    string getLastOrderIdQuery = "SELECT TOP 1 ID FROM [Order] ORDER BY ID DESC";
                    using (SqlCommand getLastOrderIdCmd = new SqlCommand(getLastOrderIdQuery, connection))
                    {
                        object result = getLastOrderIdCmd.ExecuteScalar();

                        if (result != null)
                        {
                            string lastOrderId = result.ToString();
                            int lastOrderNumber = int.Parse(lastOrderId.Substring(2));
                            newOrderId = "OD" + (lastOrderNumber + 1).ToString("D2");
                        }
                    }
                }

                return newOrderId;
            }
        }

        public string GenerateNewOrderItemID()
        {
            lock (orderItemLock) // Sử dụng lock để đồng bộ truy cập hàm này
            {
                string newOrderItemId = "OI01";

                using (SqlConnection connection = conn.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Get largest OrderItemID 
                    string getLastOrderItemIdQuery = "SELECT TOP 1 ID FROM OrderItem ORDER BY ID DESC";
                    using (SqlCommand getLastOrderItemIdCmd = new SqlCommand(getLastOrderItemIdQuery, connection))
                    {
                        object result = getLastOrderItemIdCmd.ExecuteScalar();

                        if (result != null)
                        {
                            string lastOrderItemId = result.ToString();
                            int lastOrderItemNumber = int.Parse(lastOrderItemId.Substring(2));
                            newOrderItemId = "OI" + (lastOrderItemNumber + 1).ToString("D2");
                        }
                    }
                }

                return newOrderItemId;
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
                        ClientName = reader["ClientName"].ToString(),
                        EmployeeName = reader["EmployeeName"].ToString(), 
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"])
                    };
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }
    }
}
