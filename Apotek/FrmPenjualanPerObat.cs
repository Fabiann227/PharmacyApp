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
    public partial class FrmPenjualanPerObat : Form
    {
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;

        public int kodeBarang { get; set; }

        dsDetailBarangMasuk dataSet1 = new dsDetailBarangMasuk();

        public FrmPenjualanPerObat()
        {
            InitializeComponent();
        }

        private void FrmPenjualanPerObat_Load(object sender, EventArgs e)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT tbm.kode_barang, tbm.nama_barang, tbm.modal, tbm.stok_awal, tbm.stok_akhir, tbm.total_harga, SUM(tbk.subtotal) AS total_akhir, tbm.harga_jual1, tbm.harga_jual2, tbm.harga_jual3, tbk.total_keuntungan, tbk.total_barang, tbk.qty, tbk.qty_lempeng, tbk.qty_butir FROM tb_barang_masuk AS tbm JOIN tb_barang_keluar AS tbk ON tbm.kode_barang = tbk.kode_barang WHERE tbm.kode_barang = '"+ kodeBarang +"' GROUP BY tbm.kode_barang";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataSet1, "dtBarangMasuk");
                }
            }

            ReportDataSource reportDataSource = new ReportDataSource("DataSet2", dataSet1.Tables["dtBarangMasuk"]); // Ganti DataSet1 dengan nama dataset dalam ReportViewer
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            // Setel path ke file RDLC Anda (misalnya: YourReport.rdlc)
            reportViewer1.LocalReport.ReportPath = $"{Application.StartupPath}/Report1.rdlc";

            this.reportViewer1.RefreshReport();
        }
    }
}
