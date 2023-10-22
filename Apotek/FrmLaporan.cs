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
    public partial class FrmLaporan : Form
    {
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;

        public FrmLaporan()
        {
            InitializeComponent();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmLaporan_Load(object sender, EventArgs e)
        {
            BarangKeluar();
        }

        private void GetDataByDateRange()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            DateTime startDate = STARTDATE.Value;
            DateTime endDate = ENDDATE.Value;

            string formattedsDate = startDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
            string formattedeDate = endDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            Console.WriteLine(startDate);

            string query = "SELECT DISTINCT no_faktur, tgl, nama_pelanggan, alamat, total_barang, total_harga, tgl_peng, total_keuntungan FROM tb_barang_keluar WHERE tgl_peng BETWEEN @startDate AND @endDate";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        cmd.Parameters.Add("@startDate", MySqlDbType.Date).Value = startDate;
                        cmd.Parameters.Add("@endDate", MySqlDbType.Date).Value = endDate;

                        lbl_TGL.Text = "Penjualan dari "+ formattedsDate + " - " + formattedeDate;

                        decimal totalPenjualan = 0;
                        decimal totalkeuntungan = 0;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dgv.Rows.Clear();
                            // Mengecek apakah ada data yang bisa dibaca
                            if (reader.HasRows)
                            {
                                // Loop melalui hasil pembacaan
                                while (reader.Read())
                                {
                                    int no_faktur = reader.GetInt32(0);
                                    string tgl = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                                    string namaPelanggan = reader.GetString(2);
                                    string alamat = reader.GetString(3);
                                    int totalBarang = reader.GetInt32(4);
                                    decimal totalHarga = reader.GetDecimal(5);
                                    string tgl_peng = reader.GetDateTime(6).ToString("yyyy-MM-dd");
                                    decimal totalKeuntungan = reader.GetDecimal(7);

                                    string total_harga = totalHarga.ToString("C", new CultureInfo("id-ID"));
                                    total_harga = total_harga.Replace("Rp", "");

                                    string total_keuntungan = totalKeuntungan.ToString("C", new CultureInfo("id-ID"));
                                    total_keuntungan = total_keuntungan.Replace("Rp", "");

                                    Image editIcon = Properties.Resources.icons8_info_24px_1;
                                    Image returIcon = Properties.Resources.icons8_refund_2_20px;
                                    Image deleteIcon = Properties.Resources.icons8_delete_24px_1;

                                    dgv.Rows.Add(no_faktur, tgl, namaPelanggan, alamat, totalBarang, total_harga, total_keuntungan, tgl_peng, editIcon, returIcon, deleteIcon);

                                    totalPenjualan += totalHarga;
                                    totalkeuntungan += totalKeuntungan;
                                }

                                lbl_TOTALPENJUALAN.Text = totalPenjualan.ToString("C", new CultureInfo("id-ID"));
                                lbl_TOTALKEUNTUNGAN.Text = totalkeuntungan.ToString("C", new CultureInfo("id-ID"));
                            }
                            else
                            {
                                lbl_TOTALPENJUALAN.Text = "-";
                                lbl_TOTALKEUNTUNGAN.Text = "-";
                                //MessageBox.Show("Tidak ada data yang ditemukan.");
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

        private void SearchData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT DISTINCT no_faktur, tgl, nama_pelanggan, alamat, total_barang, total_harga, tgl_peng, total_keuntungan FROM tb_barang_keluar WHERE no_faktur LIKE '%" + SEARCH.Text + "%' OR nama_pelanggan LIKE '%" + SEARCH.Text + "%'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        decimal totalPenjualan = 0;
                        decimal totalkeuntungan = 0;

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
                                    int no_faktur = reader.GetInt32(0);
                                    string tgl = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                                    string namaPelanggan = reader.GetString(2);
                                    string alamat = reader.GetString(3);
                                    int totalBarang = reader.GetInt32(4);
                                    decimal totalHarga = reader.GetDecimal(5);
                                    string tgl_peng = reader.GetDateTime(6).ToString("yyyy-MM-dd");
                                    decimal totalKeuntungan = reader.GetDecimal(7);

                                    string total_harga = totalHarga.ToString("C", new CultureInfo("id-ID"));
                                    total_harga = total_harga.Replace("Rp", "");

                                    string total_keuntungan = totalKeuntungan.ToString("C", new CultureInfo("id-ID"));
                                    total_keuntungan = total_keuntungan.Replace("Rp", "");

                                    Image editIcon = Properties.Resources.icons8_info_24px_1;
                                    Image returIcon = Properties.Resources.icons8_refund_2_20px;
                                    Image deleteIcon = Properties.Resources.icons8_delete_24px_1;

                                    dgv.Rows.Add(no_faktur, tgl, namaPelanggan, alamat, totalBarang, total_harga, total_keuntungan, tgl_peng, editIcon, returIcon, deleteIcon);

                                    totalPenjualan += totalHarga;
                                    totalkeuntungan += totalKeuntungan;
                                }

                                lbl_TOTALPENJUALAN.Text = totalPenjualan.ToString("C", new CultureInfo("id-ID"));
                                lbl_TOTALKEUNTUNGAN.Text = totalkeuntungan.ToString("C", new CultureInfo("id-ID"));
                            }
                            else
                            {
                                lbl_TOTALPENJUALAN.Text = "-";
                                lbl_TOTALKEUNTUNGAN.Text = "-";
                                //MessageBox.Show("Tidak ada data yang ditemukan.");
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
        private void BarangKeluar()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT DISTINCT no_faktur, DATE(tgl) AS tgl, nama_pelanggan, alamat, total_barang, total_harga, tgl_peng, total_keuntungan FROM tb_barang_keluar";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        decimal totalPenjualan = 0;
                        decimal totalkeuntungan = 0;

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
                                    int no_faktur = reader.GetInt32(0);
                                    string tgl = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                                    string namaPelanggan = reader.GetString(2);
                                    string alamat = reader.GetString(3);
                                    int totalBarang = reader.GetInt32(4);
                                    decimal totalHarga = reader.GetDecimal(5);
                                    string tgl_peng = reader.GetDateTime(6).ToString("yyyy-MM-dd");
                                    decimal totalKeuntungan = reader.GetDecimal(7);

                                    string total_harga = totalHarga.ToString("C", new CultureInfo("id-ID"));
                                    total_harga = total_harga.Replace("Rp", "");

                                    string total_keuntungan = totalKeuntungan.ToString("C", new CultureInfo("id-ID"));
                                    total_keuntungan = total_keuntungan.Replace("Rp", "");

                                    Image editIcon = Properties.Resources.icons8_info_24px_1;
                                    Image returIcon = Properties.Resources.icons8_refund_2_20px;
                                    Image deleteIcon = Properties.Resources.icons8_delete_24px_1;

                                    dgv.Rows.Add(no_faktur, tgl, namaPelanggan, alamat, totalBarang, total_harga, total_keuntungan, tgl_peng, editIcon, returIcon, deleteIcon);

                                    totalPenjualan += totalHarga;
                                    totalkeuntungan += totalKeuntungan;
                                }

                                lbl_TOTALPENJUALAN.Text = totalPenjualan.ToString("C", new CultureInfo("id-ID"));
                                lbl_TOTALKEUNTUNGAN.Text = totalkeuntungan.ToString("C", new CultureInfo("id-ID"));
                            }
                            else
                            {
                                lbl_TOTALPENJUALAN.Text = "-";
                                lbl_TOTALKEUNTUNGAN.Text = "-";
                                //MessageBox.Show("Tidak ada data yang ditemukan.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1: " + ex.Message);
                    }
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["column7"].Index && e.RowIndex >= 0)
            {
                int nofaktur = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["column1"].Value);
                FrmCetakFaktur frmCetak = new FrmCetakFaktur();
                frmCetak.NoFaktur = nofaktur.ToString();
                frmCetak.ShowDialog();
            }
            else if (e.ColumnIndex == dgv.Columns["column10"].Index && e.RowIndex >= 0)
            {
                int nofaktur = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["column1"].Value);
                FrmRetur frmRetur = new FrmRetur();
                frmRetur.NoFaktur = nofaktur.ToString();
                frmRetur.ShowDialog();
            }
            else if (e.ColumnIndex == dgv.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Column1"].Value);
                if (MessageBox.Show("Apakah kamu yakin ingin menghapus data ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                        MySqlConnection connection = new MySqlConnection(connectionString);
                        connection.Open();

                        string deleteQuery = "DELETE FROM tb_barang_keluar WHERE no_faktur = @id";
                        MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
                        deleteCmd.Parameters.AddWithValue("@id", id);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                            MessageBox.Show("Data berhasil dihapus");
                        }
                        else
                        {
                            MessageBox.Show("Data tidak ditemukan atau gagal dihapus");
                        }

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 5: " + ex.Message);
                    }
                }
            }
        }

        private void ENDDATE_ValueChanged(object sender, EventArgs e)
        {
            GetDataByDateRange();
        }

        private void SEARCH_TextChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void STARTDATE_ValueChanged(object sender, EventArgs e)
        {
            GetDataByDateRange();
        }

        private void PRINTALL_Click(object sender, EventArgs e)
        {
            LapAkhirPenjualan lapAkhirPenjualan = new LapAkhirPenjualan();
            lapAkhirPenjualan.ShowDialog();
        }
    }
}
