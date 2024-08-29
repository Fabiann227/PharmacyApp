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

        //tambah
        string harga_jual1;
        string harga_jual2;
        string harga_jual3;
        string harga_jual4;
        string kode_barang;
        string stokLempengBox;
        string stokBarang;

        //edit
        string e_harga_jual1;
        string e_harga_jual2;
        string e_harga_jual3;
        string e_harga_jual4;
        int currentStok = 0;
        private bool isFirstCall = true;

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
            string query = "SELECT id, nama_barang, level_harga, satuan, subtotal, qty, qty_lempeng, qty_butir, harga, harga_lempeng, harga_butir, CASE WHEN qty != 0 THEN qty WHEN qty_butir != 0 THEN qty_butir ELSE qty_lempeng END AS qty_total, CASE WHEN qty != 0 THEN 'Box' WHEN qty_lempeng != 0 THEN 'Strip' WHEN qty_butir != 0 THEN 'Tablet' END AS satuan_akhir, nama_pelanggan, alamat, retur, subretur, kode_barang FROM tb_barang_keluar WHERE no_faktur = @noFaktur";

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

                                    decimal harga = reader.GetDecimal(8);
                                    int qty = reader.GetInt32(11);
                                    string satuan = reader.GetString(12);
                                    string nama = reader.GetString(13);
                                    string alamat = reader.GetString(14);
                                    int retur = reader.GetInt32(15);
                                    int kode_barang = reader.GetInt32(17);

                                    lbl_NAMA.Text = nama;
                                    lbl_NOFAKTUR.Text = NoFaktur;
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
                        MessageBox.Show("Terjadi kesalahan 1 | Retur: " + ex.Message);
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
                EDIT_LEVEL.Text = row.Cells["column2"].Value.ToString();
                NAMABARANG.Text = row.Cells["column1"].Value.ToString();
                EDIT_HARGA.Text = row.Cells["column3"].Value.ToString();
                
                SUBTOTAL.Text = row.Cells["column6"].Value.ToString();
                SATUAN.Text = row.Cells["column5"].Value.ToString();
                decimal diRetur = Convert.ToDecimal(row.Cells["column8"].Value);
                decimal diBeli = Convert.ToDecimal(row.Cells["column4"].Value);

                DIRETUR.Maximum = (diBeli - diRetur);
                DIBELI.Minimum = diBeli;
                currentStok = Convert.ToInt32(row.Cells["column4"].Value.ToString());

                DIBELI.Value = currentStok;
                DITAMBAH.Value = 0;
                DIRETUR.Enabled = true;
                SUBRETUR.Enabled = true;
                btnSave.Enabled = true;
                lbl_NAMA.Enabled = true;
                lbl_ALAMAT.Enabled = true;

                //edit
                string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    string query = "SELECT `harga_jual1`, `harga_jual2`, `harga_jual3`, `harga_lempeng` FROM `tb_barang_masuk` WHERE `kode_barang` = @kode_barang";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@kode_barang", SelectedKode);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal hargaJual1 = reader.GetDecimal(0);
                        decimal hargaJual2 = reader.GetDecimal(1);
                        decimal hargaJual3 = reader.GetDecimal(2);
                        decimal hargaJual4 = reader.GetDecimal(3);
                        
                        e_harga_jual1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                        e_harga_jual2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                        e_harga_jual3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));
                        e_harga_jual4 = hargaJual4.ToString("N0", new CultureInfo("id-ID"));
                    }
                    else
                    {
                        MessageBox.Show("Kode Barang tidak ditemukan");
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan (FrmFaktur): " + ex.Message);
                }
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

            string harga = this.EDIT_HARGA.Text;
            harga = harga.Replace(".", "");
            
            string subTotal = this.SUBTOTAL.Text;
            subTotal = subTotal.Replace(".", "");

            string subRetur = this.SUBRETUR.Text;
            subRetur = subRetur.Replace(".", "");

            string query = "UPDATE `tb_barang_keluar` SET " +
                            "`harga` = '" + harga + "', " +
                            "`level_harga` = '" + EDIT_LEVEL.Text + "', ";

                            if (SATUAN.Text == "Box")
                            {
                                query += "`qty` = '" + DIBELI.Value + "', ";
                            }
                            else if (SATUAN.Text == "Lempeng")
                            {
                                query += "`qty_lempeng` = '" + DIBELI.Value + "', ";
                            }
                            else if (SATUAN.Text == "Butir")
                            {
                                query += "`qty_butir` = '" + DIBELI.Value + "', ";
                            }

                            query += "`subtotal` = '" + subTotal + "', " +
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
                            RefreshCart();
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

            Console.WriteLine("UpdateStokDeleted: " + kodeBarang + " | " + retur + " | " + lempengPerBox);

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

        private void KurangiStok(string kodeBarang)
        {
            int lempengPerBox = GetLempengPerBox(kodeBarang);
            int butirPerLempeng = GetButirPerLempeng(kodeBarang);
            int sisaBox = GetStokSatuan(kodeBarang);
            int sisaLempeng = GetStokStrip(kodeBarang);
            int sisaButir = GetStokTablet(kodeBarang);

            int tambah = int.Parse(DITAMBAH.Text);

            Console.WriteLine("KurangiStok: " + kodeBarang + " | " + tambah + " | " + lempengPerBox);

            if (SATUAN.Text == "Box")
            {
                // Mengurangkan stok untuk per satuan
                if (sisaBox >= tambah)
                {
                    KurangiStok(kodeBarang, tambah);
                }
                else
                {
                    MessageBox.Show("Stok tidak cukup untuk barang dengan kode " + kodeBarang);
                    return;
                }
            }

            if (SATUAN.Text == "Strip")
            {
                //per strip
                int sisaSetelahBeli = sisaLempeng - tambah;

                Console.WriteLine("AL: " + sisaLempeng + " - " + tambah + " = " + sisaSetelahBeli);
                if (sisaSetelahBeli < 0)
                {
                    if (sisaBox >= 1)
                    {
                        KurangiStok(kodeBarang, 1);
                        sisaLempeng = lempengPerBox + sisaSetelahBeli;
                        Console.WriteLine("BL: " + lempengPerBox + " + " + sisaSetelahBeli + " = " + sisaLempeng);
                        KurangiStokLempeng(kodeBarang, sisaLempeng);
                    }
                    else
                    {
                        MessageBox.Show("Stok tidak cukup untuk barang dengan kode " + kodeBarang);
                        return;
                    }
                }
                else
                {
                    sisaLempeng = sisaSetelahBeli;
                    KurangiStokLempeng(kodeBarang, sisaLempeng);
                }
            }

            if (SATUAN.Text == "Tablet")
            {
                int sisaSetelahBeliButir = sisaButir - tambah;

                Console.WriteLine("A: " + sisaButir + " - " + tambah + " = " + sisaSetelahBeliButir);
                if (sisaSetelahBeliButir < 0)
                {
                    // Pengurangan stok butir
                    sisaButir = butirPerLempeng + sisaSetelahBeliButir;
                    Console.WriteLine("B: " + butirPerLempeng + " + " + sisaSetelahBeliButir + " = " + sisaButir);
                    KurangiStokButir2(kodeBarang, Math.Abs(sisaButir));

                    int sisaSetelahBeli = sisaLempeng - 1;

                    if (sisaSetelahBeli < 0)
                    {
                        if (sisaBox >= 1)
                        {
                            KurangiStok(kodeBarang, 1);
                            sisaLempeng = lempengPerBox + sisaSetelahBeli;
                            KurangiStokLempeng(kodeBarang, sisaLempeng);
                            Console.WriteLine("C: " + sisaLempeng);
                        }
                        else
                        {
                            MessageBox.Show("Stok tidak cukup untuk barang dengan kode " + kodeBarang);
                            return;
                        }
                    }
                    else
                    {
                        sisaLempeng = sisaSetelahBeli;
                        KurangiStokLempeng(kodeBarang, sisaLempeng);
                        Console.WriteLine("D: " + sisaLempeng);
                    }
                }
                else
                {
                    // Pengurangan stok butir
                    sisaButir = sisaSetelahBeliButir;
                    KurangiStokButir2(kodeBarang, sisaButir);
                    Console.WriteLine("E: " + sisaButir);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DIRETUR.Value > 0)
            {
                UpdateStok(SelectedKode);
            }
            else if (DITAMBAH.Value > 0)
            {
                KurangiStok(SelectedKode);
            }
            updateData();
            MessageBox.Show("Data Berhasil Diubah");
        }

        private void DIRETUR_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = EDIT_HARGA.Text;

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
            string hargaText = EDIT_HARGA.Text;

            int jumlahBarang = Convert.ToInt32(DIBELI.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString();
        }

        public int GetJumlahBarangAwal(string kodeBarang)
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

        public int GetLempengBox(string kodeBarang)
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

        public int GetButirLempeng(string kodeBarang)
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

        public void KurangiStok(string kodeBarang, int jumlahDikurangkan)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "UPDATE tb_barang_masuk SET stok_satuan = stok_satuan - @jumlahDikurangkan WHERE kode_barang = @kodeBarang";

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
        public void KurangiStokLempeng(string kodeBarang, int sisaStok)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "UPDATE tb_barang_masuk SET stok_lempeng = @sisaStok WHERE kode_barang = @kodeBarang";

            Console.WriteLine("Lempeng: " + sisaStok);

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
        private void KurangiStokButir2(string kodeBarang, int jumlah)
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

        private void TB_UpdateStok()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string harga = TB_HARGA.Text;
                harga = harga.Replace(".", "");

                string subtotal = TB_SUBTOTAL.Text;
                subtotal = subtotal.Replace(".", "");

                string noFaktur = lbl_NOFAKTUR.Text;
                string kodeBarang = TB_KODEBARANG.Text;
                string namaBarang = TB_NAMABARANG.Text;
                string levelHarga = LEVELHARGA.Text;
                decimal qty = QTY.Value;

                decimal qty_lempeng = LEMPENG.Value;

                decimal qty_butir = BUTIR.Value;
                string namaPelanggan = lbl_NAMA.Text;
                string alamat = lbl_ALAMAT.Text;

                harga = harga.Replace(".", "");

                int lempengPerBox = GetLempengPerBox(kodeBarang);
                int butirPerLempeng = GetButirPerLempeng(kodeBarang);
                int sisaBox = GetJumlahBarangAwal(kodeBarang);
                int sisaLempeng = GetLempengBox(kodeBarang);
                int sisaButir = GetButirLempeng(kodeBarang);

                int beliBox = (int)qty;
                int beliLempeng = (int)qty_lempeng;
                int beliButir = (int)qty_butir;

                if (beliBox > 0)
                {
                    // Mengurangkan stok untuk per satuan
                    if (sisaBox >= beliBox)
                    {
                        KurangiStok(kodeBarang, beliBox);
                    }
                    else
                    {
                        MessageBox.Show("Stok tidak cukup untuk barang dengan kode " + kodeBarang);
                        return;
                    }
                }

                if (beliLempeng > 0)
                {
                    //per strip
                    int sisaSetelahBeli = sisaLempeng - beliLempeng;

                    Console.WriteLine("AL: " + sisaLempeng + " - " + beliLempeng + " = " + sisaSetelahBeli);
                    if (sisaSetelahBeli < 0)
                    {
                        if (sisaBox >= 1)
                        {
                            KurangiStok(kodeBarang, 1);
                            sisaLempeng = lempengPerBox + sisaSetelahBeli;
                            Console.WriteLine("BL: " + lempengPerBox + " + " + sisaSetelahBeli + " = " + sisaLempeng);
                            KurangiStokLempeng(kodeBarang, sisaLempeng);
                        }
                        else
                        {
                            MessageBox.Show("Stok tidak cukup untuk barang dengan kode " + kodeBarang);
                            return;
                        }
                    }
                    else
                    {
                        sisaLempeng = sisaSetelahBeli;
                        KurangiStokLempeng(kodeBarang, sisaLempeng);
                    }
                }

                if (beliButir > 0)
                {
                    int sisaSetelahBeliButir = sisaButir - beliButir;

                    Console.WriteLine("A: " + sisaButir + " - " + beliButir + " = " + sisaSetelahBeliButir);
                    if (sisaSetelahBeliButir < 0)
                    {
                        // Pengurangan stok butir
                        sisaButir = butirPerLempeng + sisaSetelahBeliButir;
                        Console.WriteLine("B: " + butirPerLempeng + " + " + sisaSetelahBeliButir + " = " + sisaButir);
                        KurangiStokButir2(kodeBarang, Math.Abs(sisaButir));

                        int sisaSetelahBeli = sisaLempeng - 1;

                        if (sisaSetelahBeli < 0)
                        {
                            if (sisaBox >= 1)
                            {
                                KurangiStok(kodeBarang, 1);
                                sisaLempeng = lempengPerBox + sisaSetelahBeli;
                                KurangiStokLempeng(kodeBarang, sisaLempeng);
                                Console.WriteLine("C: " + sisaLempeng);
                            }
                            else
                            {
                                MessageBox.Show("Stok tidak cukup untuk barang dengan kode " + kodeBarang);
                                return;
                            }
                        }
                        else
                        {
                            sisaLempeng = sisaSetelahBeli;
                            KurangiStokLempeng(kodeBarang, sisaLempeng);
                            Console.WriteLine("D: " + sisaLempeng);
                        }
                    }
                    else
                    {
                        // Pengurangan stok butir
                        sisaButir = sisaSetelahBeliButir;
                        KurangiStokButir2(kodeBarang, sisaButir);
                        Console.WriteLine("E: " + sisaButir);
                    }
                }

                string totalHarga = lbl_TOTAL.Text;
                totalHarga = totalHarga.Replace(".", "");
                string totalBarang = lbl_TOTALBARANG.Text;

                string query = "INSERT INTO `tb_barang_keluar` (`no_faktur`, `kode_barang`, `nama_barang`, `level_harga`, `harga`, `qty`, `qty_lempeng`, `qty_butir`, `satuan`, `nama_pelanggan`, `alamat`, `tgl_peng`, `jatuh_tempo`, `subtotal`, `total_barang`, `total_harga`) " +
                                "VALUES (@noFaktur, @kodeBarang, @namaBarang, @levelHarga, @harga, @qty, @qty_lempeng, @qty_butir, '-', @namaPelanggan, @alamat, @tglPengembalian, @jatuhTempo, @subtotal, @totalBarang, @totalHarga)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@noFaktur", noFaktur);
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);
                    cmd.Parameters.AddWithValue("@namaBarang", namaBarang);
                    cmd.Parameters.AddWithValue("@levelHarga", levelHarga);
                    cmd.Parameters.AddWithValue("@harga", harga);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@qty_lempeng", qty_lempeng);
                    cmd.Parameters.AddWithValue("@qty_butir", qty_butir);
                    cmd.Parameters.AddWithValue("@namaPelanggan", namaPelanggan);
                    cmd.Parameters.AddWithValue("@alamat", alamat);
                    cmd.Parameters.AddWithValue("@tglPengembalian", "2001-01-01");
                    cmd.Parameters.AddWithValue("@jatuhTempo", "2001-01-01");
                    cmd.Parameters.AddWithValue("@subtotal", subtotal);
                    cmd.Parameters.AddWithValue("@totalHarga", totalHarga);
                    cmd.Parameters.AddWithValue("@totalBarang", totalBarang);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Data Berhasil Ditambah");
                RefreshCart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan 3: " + ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPilihBarang frmPilihBarang = new FrmPilihBarang();
            frmPilihBarang.OnBarangInfoSelected += (barangInfo) =>
            {
                kode_barang = barangInfo.KodeBarang;

                groupBox4.Enabled = true;
                tb_btnSave.Enabled = true;

                string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    string query = "SELECT nama_barang, stok_satuan, stok_lempeng, harga_lempeng, harga_jual1, harga_jual2, harga_jual3, kode_barang FROM tb_barang_masuk WHERE kode_barang = @kode_barang";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@kode_barang", barangInfo.KodeBarang);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        TB_NAMABARANG.Text = reader["nama_barang"].ToString().ToUpper();

                        decimal hargaJual4 = reader.GetDecimal(3);
                        decimal hargaJual1 = reader.GetDecimal(4);
                        decimal hargaJual2 = reader.GetDecimal(5);
                        decimal hargaJual3 = reader.GetDecimal(6);
                        string kode = reader.GetString(7);

                        TB_KODEBARANG.Text = kode;

                        harga_jual4 = hargaJual4.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));

                        stokBarang = reader["stok_satuan"].ToString();
                        stokLempengBox = reader["stok_lempeng"].ToString();

                        decimal StokBarang;

                        if (decimal.TryParse(stokBarang, out StokBarang))
                        {
                            if (StokBarang == 0)
                            {
                                MessageBox.Show("Stok Barang Habis / Akan Habis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                QTY.Maximum = StokBarang;
                            }
                        }

                        int lempengPerBox = GetLempengPerBox(barangInfo.KodeBarang);
                        int butirPerLempeng = GetButirPerLempeng(barangInfo.KodeBarang);

                        if (lempengPerBox > 0)
                        {
                            LEMPENG.Enabled = true;
                        }
                        else
                        {
                            LEMPENG.Enabled = false;
                        }

                        if (butirPerLempeng > 0)
                        {
                            BUTIR.Enabled = true;
                        }
                        else
                        {
                            BUTIR.Enabled = false;
                        }

                        LEVELHARGA.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Kode Barang tidak ditemukan");
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan 4: " + ex.Message);
                }
            };

            frmPilihBarang.ShowDialog();
        }
        private void LEVELHARGA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = LEVELHARGA.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "1":
                    TB_HARGA.Text = harga_jual1;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                case "2":
                    TB_HARGA.Text = harga_jual2;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                case "3":
                    TB_HARGA.Text = harga_jual3;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                case "4":
                    TB_HARGA.Text = harga_jual4;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                default:
                    TB_HARGA.Text = "";
                    break;
            }
        }

        private void tb_btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TB_NAMABARANG.Text) ||
                TB_SUBTOTAL.Text == "0.000" ||
                string.IsNullOrEmpty(TB_HARGA.Text) ||
                string.IsNullOrEmpty(LEVELHARGA.Text) ||
                string.IsNullOrEmpty(TB_SUBTOTAL.Text))
            {
                MessageBox.Show("Harap isi semua kolom!");
            }
            else
            {
                if ((QTY.Value != 0 && LEMPENG.Value != 0 && BUTIR.Value != 0) || (QTY.Value == 0 && LEMPENG.Value == 0 && BUTIR.Value == 0))
                {
                    MessageBox.Show("Pilih salah satu per Lempeng, Box, atau Butir");
                }
                else
                {
                    TB_UpdateStok();
                }
            }
        }

        private void QTY_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = TB_HARGA.Text;

            int jumlahBarang = Convert.ToInt32(QTY.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            TB_SUBTOTAL.Text = totalHarga.ToString();
        }

        private void LEMPENG_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = TB_HARGA.Text;

            int jumlahBarang = Convert.ToInt32(LEMPENG.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            TB_SUBTOTAL.Text = totalHarga.ToString();
        }

        private void BUTIR_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = TB_HARGA.Text;

            int jumlahBarang = Convert.ToInt32(BUTIR.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            TB_SUBTOTAL.Text = totalHarga.ToString();
        }

        private void TB_HARGA_TextChanged(object sender, EventArgs e)
        {
            string hargaText = TB_HARGA.Text;

            int jumlahBarang = Convert.ToInt32(QTY.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            TB_SUBTOTAL.Text = totalHarga.ToString();
        }

        private void EDIT_LEVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = EDIT_LEVEL.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "1":
                    EDIT_HARGA.Text = e_harga_jual1;
                    break;
                case "2":
                    EDIT_HARGA.Text = e_harga_jual2;
                    break;
                case "3":
                    EDIT_HARGA.Text = e_harga_jual3;
                    break;
                case "4":
                    EDIT_HARGA.Text = e_harga_jual4;
                    break;
                default:
                    EDIT_HARGA.Text = "";
                    break;
            }
        }

        private void EDIT_HARGA_TextChanged(object sender, EventArgs e)
        {
            string hargaText = EDIT_HARGA.Text;

            int jumlahBarang = Convert.ToInt32(DIBELI.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString();
        }

        private void DIBELI_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = EDIT_HARGA.Text;

            int jumlahBarang = Convert.ToInt32(DIBELI.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString();

            if (isFirstCall)
            {
                isFirstCall = false;
            }
            else
            {
                DITAMBAH.Value = jumlahBarang - currentStok;
            }
        }

        private void btnSaveNama_Click(object sender, EventArgs e)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "UPDATE `tb_barang_keluar` SET " +
                "`nama_pelanggan` = '" + lbl_NAMA.Text + "', " +
                "`alamat` = '" + lbl_ALAMAT.Text + "' " +
                "WHERE `no_faktur` = '" + NoFaktur + "'";

            Console.WriteLine(query);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil Update Nama/Alamat Pelanggan");
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex.Message);
                    }
                }
            }
        }
    }
}
