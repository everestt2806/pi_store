using pi_store.Controllers;
using pi_store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Font = iTextSharp.text.Font;
using Paragraph = iTextSharp.text.Paragraph;

namespace pi_store.Views.ChildForm.GenerateBill
{
    public partial class BillGenerator : Form
    {
        private OrderController orderController;
        private ClientController clientController;
        private ProductController productController;
        public BillGenerator()
        {
            InitializeComponent();
            orderController = new OrderController();
            clientController = new ClientController();
            productController = new ProductController();
        }

        private void BillGenerator_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        public void FormLoad() {
            LoadOrder();
            grd_Order.ClearSelection();
            EnableButtonCustom(btnGenerateBill, false);
            txtClientID.Enabled = false;
            txtClientName.Enabled = false;
            txtAddress.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtEmployeeName.Enabled = false;
            txtTotalCost.Enabled = false;
            
        }
        string clientID;
        private void LoadOrder()
        {
            List<Order> orders = orderController.GetAllOrders();
            grd_Order.Rows.Clear();

            foreach (var order in orders)
            {
                int rowIndex = grd_Order.Rows.Add();
                DataGridViewRow row = grd_Order.Rows[rowIndex];
                row.Cells["order_id"].Value = order.ID;
                row.Cells["client_name"].Value = order.ClientName;
                row.Cells["order_date"].Value = order.OrderDate;
                row.Cells["total_price"].Value = order.TotalPrice.ToString("N0") + " VND";
               
            }
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
        private void grd_Order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableButtonCustom(btnGenerateBill, true);
            string orderID = grd_Order.CurrentRow.Cells[0].Value.ToString();
            List<Product> products = productController.LoadProductsByOrderID(orderID);
            grd_Product.Rows.Clear();

            foreach (var product in products)
            {
                int rowIndex = grd_Product.Rows.Add();
                DataGridViewRow row = grd_Product.Rows[rowIndex];
                row.Cells["productname"].Value = product.Name;
                row.Cells["price"].Value = product.Price.ToString("N0") + " VND"; ;
                row.Cells["quantity"].Value = product.Quantity;
            }
            grd_Product.ClearSelection();
            string clientID = orderController.getClientIDbyOrder(orderID);
            Client client = clientController.GetClientById(clientID);
            txtEmployeeName.Text = orderController.getEmployeeNameByOrderID(orderID);
            txtTotalCost.Text = grd_Order.CurrentRow.Cells[3].Value.ToString();
            txtClientID.Text = client.ID;
            txtClientName.Text = client.Name;
            txtAddress.Text = client.Address;
            txtEmail.Text = client.Email;
            txtPhone.Text = client.Phone;

        }

        private string GetFontPath()
        {
            
            string fontPath = Path.Combine(Application.StartupPath, "Fonts", "segoeui.ttf");

            
            if (!File.Exists(fontPath))
            {
                
                fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            }

            return fontPath;
        }

        private Font GetFont(float size, int style)
        {
            try
            {
                BaseFont baseFont = BaseFont.CreateFont(GetFontPath(), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                return new Font(baseFont, size, style);
            }
            catch
            {
                
                return FontFactory.GetFont(FontFactory.HELVETICA, size, style);
            }
        }
        public string ConvertNumberToWords(decimal number)
        {
            if (number == 0)
                return "Không đồng";

            string[] unitsMap = new[] { "đồng", "nghìn", "triệu", "tỷ" };
            string[] oneMap = new[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] twoMap = new[] { "", "mười", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };

            string words = "";
            int unitIndex = 0;

            
            while (number > 0)
            {
                int group = (int)(number % 1000);
                number /= 1000;

                if (group > 0)
                {
                    string groupWords = ConvertGroupToWords(group);
                    words = groupWords + (unitIndex > 0 ? " " + unitsMap[unitIndex] : "") + " " + words;
                }

                unitIndex++;
            }

            return words.Trim();
        }

        private string ConvertGroupToWords(int group)
        {
            string[] oneMap = new[] { "Không", "Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín" };
            string[] twoMap = new[] { "", "Mười", "Hai Mươi", "Ba Mươi", "Bốn Mươi", "Năm Mươi", "Sáu Mươi", "Bảy Mươi", "Tám Mươi", "Chín Mươi" };

            string result = "";

            if (group > 99)
            {
                result += oneMap[group / 100] + " Trăm ";
                group %= 100;
            }

            if (group > 19)
            {
                result += twoMap[group / 10] + " ";
                group %= 10;
            }

            if (group > 0)
            {
                if (group == 1 && result != "")
                    result += "Một";
                else
                    result += oneMap[group];
            }

            return result.Trim();
        }

        private void btnGenerateBill_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = $"Bill_{grd_Order.CurrentRow.Cells[0].Value}.pdf";
                bool fileError = false;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể ghi file vì file đang được sử dụng.");
                        }
                    }

                    if (!fileError)
                    {
                        using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();

                           
                            Font normalFont = GetFont(12, 0); 
                            Font boldFont = GetFont(12, 1); 
                            Font headerFont = GetFont(18, 1); 
                            Font titleFont = GetFont(14, 1); 


                            // Header
                            Paragraph header = new Paragraph("PI STORE", headerFont);
                            header.Alignment = Element.ALIGN_CENTER;
                            pdfDoc.Add(header);

                            Paragraph orderID = new Paragraph($"Order ID: {grd_Order.CurrentRow.Cells[0].Value}", boldFont);
                            orderID.Alignment = Element.ALIGN_CENTER;
                            orderID.SpacingBefore = 10f;
                            orderID.SpacingAfter = 20f;
                            pdfDoc.Add(orderID);

                           
                            Paragraph customerTitle = new Paragraph("THÔNG TIN KHÁCH HÀNG", titleFont);
                            customerTitle.SpacingAfter = 10f;
                            pdfDoc.Add(customerTitle);

                            pdfDoc.Add(new Paragraph($"Tên khách hàng: {txtClientName.Text}", normalFont));
                            pdfDoc.Add(new Paragraph($"Số điện thoại: {txtPhone.Text}", normalFont));
                            pdfDoc.Add(new Paragraph($"Email: {txtEmail.Text}", normalFont));
                            pdfDoc.Add(new Paragraph($"Địa chỉ: {txtAddress.Text}", normalFont));
                            pdfDoc.Add(new Paragraph($"Nhân viên thực hiện: {txtEmployeeName.Text}", normalFont));

                            Paragraph spacer = new Paragraph("\n");
                            pdfDoc.Add(spacer);

                          
                            Paragraph productsTitle = new Paragraph("CHI TIẾT ĐƠN HÀNG", titleFont);
                            productsTitle.SpacingAfter = 10f;
                            pdfDoc.Add(productsTitle);

                            PdfPTable table = new PdfPTable(4);
                            float[] widths = new float[] { 4f, 2f, 2f, 2f };
                            table.SetWidths(widths);
                            table.WidthPercentage = 100;

                          
                            table.AddCell(new PdfPCell(new Phrase("Tên sản phẩm", boldFont)));
                            table.AddCell(new PdfPCell(new Phrase("Đơn giá", boldFont)));
                            table.AddCell(new PdfPCell(new Phrase("Số lượng", boldFont)));
                            table.AddCell(new PdfPCell(new Phrase("Thành tiền", boldFont)));

                            
                            foreach (DataGridViewRow row in grd_Product.Rows)
                            {
                                if (row.Cells["productname"].Value != null)
                                {
                                    table.AddCell(new Phrase(row.Cells["productname"].Value.ToString(), normalFont));
                                    table.AddCell(new Phrase(row.Cells["price"].Value.ToString(), normalFont));
                                    table.AddCell(new Phrase(row.Cells["quantity"].Value.ToString(), normalFont));

                                    
                                    string priceStr = row.Cells["price"].Value.ToString().Replace(" VND", "").Replace(",", "");
                                    decimal price = decimal.Parse(priceStr);
                                    int quantity = int.Parse(row.Cells["quantity"].Value.ToString());
                                    decimal total = price * quantity;
                                    table.AddCell(new Phrase(total.ToString("N0") + " VND", normalFont));
                                }
                            }

                            pdfDoc.Add(table);

                            // Tổng tiền
                            Paragraph totalAmount = new Paragraph($"Tổng tiền: {txtTotalCost.Text}", boldFont);
                            totalAmount.Alignment = Element.ALIGN_RIGHT;
                            totalAmount.SpacingBefore = 20f;
                            pdfDoc.Add(totalAmount);
                            decimal totalCost = decimal.Parse(txtTotalCost.Text.Replace(" VND", "").Replace(".", ""));
                            string totalCostInWords = ConvertNumberToWords(totalCost);
                            Paragraph totalAmountInWords = new Paragraph($"{totalCostInWords} ", boldFont);
                            totalAmountInWords.Alignment = Element.ALIGN_RIGHT;
                            totalAmountInWords.SpacingBefore = 10f;
                            pdfDoc.Add(totalAmountInWords);

                            // Footer
                            Paragraph footer = new Paragraph("Cảm ơn quý khách đã sử dụng dịch vụ!", normalFont);
                            footer.Alignment = Element.ALIGN_CENTER;
                            footer.SpacingBefore = 30f;
                            pdfDoc.Add(footer);

                            pdfDoc.Close();
                            stream.Close();

                            MessageBox.Show("Xuất hóa đơn PDF thành công!", "Info");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

    }
}
