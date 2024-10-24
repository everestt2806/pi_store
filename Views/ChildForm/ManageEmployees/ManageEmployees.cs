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
            EnableButtonCustom(btnAdd, true);
            grd_Employee.ClearSelection();
            grd_Employee.Enabled = true;
            ClearText();
            LoadEmployees();
            EnableButtonCustom(btnSave, false);
            EnableButtonCustom(btnCancel, false);
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);

            txtID.Enabled = false;
            txtName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtSalary.Enabled = false;
            txtAddress.Enabled = false;
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

        private void EnableButtonCustom(Button btn, bool opt)
        {
            if (!opt)
            {
                btn.BackColor = Color.FromArgb(169, 169, 169);
                btn.Enabled = false;

            }
            else
            {
                btn.Enabled = true;

                if (btn.Name.Equals("btnSave"))
                {
                    btn.BackColor = Color.ForestGreen;
                }
                else if (btn.Name.Equals("btnCancel"))
                {
                    btn.BackColor = Color.DarkRed;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(94, 148, 255);
                }
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
            txtID.Enabled = false;
            txtName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtSalary.Enabled = false;
            txtAddress.Enabled = false;
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
                    updatedEmployee.ID = txtID.Text;
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
            grd_Employee.Enabled = false;
            EnableButtonCustom(btnAdd, false);
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);
            EnableButtonCustom(btnSave, true);
            EnableButtonCustom(btnCancel, true);
            txtID.Enabled = false;
            txtName.Enabled = true;
            txtEmail.Enabled = true;
            txtPhone.Enabled = true;
            txtSalary.Enabled = true;
            txtAddress.Enabled = true;
            dtHiredate.Enabled = false;
            txtName.Focus();
        }

        private void grd_Employee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || grd_Employee.Rows.Count == 0) return;
            if (grd_Employee.CurrentRow == null) return;

            try
            {
                EnableButtonCustom(btnUpdate, true);
                EnableButtonCustom(btnDelete, true);

                txtID.Text = grd_Employee.CurrentRow.Cells[0].Value?.ToString().Trim() ?? "";
                txtName.Text = grd_Employee.CurrentRow.Cells[1].Value?.ToString().Trim() ?? "";
                txtEmail.Text = grd_Employee.CurrentRow.Cells[2].Value?.ToString().Trim() ?? "";
                txtPhone.Text = grd_Employee.CurrentRow.Cells[3].Value?.ToString().Trim() ?? "";
                txtAddress.Text = grd_Employee.CurrentRow.Cells[4].Value?.ToString().Trim() ?? "";
                dtHiredate.Text = grd_Employee.CurrentRow.Cells[5].Value?.ToString().Trim() ?? "";

                string salary = grd_Employee.CurrentRow.Cells[6].Value?.ToString() ?? "0";
                if (!string.IsNullOrEmpty(salary))
                {
                    salary = salary.Replace(" VND", "").Replace(".", "").Trim();
                    txtSalary.Text = salary;
                }
                else
                {
                    txtSalary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);            
            }
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

            if (string.IsNullOrEmpty(txtSearch.Text)) 
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
                }
            }
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
            EnableButtonCustom(btnAdd, false);
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);

            EnableButtonCustom(btnSave, true);
            EnableButtonCustom(btnCancel, true);

            txtID.Enabled = false;
            txtName.Enabled = true;
            txtEmail.Enabled = true;
            txtPhone.Enabled = true;
            txtAddress.Enabled = true;
            txtSalary.Enabled = true;
            dtHiredate.Enabled = false;
            dtHiredate.Value = DateTime.Now;
            txtName.Focus();
        }
    }
}
