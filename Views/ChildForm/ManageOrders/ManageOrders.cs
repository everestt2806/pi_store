using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml.Linq;
using Guna.UI2.WinForms;
using pi_store.Controllers;
using pi_store.Models;

namespace pi_store.Views.ChildForm.ManageOrders
{
    public partial class ManageOrders : Form
    {
        private OrderController orderController;
        private EmployeeController employeeController;
        private ClientController clientController;
        private string option = "";
        private bool isLoadingData = false;
        public ManageOrders()
        {
            InitializeComponent();
            orderController = new OrderController();
            employeeController = new EmployeeController();
            clientController = new ClientController();
        }

        private void ManageOrders_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void FormLoad()
        {
            grd_Order.ClearSelection();
            grd_Order.Enabled = true;
            ClearText();
            LoadComboboxData();
            LoadOrder();
            btnSave1.Enabled = false;
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            DisableTextBox(txtOrderID, true);
            cbClientID.Enabled = false;
            cbClientName.Enabled = false;
            cbEmployeeID.Enabled = false;
            cbEmployeeName.Enabled = false;
            DisableTextBox(txtTotalPrice, true);
            dpOrderdate.Enabled = false;
        }

        private void LoadOrder()
        {
            List<Order> orders = orderController.GetAllOrders();
            grd_Order.Rows.Clear();

            foreach (var order in orders)
            {
                int rowIndex = grd_Order.Rows.Add();
                DataGridViewRow row = grd_Order.Rows[rowIndex];
                row.Cells["order_id"].Value = order.ID;
                row.Cells["client_id"].Value = order.ClientID;
                row.Cells["client_name"].Value = order.ClientName;
                row.Cells["employee_id"].Value = order.EmployeeID;
                row.Cells["employee_name"].Value = order.EmployeeName;
                row.Cells["orderdate"].Value = order.OrderDate;
                string totalPrice = order.TotalPrice.ToString("N0") + " VND";
                row.Cells["total_price"].Value = totalPrice;
            }
        }

        private void DisableTextBox(Guna2TextBox textBox, bool isDisabled)
        {
            textBox.ReadOnly = isDisabled;
            textBox.FillColor = isDisabled ? Color.FromArgb(226, 226, 226) : Color.White;
            textBox.HoverState.BorderColor = isDisabled ? Color.FromArgb(226, 226, 226) : Color.FromArgb(94, 148, 255);
        }

        private void ClearText()
        {
            txtOrderID.Clear();
            cbClientID.SelectedIndex = -1;
            cbClientName.SelectedIndex = -1;
            cbEmployeeID.SelectedIndex = -1;
            cbEmployeeName.SelectedIndex = -1;
            txtTotalPrice.Clear();
            dpOrderdate.Value = new DateTime(DateTime.Now.Year, 1, 1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if (option.Equals("update"))
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you want to update this order information?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Order updatedOrder = new Order
                    {
                        ID = txtOrderID.Text,
                        ClientID = cbClientID.SelectedValue.ToString(),
                        ClientName = cbClientName.Text,
                        EmployeeID = cbEmployeeID.SelectedValue.ToString(),
                        EmployeeName = cbEmployeeName.Text,
                        OrderDate = dpOrderdate.Value,
                        TotalPrice = Convert.ToInt32(txtTotalPrice.Text)
                    };

                    orderController.UpdateOrder(updatedOrder);
                    FormLoad();
                    MessageBox.Show(this, "Order information updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            option = "update";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave1.Enabled = true;
            btnCancel.Enabled = true;

            DisableTextBox(txtOrderID, true);
            cbClientID.Enabled = true;
            cbClientName.Enabled = true;
            cbEmployeeID.Enabled = true;
            cbEmployeeName.Enabled = true;
            dpOrderdate.Enabled = true;
            DisableTextBox(txtTotalPrice, false);
           
        }

        private void grd_Order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtOrderID.Text = grd_Order.CurrentRow.Cells[0].Value.ToString().Trim();
            cbClientID.Text = grd_Order.CurrentRow.Cells[1].Value.ToString().Trim();
            cbClientName.Text = grd_Order.CurrentRow.Cells[2].Value.ToString().Trim();
            cbEmployeeID.Text = grd_Order.CurrentRow.Cells[3].Value.ToString().Trim();
            cbEmployeeName.Text = grd_Order.CurrentRow.Cells[4].Value.ToString().Trim();
            dpOrderdate.Text = grd_Order.CurrentRow.Cells[5].Value.ToString().Trim();
            string total = grd_Order.CurrentRow.Cells[6].Value.ToString().Replace(" VND", "").Replace(".", "");
            txtTotalPrice.Text = total.Trim();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to delete order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string deleteID = grd_Order.CurrentRow.Cells[0].Value.ToString().Trim();
                orderController.DeleteOrder(deleteID);
                FormLoad();
                MessageBox.Show(this, "Order deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtSearch.Text.Trim();
            List<Order> orders = orderController.SearchOrders(searchString);
            grd_Order.Rows.Clear();

            foreach (var order in orders)
            {
                int rowIndex = grd_Order.Rows.Add();
                DataGridViewRow row = grd_Order.Rows[rowIndex];
                row.Cells["order_id"].Value = order.ID;
                row.Cells["client_id"].Value = order.ClientID;
                row.Cells["client_name"].Value = order.ClientName;
                row.Cells["employee_id"].Value = order.EmployeeID;
                row.Cells["employee_name"].Value = order.EmployeeName;
                row.Cells["orderdate"].Value = order.OrderDate;
                string totalPrice = order.TotalPrice.ToString("N0") + " VND";
                row.Cells["total_price"].Value = totalPrice;
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


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            placeholderLabel.Visible = string.IsNullOrEmpty(txtSearch.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void LoadComboboxData()
        {
            isLoadingData = true;

            List<Employee> employees = employeeController.GetAllEmployees();
            cbEmployeeID.DataSource = employees;
            cbEmployeeID.DisplayMember = "ID";
            cbEmployeeID.ValueMember = "ID";

            cbEmployeeName.DataSource = new List<Employee>(employees);
            cbEmployeeName.DisplayMember = "Name";
            cbEmployeeName.ValueMember = "ID";

            List<Client> clients = clientController.GetAllClients();
            cbClientID.DataSource = clients;
            cbClientID.DisplayMember = "ID";
            cbClientID.ValueMember = "ID";

            cbClientName.DataSource = new List<Client>(clients);
            cbClientName.DisplayMember = "Name";
            cbClientName.ValueMember = "ID";

            isLoadingData = false;
        }

        private void cbClientID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbClientID.SelectedValue != null)
            {
                cbClientName.SelectedValue = cbClientID.SelectedValue;
            }
        }

        private void cbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbClientName.SelectedValue != null)
            {
                cbClientID.SelectedValue = cbClientName.SelectedValue;
            }
        }

        private void cbEmployeeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbEmployeeID.SelectedValue != null)
            {
                cbEmployeeName.SelectedValue = cbEmployeeID.SelectedValue;
            }
        }

        private void cbEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbEmployeeName.SelectedValue != null)
            {
                cbEmployeeID.SelectedValue = cbEmployeeName.SelectedValue;
            }
        }
    }
}
