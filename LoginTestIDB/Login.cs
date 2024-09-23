using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D; // Needed for GraphicsPath

namespace LoginTestIDB
{
    public partial class Login : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;

        private bool ValidateLogin(string username, string password)
        {
            string connectionString = "server=localhost;database=logindb;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return false;
                }
            }
        }
        public Login()
        {
            InitializeComponent();
            alamat = "server=localhost; database=logindb; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Username and Password cannot be empty!");
                    return;
                }

                query = "SELECT * FROM tbl_pengguna WHERE username = @username";
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                perintah.Parameters.AddWithValue("@username", txtUsername.Text);
                adapter = new MySqlDataAdapter(perintah);
                adapter.Fill(ds);
                koneksi.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string password = ds.Tables[0].Rows[0]["password"].ToString();
                    if (password == txtPassword.Text)
                    {
                        FrmMain frmMain = new FrmMain();
                        frmMain.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Password Salah!");
                    }
                }
                else
                {
                    MessageBox.Show("Username Tidak Ditemukan!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        // Implement rounded corners for panel1
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            // Create a GraphicsPath for rounded rectangle
            GraphicsPath path = new GraphicsPath();
            int cornerRadius = 25; // Set the radius for rounded corners
            Rectangle bounds = new Rectangle(0, 0, panel.Width, panel.Height);

            // Add the rounded rectangle to the path
            path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseFigure();

            // Set the region of the panel to be the rounded path
            panel.Region = new Region(path);

            // Optional: Draw a border around the panel
            Pen pen = new Pen(Color.FromArgb(161, 196, 253), 2);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(pen, path);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Subscribe the Paint event to the panel1
            panel1.Paint += new PaintEventHandler(panel1_Paint);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
