using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using pi_store.Services;
using pi_store.Models;
using System.Data;
using System.Windows.Forms;

namespace pi_store.DataAccess
{
    public class ProductDAO
    {
        private DBConnection conn;

        public ProductDAO()
        {
            this.conn = new DBConnection();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM Product";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }

        public Product GetProductByName(string name)
        {
            string query = "SELECT * FROM Product WHERE Name = @Name";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@Name", name);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Product product = new Product
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                    };
                    reader.Close();
                    return product;
                }
                reader.Close();
                return null;
            }
        }

        public List<Product> LoadProductsByOrderID(string orderID)
        {
            List<Product> products = new List<Product>();

            string query = @"
        SELECT p.ID, p.Name, p.Price, oi.Quantity
        FROM OrderItem oi
        JOIN Product p ON oi.ProductID = p.ID
        WHERE oi.OrderID = @OrderID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@OrderID", orderID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ID = reader["ID"].ToString(),
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToInt32(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }

        public bool UpdateProductQuantityInDb(string productId, int newQuantity)
        {
            try
            {
                string query = "UPDATE Product SET Quantity = @Quantity WHERE ID = @ProductId";
                using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Quantity", newQuantity);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    
                    int rowsAffected = command.ExecuteNonQuery();
                    

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ghi log nếu cần
                Console.WriteLine($"Error updating product quantity: {ex.Message}");
                return false;
            }
        }

        public Product GetProductById(string id)
        {
            string query = "SELECT * FROM Product WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Product product = new Product
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                    };
                    reader.Close();
                    return product;
                }
                reader.Close();
                return null;
            }
        }

        public void AddProduct(Product product)
        {

            using (SqlCommand command = new SqlCommand("sp_AddProduct", conn.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

              
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            string query = @"UPDATE Product 
                             SET Name = @Name, Description = @Description, Price = @Price, Quantity = @Quantity 
                             WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", product.ID);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(string productID)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn.GetConnection();

                
                command.CommandText = "DELETE FROM [OrderItem] WHERE ProductID = @ProductID";
                command.Parameters.AddWithValue("@ProductID", productID);
                command.ExecuteNonQuery();

                
                command.CommandText = "DELETE FROM [Product] WHERE ID = @ProductID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ProductID", productID);
                command.ExecuteNonQuery();
            }
        }


        public List<Product> SearchProducts(string searchTerm)
        {
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM Product WHERE Name LIKE @SearchTerm OR ID LIKE @SearchTerm";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }

        public List<string> GetBestSellingProductNames()
        {
            List<string> bestSellingProductNames = new List<string>();
            string query = @"
SELECT TOP 3 Product.Name
FROM OrderItem
JOIN Product ON OrderItem.ProductID = Product.ID
GROUP BY Product.Name
ORDER BY SUM(OrderItem.Quantity) DESC";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        bestSellingProductNames.Add(result["Name"].ToString());
                    }
                }
            }
            return bestSellingProductNames;
        }

    }
}

