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
        private string option = "";
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
            btnAddCart.Enabled = false;
            btnSubmit.Enabled = false;
            btnDelete.Enabled = false;
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

        private void cbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cbProductName.SelectedIndex > 0) 
            {
                Product selectedProduct = (Product)cbProductName.SelectedItem;
                txtProductID.Text = selectedProduct.ID.ToString(); 
                txtQuantity.Text = "1"; 
                btnAddCart.Enabled = true;
            }
            else
            {
                txtProductID.Clear();
                txtQuantity.Clear();
                btnAddCart.Enabled = false;
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
            if (!isLoadingData && cbEmployeeName.SelectedIndex > 0)  // Kiểm tra cbEmployeeName
            {
                Employee selectedEmployee = (Employee)cbEmployeeName.SelectedItem;
                txtEmployeeID.Text = selectedEmployee.ID.ToString();
            }
            else
            {
                txtEmployeeID.Clear();
            }

            UpdateSubmitButtonState(); // Gọi sau khi xử lý
        }


        private void btnAddCart_Click(object sender, EventArgs e)
        {
            if (cbProductName.SelectedIndex > 0 && int.TryParse(txtQuantity.Text, out int quantity))
            {
                Product selectedProduct = (Product)cbProductName.SelectedItem;

                bool productExists = false;
                foreach (DataGridViewRow row in grd_Cart.Rows)
                {
                    if (row.Cells["productname"].Value.ToString() == selectedProduct.Name)
                    {
                        row.Cells["quantity"].Value = (int)row.Cells["quantity"].Value + quantity;
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
                UpdateTotalPrice(); // Gọi hàm cập nhật tổng giá
            }
        }
        private void UpdateTotalPrice()
        {
            int totalPrice = 0;

            foreach (DataGridViewRow row in grd_Cart.Rows)
            {
                if (row.Cells["price"].Value != null && row.Cells["quantity"].Value != null)
                {
                    // Chuyển đổi giá trị ô giá thành chuỗi và loại bỏ " VND" và dấu phẩy
                    string priceString = row.Cells["price"].Value.ToString().Trim().Replace(" VND", "").Replace(".", "").Replace(",", "");
                    string quantityString = row.Cells["quantity"].Value.ToString().Trim();

                    // Kiểm tra và chuyển đổi giá trị
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

            // Hiển thị tổng giá trị với định dạng tiền tệ
            txtTotalPrice.Text = totalPrice.ToString("N0") + " VND";
        }


        private void ClearProductSelection()
        {
            cbProductName.SelectedIndex = -1;
            txtProductID.Clear();
            txtQuantity.Clear();
            btnAddCart.Enabled = false;
        }

        private void UpdateSubmitButtonState()
        {
            bool isReady = grd_Cart.RowCount > 0 &&  
                           cbEmployeeName.SelectedIndex > 0 &&  
                           cbClientName.SelectedIndex > 0;    

            btnSubmit.Enabled = isReady;
        }



        private void Clear() {
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
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grd_Cart.SelectedRows.Count > 0)
            {
                grd_Cart.Rows.RemoveAt(grd_Cart.SelectedRows[0].Index);
                UpdateTotalPrice();


                if (grd_Cart.Rows.Count == 0 || (grd_Cart.Rows.Count == 1 && grd_Cart.AllowUserToAddRows))
                {
                    btnDelete.Enabled = false; 
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
                // Lấy dữ liệu từ các TextBox và ComboBox
                string orderID = orderController.GenerateNewOrderID(); // Hàm tự động sinh mã OrderID (như đã tạo trước đó)
                string clientID = txtClientID.Text;
                string employeeID = txtEmployeeID.Text;
                DateTime orderDate = DateTime.Now; // Ngày hiện tại
                string price = txtTotalPrice.Text.ToString().Replace(" VND", "").Replace(".", "");
                int totalPrice = Convert.ToInt32(price.Trim());

                // Tạo đối tượng Order mới
                Order newOrder = new Order
                {
                    ID = orderID,
                    ClientID = clientID,
                    EmployeeID = employeeID,
                    OrderDate = orderDate,
                    TotalPrice = totalPrice
                };

                // Thêm order vào cơ sở dữ liệu
                orderController.AddOrder(newOrder);

                // Thêm các sản phẩm trong giỏ hàng (grd_Cart) vào bảng OrderItem
                foreach (DataGridViewRow row in grd_Cart.Rows)
                {
                    if (row.IsNewRow) continue;

                    string productName = row.Cells["productname"].Value.ToString();
                    Product product = productController.GetProductByName(productName); // Giả sử bạn có hàm này

                    int quantity = int.Parse(row.Cells["quantity"].Value.ToString());

                    // Tạo đối tượng OrderItem mới
                    OrderItem newOrderItem = new OrderItem
                    {
                        ID = orderController.GenerateNewOrderItemID(), // Hàm sinh mã OrderItem
                        OrderID = orderID,
                        ProductID = product.ID,
                        Quantity = quantity
                    };

                    // Thêm order item vào cơ sở dữ liệu
                    orderController.AddOrderItem(newOrderItem);
                }

                // Hiển thị thông báo thành công
                MessageBox.Show("Order has been submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset lại form sau khi submit
                Clear();
                grd_Cart.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while submitting the order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
