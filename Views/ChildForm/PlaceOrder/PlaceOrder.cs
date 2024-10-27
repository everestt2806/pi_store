using pi_store.Controllers;
using pi_store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pi_store.Views.ChildForm.PlaceOrder
{
    public partial class PlaceOrder : Form
    {
        private ProductController productController;
        private EmployeeController employeeController;
        private ClientController clientController;
        private OrderController orderController; 
        private bool isLoadingData = false;

        public PlaceOrder()
        {
            InitializeComponent();
            productController = new ProductController();
            employeeController = new EmployeeController();
            clientController = new ClientController();
            orderController = new OrderController();
        }

        private void PlaceOrder_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
            grd_Cart.ClearSelection();
            EnableButtonCustom(btnAddCart, false);
            EnableButtonCustom(btnSubmit, false);
            EnableButtonCustom(btnDelete, false);
            LoadComboboxData();
            txtTotalPrice.Text = "0 VND";

        }

        private void LoadComboboxData()
        {
            isLoadingData = true;

            List<Employee> employees = employeeController.GetAllEmployees();
            employees.Insert(0, new Employee { Name = "Select employee" });
            cbEmployeeName.DataSource = employees;
            cbEmployeeName.DisplayMember = "Name";
            cbEmployeeName.ValueMember = "Name";

            cbEmployeeName.DataSource = new List<Employee>(employees);
            cbEmployeeName.DisplayMember = "Name";
            cbEmployeeName.ValueMember = "Name";

            List<Client> clients = clientController.GetAllClients();
            clients.Insert(0, new Client { Name = "Select client" });
            cbClientName.DataSource = clients;
            cbClientName.DisplayMember = "Name";
            cbClientName.ValueMember = "Name";

            cbClientName.DataSource = new List<Client>(clients);
            cbClientName.DisplayMember = "Name";
            cbClientName.ValueMember = "Name";


            List<Product> products = productController.GetAllProducts();
            products.Insert(0, new Product { Name = "Select product" });
            cbProductName.DataSource = products;
            cbProductName.DisplayMember = "Name";
            cbProductName.ValueMember = "Name";

            cbProductName.DataSource = new List<Product>(products);
            cbProductName.DisplayMember = "Name";
            cbProductName.ValueMember = "Name";
            isLoadingData = false;
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
                else if (btn.Name.Equals("btnDelete") || btn.Name.Equals("btnReset"))
                {
                    btn.BackColor = Color.DarkRed;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(94, 148, 255);
                }
            }
        }

        private void cbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbProductName.SelectedIndex > 0)
            {
                Product selectedProduct = (Product)cbProductName.SelectedItem;
                txtProductID.Text = selectedProduct.ID.ToString();
                EnableButtonCustom(btnAddCart, true);
            }
            else
            {
                txtProductID.Clear();
                txtProductID.Clear();
                EnableButtonCustom(btnAddCart, false);
            }
            UpdateSubmitButtonState();
        }
        private void cbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbClientName.SelectedIndex > 0)
            {
                Client selectedClient = (Client)cbClientName.SelectedItem;
                txtClientID.Text = selectedClient.ID.ToString();
                txtEmail.Text = selectedClient.Email.ToString();
                txtPhone.Text = selectedClient.Phone.ToString();
                txtAddress.Text = selectedClient.Address.ToString();
            }
            else
            {
                txtClientID.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
                txtAddress.Clear();
            }
            UpdateSubmitButtonState();
        }
        private void cbEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbEmployeeName.SelectedIndex > 0) 
            {
                Employee selectedEmployee = (Employee)cbEmployeeName.SelectedItem;
                txtEmployeeID.Text = selectedEmployee.ID.ToString();
            }
            else
            {
                txtEmployeeID.Clear();
            }

            UpdateSubmitButtonState(); 
        }


        private void btnAddCart_Click(object sender, EventArgs e)
        {
            if (cbProductName.SelectedIndex > 0 && int.TryParse(txtQuantity.Text, out int quantity))
            {
                Product selectedProduct = (Product)cbProductName.SelectedItem;

             
                if (quantity > selectedProduct.Quantity)
                {
                    MessageBox.Show("Số lượng yêu cầu vượt quá số lượng hàng trong kho.", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool productExists = false;
                foreach (DataGridViewRow row in grd_Cart.Rows)
                {
                    if (row.Cells["productname"].Value.ToString() == selectedProduct.Name)
                    {
                        int currentQuantity = (int)row.Cells["quantity"].Value;

                        
                        if (currentQuantity + quantity > selectedProduct.Quantity)
                        {
                            MessageBox.Show("Số lượng yêu cầu vượt quá số lượng hàng trong kho.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        row.Cells["quantity"].Value = currentQuantity + quantity;
                        productExists = true;
                        break;
                    }
                }

                if (!productExists)
                {
                    string price = selectedProduct.Price.ToString("N0") + " VND";
                    grd_Cart.Rows.Add(selectedProduct.Name, price, quantity);
                }

                ClearProductSelection();
                UpdateTotalPrice();
            }
        }

        private void UpdateTotalPrice()
        {
            int totalPrice = 0;

            foreach (DataGridViewRow row in grd_Cart.Rows)
            {
                if (row.Cells["price"].Value != null && row.Cells["quantity"].Value != null)
                {
                  
                    string priceString = row.Cells["price"].Value.ToString().Trim().Replace(" VND", "").Replace(".", "").Replace(",", "");
                    string quantityString = row.Cells["quantity"].Value.ToString().Trim();

                    
                    if (int.TryParse(priceString, out int price) && int.TryParse(quantityString, out int quantity))
                    {
                        totalPrice += price * quantity;
                    }
                    else
                    {
                        MessageBox.Show("Invalid price or quantity in cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            
            txtTotalPrice.Text = totalPrice.ToString("N0") + " VND";
        }


        private void ClearProductSelection()
        {
            cbProductName.SelectedIndex = -1;
            txtProductID.Clear();
            txtQuantity.Clear();
            EnableButtonCustom(btnAddCart, false);
            grd_Cart.ClearSelection();
        }

        private void UpdateSubmitButtonState()
        {
            bool isReady = grd_Cart.RowCount > 0 &&
                           cbEmployeeName.SelectedIndex > 0 &&
                           cbClientName.SelectedIndex > 0;

            EnableButtonCustom(btnSubmit, isReady);
        }



        private void Clear()
        {
            txtClientID.Clear();
            txtEmployeeID.Clear();
            txtPhone.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtProductID.Clear();
            txtEmail.Clear();
            txtTotalPrice.Clear();
            cbClientName.SelectedIndex = -1;
            cbProductName.SelectedIndex = -1;
            cbEmployeeName.SelectedIndex = -1;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void grd_Cart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableButtonCustom(btnDelete, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grd_Cart.SelectedRows.Count > 0)
            {
                grd_Cart.Rows.RemoveAt(grd_Cart.SelectedRows[0].Index);
                UpdateTotalPrice();


                if (grd_Cart.Rows.Count == 0 || (grd_Cart.Rows.Count == 1 && grd_Cart.AllowUserToAddRows))
                {
                    EnableButtonCustom(btnDelete, false);
                    UpdateSubmitButtonState();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(txtClientID.Text) || string.IsNullOrEmpty(txtEmployeeID.Text))
                {
                    MessageBox.Show("Please enter all required fields.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse total price
                string price = txtTotalPrice.Text.ToString().Replace(" VND", "").Replace(".", "");
                if (!int.TryParse(price.Trim(), out int totalPrice))
                {
                    MessageBox.Show("Invalid price format.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create new order
                Order newOrder = new Order
                {
                    ClientID = txtClientID.Text.Trim(),
                    EmployeeID = txtEmployeeID.Text.Trim(),
                    OrderDate = DateTime.Now,
                    TotalPrice = totalPrice
                };

                // Add order and get new order ID
                string orderID = orderController.AddOrder(newOrder);
                if (string.IsNullOrEmpty(orderID))
                {
                    throw new Exception("Failed to create new order.");
                }

                // Process order items
                foreach (DataGridViewRow row in grd_Cart.Rows)
                {
                    if (row.IsNewRow) continue;

                    string productName = row.Cells["productname"].Value?.ToString();
                    if (string.IsNullOrEmpty(productName)) continue;

                    Product product = productController.GetProductByName(productName);
                    if (product == null) continue;

                    if (!int.TryParse(row.Cells["quantity"].Value?.ToString(), out int quantity))
                        continue;

                    // Create new OrderItem
                    OrderItem newOrderItem = new OrderItem
                    {
                        OrderID = orderID,
                        ProductID = product.ID,
                        Quantity = quantity
                    };

                    string orderItemID = orderController.AddOrderItem(newOrderItem);
                    if (string.IsNullOrEmpty(orderItemID))
                    {
                        throw new Exception($"Failed to add order item for product {productName}");
                    }

                    // Update product quantity in stock
                    int newQuantityInStock = product.Quantity - quantity;
                    productController.UpdateProductQuantityInDb(product.ID, newQuantityInStock);
                }

                MessageBox.Show($"Order {orderID} has been submitted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear();
                grd_Cart.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while submitting the order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
