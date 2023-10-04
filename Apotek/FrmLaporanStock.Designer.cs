
namespace Apotek
{
    partial class FrmLaporanStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dtBarangMasukBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsDetailBarangMasuk = new Apotek.dsDetailBarangMasuk();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataCart = new Apotek.dataCart();
            this.tb_barang_keluarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtBarangMasukBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetailBarangMasuk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_barang_keluarBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtBarangMasukBindingSource
            // 
            this.dtBarangMasukBindingSource.DataMember = "dtBarangMasuk";
            this.dtBarangMasukBindingSource.DataSource = this.dsDetailBarangMasuk;
            // 
            // dsDetailBarangMasuk
            // 
            this.dsDetailBarangMasuk.DataSetName = "dsDetailBarangMasuk";
            this.dsDetailBarangMasuk.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.dtBarangMasukBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Apotek.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(259, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(413, 432);
            this.reportViewer1.TabIndex = 0;
            // 
            // dataCart
            // 
            this.dataCart.DataSetName = "dataCart";
            this.dataCart.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tb_barang_keluarBindingSource
            // 
            this.tb_barang_keluarBindingSource.DataMember = "tb_barang_keluar";
            this.tb_barang_keluarBindingSource.DataSource = this.dataCart;
            // 
            // FrmLaporanStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 698);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLaporanStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLaporanStock";
            this.Load += new System.EventHandler(this.FrmLaporanStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtBarangMasukBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetailBarangMasuk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_barang_keluarBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dtBarangMasukBindingSource;
        private dsDetailBarangMasuk dsDetailBarangMasuk;
        private System.Windows.Forms.BindingSource tb_barang_keluarBindingSource;
        private dataCart dataCart;
    }
}