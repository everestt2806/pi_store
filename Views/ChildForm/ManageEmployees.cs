using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using pi_store.Controllers;
using pi_store.Models;

namespace pi_store.Views.ChildForm
{
    public partial class ManageEmployees : Form
    {
        private EmployeeController employeeController;
        private string option = ""; //add to Save Addition changed, Update to save update infor
        public ManageEmployees()
        {
            InitializeComponent();
            employeeController = new EmployeeController();
        }

        private void FormLoad()
        {
            btnAdd.Enabled = true;
            ClearText();
            LoadEmployees();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            DisableTextBox(txtID, true);
            DisableTextBox(txtName, true);
            DisableTextBox(txtEmail, true);
            DisableTextBox(txtPhone, true);
            DisableTextBox(txtSalary, true);
            DisableTextBox(txtAddress, true);
            dtHiredate.Enabled = false;
        }
        private void LoadEmployees()
        {
            List<Employee> employees = employeeController.GetAllEmployees();

            grd_Employee.Rows.Clear();

            foreach (var employee in employees)
            {
                int rowIndex = grd_Employee.Rows.Add();
                DataGridViewRow row = grd_Employee.Rows[rowIndex];

                row.Cells["ID"].Value = employee.ID;
                row.Cells["Name"].Value = employee.Name;
                row.Cells["Email"].Value = employee.Email;
                row.Cells["Phone"].Value = employee.Phone;
                row.Cells["Address"].Value = employee.Address;
                row.Cells["HireDate"].Value = employee.HireDate.ToString("dd/MM/yyyy");
                row.Cells["Salary"].Value = employee.Salary.ToString("C2");
            }
        }



        private void ManageEmployees_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void grd_Employee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtID.Text = grd_Employee.CurrentRow.Cells[0].Value.ToString().Trim();
            txtName.Text = grd_Employee.CurrentRow.Cells[1].Value.ToString().Trim();
            txtEmail.Text = grd_Employee.CurrentRow.Cells[2].Value.ToString().Trim();
            txtPhone.Text = grd_Employee.CurrentRow.Cells[3].Value.ToString().Trim();
            txtAddress.Text = grd_Employee.CurrentRow.Cells[4].Value.ToString().Trim();
            dtHiredate.Text = grd_Employee.CurrentRow.Cells[5].Value.ToString().Trim();
            txtSalary.Text = grd_Employee.CurrentRow.Cells[6].Value.ToString().Trim();
        }

        private void DisableTextBox(Guna2TextBox t, bool b)
        {
            t.ReadOnly = b;
            if (b)
            {
                t.FillColor = Color.FromArgb(226, 226, 226);
                t.HoverState.BorderColor = Color.FromArgb(226, 226, 226);
            }
            else
            {
                t.FillColor = Color.White;
                t.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            option = "add";
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            DisableTextBox(txtID, false);
            DisableTextBox(txtName, false);
            DisableTextBox(txtEmail, false);
            DisableTextBox(txtPhone, false);
            DisableTextBox(txtSalary, false);
            DisableTextBox(txtAddress, false);
            dtHiredate.Enabled = true;
            txtID.Focus();
        }
        private void ClearText() {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            dtHiredate.Value = new DateTime(DateTime.Now.Year, 1, 1);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (option.Equals("add")) {
                Employee newEmployee = new Employee();
                newEmployee.ID = txtID.Text;
                newEmployee.Name = txtName.Text;
                newEmployee.Email = txtEmail.Text;
                newEmployee.Phone = txtPhone.Text;
                newEmployee.Address = txtAddress.Text;
                newEmployee.HireDate = dtHiredate.Value;
                newEmployee.Salary = Decimal.Parse(txtSalary.Text);
                employeeController.AddEmployee(newEmployee);
                FormLoad();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormLoad();
        }
    }

    }
