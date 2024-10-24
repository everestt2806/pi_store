using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using pi_store.Controllers;
using pi_store.Models;

namespace pi_store.Views.ChildForm.ManageClients
{
    public partial class ManageClients : Form
    {
        private ClientController clientController;
        private string option = ""; // add for addition, update for modifying info

        public ManageClients()
        {
            InitializeComponent();
            clientController = new ClientController();
        }

        private void ManageClients_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
            EnableButtonCustom(btnAdd, true);
            grd_Client.ClearSelection();
            grd_Client.Enabled = true;
            ClearText();
            LoadClient();
            EnableButtonCustom(btnSave, false);
            EnableButtonCustom(btnCancel, false);
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);

            txtID.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtName.Enabled = false;
            txtAddress.Enabled = false;
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

        private void LoadClient()
        {
            List<Client> clients = clientController.GetAllClients();
            grd_Client.Rows.Clear();

            foreach (var client in clients)
            {
                int rowIndex = grd_Client.Rows.Add();
                DataGridViewRow row = grd_Client.Rows[rowIndex];
                row.Cells["ID"].Value = client.ID;
                row.Cells["Name"].Value = client.Name;
                row.Cells["Email"].Value = client.Email;
                row.Cells["Phone"].Value = client.Phone;
                row.Cells["Address"].Value = client.Address;
            }
        }

        private void ClearText()
        {
            txtID.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtName.Clear();
            txtAddress.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(txtPhone.Text)
                || string.IsNullOrWhiteSpace(txtEmail.Text)
                || string.IsNullOrWhiteSpace(txtName.Text)
                || string.IsNullOrWhiteSpace(txtAddress.Text)
            )
            {
                MessageBox.Show(
                    this,
                    "Please fill in all fields.",
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
                    "Invalid email address.",
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

            if (option.Equals("add"))
            {
                DialogResult result = MessageBox.Show(
                    this,
                    "Are you sure you want to save this client information?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    Client newClient = new Client
                    {
                        Name = txtPhone.Text,
                        Email = txtEmail.Text,
                        Phone = txtName.Text,
                        Address = txtAddress.Text,
                    };
                    clientController.AddClient(newClient);
                    FormLoad();
                    MessageBox.Show(
                        this,
                        "Client information saved successfully.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            else if (option.Equals("update"))
            {
                DialogResult result = MessageBox.Show(
                    this,
                    "Are you sure you want to update this client information?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    Client updatedClient = new Client
                    {
                        ID = txtID.Text,
                        Name = txtPhone.Text,
                        Email = txtEmail.Text,
                        Phone = txtName.Text,
                        Address = txtAddress.Text,
                    };
                    clientController.UpdateClient(updatedClient);
                    FormLoad();
                    MessageBox.Show(
                        this,
                        "Client information updated successfully.",
                        "Information",
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
            grd_Client.Enabled = false;
            EnableButtonCustom(btnAdd, false);
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);
            EnableButtonCustom(btnSave, true);
            EnableButtonCustom(btnCancel, true);

            txtID.Enabled = false;
            txtPhone.Enabled = true;
            txtEmail.Enabled = true;
            txtName.Enabled = true;
            txtAddress.Enabled = true;
            txtName.Focus();
        }

        private void grd_Client_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || grd_Client.Rows.Count == 0) return;
            if (grd_Client.CurrentRow == null) return;

            try
            {
                EnableButtonCustom(btnUpdate, true);
                EnableButtonCustom(btnDelete, true);

                txtID.Text = grd_Client.CurrentRow.Cells[0].Value?.ToString().Trim() ?? "";
                txtPhone.Text = grd_Client.CurrentRow.Cells[1].Value?.ToString().Trim() ?? "";
                txtEmail.Text = grd_Client.CurrentRow.Cells[2].Value?.ToString().Trim() ?? "";
                txtName.Text = grd_Client.CurrentRow.Cells[3].Value?.ToString().Trim() ?? "";
                txtAddress.Text = grd_Client.CurrentRow.Cells[4].Value?.ToString().Trim() ?? "";
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
                "Are you sure you want to delete this client?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                string deleteID = grd_Client.CurrentRow.Cells[0].Value.ToString().Trim();
                clientController.DeleteClient(deleteID);
                FormLoad();
                MessageBox.Show(
                    this,
                    "Client deleted successfully.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtSearch.Text.Trim();
            List<Client> clients = clientController.SearchClient(searchString);
            grd_Client.Rows.Clear();

            foreach (var client in clients)
            {
                int rowIndex = grd_Client.Rows.Add();
                DataGridViewRow row = grd_Client.Rows[rowIndex];
                row.Cells["ID"].Value = client.ID;
                row.Cells["Name"].Value = client.Name;
                row.Cells["Email"].Value = client.Email;
                row.Cells["Phone"].Value = client.Phone;
                row.Cells["Address"].Value = client.Address;
            }
        }

        private void placeholderLabel_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
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
            grd_Client.Enabled = false;
            ClearText();
            EnableButtonCustom(btnAdd, false);
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);
            EnableButtonCustom(btnSave, true);
            EnableButtonCustom(btnCancel, true);

            txtID.Enabled = false;
            txtPhone.Enabled = true;
            txtEmail.Enabled = true;
            txtName.Enabled = true;
            txtAddress.Enabled = true;
            txtName.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            placeholderLabel.Visible = string.IsNullOrEmpty(txtSearch.Text);

            if (string.IsNullOrEmpty(txtSearch.Text)) 
            {
              
                List<Client> clients = clientController.GetAllClients(); 
                grd_Client.Rows.Clear();

                foreach (var client in clients)
                {
                    int rowIndex = grd_Client.Rows.Add();
                    DataGridViewRow row = grd_Client.Rows[rowIndex];
                    row.Cells["ID"].Value = client.ID;
                    row.Cells["Name"].Value = client.Name;
                    row.Cells["Email"].Value = client.Email;
                    row.Cells["Phone"].Value = client.Phone;
                    row.Cells["Address"].Value = client.Address;
                }
            }
        }
    }
}
