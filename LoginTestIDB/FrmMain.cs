using LoginTestIDB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LoginTestIDB
{
    public partial class FrmMain : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;

        public FrmMain()
        {
            alamat = "server=localhost; database=logindb; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            query = "SELECT * FROM tbl_pengguna";
            ds.Clear();
            koneksi.Open();
            perintah = new MySqlCommand(query, koneksi);
            adapter = new MySqlDataAdapter(perintah);
            adapter.Fill(ds);
            koneksi.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "" && txtNama.Text != "")
                {
                    query = string.Format("INSERT INTO tbl_pengguna (username, password, nama_pengguna, level) VALUES ('{0}', '{1}', '{2}', {3});",
                        txtUsername.Text, txtPassword.Text, txtNama.Text, CBLevel.Text);
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.ExecuteNonQuery();
                    koneksi.Close();
                    MessageBox.Show("Insert Data Success ...");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Data Tidak Lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "")
                {
                    query = string.Format("SELECT * FROM tbl_pengguna WHERE username = '{0}'", txtUsername.Text);
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow kolom = ds.Tables[0].Rows[0];
                        txtID.Text = kolom["id_pengguna"].ToString();
                        txtPassword.Text = kolom["password"].ToString();
                        txtNama.Text = kolom["nama_pengguna"].ToString();
                        CBLevel.Text = kolom["level"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ada !!");
                    }
                }
                else
                {
                    MessageBox.Show("Masukkan Username !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text != "" && txtPassword.Text != "" && txtNama.Text != "" && CBLevel.Text != "")
                {
                    query = string.Format("UPDATE tbl_pengguna SET password = '{0}', nama_pengguna = '{1}', level = {2} WHERE id_pengguna = {3}",
                        txtPassword.Text, txtNama.Text, CBLevel.Text, txtID.Text);
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.ExecuteNonQuery();
                    koneksi.Close();
                    MessageBox.Show("Update Data Success ...");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Data Tidak Lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text != "")
                {
                    query = string.Format("DELETE FROM tbl_pengguna WHERE id_pengguna = {0}", txtID.Text);
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.ExecuteNonQuery();
                    koneksi.Close();
                    MessageBox.Show("Delete Data Success ...");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Pilih Data yang Akan Dihapus !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtNama.Clear();
            CBLevel.SelectedIndex = -1;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
