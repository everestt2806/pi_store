﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using pi_store.Models;
using pi_store.Services;

namespace pi_store.DataAccess
{
    public class OrderDAO
    {
        private DBConnection conn;
        private ClientDAO clientDAO;

        public OrderDAO()
        {
            this.conn = new DBConnection();
            clientDAO = new ClientDAO();
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            string query =
                @"SELECT o.ID, o.ClientID, o.EmployeeID, c.Name AS ClientName, 
                    e.Name AS EmployeeName, o.OrderDate, o.TotalPrice
             FROM [Order] o
             JOIN Client c ON o.ClientID = c.ID
             LEFT JOIN Employee e ON o.EmployeeID = e.ID
             ORDER BY o.OrderDate DESC";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order
                    {
                        ID = reader["ID"].ToString(),
                        ClientID = reader["ClientID"].ToString(),
                        EmployeeID = reader["EmployeeID"] != DBNull.Value ? reader["EmployeeID"].ToString() : null,
                        ClientName = reader["ClientName"].ToString(),
                        EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : "Unknown",
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"]),
                    };
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }


        public List<Order> GetAllOrders_1()
        {
            List<Order> orders = new List<Order>();
            string query =
                @"SELECT o.ID, o.ClientID, o.EmployeeID, e.Name AS EmployeeName, 
                    o.OrderDate, o.TotalPrice
             FROM [Order] o
             JOIN Employee e ON o.EmployeeID = e.ID
             ORDER BY o.OrderDate DESC";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string clientId = reader["ClientID"].ToString();
                    string clientName = clientDAO.getClientNamebyID(clientId);

                    Order order = new Order
                    {
                        ID = reader["ID"].ToString(),
                        ClientID = clientId,
                        EmployeeID = reader["EmployeeID"].ToString(),
                        ClientName = clientName,
                        EmployeeName = reader["EmployeeName"].ToString(),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"]),
                    };
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }

        public string getClientIDbyOrder(string orderID)
        {
            string clientID = string.Empty;
            string query = "SELECT ClientID FROM [Order] WHERE ID = @OrderID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@OrderID", orderID);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    clientID = result.ToString();
                }
            }
            return clientID;
        }

        public string getEmployeeNameByOrderID(string orderID)
        {
            string employeeName = string.Empty;
            string query =
                @"
        SELECT e.Name 
        FROM [Order] o
        JOIN Employee e ON o.EmployeeID = e.ID
        WHERE o.ID = @OrderID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@OrderID", orderID);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    employeeName = result.ToString();
                }
            }
            return employeeName;
        }

        public (int orderCount, int clientCount) GetOrderAndClientCounts()
        {
            int orderCount = 0;
            int clientCount = 0;

            string orderQuery = "SELECT COUNT(*) FROM [Order]";
            string clientQuery = "SELECT COUNT(*) FROM Client";

            // Lấy số lượng Order
            using (SqlCommand orderCommand = new SqlCommand(orderQuery, conn.GetConnection()))
            {
                orderCount = (int)orderCommand.ExecuteScalar();
            }

            // Lấy số lượng Client
            using (SqlCommand clientCommand = new SqlCommand(clientQuery, conn.GetConnection()))
            {
                clientCount = (int)clientCommand.ExecuteScalar();
            }

            return (orderCount, clientCount);
        }

        public Order GetOrderByID(string id)
        {
            string query =
                @"SELECT o.ID, c.Name AS ClientName, e.Name AS EmployeeName, 
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
                        ClientName = reader["ClientName"].ToString(),
                    };
                    reader.Close();
                    return order;
                }
                reader.Close();
                return null;
            }
        }

        public string AddOrder(Order order)
        {
            string newOrderID = null;
            using (SqlConnection connection = conn.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("sp_AddOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientID", order.ClientID);
                    command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            newOrderID = reader["NewOrderID"].ToString();
                        }
                    }
                }
            }
            return newOrderID;
        }

        public string AddOrderItem(OrderItem orderItem)
        {
            string newOrderItemID = null;
            using (SqlConnection connection = conn.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("sp_AddOrderItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderID", orderItem.OrderID);
                    command.Parameters.AddWithValue("@ProductID", orderItem.ProductID);
                    command.Parameters.AddWithValue("@Quantity", orderItem.Quantity);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            newOrderItemID = reader["NewOrderItemID"].ToString();
                        }
                    }
                }
            }
            return newOrderItemID;
        }

        public void UpdateOrder(Order order)
        {
            string query =
                @"UPDATE [Order] 
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
            string query =
                @"SELECT o.ID, c.Name AS ClientName, e.Name AS EmployeeName, 
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
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"]),
                    };
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }

        public long GetTotalRevenue() 
        {
            long totalRevenue = 0; 
            string query = "SELECT SUM(TotalPrice) FROM [Order]";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                var result = command.ExecuteScalar();
                if (result != DBNull.Value)
                    totalRevenue = Convert.ToInt64(result); 
            }
            return totalRevenue;
        }

        public ChartData GetDashboardChartData()
        {
            var dashboardData = new ChartData
            {
                RevenueData = new List<(DateTime, decimal)>(), 
                TopSellingProducts = new Dictionary<string, int>()
            };

            string revenueQuery = @"
        SELECT CAST(OrderDate AS DATE) AS OrderDate, SUM(TotalPrice) AS TotalRevenue
        FROM [Order]
        GROUP BY CAST(OrderDate AS DATE)
        ORDER BY OrderDate ASC";

            string topProductsQuery = @"
        SELECT Product.Name, SUM(OrderItem.Quantity) AS QuantitySold
        FROM [Order]
        JOIN OrderItem ON [Order].ID = OrderItem.OrderID
        JOIN Product ON OrderItem.ProductID = Product.ID
        GROUP BY Product.Name
        ORDER BY QuantitySold DESC";

            using (var connection = conn.GetConnection())
            {
                // Revenue query
                using (var revenueCommand = new SqlCommand(revenueQuery, connection))
                using (var revenueReader = revenueCommand.ExecuteReader())
                {
                    while (revenueReader.Read())
                    {
                        DateTime date = revenueReader.GetDateTime(0);
                        decimal totalRevenue = revenueReader.GetDecimal(1); 
                        dashboardData.RevenueData.Add((date, totalRevenue));
                    }
                }

                // Top products query
                using (var productCommand = new SqlCommand(topProductsQuery, connection))
                using (var productReader = productCommand.ExecuteReader())
                {
                    while (productReader.Read())
                    {
                        string productName = productReader.GetString(0);
                        int quantitySold = productReader.GetInt32(1);
                        dashboardData.TopSellingProducts[productName] = quantitySold;
                    }
                }
            }

            return dashboardData;
        }

    }
}
