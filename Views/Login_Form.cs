using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pi_store.Services;
using pi_store.Views;


namespace pi_store
{
    public partial class Login_Form : Form
    {
        private DBConnection conn;

        public Login_Form()
        {
            InitializeComponent();
            conn = new DBConnection();
            StartPosition = FormStartPosition.CenterScreen;
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {

           
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query = "SELECT COUNT(*) FROM UserInfo WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {

                        Management_Form managementForm = new Management_Form();
                        managementForm.Show();
                        this.Hide();

                        managementForm.FormClosed += (s, args) => Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                e.SuppressKeyPress = true;

                
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                e.SuppressKeyPress = true;

                
                btnLogin.PerformClick();
            }
        }
    }
}
