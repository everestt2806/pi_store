using Guna.UI2.WinForms;
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
using System.Windows.Forms;

namespace pi_store.Views.ChildForm.ManageProducts
{
    public partial class ManageProducts : Form
    {
        private ProductController productController;
        private string option = "";
        public ManageProducts()
        {
            InitializeComponent();
            productController = new ProductController();
        }

        private void FormLoad()
        {
            btnAdd.Enabled = true;
            grd_Product.ClearSelection();
            grd_Product.Enabled = true;
            ClearText();
            LoadProducts();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            DisableTextBox(txtID, true);
            DisableTextBox(txtName, true);
            DisableTextBox(txtPrice, true);
            DisableTextBox(txtQuantity, true);
            DisableTextBox(txtDescription, true);
        }

        private void LoadProducts()
        {
            List<Product> products = productController.GetAllProducts();

            grd_Product.Rows.Clear();

            foreach (var product in products)
            {
                int rowIndex = grd_Product.Rows.Add();
                DataGridViewRow row = grd_Product.Rows[rowIndex];

                row.Cells["ID"].Value = product.ID;
                row.Cells["Name"].Value = product.Name;
                string price = product.Price.ToString("N0") + " VND";
                row.Cells["Price"].Value = price;
                row.Cells["Quantity"].Value = product.Quantity;
                row.Cells["Description"].Value = product.Description;
            }
        }


        private void ManageProducts_Load(object sender, EventArgs e)
        {
            FormLoad();
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
            txtPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();   
        }

        private void DisableAllInputField()
        {
            DisableTextBox(txtID, false);
            DisableTextBox(txtName, false);
            DisableTextBox(txtPrice, false);
            DisableTextBox(txtQuantity, false);
            DisableTextBox(txtDescription, false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (option.Equals("add"))
            {
                if (
                    string.IsNullOrWhiteSpace(txtID.Text)
                    || string.IsNullOrWhiteSpace(txtName.Text)
                    || string.IsNullOrWhiteSpace(txtPrice.Text)
                    || string.IsNullOrWhiteSpace(txtQuantity.Text)
                    || string.IsNullOrWhiteSpace(txtDescription.Text)    
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

                decimal price;
                if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
                {
                    MessageBox.Show(
                        this,
                        "Invalid price.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                DialogResult result = MessageBox.Show(
                    this,
                    "Are you sure you want to add new product?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    Product newProduct = new Product();
                    newProduct.ID = txtID.Text;
                    newProduct.Name = txtName.Text;
                    newProduct.Price = Convert.ToInt32(txtPrice.Text);
                    newProduct.Quantity = Convert.ToInt32(txtQuantity.Text);
                    newProduct.Description = txtDescription.Text;
                    productController.AddProduct(newProduct);
                    FormLoad();
                    MessageBox.Show(
                        this,
                        "Product added successfully.",
                        "Notification",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            else if (option.Equals("update"))
            {
                if (
                    string.IsNullOrWhiteSpace(txtID.Text)
                    || string.IsNullOrWhiteSpace(txtName.Text)
                    || string.IsNullOrWhiteSpace(txtPrice.Text)
                    || string.IsNullOrWhiteSpace(txtQuantity.Text)
                    || string.IsNullOrWhiteSpace(txtDescription.Text)
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

                decimal price;
                if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
                {
                    MessageBox.Show(
                        this,
                        "Invalid price.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                DialogResult result = MessageBox.Show(
                    this,
                    "Are you sure you want to update product information?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    Product updatedProduct = new Product();
                    updatedProduct.ID = txtID.Text;
                    updatedProduct.Name = txtName.Text;
                    updatedProduct.Price = Convert.ToInt32(txtPrice.Text);
                    updatedProduct.Quantity = Convert.ToInt32(txtQuantity.Text);
                    updatedProduct.Description = txtDescription.Text;
                    productController.UpdateProduct(updatedProduct);
                    FormLoad();
                    MessageBox.Show(
                        this,
                        "Product information updated successfully.",
                        "Notification",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        

     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            option = "add";
            grd_Product.Enabled = false;
            ClearText();
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            DisableTextBox(txtID, false);
            DisableTextBox(txtName, false);
            DisableTextBox(txtPrice, false);
            DisableTextBox(txtQuantity, false);
            DisableTextBox(txtDescription, false); 
            txtID.Focus();
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
            DisableTextBox(txtPrice, false);
            DisableTextBox(txtQuantity, false);
            DisableTextBox(txtDescription, false);
            txtName.Focus();
        }

        private void grd_Product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtID.Text = grd_Product.CurrentRow.Cells[0].Value.ToString().Trim();
            txtName.Text = grd_Product.CurrentRow.Cells[1].Value.ToString().Trim();
            string price = grd_Product.CurrentRow.Cells[2].Value.ToString().Replace(" VND", "").Replace(".", "");
            txtPrice.Text = price.Trim();
            txtQuantity.Text = grd_Product.CurrentRow.Cells[3].Value.ToString().Trim();
            txtDescription.Text = grd_Product.CurrentRow.Cells[4].Value.ToString().Trim();  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                this,
                "Are you sure you want to delete this product information??",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                string deleteID = grd_Product.CurrentRow.Cells[0].Value.ToString().Trim();
                productController.DeleteProduct(deleteID);
                FormLoad();
                MessageBox.Show(
                    this,
                    "Product information deleted successfully.",
                    "Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtSearch.Text.Trim();
            List<Product> products = productController.SearchProducts(searchString);
            grd_Product.Rows.Clear();

            foreach (var product in products)
            {
                int rowIndex = grd_Product.Rows.Add();
                DataGridViewRow row = grd_Product.Rows[rowIndex];

                row.Cells["ID"].Value = product.ID;
                row.Cells["Name"].Value = product.Name;
                string price = product.Price.ToString("N0") + " VND";
                row.Cells["Price"].Value = price;
                row.Cells["Quantity"].Value = product.Quantity;
                row.Cells["Description"].Value = product.Description;        
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
