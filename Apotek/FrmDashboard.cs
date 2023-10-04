using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Apotek
{
    public partial class FrmDashboard : Form
    {
        Button currentButton;
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;

        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            DatePicker.Value = DateTime.Now;
            this.ControlBox = false;
        }

        void TotalPenjualan()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            DateTime date = DatePicker.Value;

            string query = "SELECT total_harga FROM tb_barang_keluar WHERE tgl_peng = @date";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        cmd.Parameters.Add("@date", MySqlDbType.Date).Value = date;

                        decimal totalPenjualan = 0;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Loop melalui hasil pembacaan
                                while (reader.Read())
                                {
                                    decimal totalHarga = reader.GetDecimal(0);
                                    totalPenjualan += totalHarga;
                                }

                                lbl_PENJUALAN.Text = totalPenjualan.ToString("C", new CultureInfo("id-ID"));
                            }
                            else
                            {
                                lbl_PENJUALAN.Text = "-";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                    }
                }
            }
        }
        public void LoadForm(object Form)
        {
            Form previousForm = mainPanel.Controls.OfType<Form>().FirstOrDefault();
            if (previousForm != null)
            {
                previousForm.Dispose();
                mainPanel.Controls.Remove(previousForm);
            }

            mainPanel.Visible = true;
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(f);

            f.Show();
        }
        void activebutton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    resetbutton();
                    currentButton = (Button)btnSender;
                    activeSideBar.Height = currentButton.Height;
                    activeSideBar.Top = currentButton.Top;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        void resetbutton()
        {
            btnDashboard.BackColor = Color.FromArgb(31, 31, 56);
            btnDashboard.ForeColor = Color.Gainsboro;
            btnDashboard.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            btnMaster.BackColor = Color.FromArgb(31, 31, 56);
            btnMaster.ForeColor = Color.Gainsboro;
            btnMaster.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            btnFaktur.BackColor = Color.FromArgb(31, 31, 56);
            btnFaktur.ForeColor = Color.Gainsboro;
            btnFaktur.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            btnRetur.BackColor = Color.FromArgb(31, 31, 56);
            btnRetur.ForeColor = Color.Gainsboro;
            btnRetur.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            btnlaporan.BackColor = Color.FromArgb(31, 31, 56);
            btnlaporan.ForeColor = Color.Gainsboro;
            btnlaporan.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            btnLaporanStock.BackColor = Color.FromArgb(31, 31, 56);
            btnLaporanStock.ForeColor = Color.Gainsboro;
            btnLaporanStock.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            btnSupplier.BackColor = Color.FromArgb(31, 31, 56);
            btnSupplier.ForeColor = Color.Gainsboro;
            btnSupplier.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            activebutton(sender);
            LoadForm(new FrmMaster());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            activebutton(sender);
            mainPanel.Visible = false;
        }

        private void btnFaktur_Click(object sender, EventArgs e)
        {
            activebutton(sender);
            LoadForm(new FrmFaktur());
        }

        private void btnlaporan_Click(object sender, EventArgs e)
        {
            activebutton(sender);
            LoadForm(new FrmLaporan());
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnLaporanStock_Click(object sender, EventArgs e)
        {
            activebutton(sender);
            LoadForm(new FrmLaporanStock());
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Apakah kamu yakin ingin keluar dari app?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        void resetBtn()
        {
            btnHari.FillColor = System.Drawing.Color.White;
            btnHari.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));

            btnBulan.FillColor = System.Drawing.Color.White;
            btnBulan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));

            btnTahun.FillColor = System.Drawing.Color.White;
            btnTahun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        }

        private void btnHari_Click(object sender, EventArgs e)
        {
            resetBtn();
            btnHari.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            btnHari.ForeColor = System.Drawing.Color.White;
        }

        private void btnBulan_Click(object sender, EventArgs e)
        {
            resetBtn();
            btnBulan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            btnBulan.ForeColor = System.Drawing.Color.White;
        }

        private void btnTahun_Click(object sender, EventArgs e)
        {
            resetBtn();
            btnTahun.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            btnTahun.ForeColor = System.Drawing.Color.White;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            activebutton(sender);
            LoadForm(new FrmMasterPBF());
        }

        private void DatePicker_ValueChanged_1(object sender, EventArgs e)
        {
            TotalPenjualan();
        }
    }
}
