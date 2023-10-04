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
    public partial class FrmRetur : Form
    {
        public string NoFaktur { get; set; }
        private string SelectedID;
        private string SelectedKode;

        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;
        public FrmRetur()
        {
            InitializeComponent();
        }
        private void RefreshCart()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT id, nama_barang, level_harga, satuan, subtotal, qty, qty_lempeng, qty_butir, harga, harga_lempeng, harga_butir, CASE WHEN qty != 0 THEN harga WHEN qty_butir != 0 THEN harga_butir ELSE harga_lempeng END AS harga_total, CASE WHEN qty != 0 THEN qty WHEN qty_butir != 0 THEN qty_butir ELSE qty_lempeng END AS qty_total, CASE WHEN qty != 0 THEN 'Box' WHEN qty_lempeng != 0 THEN 'Strip' WHEN qty_butir != 0 THEN 'Tablet' END AS satuan_akhir, nama_pelanggan, alamat, retur, subretur, kode_barang FROM tb_barang_keluar WHERE no_faktur = @noFaktur";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        cmd.Parameters.AddWithValue("@noFaktur", NoFaktur);

                        decimal totalHarga = 0;
                        int totalBarang = 0;
                        int no = 0;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dgv.Rows.Clear();

                                while (reader.Read())
                                {
                                    no++;
                                    int id = reader.GetInt32(0);
                                    string namaBarang = reader.GetString(1);
                                    string levelHarga = reader.GetString(2);
                                    decimal subtotal = reader.GetDecimal(4);

                                    decimal harga = reader.GetDecimal(11);
                                    int qty = reader.GetInt32(12);
                                    string satuan = reader.GetString(13);
                                    string nama = reader.GetString(14);
                                    string alamat = reader.GetString(15);
                                    int retur = reader.GetInt32(16);
                                    int kode_barang = reader.GetInt32(18);

                                    lbl_NAMA.Text = nama;
                                    lbl_NOFAKTUR.Text = ": " + NoFaktur;
                                    lbl_ALAMAT.Text = alamat;

                                    string hargaBrg = harga.ToString("N0", new CultureInfo("id-ID"));
                                    string subTotal = subtotal.ToString("N0", new CultureInfo("id-ID"));

                                    Image editIcon = Properties.Resources.icons8_delete_24px_1;

                                    dgv.Rows.Add(kode_barang, id, no, namaBarang, levelHarga, hargaBrg, qty, retur, satuan, subTotal, editIcon);

                                    totalHarga += subtotal;
                                    totalBarang += qty;
                                }

                                lbl_TOTAL.Text = totalHarga.ToString("N0", new CultureInfo("id-ID"));
                                lbl_TOTALBARANG.Text = totalBarang.ToString();
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

        private void FrmRetur_Load(object sender, EventArgs e)
        {
            RefreshCart();
        }

        public int GetStokSatuan(string kodeBarang)
        {
            int jumlahBarangAwal = 0;

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT stok_satuan FROM tb_barang_masuk WHERE kode_barang = @kodeBarang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    try
                    {
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            jumlahBarangAwal = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("Stok Habis: " + kodeBarang);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1.1: " + ex.Message);
                    }
                }
            }

            return jumlahBarangAwal;
        }

        // Fungsi untuk mengambil nilai lempeng_box dari database
        public int GetStokStrip(string kodeBarang)
        {
            int lempengBox = 0;

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT stok_lempeng FROM tb_barang_masuk WHERE kode_barang = @kodeBarang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    try
                    {
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            lempengBox = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1.2: " + ex.Message);
                    }
                }
            }
            return lempengBox;
        }

        public int GetStokTablet(string kodeBarang)
        {
            int ButirLempeng = 0;

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT stok_butir FROM tb_barang_masuk WHERE kode_barang = @kodeBarang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    try
                    {
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            ButirLempeng = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1.2: " + ex.Message);
                    }
                }
            }

            return ButirLempeng;
        }

        public int GetLempengPerBox(string kodeBarang)
        {
            int lempengBox = 0;

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT lempeng_box FROM tb_barang_masuk WHERE kode_barang = @kodeBarang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    try
                    {
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            lempengBox = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1.2: " + ex.Message);
                    }
                }
            }

            return lempengBox;
        }

        public int GetButirPerLempeng(string kodeBarang)
        {
            int ButirLempeng = 0;

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT butir_lempeng FROM tb_barang_masuk WHERE kode_barang = @kodeBarang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    try
                    {
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            ButirLempeng = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1.2: " + ex.Message);
                    }
                }
            }

            return ButirLempeng;
        }

        public void TambahStok(string kodeBarang, int jumlahDikurangkan)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "UPDATE tb_barang_masuk SET stok_satuan = stok_satuan + @jumlahDikurangkan WHERE kode_barang = @kodeBarang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);
                    cmd.Parameters.AddWithValue("@jumlahDikurangkan", jumlahDikurangkan);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1.3: " + ex.Message);
                    }
                }
            }
        }
        public void TambahStokStrip(string kodeBarang, int sisaStok)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "UPDATE tb_barang_masuk SET stok_lempeng = @sisaStok WHERE kode_barang = @kodeBarang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);
                    cmd.Parameters.AddWithValue("@sisaStok", sisaStok);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 1.4: " + ex.Message);
                    }
                }
            }
        }
        private void TambahStokTablet(string kodeBarang, int jumlah)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            Console.WriteLine("Butir2: " + jumlah);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE tb_barang_masuk SET stok_butir = @jumlah WHERE kode_barang = @kodeBarang";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@jumlah", jumlah);
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                SelectedID = row.Cells["ID"].Value.ToString();
                SelectedKode = row.Cells["KODE"].Value.ToString();
                NAMABARANG.Text = row.Cells["column1"].Value.ToString();
                HARGA.Text = row.Cells["column3"].Value.ToString();
                DIBELI.Text = row.Cells["column4"].Value.ToString();
                SUBTOTAL.Text = row.Cells["column6"].Value.ToString();
                SATUAN.Text = row.Cells["column5"].Value.ToString();
                decimal diRetur = Convert.ToDecimal(row.Cells["column8"].Value);

                DIRETUR.Maximum = (DIBELI.Value - diRetur);
                DIRETUR.Enabled = true;
                SUBRETUR.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
        private void HapusDataBarangKeluar(int kodeBarangToDelete)
        {
            try
            {
                string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string deleteQuery = "DELETE FROM tb_barang_keluar WHERE id = @kodeBarangToDelete";

                MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
                deleteCmd.Parameters.AddWithValue("@kodeBarangToDelete", kodeBarangToDelete);

                int rowsAffected = deleteCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                    MessageBox.Show("Data berhasil dihapus dan stok sudah di Pulihkan, Tidak Perlu tekan tombol Save! Langsung Close saja", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshCart();
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
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["column11"].Index && e.RowIndex >= 0)
            {
                int kodeBarangToDelete = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ID"].Value);
                int retur = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Column4"].Value);
                string kodeBarang = dgv.Rows[e.RowIndex].Cells["KODE"].Value.ToString();
                HapusDataBarangKeluar(kodeBarangToDelete);
                UpdateStokDeleted(kodeBarang, retur);
            }
        }

        private void updateData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            string harga = this.HARGA.Text;
            harga = harga.Replace(".", "");
            
            string subTotal = this.SUBTOTAL.Text;
            subTotal = subTotal.Replace(".", "");

            string subRetur = this.SUBRETUR.Text;
            subRetur = subRetur.Replace(".", "");

            string query = "UPDATE `tb_barang_keluar` SET " +
                    "`harga` = '" + harga + "', " +
                    "`subtotal` = '" + subTotal + "', " +
                    "`retur` = `retur` + '" + this.DIRETUR.Text + "', " +
                    "`subretur` = `subretur` + '" + subRetur + "' " +
                    "WHERE `id` = '" + SelectedID + "'";

            if (DIRETUR.Value != DIRETUR.Maximum)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Berhasil Retur barang dengan Kode : " + SelectedID);
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                HapusDataBarangKeluar(Convert.ToInt32(SelectedID));
            }         
        }

        private void UpdateStokDeleted(string kodeBarang, int retur)
        {
            int lempengPerBox = GetLempengPerBox(kodeBarang);
            int butirPerLempeng = GetButirPerLempeng(kodeBarang);
            int sisaBox = GetStokSatuan(kodeBarang);
            int sisaLempeng = GetStokStrip(kodeBarang);
            int sisaButir = GetStokTablet(kodeBarang);

            Console.WriteLine("Var: " + kodeBarang + " | " + retur + " | " + lempengPerBox);

            if (SATUAN.Text == "Box")
            {
                TambahStok(kodeBarang, retur);
            }

            if (SATUAN.Text == "Strip")
            {
                //per strip
                int sisaSetelahBeli = sisaLempeng + retur;

                Console.WriteLine("Z: " + sisaLempeng + " - " + retur + " = " + sisaSetelahBeli);
                if (sisaSetelahBeli > lempengPerBox)
                {
                    TambahStok(kodeBarang, 1);
                    sisaLempeng = sisaSetelahBeli - lempengPerBox;
                    Console.WriteLine("B: 10 + " + sisaSetelahBeli + " = " + sisaLempeng);
                    TambahStokStrip(kodeBarang, sisaLempeng);
                }
                else
                {
                    sisaLempeng = sisaSetelahBeli;
                    TambahStokStrip(kodeBarang, sisaLempeng);
                }
            }

            if (SATUAN.Text == "Tablet")
            {
                int sisaSetelahBeliButir = sisaButir + retur;

                Console.WriteLine("A: " + sisaButir + " - " + retur + " = " + sisaSetelahBeliButir);
                if (sisaSetelahBeliButir > butirPerLempeng)
                {
                    // Pengurangan stok butir
                    sisaButir = sisaSetelahBeliButir - butirPerLempeng;
                    Console.WriteLine("B: " + sisaSetelahBeliButir + " - " + butirPerLempeng + " = " + sisaButir);
                    TambahStokTablet(kodeBarang, Math.Abs(sisaButir));

                    int sisaSetelahBeli = sisaLempeng + 1;

                    if (sisaSetelahBeli > lempengPerBox)
                    {
                        TambahStok(kodeBarang, 1);
                        sisaLempeng = sisaSetelahBeli - lempengPerBox;
                        TambahStokStrip(kodeBarang, sisaLempeng);
                        Console.WriteLine("C: " + sisaLempeng);
                    }
                    else
                    {
                        sisaLempeng = sisaSetelahBeli;
                        TambahStokStrip(kodeBarang, sisaLempeng);
                        Console.WriteLine("D: " + sisaLempeng);
                    }
                }
                else
                {
                    // Pengurangan stok butir
                    sisaButir = sisaSetelahBeliButir;
                    TambahStokTablet(kodeBarang, sisaButir);
                    Console.WriteLine("E: " + sisaButir);
                }
            }
        }

        private void UpdateStok(string kodeBarang)
        {
            int lempengPerBox = GetLempengPerBox(kodeBarang);
            int butirPerLempeng = GetButirPerLempeng(kodeBarang);
            int sisaBox = GetStokSatuan(kodeBarang);
            int sisaLempeng = GetStokStrip(kodeBarang);
            int sisaButir = GetStokTablet(kodeBarang);

            int retur = int.Parse(DIRETUR.Text);

            Console.WriteLine("Var: " + kodeBarang + " | " + retur + " | " + lempengPerBox);

            if (SATUAN.Text == "Box")
            {
                TambahStok(kodeBarang, retur);
            }

            if (SATUAN.Text == "Strip")
            {
                //per strip
                int sisaSetelahBeli = sisaLempeng + retur;

                Console.WriteLine("Z: " + sisaLempeng + " - " + retur + " = " + sisaSetelahBeli);
                if (sisaSetelahBeli > lempengPerBox)
                {
                    TambahStok(kodeBarang, 1);
                    sisaLempeng = sisaSetelahBeli - lempengPerBox;
                    Console.WriteLine("B: 10 + " + sisaSetelahBeli + " = " + sisaLempeng);
                    TambahStokStrip(kodeBarang, sisaLempeng);
                }
                else
                {
                    sisaLempeng = sisaSetelahBeli;
                    TambahStokStrip(kodeBarang, sisaLempeng);
                }
            }

            if (SATUAN.Text == "Tablet")
            {
                int sisaSetelahBeliButir = sisaButir + retur;

                Console.WriteLine("A: " + sisaButir + " - " + retur + " = " + sisaSetelahBeliButir);
                if (sisaSetelahBeliButir > butirPerLempeng)
                {
                    // Pengurangan stok butir
                    sisaButir = sisaSetelahBeliButir - butirPerLempeng;
                    Console.WriteLine("B: " + sisaSetelahBeliButir + " - " + butirPerLempeng + " = " + sisaButir);
                    TambahStokTablet(kodeBarang, Math.Abs(sisaButir));

                    int sisaSetelahBeli = sisaLempeng + 1;

                    if (sisaSetelahBeli > lempengPerBox)
                    {
                        TambahStok(kodeBarang, 1);
                        sisaLempeng = sisaSetelahBeli - lempengPerBox;
                        TambahStokStrip(kodeBarang, sisaLempeng);
                        Console.WriteLine("C: " + sisaLempeng);
                    }
                    else
                    {
                        sisaLempeng = sisaSetelahBeli;
                        TambahStokStrip(kodeBarang, sisaLempeng);
                        Console.WriteLine("D: " + sisaLempeng);
                    }
                }
                else
                {
                    // Pengurangan stok butir
                    sisaButir = sisaSetelahBeliButir;
                    TambahStokTablet(kodeBarang, sisaButir);
                    Console.WriteLine("E: " + sisaButir);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateStok(SelectedKode);
            updateData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void DIRETUR_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = HARGA.Text;

            int jumlahBarang = Convert.ToInt32(DIRETUR.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBRETUR.Text = totalHarga.ToString();
        }

        private void HARGA_TextChanged(object sender, EventArgs e)
        {
            string hargaText = HARGA.Text;

            int jumlahBarang = Convert.ToInt32(DIBELI.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString();
        }
    }
}
