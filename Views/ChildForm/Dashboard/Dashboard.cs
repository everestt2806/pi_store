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
using System.Windows.Forms.DataVisualization.Charting;


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
            LoadBestSeller();
            grd_Product.ClearSelection();
            grd_Client.ClearSelection();
            LoadData();
            LoadDashboardData();
        }

        public void LoadTopCustomers()
        {
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

        public void LoadBestSeller()
        {
            List<string> products = productController.GetBestSellingProductNames();
            grd_Product.Rows.Clear();

            foreach (var product in products)
            {
                int rowIndex = grd_Product.Rows.Add();
                DataGridViewRow row = grd_Product.Rows[rowIndex];
                row.Cells["productname"].Value = product.ToString();
            }
        }

        public void LoadData()
        {
            var (orderCount, clientCount) = orderController.GetOrderAndClientCounts();
            long total = orderController.GetTotalRevenue();
            lbOrder.Text = orderCount.ToString();
            lbClient.Text = clientCount.ToString();
            lbRevenue.Text = total.ToString("N0") + " VND";
        }

        private void LoadDashboardData()
        {
            try
            {
                var dashboardData = orderController.GetDashboardChartData();

                
                ConfigureRevenueChart(dashboardData.RevenueData);

                
                ConfigureTopProductsChart(dashboardData.TopSellingProducts);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureRevenueChart(List<(DateTime Date, decimal Revenue)> revenueData)
        {
            chartRevenue.Series.Clear();
            var revenueSeries = new Series
            {
                Name = "Doanh thu",
                Color = Color.Blue,
                ChartType = SeriesChartType.Column
            };

            foreach (var data in revenueData)
            {
                revenueSeries.Points.AddXY(data.Date.ToShortDateString(), data.Revenue);
            }

            chartRevenue.Series.Add(revenueSeries);

           
            var chartArea = chartRevenue.ChartAreas[0];
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisY.Title = "Doanh thu (VNĐ)";
            chartArea.AxisY.LabelStyle.Format = "N0"; 
            chartArea.AxisX.Interval = 1; 
            chartArea.AxisX.LabelStyle.Angle = -45; 
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 7f);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartRevenue.Legends[0].Enabled = false;
           
        }

        private void ConfigureTopProductsChart(Dictionary<string, int> topProducts)
        {
            chartTopProducts.Series.Clear();
            var productSeries = new Series
            {
                Name = "Sản phẩm bán chạy",
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                LabelFormat = "#,##0 sp",
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

           
            productSeries["PieLabelStyle"] = "Outside";

            
            foreach (var product in topProducts.Take(5))
            {
                productSeries.Points.AddXY(product.Key, product.Value);
            }

            chartTopProducts.Series.Add(productSeries);
            chartTopProducts.BackColor = Color.Transparent;
            chartTopProducts.ChartAreas[0].BackColor = Color.Transparent;

            
            ChartArea chartArea = chartTopProducts.ChartAreas[0];
            chartArea.Position.X = 5;
            chartArea.Position.Y = 5;
            chartArea.Position.Width = 100;  
            chartArea.Position.Height = 100; 
            

            
            chartTopProducts.Legends[0].Docking = Docking.Bottom;
            chartTopProducts.Legends[0].Font = new Font("Segoe UI", 6, FontStyle.Bold);
           

            
            productSeries["PieDrawingStyle"] = "Default";

            
            productSeries["CollectedThreshold"] = "0";
        }

        private void Dashboard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            
            Pen pen = new Pen(Color.Black, 1);

            
            g.DrawLine(pen, 785, 300, 785, this.Height); 

           
            pen.Dispose();
        }
    }
}
