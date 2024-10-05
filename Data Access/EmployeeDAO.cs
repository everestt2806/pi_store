using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pi_store.Services;
using pi_store.Models;

namespace pi_store.DataAccess
{
    public class EmployeeDAO
    {
        private DBConnection conn;

        public EmployeeDAO()
        {
            this.conn = new DBConnection();
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT * FROM Employee";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        HireDate = Convert.ToDateTime(reader["HireDate"])
                    };
                    employees.Add(employee);
                }
                reader.Close();
            }
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            string query = "SELECT * FROM Employee WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        HireDate = Convert.ToDateTime(reader["HireDate"])
                    };
                    reader.Close();
                    return employee;
                }
                reader.Close();
                return null;
            }
        }

        public void AddEmployee(Employee employee)
        {
            string query = @"INSERT INTO Employee (ID, Name, Email, Phone, Address, Salary, HireDate) 
                         VALUES (@ID, @Name, @Email, @Phone, @Address, @Salary, @HireDate)";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", employee.ID);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Phone", employee.Phone);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@HireDate", employee.HireDate);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            string query = @"UPDATE Employee 
                         SET Name = @Name, Email = @Email, Phone = @Phone, 
                             Address = @Address, Salary = @Salary, HireDate = @HireDate 
                         WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", employee.ID);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Phone", employee.Phone);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@HireDate", employee.HireDate);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(string id)
        {
            string query = "DELETE FROM Employee WHERE ID = @ID";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }

        public List<Employee> SearchEmployees(string searchTerm)
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT * FROM Employee WHERE Name LIKE @SearchTerm OR ID LIKE @SearchTerm";

            using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        ID = reader["ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        HireDate = Convert.ToDateTime(reader["HireDate"]),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                    employees.Add(employee);
                }
                reader.Close();
            }
            return employees;
        }
    }
}
