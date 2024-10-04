using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pi_store.Controllers;
using pi_store.Models;

namespace pi_store.Views.ChildForm
{
    public partial class ManageEmployees : Form
    {
        private EmployeeController employeeController;
        public ManageEmployees()
        {
            InitializeComponent();
            employeeController = new EmployeeController();
        }

        private void FormLoad() {
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            // Lấy danh sách nhân viên từ controller
            List<Employee> employees = employeeController.GetAllEmployees();

            // Xóa các hàng hiện tại (nếu có) trước khi thêm hàng mới
            grd_Employee.Rows.Clear();

            // Duyệt qua danh sách nhân viên và thêm từng hàng vào DataGridView
            foreach (var employee in employees)
            {
                int rowIndex = grd_Employee.Rows.Add(); // Thêm hàng mới và lấy chỉ số hàng
                DataGridViewRow row = grd_Employee.Rows[rowIndex]; // Lấy đối tượng hàng vừa thêm

                // Gán dữ liệu cho các cột tương ứng
                row.Cells["ID"].Value = employee.ID;                  // Cột ID
                row.Cells["Name"].Value = employee.Name;              // Cột Name
                row.Cells["Email"].Value = employee.Email;            // Cột Email
                row.Cells["Phone"].Value = employee.Phone;            // Cột Phone
                row.Cells["Address"].Value = employee.Address;        // Cột Address
                row.Cells["HireDate"].Value = employee.HireDate.ToString("dd/MM/yyyy"); // Cột Hire Date
                row.Cells["Salary"].Value = employee.Salary.ToString("C2"); // Cột Salary
            }
        }



        private void ManageEmployees_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
}
