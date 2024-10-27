using pi_store.Controllers;
using pi_store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;


namespace pi_store.Views.ChildForm.Dashboard
{
    public partial class Dashboard : Form
    {
        private OrderController orderController;
        private ClientController clientController;
        private ProductController productController;
        public Dashboard()
        {
            InitializeComponent();
            orderController = new OrderController();
            clientController = new ClientController();
            productController = new ProductController();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        public void FormLoad()
        {
            LoadTopCustomers();
            grd_Client.ClearSelection();
            LoadData();
        }

        public void LoadTopCustomers() { 
            List<CustomerInfo> clients = clientController.GetTopCustomers();
            grd_Client.Rows.Clear();

            foreach (var client in clients)
            {
                int rowIndex = grd_Client.Rows.Add();
                DataGridViewRow row = grd_Client.Rows[rowIndex];
                row.Cells["name"].Value = client.Name;
                row.Cells["phone"].Value = client.Phone;
                row.Cells["totalspent"].Value = client.TotalSpent.ToString("N0") + " VND";
            }
        }

        public void LoadData() {
            var (orderCount, clientCount) = orderController.GetOrderAndClientCounts();
            long total = orderController.GetTotalRevenue();
            lbOrder.Text = orderCount.ToString();
            lbClient.Text = clientCount.ToString();
            lbRevenue.Text = total.ToString("N0") + " VND";
        }
    }
}
