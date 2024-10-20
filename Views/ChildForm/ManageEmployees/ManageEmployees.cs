﻿using System;
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

namespace pi_store.Views.ChildForm.ManageEmployees
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
            grd_Employee.ClearSelection();
            grd_Employee.Enabled = true;
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
                string salary = employee.Salary.ToString("N0") + " VND";
                row.Cells["Salary"].Value = salary;
            }
        }

        private void ManageEmployees_Load(object sender, EventArgs e)
        {
            FormLoad();
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

        private void ClearText()
        {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            dtHiredate.Value = new DateTime(DateTime.Now.Year, 1, 1);
        }

        private void DisableAllInputField()
        {
            DisableTextBox(txtID, false);
            DisableTextBox(txtName, false);
            DisableTextBox(txtEmail, false);
            DisableTextBox(txtPhone, false);
            DisableTextBox(txtSalary, false);
            DisableTextBox(txtAddress, false);
            dtHiredate.Value = new DateTime(DateTime.Now.Year, 1, 1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (option.Equals("add"))
            {
                if ( string.IsNullOrWhiteSpace(txtName.Text)
                    || string.IsNullOrWhiteSpace(txtEmail.Text)
                    || string.IsNullOrWhiteSpace(txtPhone.Text)
                    || string.IsNullOrWhiteSpace(txtAddress.Text)
                    || string.IsNullOrWhiteSpace(txtSalary.Text)
                )
                {
                    MessageBox.Show(
                        this,
                        "Please fill in all information.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show(
                        this,
                        "Invalid email.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!IsValidPhoneNumber(txtPhone.Text))
                {
                    MessageBox.Show(
                        this,
                        "Invalid phone number.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (dtHiredate.Value > DateTime.Now)
                {
                    MessageBox.Show(
                        this,
                        "HireDate cannot be a future date.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                int salary;
                if (!int.TryParse(txtSalary.Text, out salary) || salary < 0)
                {
                    MessageBox.Show(
                        this,
                        "Invalid salary.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                DialogResult result = MessageBox.Show(
                    this,
                    "Are you sure you want to save this employee information?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    Employee newEmployee = new Employee();
                    newEmployee.Name = txtName.Text;
                    newEmployee.Email = txtEmail.Text;
                    newEmployee.Phone = txtPhone.Text;
                    newEmployee.Address = txtAddress.Text;
                    newEmployee.HireDate = dtHiredate.Value;
                    newEmployee.Salary = salary;
                    employeeController.AddEmployee(newEmployee);
                    FormLoad();
                    MessageBox.Show(
                        this,
                        "Employee information added successfully.",
                        "Notification",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            else if (option.Equals("update"))
            {
                if (
                     string.IsNullOrWhiteSpace(txtName.Text)
                    || string.IsNullOrWhiteSpace(txtEmail.Text)
                    || string.IsNullOrWhiteSpace(txtPhone.Text)
                    || string.IsNullOrWhiteSpace(txtAddress.Text)
                    || string.IsNullOrWhiteSpace(txtSalary.Text)
                )
                {
                    MessageBox.Show(
                        this,
                        "Please fill in all information.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show(
                        this,
                        "Invalid email.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!IsValidPhoneNumber(txtPhone.Text))
                {
                    MessageBox.Show(
                        this,
                        "Invalid phone number.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (dtHiredate.Value > DateTime.Now)
                {
                    MessageBox.Show(
                        this,
                        "HireDate cannot be a future date.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                int salary;
                if (!int.TryParse(txtSalary.Text, out salary) || salary < 0)
                {
                    MessageBox.Show(
                        this,
                        "Invalid salary.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                DialogResult result = MessageBox.Show(
                    this,
                    "Are you sure you want to update this employee information?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    Employee updatedEmployee = new Employee();
                    updatedEmployee.Name = txtName.Text;
                    updatedEmployee.Email = txtEmail.Text;
                    updatedEmployee.Phone = txtPhone.Text;
                    updatedEmployee.Address = txtAddress.Text;
                    updatedEmployee.HireDate = dtHiredate.Value;
                    updatedEmployee.Salary = salary;
                    employeeController.UpdateEmployee(updatedEmployee);
                    FormLoad();
                    MessageBox.Show(
                        this,
                        "Employee information updated successfully.",
                        "Notification",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\d{10,11}$");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            option = "update";
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            DisableTextBox(txtID, true);
            DisableTextBox(txtName, false);
            DisableTextBox(txtEmail, false);
            DisableTextBox(txtPhone, false);
            DisableTextBox(txtSalary, false);
            DisableTextBox(txtAddress, false);
            dtHiredate.Enabled = true;
            txtName.Focus();
        }

        private void grd_Employee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtID.Text = grd_Employee.CurrentRow.Cells[0].Value.ToString().Trim();
            txtName.Text = grd_Employee.CurrentRow.Cells[1].Value.ToString().Trim();
            txtEmail.Text = grd_Employee.CurrentRow.Cells[2].Value.ToString().Trim();
            txtPhone.Text = grd_Employee.CurrentRow.Cells[3].Value.ToString().Trim();
            txtAddress.Text = grd_Employee.CurrentRow.Cells[4].Value.ToString().Trim();
            dtHiredate.Text = grd_Employee.CurrentRow.Cells[5].Value.ToString().Trim();
            string salary = grd_Employee.CurrentRow.Cells[6].Value.ToString().Replace(" VND", "").Replace(".", "");
            txtSalary.Text = salary;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                this,
                "Are you sure you want to delete this employee information??",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                string deleteID = grd_Employee.CurrentRow.Cells[0].Value.ToString().Trim();
                employeeController.DeleteEmployee(deleteID);
                FormLoad();
                MessageBox.Show(
                    this,
                    "Employee information deleted successfully.",
                    "Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtSearch.Text.Trim();
            List<Employee> employees = employeeController.SearchEmployees(searchString);
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
                string salary = employee.Salary.ToString("N0") + " VND";
                row.Cells["Salary"].Value = salary;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            placeholderLabel.Visible = string.IsNullOrEmpty(txtSearch.Text);
        }

        private void placeholderLabel_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            option = "add";
            grd_Employee.Enabled = false;
            ClearText();
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            DisableTextBox(txtID, true);
            DisableTextBox(txtName, false);
            DisableTextBox(txtEmail, false);
            DisableTextBox(txtPhone, false);
            DisableTextBox(txtAddress, false);
            DisableTextBox(txtSalary, false);
            dtHiredate.Enabled = true;
            txtName.Focus();
        }
    }
}
