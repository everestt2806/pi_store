﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml.Linq;
using ClosedXML.Excel;
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
            EnableButtonCustom(btnSave, false);
            EnableButtonCustom(btnCancel, false);
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);

            txtOrderID.Enabled = false;
            cbClientID.Enabled = false;
            cbClientName.Enabled = false;
            cbEmployeeID.Enabled = false;
            cbEmployeeName.Enabled = false;
            txtTotalPrice.Enabled = false;
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
            grd_Order.ClearSelection();
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
            grd_Order.Enabled = false;
            EnableButtonCustom(btnUpdate, false);
            EnableButtonCustom(btnDelete, false);
            EnableButtonCustom(btnSave, true);
            EnableButtonCustom(btnCancel, true);

            txtOrderID.Enabled = false;
            cbClientID.Enabled = true;
            cbClientName.Enabled = true;
            cbEmployeeID.Enabled = true;
            cbEmployeeName.Enabled = true;
            dpOrderdate.Enabled = true;
            txtTotalPrice.Enabled = true;
           
        }

        private void grd_Order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex < 0 || grd_Order.Rows.Count == 0) return;

           
            if (grd_Order.CurrentRow == null) return;

            try
            {
                EnableButtonCustom(btnUpdate, true);
                EnableButtonCustom(btnDelete, true);

                
                txtOrderID.Text = grd_Order.CurrentRow.Cells[0].Value?.ToString().Trim() ?? "";
                cbClientID.Text = grd_Order.CurrentRow.Cells[1].Value?.ToString().Trim() ?? "";
                cbClientName.Text = grd_Order.CurrentRow.Cells[2].Value?.ToString().Trim() ?? "";
                cbEmployeeID.Text = grd_Order.CurrentRow.Cells[3].Value?.ToString().Trim() ?? "";
                cbEmployeeName.Text = grd_Order.CurrentRow.Cells[4].Value?.ToString().Trim() ?? "";
                dpOrderdate.Text = grd_Order.CurrentRow.Cells[5].Value?.ToString().Trim() ?? "";
                txtTotalPrice.Text = grd_Order.CurrentRow.Cells[6].Value?.ToString()
                      .Replace(" VND", "")
                      .Replace(".", "")
                      .Trim() ?? "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                

            }
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

            if (string.IsNullOrEmpty(txtSearch.Text))
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
                    row.Cells["total_price"].Value = order.TotalPrice.ToString("N0") + " VND";
                }
            }
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            grd_Order.ClearSelection();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.FileName = "Orders.csv"; // Default filename for the file

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    // Add header to the first row
                    for (int j = 0; j < grd_Order.Columns.Count; j++)
                    {
                        worksheet.Cell(1, j + 1).Value = grd_Order.Columns[j].HeaderText; // j + 1 because Excel starts at 1
                    }

                    // Populate data from DataGridView to worksheet starting from the second row
                    for (int i = 0; i < grd_Order.Rows.Count; i++)
                    {
                        for (int j = 0; j < grd_Order.Columns.Count; j++)
                        {
                            object cellValue = grd_Order.Rows[i].Cells[j].Value;
                            worksheet.Cell(i + 2, j + 1).Value = cellValue?.ToString(); // i + 2 to start from the second row
                        }
                    }

                    // Save file as CSV
                    using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        workbook.SaveAs(stream);
                    }

                    MessageBox.Show("Data exported successfully!", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
