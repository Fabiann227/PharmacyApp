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
    public partial class FrmFaktur : Form
    {
        public DataTable fakturDataTable;
        private int previousQuantity = -1;
        private int previousQtyButir = -1;
        private int previousQtyLempeng = -1;
        string harga_jual1;
        string harga_jual2;
        string harga_jual3;
        string harga_jual4;

        string kode_barang;

        string stokLempengBox;
        string stokBarang;
        string IdPBF;

        private MySqlConnection connection;
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;

        public FrmFaktur()
        {
            InitializeComponent();
            this.KeyPreview = true; // Penting untuk mendeteksi event key press di level form
            this.KeyDown += FrmFaktur_KeyDown;
        }

        private void FrmFaktur_Load(object sender, EventArgs e)
        {
            lbl_TOTAL.Text = "0";
            dgv.Rows.Clear();
            InitializeDatabaseConnection();
            this.ActiveControl = NAMABARANG;
        }

        string LoadNoFaktur()
        {
            TGLPENGAMBILAN.Value = DateTime.Now;

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT MAX(no_faktur) FROM tb_barang_keluar";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int highestFaktur = Convert.ToInt32(result);
                        int nextFaktur = highestFaktur + 1;
                        
                        return nextFaktur.ToString();
                    }
                    else
                    {
                        return "1000";
                    }
                }
            }
        }


        private void InitializeDatabaseConnection()
        {
            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
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

        // Fungsi untuk mengambil nilai lempeng_box dari database
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

        // Fungsi untuk mengurangkan stok dalam basis data
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
        private void RefreshCart()
        {
            decimal totalHarga = 0;
            int totalBarang = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                decimal subtotal = Convert.ToDecimal(row.Cells["Column6"].Value);
                int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
                int qty_lempeng = Convert.ToInt32(row.Cells["ColumnQtyLempeng"].Value);
                int qty_butir = Convert.ToInt32(row.Cells["ColumnQtyButir"].Value);

                totalHarga += subtotal;
                Console.WriteLine("Subtotal : " + subtotal);
                totalBarang += (qty + qty_lempeng + qty_butir);
            }

            lbl_TOTAL.Text = totalHarga.ToString();
            lbl_TOTALBARANG.Text = totalBarang.ToString();

            QTY.Value = 0;
            LEMPENG.Value = 0;
            BUTIR.Value = 0;
        }

        public void insertData()
        {
            if (string.IsNullOrEmpty(NAMAPELANGGAN.Text) || string.IsNullOrEmpty(ALAMAT.Text))
            {
                MessageBox.Show("Harap isi Data Pelanggan terlebih dahulu!");
            }
            else
            {
                try
                {
                    if (dgv.Rows.Count > 0)
                    {
                        //versi 5 memperbaiki stok yang kurang untuk barang yang stoknya habis
                        // Cek stok semua barang terlebih dahulu
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            string kodeBarang = row.Cells["column10"].Value.ToString();
                            string namaBarang = row.Cells["column1"].Value.ToString();
                            int beliBox = int.Parse(row.Cells["Quantity"].Value.ToString());
                            int beliLempeng = int.Parse(row.Cells["ColumnQtyLempeng"].Value.ToString());
                            int beliButir = int.Parse(row.Cells["ColumnQtyButir"].Value.ToString());

                            int sisaBox = GetJumlahBarangAwal(kodeBarang);
                            int sisaLempeng = GetLempengBox(kodeBarang);
                            int sisaButir = GetButirLempeng(kodeBarang);

                            // Cek stok Box
                            if (beliBox > 0 && beliBox > sisaBox)
                            {
                                MessageBox.Show("Stok tidak cukup (Box): " + namaBarang);
                                return;
                            }

                            // Cek stok Lempeng
                            int sisaSetelahBeliLempeng = sisaLempeng - beliLempeng;
                            if (beliLempeng > 0 && sisaSetelahBeliLempeng < 0 && sisaBox < 1)
                            {
                                MessageBox.Show("Stok tidak cukup (Lempeng): " + namaBarang);
                                return;
                            }

                            // Cek stok Butir
                            int sisaSetelahBeliButir = sisaButir - beliButir;
                            if (beliButir > 0 && sisaSetelahBeliButir < 0 && (sisaLempeng < 1 || (sisaSetelahBeliLempeng < 0 && sisaBox < 1)))
                            {
                                MessageBox.Show("Stok tidak cukup (Butir): " + namaBarang);
                                return;
                            }
                        }

                        // Jika semua stok cukup, lanjutkan dengan eksekusi query
                        if (this.connection.State == ConnectionState.Closed)
                            this.connection.Open();

                        string noFaktur = LoadNoFaktur();

                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            string kodeBarang = row.Cells["column10"].Value.ToString();
                            string namaBarang = row.Cells["column1"].Value.ToString();
                            string levelHarga = row.Cells["column2"].Value.ToString();
                            string harga = row.Cells["column3"].Value.ToString().Replace(".", "");
                            string qty = row.Cells["Quantity"].Value.ToString();
                            string qty_lempeng = row.Cells["ColumnQtyLempeng"].Value.ToString();
                            string qty_butir = row.Cells["ColumnQtyButir"].Value.ToString();
                            string namaPelanggan = NAMAPELANGGAN.Text;
                            string alamat = ALAMAT.Text;
                            string tglPengembalian = TGLPENGAMBILAN.Value.ToString("yyyy-MM-dd");
                            decimal getjatuhTempo = JATUHTEMPO.Value;

                            int lempengPerBox = GetLempengPerBox(kodeBarang);
                            int butirPerLempeng = GetButirPerLempeng(kodeBarang);

                            int beliBox = int.Parse(qty);
                            int beliLempeng = int.Parse(qty_lempeng);
                            int beliButir = int.Parse(qty_butir);

                            // Pengurangan stok Box
                            if (beliBox > 0)
                            {
                                KurangiStok(kodeBarang, beliBox);
                            }

                            // Pengurangan stok Lempeng
                            if (beliLempeng > 0)
                            {
                                int sisaLempeng = GetLempengBox(kodeBarang);
                                int sisaSetelahBeli = sisaLempeng - beliLempeng;
                                if (sisaSetelahBeli < 0)
                                {
                                    KurangiStok(kodeBarang, 1);
                                    sisaLempeng = lempengPerBox + sisaSetelahBeli;
                                }
                                else
                                {
                                    sisaLempeng = sisaSetelahBeli;
                                }
                                KurangiStokLempeng(kodeBarang, sisaLempeng);
                            }

                            // Pengurangan stok Butir
                            if (beliButir > 0)
                            {
                                int sisaButir = GetButirLempeng(kodeBarang);
                                int sisaSetelahBeliButir = sisaButir - beliButir;
                                if (sisaSetelahBeliButir < 0)
                                {
                                    sisaButir = butirPerLempeng + sisaSetelahBeliButir;
                                    KurangiStokButir2(kodeBarang, Math.Abs(sisaButir));

                                    int sisaLempeng = GetLempengBox(kodeBarang) - 1;
                                    if (sisaLempeng < 0)
                                    {
                                        KurangiStok(kodeBarang, 1);
                                        sisaLempeng = lempengPerBox + sisaLempeng;
                                    }
                                    KurangiStokLempeng(kodeBarang, sisaLempeng);
                                }
                                else
                                {
                                    KurangiStokButir2(kodeBarang, sisaSetelahBeliButir);
                                }
                            }

                            DateTime tglPengembalianDateTime = DateTime.Parse(tglPengembalian);
                            DateTime jatuhTempoDateTime = tglPengembalianDateTime.AddDays(Convert.ToDouble(getjatuhTempo));
                            string jatuhTempo = jatuhTempoDateTime.ToString("yyyy-MM-dd");

                            string totalHarga = lbl_TOTAL.Text.Replace(".", "");
                            string totalBarang = lbl_TOTALBARANG.Text;
                            string subtotal = row.Cells["column6"].Value.ToString().Replace(".", "");

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
                                cmd.Parameters.AddWithValue("@tglPengembalian", tglPengembalian);
                                cmd.Parameters.AddWithValue("@jatuhTempo", jatuhTempo);
                                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                                cmd.Parameters.AddWithValue("@totalBarang", totalBarang);
                                cmd.Parameters.AddWithValue("@totalHarga", totalHarga);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Data Berhasil Disimpan");
                        this.connection.Close();
                        dgv.Rows.Clear();

                        FrmCetakFaktur frmCetak = new FrmCetakFaktur();
                        frmCetak.NoFaktur = noFaktur;
                        frmCetak.Subtotal = SUBTOTAL.Text;
                        frmCetak.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No data to insert.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan 2: " + ex.Message);
                }
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPilihBarang frmPilihBarang = new FrmPilihBarang();
            frmPilihBarang.OnBarangInfoSelected += (barangInfo) =>
            {
                kode_barang = barangInfo.KodeBarang;

                groupBox2.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;

                string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    string query = "SELECT nama_barang, stok_satuan, stok_lempeng, harga_lempeng, harga_jual1, harga_jual2, harga_jual3 FROM tb_barang_masuk WHERE kode_barang = @kode_barang";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@kode_barang", barangInfo.KodeBarang);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        NAMABARANG.Text = reader["nama_barang"].ToString().ToUpper();

                        decimal hargaJual4 = reader.GetDecimal(3);
                        decimal hargaJual1 = reader.GetDecimal(4);
                        decimal hargaJual2 = reader.GetDecimal(5);
                        decimal hargaJual3 = reader.GetDecimal(6);

                        harga_jual4 = hargaJual4.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));

                        if (LEVELHARGA.SelectedItem != null && LEVELHARGA.SelectedItem.ToString() == "1")
                        {
                            HARGA.Text = harga_jual1;
                            BUTIR.Value = 0;
                            LEMPENG.Value = 0;
                            QTY.Value = 0;
                        }
                        else if (LEVELHARGA.SelectedItem != null && LEVELHARGA.SelectedItem.ToString() == "2")
                        {
                            HARGA.Text = harga_jual2;
                            BUTIR.Value = 0;
                            LEMPENG.Value = 0;
                            QTY.Value = 0;
                        }
                        else if (LEVELHARGA.SelectedItem != null && LEVELHARGA.SelectedItem.ToString() == "3")
                        {
                            HARGA.Text = harga_jual3;
                            BUTIR.Value = 0;
                            LEMPENG.Value = 0;
                            QTY.Value = 0;
                        }
                        else if (LEVELHARGA.SelectedItem != null && LEVELHARGA.SelectedItem.ToString() == "4")
                        {
                            HARGA.Text = harga_jual4;
                            BUTIR.Value = 0;
                            LEMPENG.Value = 0;
                            QTY.Value = 0;
                        }

                        stokBarang = reader["stok_satuan"].ToString();
                        stokLempengBox = reader["stok_lempeng"].ToString();

                        decimal StokBarang;

                        if (decimal.TryParse(stokBarang, out StokBarang))
                        {
                            if(StokBarang == 0)
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
            };

            frmPilihBarang.ShowDialog();
        }

        private void LEVELHARGA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = LEVELHARGA.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "1":
                    HARGA.Text = harga_jual1;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                case "2":
                    HARGA.Text = harga_jual2;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                case "3":
                    HARGA.Text = harga_jual3;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                case "4":
                    HARGA.Text = harga_jual4;
                    QTY.Value = 0;
                    LEMPENG.Value = 0;
                    BUTIR.Value = 0;
                    break;
                default:
                    HARGA.Text = "";
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NAMABARANG.Text) ||
                SUBTOTAL.Text == "0.000" ||
                string.IsNullOrEmpty(HARGA.Text) ||
                string.IsNullOrEmpty(LEVELHARGA.Text) ||
                string.IsNullOrEmpty(SUBTOTAL.Text))
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
                    string hargaText = HARGA.Text;
                    decimal subTotalValue = decimal.Parse(SUBTOTAL.Text);
                    string subTotal = subTotalValue.ToString("N0", new CultureInfo("id-ID"));

                    Image editIcon = Properties.Resources.icons8_delete_24px_1;
                    dgv.Rows.Add("0", "-", kode_barang, NAMABARANG.Text, LEVELHARGA.Text, hargaText, QTY.Text, LEMPENG.Text, BUTIR.Text, subTotal, "Hapus");
                    RefreshCart();
                    btnNew.PerformClick();
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["column11"].Index && e.RowIndex >= 0)
            {
                dgv.Rows.RemoveAt(e.RowIndex);
                RefreshCart();
            }
        }
        
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            insertData();
        }

        private void QTY_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = HARGA.Text;

            int jumlahBarang = Convert.ToInt32(QTY.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString().Replace(".", "");
        }

        private void HARGA_TextChanged(object sender, EventArgs e)
        {
            string hargaText = HARGA.Text;

            int jumlahBarang = Convert.ToInt32(QTY.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString().Replace(".", "");
        }

        private void LEMPENG_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = HARGA.Text;

            int jumlahBarang = Convert.ToInt32(LEMPENG.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString().Replace(".", "");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
        }

        private void BUTIR_ValueChanged(object sender, EventArgs e)
        {
            string hargaText = HARGA.Text;

            int jumlahBarang = Convert.ToInt32(BUTIR.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(hargaText, out decimal harga))
            {
                totalHarga = harga * jumlahBarang;
            }

            SUBTOTAL.Text = totalHarga.ToString().Replace(".", "");
        }

        private void NAMAPELANGGAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                FrmPilihPBF pilih = new FrmPilihPBF();
                pilih.OnBarangInfoSelected += (barangInfo) =>
                {
                    IdPBF = barangInfo.IDBarang;

                    string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                    MySqlConnection connection = new MySqlConnection(connectionString);

                    try
                    {
                        connection.Open();

                        string query = "SELECT id, nama, alamat FROM tb_supplier WHERE id = @idBarang";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@idBarang", IdPBF);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string nama = reader.GetString(1);
                            string alamat = reader.GetString(2);

                            NAMAPELANGGAN.Text = nama;
                            ALAMAT.Text = alamat;
                        }
                        else
                        {
                            MessageBox.Show("Nama Pelanggan tidak ditemukan");
                        }

                        reader.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan 99: " + ex.Message);
                    }
                };
                pilih.ShowDialog();
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RefreshCart();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];

                if (e.ColumnIndex == dgv.Columns["Quantity"].Index)
                {
                    int newQty = Convert.ToInt32(row.Cells["Quantity"].Value);

                    // Tambahkan kondisi untuk memeriksa nilai Quantity awal
                    if (previousQuantity == 0)
                    {
                        // Jika nilai Quantity awal adalah 0, batalkan perubahan
                        MessageBox.Show("Perubahan tidak diperbolehkan karena nilai Quantity awal adalah 0.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Kembalikan nilai Quantity ke nilai awal
                        row.Cells["Quantity"].Value = previousQuantity;
                        return; // Keluar dari metode
                    }

                    // Lanjutkan dengan perhitungan jika nilai Quantity awal tidak 0
                    string hargaJualStr = row.Cells["Column3"].Value.ToString().Replace(".", "");
                    decimal hargaJual = Convert.ToDecimal(hargaJualStr);

                    decimal subtotal = newQty * hargaJual;
                    string formattedSubtotal = subtotal.ToString("N0", new CultureInfo("id-ID"));

                    // Mengatur nilai sel Column6 dengan hasil yang diformat
                    row.Cells["Column6"].Value = formattedSubtotal;
                }
                else if (e.ColumnIndex == dgv.Columns["Column3"].Index)
                {
                    int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    string hargaJualStr = row.Cells["Column3"].Value.ToString().Replace(".", "");
                    decimal newHargaJual = Convert.ToDecimal(hargaJualStr);

                    decimal subtotal = qty * newHargaJual;
                    string formattedSubtotal = subtotal.ToString("N0", new CultureInfo("id-ID"));

                    // Mengatur nilai sel Column6 dengan hasil yang diformat
                    row.Cells["Column6"].Value = formattedSubtotal;
                }
                else if (e.ColumnIndex == dgv.Columns["ColumnQtyLempeng"].Index)
                {
                    int newQty = Convert.ToInt32(row.Cells["ColumnQtyLempeng"].Value);

                    if (previousQtyLempeng == 0)
                    {
                        MessageBox.Show("Perubahan tidak diperbolehkan karena nilai Quantity awal adalah 0.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        row.Cells["ColumnQtyLempeng"].Value = previousQtyLempeng;
                        return;
                    }

                    string hargaJualStr = row.Cells["Column3"].Value.ToString().Replace(".", "");
                    decimal hargaJual = Convert.ToDecimal(hargaJualStr);

                    decimal subtotal = newQty * hargaJual;
                    string formattedSubtotal = subtotal.ToString("N0", new CultureInfo("id-ID"));

                    row.Cells["Column6"].Value = formattedSubtotal;
                }
                else if (e.ColumnIndex == dgv.Columns["ColumnQtyButir"].Index)
                {
                    int newQty = Convert.ToInt32(row.Cells["ColumnQtyButir"].Value);

                    if (previousQtyButir == 0)
                    {
                        MessageBox.Show("Perubahan tidak diperbolehkan karena nilai Quantity awal adalah 0.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        row.Cells["ColumnQtyButir"].Value = previousQtyButir;
                        return;
                    }

                    string hargaJualStr = row.Cells["Column3"].Value.ToString().Replace(".", "");
                    decimal hargaJual = Convert.ToDecimal(hargaJualStr);

                    decimal subtotal = newQty * hargaJual;
                    string formattedSubtotal = subtotal.ToString("N0", new CultureInfo("id-ID"));

                    row.Cells["Column6"].Value = formattedSubtotal;
                }
            }
        }

        private void lbl_TOTAL_Click(object sender, EventArgs e)
        {
            
        }
        public void SaveData()
        {
            fakturDataTable = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                // Pastikan tipe data kolom tidak null
                Type columnType = column.ValueType ?? typeof(string); // Default ke string jika null
                fakturDataTable.Columns.Add(column.Name, columnType);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dataRow = fakturDataTable.NewRow();
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Value;
                    }
                    fakturDataTable.Rows.Add(dataRow);
                }
            }
        }

        // Method untuk memuat data dari DataTable ke DataGridView
        public void LoadData(DataTable dataTable)
        {
            if (dataTable != null)
            {
                dgv.Rows.Clear();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    int rowIndex = dgv.Rows.Add();
                    DataGridViewRow row = dgv.Rows[rowIndex];
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        row.Cells[i].Value = dataRow[i];
                    }
                }
            }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Simpan nilai Quantity sebelum diubah
            if (e.ColumnIndex == dgv.Columns["Quantity"].Index && e.RowIndex >= 0)
            {
                previousQuantity = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Quantity"].Value);
            }
            if (e.ColumnIndex == dgv.Columns["ColumnQtyLempeng"].Index && e.RowIndex >= 0)
            {
                previousQtyLempeng = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ColumnQtyLempeng"].Value);
            }
            if (e.ColumnIndex == dgv.Columns["ColumnQtyButir"].Index && e.RowIndex >= 0)
            {
                previousQtyButir = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ColumnQtyButir"].Value);
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["Quantity"].Index && e.RowIndex >= 0)
            {
                // Validasi jika input adalah numerik
                if (!int.TryParse(e.FormattedValue.ToString(), out _))
                {
                    // Jika input bukan numerik, tampilkan pesan kesalahan
                    e.Cancel = true;
                    MessageBox.Show("Masukkan hanya angka untuk Quantity.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (e.ColumnIndex == dgv.Columns["ColumnQtyLempeng"].Index && e.RowIndex >= 0)
            {
                // Validasi jika input adalah numerik
                if (!int.TryParse(e.FormattedValue.ToString(), out _))
                {
                    // Jika input bukan numerik, tampilkan pesan kesalahan
                    e.Cancel = true;
                    MessageBox.Show("Masukkan hanya angka untuk Quantity.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (e.ColumnIndex == dgv.Columns["ColumnQtyButir"].Index && e.RowIndex >= 0)
            {
                // Validasi jika input adalah numerik
                if (!int.TryParse(e.FormattedValue.ToString(), out _))
                {
                    // Jika input bukan numerik, tampilkan pesan kesalahan
                    e.Cancel = true;
                    MessageBox.Show("Masukkan hanya angka untuk Quantity.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void FrmFaktur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.T)
            {
                btnNew.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                btnSimpan.PerformClick();
            }
        }
    }
}
