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
using OfficeOpenXml.Table;
using OfficeOpenXml;
using System.IO;

namespace Apotek
{
    public partial class FrmMaster : Form
    {
        private MySqlConnection connection;
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;

        private string SelectedID;

        private enum SaveSectionEnum
        {
            None,
            Insert,
            Update
        }

        private SaveSectionEnum SaveSection;

        public FrmMaster()
        {
            InitializeComponent();
        }

        private void FrmMaster_Load(object sender, EventArgs e)
        { 
            InitializeDatabaseConnection();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
                //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            LoadData();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        private void ClearString()
        {
            NAMABARANG.Text = "";
            MODAL.Text = "";
            DISTRIBUTOR.Text = "";
            HARGAJUAL1.Text = "";
            LABA1.Text = "";
            HARGAJUAL2.Text = "";
            LABA2.Text = "";
            HARGAJUAL3.Text = "0";
            LABA3.Text = "";
            LEMPENGBOX.Value = 0;
            BUTIRBOX.Value = 0;
            JUMLAHBARANG.Value = 0;
            HARGALEMPENG.Text = "0";
        }
        private void HideTextBox()
        {
            groupBox2.Visible = false;
            gBstok.Visible = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            ClearString();
        }

        private void LoadData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT kode_barang, nama_barang, jenis_barang, satuan_barang, lempeng_box, stok_awal, total_harga, distributor, modal, harga_lempeng, explayerd, harga_jual1, laba1, harga_jual2, laba2, harga_jual3, laba3, stok_satuan, stok_lempeng, stok_butir, stok_akhir, butir_lempeng, laba_lempeng FROM tb_barang_masuk"; // Ganti dengan nama tabel dan query Anda

            if (this.connection.State == ConnectionState.Closed)
                this.connection.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                try
                {
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
                                int stokAwal = reader.GetInt32(5);
                                decimal totalHarga = reader.GetDecimal(6);
                                string distributor = reader.GetString(7);

                                decimal modal = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8);
                                decimal hargaJual1 = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                                decimal hargaJual2 = reader.IsDBNull(13) ? 0 : reader.GetDecimal(13);
                                decimal hargaJual3 = reader.IsDBNull(15) ? 0 : reader.GetDecimal(15);
                                decimal hargalempeng = reader.GetDecimal(9);
                                string labaLempeng = reader.GetString(22);
                                string explayerd = reader.GetDateTime(10).ToString("yyyy-MM-dd");
                                string laba1 = reader.GetString(12);
                                string laba2 = reader.GetString(14);
                                string laba3 = reader.GetString(16);
                                int stokSatuan = reader.GetInt32(17);
                                int stokLempeng = reader.GetInt32(18);
                                int stokButir = reader.GetInt32(19);
                                string stokAkhir = reader.GetString(20);
                                int butirStrip = reader.GetInt32(21);

                                string total_harga = totalHarga.ToString("N0", new CultureInfo("id-ID"));
                                string Modal = modal.ToString("N0", new CultureInfo("id-ID"));
                                string harga_lempeng = hargalempeng.ToString("N0", new CultureInfo("id-ID"));

                                string hargaRupiah1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                                string hargaRupiah2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                                string hargaRupiah3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));

                                string harga_strip = hargalempeng.ToString("N0", new CultureInfo("id-ID"));

                                Image editIcon = Properties.Resources.icons8_info_24px_1;
                                namaBarang = namaBarang.ToUpper();
                                // Menambahkan data ke DataGridView
                                dgv.Rows.Add(kodeBarang, namaBarang, stokAwal, lempengbox, butirStrip, stokSatuan, stokLempeng, stokButir, stokAkhir, total_harga, distributor, explayerd, Modal, hargaRupiah1, hargaRupiah2, hargaRupiah3, harga_strip, editIcon);
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
                    MessageBox.Show("Terjadi kesalahan (Master - 1): " + ex.Message);
                }
            }
        }

        private void SearchData()
        {
            string query = "SELECT kode_barang, nama_barang, jenis_barang, satuan_barang, lempeng_box, stok_awal, total_harga, distributor, modal, harga_lempeng, explayerd, harga_jual1, laba1, harga_jual2, laba2, harga_jual3, laba3, stok_satuan, stok_lempeng, stok_butir, stok_akhir, butir_lempeng, laba_lempeng FROM tb_barang_masuk WHERE kode_barang LIKE '%" + SEARCH.Text + "%' OR nama_barang LIKE '%" + SEARCH.Text + "%'";

            if (this.connection.State == ConnectionState.Closed)
                this.connection.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                try
                {
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
                                // Membaca nilai dari kolom-kolom yang sesuai
                                int kodeBarang = reader.GetInt32(0);
                                string namaBarang = reader.GetString(1);
                                string jenisBarang = reader.GetString(2);
                                string satuanBarang = reader.GetString(3);
                                int lempengbox = reader.GetInt32(4);
                                int stokAwal = reader.GetInt32(5);
                                decimal totalHarga = reader.GetDecimal(6);
                                string distributor = reader.GetString(7);
                                decimal modal = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8);
                                decimal hargaJual1 = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                                decimal hargaJual2 = reader.IsDBNull(13) ? 0 : reader.GetDecimal(13);
                                decimal hargaJual3 = reader.IsDBNull(15) ? 0 : reader.GetDecimal(15);
                                decimal hargalempeng = reader.GetDecimal(9);
                                string labaLempeng = reader.GetString(22);
                                string explayerd = reader.GetDateTime(10).ToString("yyyy-MM-dd");
                                string laba1 = reader.GetString(12);
                                string laba2 = reader.GetString(14);
                                string laba3 = reader.GetString(16);
                                int stokSatuan = reader.GetInt32(17);
                                int stokLempeng = reader.GetInt32(18);
                                int stokButir = reader.GetInt32(19);
                                string stokAkhir = reader.GetString(20);
                                int butirStrip = reader.GetInt32(21);

                                string total_harga = totalHarga.ToString("N0", new CultureInfo("id-ID"));
                                string Modal = modal.ToString("N0", new CultureInfo("id-ID"));

                                string hargaRupiah1 = hargaJual1.ToString("N0", new CultureInfo("id-ID"));
                                string hargaRupiah2 = hargaJual2.ToString("N0", new CultureInfo("id-ID"));
                                string hargaRupiah3 = hargaJual3.ToString("N0", new CultureInfo("id-ID"));

                                string harga_strip = hargalempeng.ToString("N0", new CultureInfo("id-ID"));

                                Image editIcon = Properties.Resources.icons8_info_24px_1;
                                namaBarang = namaBarang.ToUpper();
                                // Menambahkan data ke DataGridView
                                dgv.Rows.Add(kodeBarang, namaBarang, stokAwal, lempengbox, butirStrip, stokSatuan, stokLempeng, stokButir, stokAkhir, total_harga, distributor, explayerd, Modal, hargaRupiah1, hargaRupiah2, hargaRupiah3, harga_strip, editIcon);
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
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        private void updateData()
        {
            string modalText = MODAL.Text;
            modalText = modalText.Replace(".", "");

            string hargaJual1 = HARGAJUAL1.Text;
            hargaJual1 = hargaJual1.Replace(".", "");

            string hargaJual2 = HARGAJUAL2.Text;
            hargaJual2 = hargaJual2.Replace(".", "");

            string hargaJual3 = HARGAJUAL3.Text;
            hargaJual3 = hargaJual3.Replace(".", "");

            string hargaJualLempeng = HARGALEMPENG.Text;
            hargaJualLempeng = hargaJualLempeng.Replace(".", "");

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            string query = "UPDATE `tb_barang_masuk` SET " +
                "`nama_barang` = '" + this.NAMABARANG.Text + "', " +
                "`lempeng_box` = '" + this.LEMPENGBOX.Text + "', " +
                "`butir_lempeng` = '" + this.BUTIRBOX.Text + "', " +
                "`stok_awal` = '" + this.JUMLAHBARANG.Text + "', " +
                "`stok_satuan` = '" + this.JUMLAHBARANG.Text + "', " +
                "`distributor` = '" + this.DISTRIBUTOR.Text + "', " +
                "`stok_satuan` = '" + this.stokSatuan.Text + "', " +
                "`stok_lempeng` = '" + this.stokStrip.Text + "', " +
                "`stok_butir` = '" + this.stokTablet.Text + "', " +
                "`modal` = '" + modalText + "', " +
                "`harga_lempeng` = '" + hargaJualLempeng + "', " +
                "`explayerd` = '" + this.EXPLAYERD.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                "`harga_jual1` = '" + hargaJual1 + "', " +
                "`laba1` = '" + this.LABA1.Text + "', " +
                "`harga_jual2` = '" + hargaJual2 + "', " +
                "`laba2` = '" + this.LABA2.Text + "', " +
                "`harga_jual3` = '" + hargaJual3 + "', " +
                "`laba3` = '" + this.LABA3.Text + "', " +
                "`laba_lempeng` = '" + this.LABALEMPENG.Text + "' " +
                "WHERE `kode_barang` = '" + SelectedID + "'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil Update Barang dengan Kode : " + SelectedID);
                        this.connection.Close();
                        HideTextBox();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex.Message);
                    }
                }
            }
        }

        private void deleteData()
        {
            string msg = "Apakah kamu yakin untuk menghapus data ID " + SelectedID + "";

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "DELETE FROM tb_barang_masuk WHERE kode_barang = '" + SelectedID + "'";


            if (MessageBox.Show(msg, "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Berhasil hapus Barang dengan Kode : " + SelectedID);
                            this.connection.Close();
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("" + ex.Message);
                        }
                    }
                }
            }
        }

        private void InsertData()
        {
            try
            {
                string totalHarga = TOTALHARGA.Text;
                totalHarga = totalHarga.Replace(".", "");

                string modalText = MODAL.Text;
                modalText = modalText.Replace(".", "");

                string hargaJual1 = HARGAJUAL1.Text;
                hargaJual1 = hargaJual1.Replace(".", "");

                string hargaJual2 = HARGAJUAL2.Text;
                hargaJual2 = hargaJual2.Replace(".", "");

                string hargaJual3 = HARGAJUAL3.Text;
                hargaJual3 = hargaJual3.Replace(".", "");

                string hargaJualLempeng = HARGALEMPENG.Text;
                hargaJualLempeng = hargaJualLempeng.Replace(".", "");
                decimal qtyLempeng = LEMPENGBOX.Value;

                decimal qtyButir = BUTIRBOX.Value;

                string kueri = "INSERT INTO `tb_barang_masuk`(`nama_barang`, `lempeng_box`, `butir_lempeng`, `stok_awal`, `stok_satuan`, `stok_akhir`, `total_harga`, `distributor`, `modal`, `harga_lempeng`, `explayerd`, `harga_jual1`, `laba1`, `harga_jual2`, `laba2`, `harga_jual3`, `laba3`, `laba_lempeng`) VALUES ('" + this.NAMABARANG.Text + "','" + this.LEMPENGBOX.Text + "','" + this.BUTIRBOX.Text + "','" + this.JUMLAHBARANG.Text + "','" + this.JUMLAHBARANG.Text + "','" + this.JUMLAHBARANG.Text + "','" + totalHarga + "','" + this.DISTRIBUTOR.Text + "','" + modalText + "','" + hargaJualLempeng + "','" + this.EXPLAYERD.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + hargaJual1 + "','" + this.LABA1.Text + "','" + hargaJual2 + "','" + this.LABA2.Text + "','" + hargaJual3 + "','" + this.LABA3.Text + "','" + this.LABALEMPENG.Text + "')";

                if (this.connection.State == ConnectionState.Closed)
                    this.connection.Open();

                MySqlCommand cmd = new MySqlCommand(kueri, connection);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                MessageBox.Show("Succesfully Added");
                this.connection.Close();
                HideTextBox();
                LoadData();
            }
            catch (Exception ex)
            {
                // Tangani kesalahan di sini
                MessageBox.Show("Terjadi kesalahan: (312)" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NAMABARANG.Text) ||
                string.IsNullOrEmpty(JUMLAHBARANG.Text) ||
                string.IsNullOrEmpty(TOTALHARGA.Text) ||
                string.IsNullOrEmpty(DISTRIBUTOR.Text) ||
                string.IsNullOrEmpty(MODAL.Text) ||
                string.IsNullOrEmpty(HARGAJUAL1.Text) ||
                string.IsNullOrEmpty(LABA1.Text))
            {
                MessageBox.Show("Semua kolom harus diisi.");
            }
            else
            {
                if (SaveSection == SaveSectionEnum.Insert)
                {
                    InsertData();
                }
                else if (SaveSection == SaveSectionEnum.Update)
                {
                    updateData();
                }
            }
            
        }

        private void JUMLAHBARANG_ValueChanged(object sender, EventArgs e)
        {
            string modalText = MODAL.Text;

            int jumlahBarang = Convert.ToInt32(JUMLAHBARANG.Value);

            decimal totalHarga = 0;

            if (decimal.TryParse(modalText, out decimal modal))
            {
                totalHarga = modal * jumlahBarang;
            }

            TOTALHARGA.Text = totalHarga.ToString();
        }

        private void LABA2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void LABA3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void LABA1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void EXPLAYERD_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SATUANBARANG_SelectedValueChanged(object sender, EventArgs e)
        {
            /*if (SATUANBARANG.SelectedItem != null && SATUANBARANG.SelectedItem.ToString() == "Box")
            {
                LEMPENGBOX.Enabled = true;
                HARGALEMPENG.Enabled = true;

                BUTIRBOX.Enabled = true;              
                LABALEMPENG.Enabled = true;                
            }
            else
            {
                LEMPENGBOX.Enabled = false;
                HARGALEMPENG.Enabled = false;

                BUTIRBOX.Enabled = false;
                LABALEMPENG.Enabled = false;
            }*/
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            ClearString();
            groupBox2.Visible = true;
            SaveSection = SaveSectionEnum.Insert;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (SaveSection != SaveSectionEnum.Insert)
                {
                    DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                    string modalText = row.Cells["Column5"].Value.ToString();
                    modalText = modalText.Replace(".", "");

                    string hargaJual1 = row.Cells["Column11"].Value.ToString();
                    hargaJual1 = hargaJual1.Replace(".", "");

                    string hargaJual2 = row.Cells["Column13"].Value.ToString();
                    hargaJual2 = hargaJual2.Replace(".", "");

                    string hargaJual3 = row.Cells["Column15"].Value.ToString();
                    hargaJual3 = hargaJual3.Replace(".", "");

                    string hargaJualLempeng = row.Cells["Column18"].Value.ToString();
                    hargaJualLempeng = hargaJualLempeng.Replace(".", "");

                    SelectedID = row.Cells["Column1"].Value.ToString();
                    NAMABARANG.Text = row.Cells["Column2"].Value.ToString();
                    MODAL.Text = modalText;
                    JUMLAHBARANG.Text = row.Cells["Column6"].Value.ToString();
                    DISTRIBUTOR.Text = row.Cells["Column8"].Value.ToString();
                    HARGAJUAL1.Text = hargaJual1;
                    HARGAJUAL2.Text = hargaJual2;
                    HARGAJUAL3.Text = hargaJual3;
                    LEMPENGBOX.Text = row.Cells["Column17"].Value.ToString();
                    BUTIRBOX.Text = row.Cells["Column14"].Value.ToString();
                    HARGALEMPENG.Text = hargaJualLempeng;
                    //EXPLAYERD.Text = row.Cells["Column9"].Value.ToString();
                    stokSatuan.Text = row.Cells["Column16"].Value.ToString();
                    stokStrip.Text = row.Cells["Column19"].Value.ToString();
                    stokTablet.Text = row.Cells["Column20"].Value.ToString();

                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnInsert.Enabled = false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HideTextBox();
            SaveSection = SaveSectionEnum.None;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            gBstok.Visible = true;
            SaveSection = SaveSectionEnum.Update;
        }
        private void SEARCH_TextChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["column12"].Index && e.RowIndex >= 0)
            {
                int kodeBarang = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["column1"].Value);
                FrmPenjualanPerObat frmDetail = new FrmPenjualanPerObat();
                frmDetail.kodeBarang = kodeBarang;
                frmDetail.ShowDialog();
            }
        }

        private void LABALEMPENG_TextChanged(object sender, EventArgs e)
        {
            if(LEMPENGBOX.Value != 0 && MODAL.Text != "")
            {
                string modalText = MODAL.Text;
                decimal lempengPerBox = LEMPENGBOX.Value;
                string labaText = LABALEMPENG.Text;

                if (decimal.TryParse(modalText, out decimal modal) && decimal.TryParse(labaText, out decimal laba))
                {
                    // Menghitung harga lempeng dari modal dibagi lempengperbox dan labalempeng
                    decimal hargaLempeng = (modal / lempengPerBox) + ((modal / lempengPerBox) * laba / 100);
                    HARGALEMPENG.Text = hargaLempeng.ToString();
                }
            }
        }

        private void LEMPENGBOX_ValueChanged(object sender, EventArgs e)
        {
            if (LEMPENGBOX.Value != 0 && MODAL.Text != "")
            {
                string modalText = MODAL.Text;
                decimal lempengPerBox = LEMPENGBOX.Value;
                string labaText = LABALEMPENG.Text;

                if (decimal.TryParse(modalText, out decimal modal) && decimal.TryParse(labaText, out decimal laba))
                {
                    // Menghitung harga lempeng dari modal dibagi lempengperbox dan labalempeng
                    decimal hargaLempeng = (modal / lempengPerBox) + ((modal / lempengPerBox) * laba / 100);
                    HARGALEMPENG.Text = hargaLempeng.ToString();
                }
            }
        }

        private void BUTIRBOX_ValueChanged(object sender, EventArgs e)
        {/*
            if(HARGALEMPENG.Text != "" && BUTIRBOX.Value != 0)
            {
                string modalText = MODAL.Text;
                decimal lempengPerBox = LEMPENGBOX.Value;

                string hargaLempeng = HARGALEMPENG.Text;
                decimal butirPerLempeng = BUTIRBOX.Value;
                string labaText = LABABUTIR.Text;

                if (decimal.TryParse(modalText, out decimal modal) && decimal.TryParse(labaText, out decimal laba) && decimal.TryParse(hargaLempeng, out decimal modalLempeng))
                {
                    decimal harga_lempeng = (modal / lempengPerBox);
                    decimal hargaButir = (harga_lempeng / butirPerLempeng) + ((harga_lempeng / butirPerLempeng) * laba / 100);
                    HARGABUTIR.Text = hargaButir.ToString();
                }
            }*/
        }

        private void LABABUTIR_TextChanged(object sender, EventArgs e)
        {/*
            if (HARGALEMPENG.Text != "" && BUTIRBOX.Value != 0)
            {
                string modalText = MODAL.Text;
                decimal lempengPerBox = LEMPENGBOX.Value;

                string hargaLempeng = HARGALEMPENG.Text;
                decimal butirPerLempeng = BUTIRBOX.Value;
                string labaText = LABABUTIR.Text;

                if (decimal.TryParse(modalText, out decimal modal) && decimal.TryParse(labaText, out decimal laba) && decimal.TryParse(hargaLempeng, out decimal modalLempeng))
                {
                    decimal harga_lempeng = (modal / lempengPerBox);
                    decimal hargaButir = (harga_lempeng / butirPerLempeng) + ((harga_lempeng / butirPerLempeng) * laba / 100);
                    HARGABUTIR.Text = hargaButir.ToString();
                }
            }*/
        }

        private void HARGAJUAL1_TextChanged(object sender, EventArgs e)
        {
            if (HARGAJUAL1.Text != "0" && MODAL.Text != "0")
            {
                string modalText = MODAL.Text;
                string hargaJual1Text = HARGAJUAL1.Text;
                int jumlahBarang = Convert.ToInt32(JUMLAHBARANG.Text);

                decimal totalHarga = 0;

                if (decimal.TryParse(modalText, out decimal modal) && decimal.TryParse(hargaJual1Text, out decimal hargaJual1))
                {
                    totalHarga = modal * jumlahBarang;
                    TOTALHARGA.Text = totalHarga.ToString();

                    // Menghitung laba1 dari modal dan hargaJual1
                    decimal laba1 = (hargaJual1 - modal) / modal * 100;

                    // Menampilkan laba1
                    LABA1.Text = laba1.ToString("N0");
                }
            }
        }

        private void HARGAJUAL2_TextChanged(object sender, EventArgs e)
        {
            if (HARGAJUAL2.Text != "0" && MODAL.Text != "0")
            {
                string modalText = MODAL.Text;
                string hargaJual2Text = HARGAJUAL2.Text;
                int jumlahBarang = Convert.ToInt32(JUMLAHBARANG.Text);

                decimal totalHarga = 0;

                if (decimal.TryParse(modalText, out decimal modal) && decimal.TryParse(hargaJual2Text, out decimal hargaJual2))
                {
                    totalHarga = modal * jumlahBarang;
                    TOTALHARGA.Text = totalHarga.ToString();

                    // Menghitung laba2 dari modal dan hargaJual2
                    decimal laba2 = (hargaJual2 - modal) / modal * 100;

                    // Menampilkan laba2
                    LABA2.Text = laba2.ToString("N0");
                }
            }
        }

        private void HARGAJUAL3_TextChanged(object sender, EventArgs e)
        {
            if (HARGAJUAL3.Text != "0" && MODAL.Text != "0")
            {
                string modalText = MODAL.Text;
                string hargaJual3Text = HARGAJUAL3.Text;
                int jumlahBarang = Convert.ToInt32(JUMLAHBARANG.Text);

                decimal totalHarga = 0;

                if (decimal.TryParse(modalText, out decimal modal) && decimal.TryParse(hargaJual3Text, out decimal hargaJual3))
                {
                    totalHarga = modal * jumlahBarang;
                    TOTALHARGA.Text = totalHarga.ToString();

                    /*
                    // Menghitung laba2 dari modal dan hargaJual2
                    decimal laba3 = (hargaJual3 - modal) / modal * 100;

                    // Menampilkan laba2
                    LABA3.Text = laba3.ToString("N0");*/
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteData();
        }

        private void MODAL_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateStok_Click(object sender, EventArgs e)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            string query = "UPDATE `tb_barang_masuk` SET " +
                "`stok_satuan` = '" + this.stokSatuan.Text + "', " +
                "`stok_lempeng` = '" + this.stokStrip.Text + "', " +
                "`stok_butir` = '" + this.stokTablet.Text + "' " +
                "WHERE `kode_barang` = '" + SelectedID + "'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil Update Stok Barang dengan Kode : " + SelectedID);
                        this.connection.Close();
                        HideTextBox();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex.Message);
                    }
                }
            }
        }

        private void ExportToExcel(DataGridView dgv, string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            FileInfo excelFile = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");

                int colIndex = 1;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        worksheet.Cells[1, colIndex].Value = dgv.Columns[i].HeaderText;
                        colIndex++;
                    }
                }

                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    colIndex = 1;

                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        if (dgv.Columns[col].Visible)
                        {
                            worksheet.Cells[row + 2, colIndex].Value = dgv.Rows[row].Cells[col].Value;
                            colIndex++;
                        }
                    }
                }

                worksheet.Column(1).Width = 5;
                worksheet.Column(2).Width = 10;
                worksheet.Column(3).Width = 14;
                worksheet.Column(4).Width = 35;
                worksheet.Column(5).Width = 5;
                worksheet.Column(6).Width = 12;
                worksheet.Column(7).Width = 13;

                // Membuat tabel dan menerapkan gaya tabel
                var dataRange = worksheet.Cells["A1:" + worksheet.Cells[worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].Address];
                var tbl = worksheet.Tables.Add(dataRange, "MyTable");
                tbl.TableStyle = TableStyles.Medium2;

                package.Save();
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save as Excel File";
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                ExportToExcel(dgv, filePath);
                MessageBox.Show("Data berhasil diekspor ke Excel.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
