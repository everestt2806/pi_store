using pi_store.Models;
using pi_store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Controllers
{
    public class EmployeeController
    {
        private EmployeeDAO employeeDAO;

        public EmployeeController()
        {
            employeeDAO = new EmployeeDAO();
        }

        public List<Employee> GetAllEmployees()
        {
            return employeeDAO.GetAllEmployees();
        }

        public Employee GetEmployeeById(string id)
        {
            return employeeDAO.GetEmployeeById(id);
        }

        public void AddEmployee(Employee employee)
        {
            employeeDAO.AddEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            employeeDAO.UpdateEmployee(employee);
        }

        public void DeleteEmployee(string id)
        {
            employeeDAO.DeleteEmployee(id);
        }
    }
}
