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
    public partial class LapAkhirPenjualan : Form
    {
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;

        dsLapAkhirPenjualan dataSet1 = new dsLapAkhirPenjualan();
        public LapAkhirPenjualan()
        {
            InitializeComponent();
        }

        private void LapAkhirPenjualan_Load(object sender, EventArgs e)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT DISTINCT no_faktur, tgl, nama_pelanggan, alamat, total_barang, total_harga, tgl_peng, total_keuntungan FROM tb_barang_keluar";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataSet1, "dtLapAkhirPenjualan");
                }
            }

            ReportDataSource reportDataSource = new ReportDataSource("DataSet3", dataSet1.Tables["dtLapAkhirPenjualan"]); // Ganti DataSet1 dengan nama dataset dalam ReportViewer
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            // Setel path ke file RDLC Anda (misalnya: YourReport.rdlc)
            reportViewer1.LocalReport.ReportPath = $"{Application.StartupPath}/LapAkhirPenjualan.rdlc";

            this.reportViewer1.RefreshReport();
        }
    }
}
