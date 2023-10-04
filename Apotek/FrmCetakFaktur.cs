using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;

namespace Apotek
{
    public partial class FrmCetakFaktur : Form
    {
        string server = DatabaseConfig.Instance.Server;
        string database = DatabaseConfig.Instance.DatabaseName;
        string uid = DatabaseConfig.Instance.UserId;
        string password = DatabaseConfig.Instance.Password;

        public string NoFaktur { get; set; }
        public string Subtotal { get; set; }

        public FrmCetakFaktur()
        {
            InitializeComponent();
        }

        dataCart dataCart = new dataCart();
        
        private void FrmCetakFaktur_Load(object sender, EventArgs e)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            string query = "SELECT *, CASE WHEN qty != 0 THEN harga ELSE harga END AS harga_total, CASE WHEN qty != 0 THEN qty WHEN qty_butir != 0 THEN qty_butir ELSE qty_lempeng END AS qty_total, CASE WHEN qty != 0 THEN satuan WHEN qty_lempeng != 0 THEN CASE WHEN satuan = 'Kotak' THEN 'Pcs' ELSE 'Lempeng' END WHEN qty_butir != 0 THEN 'Tablet' END AS satuan_akhir FROM tb_barang_keluar WHERE no_faktur = '" + NoFaktur + "'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataCart, "tb_barang_keluar"); 
                }
            }

            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dataCart.Tables["tb_barang_keluar"]); // Ganti DataSet1 dengan nama dataset dalam ReportViewer
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            // Setel path ke file RDLC Anda (misalnya: YourReport.rdlc)
            reportViewer1.LocalReport.ReportPath = $"{Application.StartupPath}/CetakFaktur.rdlc";

            reportViewer1.SetPageSettings(new System.Drawing.Printing.PageSettings
            {
                Landscape = false,
                Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
            });

            reportViewer1.ZoomMode = ZoomMode.PageWidth;
            reportViewer1.ZoomPercent = 75;

            this.reportViewer1.RefreshReport();
        }
    }
}
