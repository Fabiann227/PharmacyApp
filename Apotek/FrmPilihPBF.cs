using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Apotek
{
    public partial class FrmPilihPBF : Form
    {
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;
        public event Action<BarangInfo> OnBarangInfoSelected;
        public FrmPilihPBF()
        {
            InitializeComponent();
        }

        private void FrmPilihPBF_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void SearchData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT id, nama, alamat FROM tb_supplier WHERE id LIKE '%" + SEARCH.Text + "%' OR nama LIKE '%" + SEARCH.Text + "%'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Bersihkan DataGridView jika sudah ada data sebelumnya
                            dgv.Rows.Clear();

                            // Mengecek apakah ada data yang bisa dibaca
                            if (reader.HasRows)
                            {
                                // Loop melalui hasil pembacaan
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string nama = reader.GetString(1);
                                    string alamat = reader.GetString(2);

                                    dgv.Rows.Add(id, nama, alamat);
                                }
                            }
                            else
                            {
                                //MessageBox.Show("Tidak ada data yang ditemukan.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan (Cari obat - 1): " + ex.Message);
                    }
                }
            }
        }

        private void LoadData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT id, nama, alamat FROM tb_supplier";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Mengecek apakah ada data yang bisa dibaca
                            if (reader.HasRows)
                            {
                                // Bersihkan DataGridView jika sudah ada data sebelumnya
                                dgv.Rows.Clear();

                                // Loop melalui hasil pembacaan
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string nama = reader.GetString(1);
                                    string alamat = reader.GetString(2);

                                    dgv.Rows.Add(id, nama, alamat);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tidak ada data yang ditemukan.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan (Pilih obat - 1): " + ex.Message);
                    }
                }
            }
        }

        private void SEARCH_TextChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BarangInfo barangInfo = new BarangInfo
            {
                IDBarang = IDBARANG.Text
            };

            OnBarangInfoSelected?.Invoke(barangInfo);

            this.Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                NAMABARANG.Text = row.Cells["Column2"].Value.ToString();
                IDBARANG.Text = row.Cells["Column1"].Value.ToString();
                btnSave.Enabled = true;
            }
        }
    }
}
