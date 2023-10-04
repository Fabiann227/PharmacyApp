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
using Microsoft.Reporting.WinForms;

namespace Apotek
{
    public partial class FrmLaporanStock : Form
    {
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;
        public FrmLaporanStock()
        {
            InitializeComponent();
        }

        dsDetailBarangMasuk dataSet1 = new dsDetailBarangMasuk();

        private void FrmLaporanStock_Load(object sender, EventArgs e)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT tbm.kode_barang, tbm.nama_barang, tbm.modal, tbm.stok_awal, tbm.stok_akhir, tbm.total_harga, SUM(tbk.total_harga) AS total_akhir, tbm.harga_jual1, tbm.harga_jual2, tbm.harga_jual3 FROM tb_barang_masuk AS tbm JOIN tb_barang_keluar AS tbk ON tbm.kode_barang = tbk.kode_barang GROUP BY tbm.kode_barang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataSet1, "dtBarangMasuk");
                }
            }

            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dataSet1.Tables["dtBarangMasuk"]); // Ganti DataSet1 dengan nama dataset dalam ReportViewer
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            // Setel path ke file RDLC Anda (misalnya: YourReport.rdlc)
            reportViewer1.LocalReport.ReportPath = $"{Application.StartupPath}/DetailBarangMasuk.rdlc";

            this.reportViewer1.RefreshReport();
        }
    }
}
