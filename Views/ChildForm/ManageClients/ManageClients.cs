using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using pi_store.Controllers;
using pi_store.Models;

namespace pi_store.Views.ChildForm
{
    public partial class ManageClients : Form
    {
        private ClientController clientController;
        private string option = ""; //add to Save Addition changed, Update to save update infor
        public ManageClients()
        {
            InitializeComponent();
            clientController = new ClientController();
        }

        private void FormLoad()
        {
            btnAdd.Enabled = true;
            grd_Client.ClearSelection();
            grd_Client.Enabled = true;
            ClearText();
            LoadClient();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            DisableTextBox(txtID, true);
            DisableTextBox(txtName, true);
            DisableTextBox(txtEmail, true);
            DisableTextBox(txtPhone, true);
            DisableTextBox(txtAddress, true);
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
        }
        private void DisableAllInputField()
        {
            DisableTextBox(txtID, false);
            DisableTextBox(txtName, false);
            DisableTextBox(txtEmail, false);
            DisableTextBox(txtPhone, false);
            DisableTextBox(txtAddress, false);

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (option.Equals("add"))
            {
                // Kiểm tra rỗng
                if (string.IsNullOrWhiteSpace(txtID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show(this, "Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra định dạng email
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show(this, "Địa chỉ email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra định dạng số điện thoại
                if (!IsValidPhoneNumber(txtPhone.Text))
                {
                    MessageBox.Show(this, "Số điện thoại không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show(this, "Bạn có chắc chắn muốn lưu thông tin khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Client newClient = new Client();
                    newClient.ID = txtID.Text;
                    newClient.Name = txtName.Text;
                    newClient.Email = txtEmail.Text;
                    newClient.Phone = txtPhone.Text;
                    newClient.Address = txtAddress.Text;
                    clientController.AddClient(newClient);
                    FormLoad();
                    MessageBox.Show(this, "Đã lưu thông tin khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (option.Equals("update"))
            {
                if (string.IsNullOrWhiteSpace(txtID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text))
                    
                {
                    MessageBox.Show(this, "Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra định dạng email
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show(this, "Địa chỉ email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra định dạng số điện thoại
                if (!IsValidPhoneNumber(txtPhone.Text))
                {
                    MessageBox.Show(this, "Số điện thoại không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                DialogResult result = MessageBox.Show(this, "Bạn có chắc chắn muốn cập nhật thông tin khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Client updatedClient = new Client();
                    updatedClient.ID = txtID.Text;
                    updatedClient.Name = txtName.Text;
                    updatedClient.Email = txtEmail.Text;
                    updatedClient.Phone = txtPhone.Text;
                    updatedClient.Address = txtAddress.Text;
                    clientController.UpdateClient(updatedClient);
                    FormLoad();
                    MessageBox.Show(this, "Đã cập nhật thông tin khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DisableTextBox(txtAddress, false);
            txtName.Focus();
        }

        private void grd_Client_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtID.Text = grd_Client.CurrentRow.Cells[0].Value.ToString().Trim();
            txtName.Text = grd_Client.CurrentRow.Cells[1].Value.ToString().Trim();
            txtEmail.Text = grd_Client.CurrentRow.Cells[2].Value.ToString().Trim();
            txtPhone.Text = grd_Client.CurrentRow.Cells[3].Value.ToString().Trim();
            txtAddress.Text = grd_Client.CurrentRow.Cells[4].Value.ToString().Trim();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string deleteID = grd_Client.CurrentRow.Cells[0].Value.ToString().Trim();
                clientController.DeleteClient(deleteID);
                FormLoad();
                MessageBox.Show(this, "Đã xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            placeholderLabel.Visible = string.IsNullOrEmpty(txtSearch.Text);
        }

        private void placeholderLabel_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void ManageClients_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            option = "add";
            grd_Client.Enabled = false;
            ClearText();
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            DisableTextBox(txtID, false);
            DisableTextBox(txtName, false);
            DisableTextBox(txtEmail, false);
            DisableTextBox(txtPhone, false);
            DisableTextBox(txtAddress, false);
            txtID.Focus();
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
    }

}
