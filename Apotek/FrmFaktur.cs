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
        }

        private void FrmFaktur_Load(object sender, EventArgs e)
        {
            LoadNoFaktur();

            InitializeDatabaseConnection();
        }

        void LoadNoFaktur()
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
                        lbl_NOFAKTUR.Text = nextFaktur.ToString();
                        lbl_TOTAL.Text = "0";
                        dgv.Rows.Clear();
                        RefreshCart();
                    }
                    else
                    {
                        lbl_NOFAKTUR.Text = "1000";
                        RefreshCart();
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
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT id, kode_barang, nama_barang, level_harga, satuan, subtotal, qty, qty_lempeng, qty_butir, harga, harga_lempeng, harga_butir FROM tb_cart WHERE no_faktur = @noFaktur";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        cmd.Parameters.AddWithValue("@noFaktur", lbl_NOFAKTUR.Text);

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
                                    int kodeBarang = reader.GetInt32(1);
                                    string namaBarang = reader.GetString(2);
                                    string levelHarga = reader.GetString(3);
                                    string satuan = reader.GetString(4);
                                    decimal subtotal = reader.GetDecimal(5);
                                    int qty = reader.GetInt32(6);
                                    int qty_lempeng = reader.GetInt32(7);
                                    int qty_butir = reader.GetInt32(8);
                                    decimal harga = reader.GetDecimal(9);
                                    decimal harga_lempeng = reader.GetDecimal(10);
                                    decimal harga_butir = reader.GetDecimal(11);

                                    string hargaLempeng = harga_lempeng.ToString("N0", new CultureInfo("id-ID"));
                                    string hargaBrg = harga.ToString("N0", new CultureInfo("id-ID"));
                                    string subTotal = subtotal.ToString("N0", new CultureInfo("id-ID"));

                                    Image editIcon = Properties.Resources.icons8_delete_24px_1; // Ganti dengan gambar ikon Anda

                                    dgv.Rows.Add(id, no, kodeBarang, namaBarang, levelHarga, hargaBrg, qty, qty_lempeng, qty_butir, satuan, subTotal, editIcon);

                                    totalHarga += subtotal;
                                    totalBarang += (qty + qty_lempeng + qty_butir);
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
                        if (this.connection.State == ConnectionState.Closed)
                            this.connection.Open();

                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            string noFaktur = lbl_NOFAKTUR.Text;
                            string kodeBarang = row.Cells["column10"].Value.ToString();
                            string namaBarang = row.Cells["column1"].Value.ToString();
                            string levelHarga = row.Cells["column2"].Value.ToString();
                            string harga = row.Cells["column3"].Value.ToString();
                            string qty = row.Cells["column4"].Value.ToString();
                            
                            string qty_lempeng = row.Cells["ColumnQtyLempeng"].Value.ToString();
                            
                            string qty_butir = row.Cells["ColumnQtyButir"].Value.ToString();
                            string satuan = row.Cells["column5"].Value.ToString();
                            string namaPelanggan = NAMAPELANGGAN.Text;
                            string alamat = ALAMAT.Text;
                            string tglPengembalian = TGLPENGAMBILAN.Value.ToString("yyyy-MM-dd");
                            decimal getjatuhTempo = JATUHTEMPO.Value;

                            harga = harga.Replace(".", "");

                            int lempengPerBox = GetLempengPerBox(kodeBarang);
                            int butirPerLempeng = GetButirPerLempeng(kodeBarang);
                            int sisaBox = GetJumlahBarangAwal(kodeBarang);
                            int sisaLempeng = GetLempengBox(kodeBarang);
                            int sisaButir = GetButirLempeng(kodeBarang);

                            int beliBox = int.Parse(qty);
                            int beliLempeng = int.Parse(qty_lempeng);
                            int beliButir = int.Parse(qty_butir);

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

                            DateTime tglPengembalianDateTime = DateTime.Parse(tglPengembalian);
                            DateTime jatuhTempoDateTime = tglPengembalianDateTime.AddDays(Convert.ToDouble(getjatuhTempo));

                            string jatuhTempo = jatuhTempoDateTime.ToString("yyyy-MM-dd");

                            string totalHarga = lbl_TOTAL.Text;
                            totalHarga = totalHarga.Replace(".", "");
                            string totalBarang = lbl_TOTALBARANG.Text;
                            //int totalBarang = int.Parse(totalBarangText);
                            
                            string subtotal = row.Cells["column6"].Value.ToString();

                            subtotal = subtotal.Replace(".", "");

                            string query = "INSERT INTO `tb_barang_keluar` (`no_faktur`, `kode_barang`, `nama_barang`, `level_harga`, `harga`, `qty`, `qty_lempeng`, `qty_butir`, `satuan`, `nama_pelanggan`, `alamat`, `tgl_peng`, `jatuh_tempo`, `subtotal`, `total_barang`, `total_harga`) " +
                                            "VALUES (@noFaktur, @kodeBarang, @namaBarang, @levelHarga, @harga, @qty, @qty_lempeng, @qty_butir, @satuan, @namaPelanggan, @alamat, @tglPengembalian, @jatuhTempo, @subtotal, @totalBarang, @totalHarga)";

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
                                cmd.Parameters.AddWithValue("@satuan", satuan);
                                cmd.Parameters.AddWithValue("@namaPelanggan", namaPelanggan);
                                cmd.Parameters.AddWithValue("@alamat", alamat);
                                cmd.Parameters.AddWithValue("@tglPengembalian", tglPengembalian);
                                cmd.Parameters.AddWithValue("@jatuhTempo", jatuhTempo);
                                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                                cmd.Parameters.AddWithValue("@totalHarga", totalHarga);
                                cmd.Parameters.AddWithValue("@totalBarang", totalBarang);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Data Berhasil Disimpan");
                        int noFakturToDelete = Convert.ToInt32(lbl_NOFAKTUR.Text);
                        HapusDataCart(noFakturToDelete);
                        this.connection.Close();

                        FrmCetakFaktur frmCetak = new FrmCetakFaktur();
                        frmCetak.NoFaktur = lbl_NOFAKTUR.Text;
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

        public void addCart()
        {
            try
            {
                string hargaText = HARGA.Text;
                hargaText = hargaText.Replace(".", "");

                string subTotal = SUBTOTAL.Text;
                subTotal = subTotal.Replace(".", "");

                string kueri = "INSERT INTO `tb_cart`(`no_faktur`, `kode_barang`, `nama_barang`, `level_harga`, `harga`, `qty`, `qty_lempeng`, `qty_butir`, `satuan`, `subtotal`) VALUES ('" + lbl_NOFAKTUR.Text + "', '" + kode_barang + "', '" + this.NAMABARANG.Text + "', '" + this.LEVELHARGA.Text + "', '" + hargaText + "', '" + this.QTY.Text + "', '" + this.LEMPENG.Text + "', '" + this.BUTIR.Text + "', '" + this.SATUANBARANG.Text + "', '" + subTotal + "')";

                if (this.connection.State == ConnectionState.Closed)
                    this.connection.Open();

                MySqlCommand cmd = new MySqlCommand(kueri, connection);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                MessageBox.Show("Succesfully Added");
                this.connection.Close();

                this.NAMABARANG.Text = "";
                this.LEVELHARGA.SelectedIndex.ToString("1");
                harga_jual1 = "0";
                harga_jual2 = "0";
                harga_jual3 = "0";
                harga_jual4 = "0";
                this.HARGA.Text = "";
                this.QTY.Value = 1;
                this.SATUANBARANG.Text = "";
                this.SUBTOTAL.Text = "";

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

                groupBox2.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;

                string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    string query = "SELECT nama_barang, satuan_barang, stok_satuan, stok_lempeng, harga_lempeng, harga_jual1, harga_jual2, harga_jual3 FROM tb_barang_masuk WHERE kode_barang = @kode_barang";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@kode_barang", barangInfo.KodeBarang);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        NAMABARANG.Text = reader["nama_barang"].ToString();
                        SATUANBARANG.Text = reader["satuan_barang"].ToString();

                        decimal hargaJual4 = reader.GetDecimal(4);
                        decimal hargaJual1 = reader.GetDecimal(5);
                        decimal hargaJual2 = reader.GetDecimal(6);
                        decimal hargaJual3 = reader.GetDecimal(7);

                        harga_jual4 = hargaJual4.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                        harga_jual3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));

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

                        if (SATUANBARANG.Text == "Box")
                        {
                            QTY.Value = 0;
                            LEMPENG.Value = 0;
                            BUTIR.Value = 0;
                            lbl_BUTIR.Enabled = true;
                            qtyText.Text = "Per Box";
                            txtLempeng.Text = "Per Strip";
                            txtLempeng.Enabled = true;

                            if(lempengPerBox > 0)
                            {
                                LEMPENG.Enabled = true;
                            }
                            if (butirPerLempeng > 0)
                            {
                                BUTIR.Enabled = true;
                            }
                        }
                        else if (SATUANBARANG.Text == "Kotak")
                        {
                            QTY.Value = 0;
                            LEMPENG.Value = 0;
                            BUTIR.Value = 0;
                            qtyText.Text = "Per Kotak";
                            txtLempeng.Text = "Per Pcs";
                            txtLempeng.Enabled = true;
                            BUTIR.Enabled = false;

                            if (lempengPerBox > 0)
                            {
                                LEMPENG.Enabled = true;
                            }
                        }
                        else
                        {
                            LEMPENG.Enabled = false;
                            txtLempeng.Enabled = false;
                            qtyText.Text = "Quantity";
                            LEMPENG.Value = 0;
                            BUTIR.Value = 0;
                            BUTIR.Enabled = false;
                            lbl_BUTIR.Enabled = false;
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
                string.IsNullOrEmpty(SATUANBARANG.Text) ||
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
                    addCart();
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["column11"].Index && e.RowIndex >= 0)
            {
                int kodeBarangToDelete = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ID"].Value);
                HapusDataBarangKeluar(kodeBarangToDelete);
            }
        }
        private void HapusDataBarangKeluar(int kodeBarangToDelete)
        {
            try
            {
                string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string deleteQuery = "DELETE FROM tb_cart WHERE id = @kodeBarangToDelete";
                MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
                deleteCmd.Parameters.AddWithValue("@kodeBarangToDelete", kodeBarangToDelete);

                int rowsAffected = deleteCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                    MessageBox.Show("Data berhasil dihapus");
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
        private void HapusDataCart(int nofaktur)
        {
            try
            {
                string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string deleteQuery = "DELETE FROM tb_cart WHERE no_faktur = @nofaktur";
                MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
                deleteCmd.Parameters.AddWithValue("@nofaktur", nofaktur);

                deleteCmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan 6: " + ex.Message);
            }
        }
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            insertData();
            LoadNoFaktur();
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

            SUBTOTAL.Text = totalHarga.ToString();
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

            SUBTOTAL.Text = totalHarga.ToString();
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

            SUBTOTAL.Text = totalHarga.ToString();
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

            SUBTOTAL.Text = totalHarga.ToString();
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
    }
}
