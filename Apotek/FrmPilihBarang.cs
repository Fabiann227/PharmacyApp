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
    public partial class FrmPilihBarang : Form
    {
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;
        public string SelectedKodeBarang { get; private set; }
        public event Action<BarangInfo> OnBarangInfoSelected;


        public FrmPilihBarang()
        {
            InitializeComponent();
        }

        private void FrmPilihBarang_Load(object sender, EventArgs e)
        {
            LoadData();
            this.BeginInvoke(new Action(() =>
            {
                SEARCH.Focus();
            }));
        }
        private void SearchData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT kode_barang, nama_barang, jenis_barang, satuan_barang, stok_lempeng, stok_butir, stok_satuan, modal, harga_jual1, laba1, harga_jual2, laba2, harga_jual3, laba3, harga_lempeng, laba_lempeng  FROM tb_barang_masuk WHERE kode_barang LIKE '%" + SEARCH.Text + "%' OR nama_barang LIKE '%" + SEARCH.Text + "%'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        // Bersihkan DataGridView jika sudah ada data sebelumnya
                        dgv.Rows.Clear();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Mengecek apakah ada data yang bisa dibaca
                            if (reader.HasRows)
                            {
                                // Loop melalui hasil pembacaan
                                while (reader.Read())
                                {
                                    // Membaca nilai dari kolom-kolom yang sesuai
                                    int kodeBarang = reader.GetInt32(0);
                                    string namaBarang = reader.GetString(1);
                                    string jenisBarang = reader.GetString(2);
                                    string satuanBarang = reader.GetString(3);
                                    int lempengbox = reader.GetInt32(4);
                                    int butirLempeng = reader.GetInt32(5);
                                    int jumlahBarang = reader.GetInt32(6);

                                    decimal modal = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);
                                    decimal hargaJual1 = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8);
                                    decimal hargaJual2 = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                                    decimal hargaJual3 = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);
                                    decimal hargaJual4 = reader.GetDecimal(14);

                                    string Modal = modal.ToString("N0", new CultureInfo("id-ID"));

                                    string hargaRupiah1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                                    string hargaRupiah2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                                    string hargaRupiah3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));
                                    string hargaRupiah4 = hargaJual4.ToString("N0", new CultureInfo("id-ID"));

                                    namaBarang = namaBarang.ToUpper();
                                    // Menambahkan data ke DataGridView
                                    dgv.Rows.Add(kodeBarang, namaBarang, jumlahBarang, lempengbox, butirLempeng, Modal, hargaRupiah1, hargaRupiah2, hargaRupiah3, hargaRupiah4);
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
                        MessageBox.Show("Terjadi kesalahan (332): " + ex.Message);
                    }
                }
            }
        }

        private void LoadData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT kode_barang, nama_barang, jenis_barang, satuan_barang, stok_lempeng, stok_butir, stok_satuan, modal, harga_jual1, laba1, harga_jual2, laba2, harga_jual3, laba3, harga_lempeng, laba_lempeng FROM tb_barang_masuk"; // Ganti dengan nama tabel dan query Anda

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
                                    // Membaca nilai dari kolom-kolom yang sesuai
                                    int kodeBarang = reader.GetInt32(0);
                                    string namaBarang = reader.GetString(1);
                                    string jenisBarang = reader.GetString(2);
                                    string satuanBarang = reader.GetString(3);
                                    int lempengbox = reader.GetInt32(4);
                                    int butirLempeng = reader.GetInt32(5);
                                    int jumlahBarang = reader.GetInt32(6);
                                    decimal modal = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);
                                    decimal hargaJual1 = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8);
                                    decimal hargaJual2 = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                                    decimal hargaJual3 = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);
                                    decimal hargaJual4 = reader.GetDecimal(14);

                                    string Modal = modal.ToString("N0", new CultureInfo("id-ID"));

                                    string hargaRupiah1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                                    string hargaRupiah2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                                    string hargaRupiah3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));
                                    string hargaRupiah4 = hargaJual4.ToString("N0", new CultureInfo("id-ID"));

                                    namaBarang = namaBarang.ToUpper();

                                    // Menambahkan data ke DataGridView
                                    dgv.Rows.Add(kodeBarang, namaBarang, jumlahBarang, lempengbox, butirLempeng, Modal, hargaRupiah1, hargaRupiah2, hargaRupiah3, hargaRupiah4);

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
                        MessageBox.Show("Terjadi kesalahan (qtbk): " + ex.Message);
                    }
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                KODEBARANG.Text = row.Cells["column2"].Value.ToString();
                KODEBRG.Text = row.Cells["column1"].Value.ToString();
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BarangInfo barangInfo = new BarangInfo
            {
                KodeBarang = KODEBRG.Text
            };

            OnBarangInfoSelected?.Invoke(barangInfo);

            this.Close();
        }

        private void SEARCH_TextChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnSave.PerformClick();
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null)
            {
                DataGridViewRow row = this.dgv.CurrentRow;
                KODEBARANG.Text = row.Cells["column2"].Value.ToString();
                KODEBRG.Text = row.Cells["column1"].Value.ToString();
                btnSave.Enabled = true;
            }
        }
    }
}
