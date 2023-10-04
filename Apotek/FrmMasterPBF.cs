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
    public partial class FrmMasterPBF : Form
    {
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
        private void ClearString()
        {
            NAMA.Text = "";
            ALAMAT.Text = "";
        }
        private void HideTextBox()
        {
            groupBox2.Visible = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            ClearString();
        }
        public FrmMasterPBF()
        {
            InitializeComponent();
        }
        private void FrmMasterPBF_Load(object sender, EventArgs e)
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
                        MessageBox.Show("Terjadi kesalahan (Pilih obat - 1): " + ex.Message);
                    }
                }
            }
        }
        private void updateData()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            string query = "UPDATE `tb_supplier` SET " +
                "`nama` = '" + this.NAMA.Text + "', " +
                "`alamat` = '" + this.ALAMAT.Text + "' " +
                "WHERE `id` = '" + SelectedID + "'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil Update data dengan Kode : " + SelectedID);
                        connection.Close();
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
            string query = "DELETE FROM tb_supplier WHERE id = '" + SelectedID + "'";


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
                            MessageBox.Show("Berhasil hapus data dengan Kode : " + SelectedID);
                            connection.Close();
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
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string kueri = "INSERT INTO `tb_supplier`(`nama`, `alamat`) VALUES ('" + this.NAMA.Text + "', '" + this.ALAMAT.Text + "')";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(kueri, connection))
                {
                    connection.Open();
                    try
                    {
                        MySqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        MessageBox.Show("Succesfully Added");
                        connection.Close();
                        HideTextBox();
                        LoadData();
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            ClearString();
            groupBox2.Visible = true;
            SaveSection = SaveSectionEnum.Insert;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            SaveSection = SaveSectionEnum.Update;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NAMA.Text) ||
                string.IsNullOrEmpty(ALAMAT.Text))
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HideTextBox();
            SaveSection = SaveSectionEnum.None;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (SaveSection != SaveSectionEnum.Insert)
                {
                    DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                    SelectedID = row.Cells["Column1"].Value.ToString();
                    NAMA.Text = row.Cells["Column2"].Value.ToString();
                    ALAMAT.Text = row.Cells["Column3"].Value.ToString();

                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnInsert.Enabled = false;
                }
            }
        }
    }
}
