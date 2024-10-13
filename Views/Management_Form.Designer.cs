namespace pi_store.Views
{
    partial class Management_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Management_Form));
            this.menu = new System.Windows.Forms.Panel();
            this.bill_generate = new FontAwesome.Sharp.IconButton();
            this.menu_placeorder = new FontAwesome.Sharp.IconButton();
            this.menu_orders = new FontAwesome.Sharp.IconButton();
            this.menu_products = new FontAwesome.Sharp.IconButton();
            this.menu_employ = new FontAwesome.Sharp.IconButton();
            this.menu_clients = new FontAwesome.Sharp.IconButton();
            this.menu_dashboard = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.title_bar = new System.Windows.Forms.Panel();
            this.childFormTitle = new System.Windows.Forms.Label();
            this.iconChildform = new FontAwesome.Sharp.IconPictureBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.title_bar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconChildform)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.menu.Controls.Add(this.bill_generate);
            this.menu.Controls.Add(this.menu_placeorder);
            this.menu.Controls.Add(this.menu_orders);
            this.menu.Controls.Add(this.menu_products);
            this.menu.Controls.Add(this.menu_employ);
            this.menu.Controls.Add(this.menu_clients);
            this.menu.Controls.Add(this.menu_dashboard);
            this.menu.Controls.Add(this.panel1);
            this.menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(291, 1024);
            this.menu.TabIndex = 0;
            // 
            // bill_generate
            // 
            this.bill_generate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.bill_generate.Dock = System.Windows.Forms.DockStyle.Top;
            this.bill_generate.FlatAppearance.BorderSize = 0;
            this.bill_generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bill_generate.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bill_generate.IconChar = FontAwesome.Sharp.IconChar.FileInvoice;
            this.bill_generate.IconColor = System.Drawing.Color.Black;
            this.bill_generate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.bill_generate.IconSize = 32;
            this.bill_generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bill_generate.Location = new System.Drawing.Point(0, 722);
            this.bill_generate.Name = "bill_generate";
            this.bill_generate.Size = new System.Drawing.Size(291, 97);
            this.bill_generate.TabIndex = 7;
            this.bill_generate.Text = "Bill Generator";
            this.bill_generate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bill_generate.UseVisualStyleBackColor = false;
            this.bill_generate.Click += new System.EventHandler(this.bill_generate_Click_1);
            // 
            // menu_placeorder
            // 
            this.menu_placeorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.menu_placeorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu_placeorder.FlatAppearance.BorderSize = 0;
            this.menu_placeorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menu_placeorder.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_placeorder.IconChar = FontAwesome.Sharp.IconChar.CartShopping;
            this.menu_placeorder.IconColor = System.Drawing.Color.Black;
            this.menu_placeorder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menu_placeorder.IconSize = 32;
            this.menu_placeorder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_placeorder.Location = new System.Drawing.Point(0, 625);
            this.menu_placeorder.Name = "menu_placeorder";
            this.menu_placeorder.Size = new System.Drawing.Size(291, 97);
            this.menu_placeorder.TabIndex = 6;
            this.menu_placeorder.Text = "Place Order";
            this.menu_placeorder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.menu_placeorder.UseVisualStyleBackColor = false;
            this.menu_placeorder.Click += new System.EventHandler(this.menu_placeorder_Click);
            // 
            // menu_orders
            // 
            this.menu_orders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.menu_orders.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu_orders.FlatAppearance.BorderSize = 0;
            this.menu_orders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menu_orders.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_orders.IconChar = FontAwesome.Sharp.IconChar.Shopify;
            this.menu_orders.IconColor = System.Drawing.Color.Black;
            this.menu_orders.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menu_orders.IconSize = 32;
            this.menu_orders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_orders.Location = new System.Drawing.Point(0, 528);
            this.menu_orders.Name = "menu_orders";
            this.menu_orders.Size = new System.Drawing.Size(291, 97);
            this.menu_orders.TabIndex = 5;
            this.menu_orders.Text = "Manage Orders";
            this.menu_orders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.menu_orders.UseVisualStyleBackColor = false;
            this.menu_orders.Click += new System.EventHandler(this.menu_orders_Click);
            // 
            // menu_products
            // 
            this.menu_products.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.menu_products.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu_products.FlatAppearance.BorderSize = 0;
            this.menu_products.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menu_products.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_products.IconChar = FontAwesome.Sharp.IconChar.Cubes;
            this.menu_products.IconColor = System.Drawing.Color.Black;
            this.menu_products.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menu_products.IconSize = 32;
            this.menu_products.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_products.Location = new System.Drawing.Point(0, 431);
            this.menu_products.Name = "menu_products";
            this.menu_products.Size = new System.Drawing.Size(291, 97);
            this.menu_products.TabIndex = 4;
            this.menu_products.Text = "Manage Products";
            this.menu_products.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.menu_products.UseVisualStyleBackColor = false;
            this.menu_products.Click += new System.EventHandler(this.menu_products_Click);
            // 
            // menu_employ
            // 
            this.menu_employ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.menu_employ.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu_employ.FlatAppearance.BorderSize = 0;
            this.menu_employ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menu_employ.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_employ.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            this.menu_employ.IconColor = System.Drawing.Color.Black;
            this.menu_employ.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menu_employ.IconSize = 32;
            this.menu_employ.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_employ.Location = new System.Drawing.Point(0, 334);
            this.menu_employ.Name = "menu_employ";
            this.menu_employ.Size = new System.Drawing.Size(291, 97);
            this.menu_employ.TabIndex = 3;
            this.menu_employ.Text = "Manage Employees";
            this.menu_employ.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.menu_employ.UseVisualStyleBackColor = false;
            this.menu_employ.Click += new System.EventHandler(this.menu_employ_Click);
            // 
            // menu_clients
            // 
            this.menu_clients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.menu_clients.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu_clients.FlatAppearance.BorderSize = 0;
            this.menu_clients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menu_clients.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_clients.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.menu_clients.IconColor = System.Drawing.Color.Black;
            this.menu_clients.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menu_clients.IconSize = 32;
            this.menu_clients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_clients.Location = new System.Drawing.Point(0, 237);
            this.menu_clients.Name = "menu_clients";
            this.menu_clients.Size = new System.Drawing.Size(291, 97);
            this.menu_clients.TabIndex = 2;
            this.menu_clients.Text = "Manage Clients";
            this.menu_clients.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.menu_clients.UseVisualStyleBackColor = false;
            this.menu_clients.Click += new System.EventHandler(this.menu_clients_Click);
            // 
            // menu_dashboard
            // 
            this.menu_dashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(178)))), ((int)(((byte)(242)))));
            this.menu_dashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu_dashboard.FlatAppearance.BorderSize = 0;
            this.menu_dashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menu_dashboard.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_dashboard.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.menu_dashboard.IconColor = System.Drawing.Color.Black;
            this.menu_dashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menu_dashboard.IconSize = 32;
            this.menu_dashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_dashboard.Location = new System.Drawing.Point(0, 140);
            this.menu_dashboard.Name = "menu_dashboard";
            this.menu_dashboard.Size = new System.Drawing.Size(291, 97);
            this.menu_dashboard.TabIndex = 1;
            this.menu_dashboard.Text = "Dashboard";
            this.menu_dashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.menu_dashboard.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 140);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(58, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 137);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // title_bar
            // 
            this.title_bar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.title_bar.Controls.Add(this.childFormTitle);
            this.title_bar.Controls.Add(this.iconChildform);
            this.title_bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.title_bar.Location = new System.Drawing.Point(291, 0);
            this.title_bar.Name = "title_bar";
            this.title_bar.Size = new System.Drawing.Size(1407, 70);
            this.title_bar.TabIndex = 1;
            // 
            // childFormTitle
            // 
            this.childFormTitle.AutoSize = true;
            this.childFormTitle.Font = new System.Drawing.Font("Britannic Bold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.childFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(24)))), ((int)(((byte)(96)))));
            this.childFormTitle.Location = new System.Drawing.Point(599, 13);
            this.childFormTitle.Name = "childFormTitle";
            this.childFormTitle.Size = new System.Drawing.Size(128, 44);
            this.childFormTitle.TabIndex = 1;
            this.childFormTitle.Text = "label1";
            this.childFormTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // iconChildform
            // 
            this.iconChildform.BackColor = System.Drawing.Color.CornflowerBlue;
            this.iconChildform.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(24)))), ((int)(((byte)(96)))));
            this.iconChildform.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconChildform.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(24)))), ((int)(((byte)(96)))));
            this.iconChildform.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconChildform.IconSize = 50;
            this.iconChildform.Location = new System.Drawing.Point(20, 12);
            this.iconChildform.Name = "iconChildform";
            this.iconChildform.Size = new System.Drawing.Size(50, 50);
            this.iconChildform.TabIndex = 0;
            this.iconChildform.TabStop = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(291, 70);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1407, 954);
            this.mainPanel.TabIndex = 2;
            // 
            // Management_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(201)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1698, 1024);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.title_bar);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Management_Form";
            this.Text = "PiStore Management";
            this.Load += new System.EventHandler(this.Management_Form_Load);
            this.menu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.title_bar.ResumeLayout(false);
            this.title_bar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconChildform)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton menu_dashboard;
        private FontAwesome.Sharp.IconButton menu_products;
        private FontAwesome.Sharp.IconButton menu_employ;
        private FontAwesome.Sharp.IconButton menu_clients;
        private FontAwesome.Sharp.IconButton menu_placeorder;
        private FontAwesome.Sharp.IconButton menu_orders;
        private System.Windows.Forms.Panel title_bar;
        private System.Windows.Forms.Label childFormTitle;
        private FontAwesome.Sharp.IconPictureBox iconChildform;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton bill_generate;
    }
}