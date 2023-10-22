
namespace Apotek
{
    partial class FrmLaporan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv = new Guna.UI2.WinForms.Guna2DataGridView();
            this.STARTDATE = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.ENDDATE = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lbl_TOTALBARANG = new System.Windows.Forms.Label();
            this.lbl_TGL = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_TOTALKEUNTUNGAN = new System.Windows.Forms.Label();
            this.lbl_TOTALPENJUALAN = new System.Windows.Forms.Label();
            this.PRINTALL = new Guna.UI2.WinForms.Guna2Button();
            this.SEARCH = new Guna.UI2.WinForms.Guna2TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewImageColumn();
            this.DeleteColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(971, 547);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeight = 35;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column9,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column8,
            this.Column6,
            this.Column7,
            this.Column10,
            this.DeleteColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv.Location = new System.Drawing.Point(3, 16);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 30;
            this.dgv.Size = new System.Drawing.Size(965, 528);
            this.dgv.TabIndex = 0;
            this.dgv.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(56)))));
            this.dgv.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgv.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv.ThemeStyle.HeaderStyle.Height = 35;
            this.dgv.ThemeStyle.ReadOnly = true;
            this.dgv.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv.ThemeStyle.RowsStyle.Height = 30;
            this.dgv.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // STARTDATE
            // 
            this.STARTDATE.Checked = true;
            this.STARTDATE.CustomFormat = "yyyy MMM dd";
            this.STARTDATE.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.STARTDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.STARTDATE.Location = new System.Drawing.Point(15, 17);
            this.STARTDATE.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.STARTDATE.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.STARTDATE.Name = "STARTDATE";
            this.STARTDATE.Size = new System.Drawing.Size(121, 30);
            this.STARTDATE.TabIndex = 3;
            this.STARTDATE.Value = new System.DateTime(2023, 9, 1, 11, 34, 0, 0);
            this.STARTDATE.ValueChanged += new System.EventHandler(this.STARTDATE_ValueChanged);
            // 
            // ENDDATE
            // 
            this.ENDDATE.Checked = true;
            this.ENDDATE.CustomFormat = "yyyy MMM dd";
            this.ENDDATE.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ENDDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ENDDATE.Location = new System.Drawing.Point(166, 17);
            this.ENDDATE.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.ENDDATE.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.ENDDATE.Name = "ENDDATE";
            this.ENDDATE.Size = new System.Drawing.Size(121, 30);
            this.ENDDATE.TabIndex = 3;
            this.ENDDATE.Value = new System.DateTime(2023, 9, 30, 11, 34, 0, 0);
            this.ENDDATE.ValueChanged += new System.EventHandler(this.ENDDATE_ValueChanged);
            // 
            // lbl_TOTALBARANG
            // 
            this.lbl_TOTALBARANG.AutoSize = true;
            this.lbl_TOTALBARANG.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TOTALBARANG.Location = new System.Drawing.Point(140, 17);
            this.lbl_TOTALBARANG.Name = "lbl_TOTALBARANG";
            this.lbl_TOTALBARANG.Size = new System.Drawing.Size(22, 25);
            this.lbl_TOTALBARANG.TabIndex = 11;
            this.lbl_TOTALBARANG.Text = "-";
            this.lbl_TOTALBARANG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_TGL
            // 
            this.lbl_TGL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TGL.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TGL.Location = new System.Drawing.Point(9, 599);
            this.lbl_TGL.Name = "lbl_TGL";
            this.lbl_TGL.Size = new System.Drawing.Size(531, 32);
            this.lbl_TGL.TabIndex = 12;
            this.lbl_TGL.Text = "Penjualan dari 01 September 2023 - 30 September 2023";
            this.lbl_TGL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 631);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Total Penjualan";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 656);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "Total Keuntungan";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(262, 631);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = ":";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(262, 656);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = ":";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Visible = false;
            // 
            // lbl_TOTALKEUNTUNGAN
            // 
            this.lbl_TOTALKEUNTUNGAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TOTALKEUNTUNGAN.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TOTALKEUNTUNGAN.Location = new System.Drawing.Point(271, 656);
            this.lbl_TOTALKEUNTUNGAN.Name = "lbl_TOTALKEUNTUNGAN";
            this.lbl_TOTALKEUNTUNGAN.Size = new System.Drawing.Size(174, 25);
            this.lbl_TOTALKEUNTUNGAN.TabIndex = 14;
            this.lbl_TOTALKEUNTUNGAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_TOTALKEUNTUNGAN.Visible = false;
            // 
            // lbl_TOTALPENJUALAN
            // 
            this.lbl_TOTALPENJUALAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TOTALPENJUALAN.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TOTALPENJUALAN.Location = new System.Drawing.Point(271, 631);
            this.lbl_TOTALPENJUALAN.Name = "lbl_TOTALPENJUALAN";
            this.lbl_TOTALPENJUALAN.Size = new System.Drawing.Size(187, 25);
            this.lbl_TOTALPENJUALAN.TabIndex = 13;
            this.lbl_TOTALPENJUALAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PRINTALL
            // 
            this.PRINTALL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PRINTALL.BorderRadius = 12;
            this.PRINTALL.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PRINTALL.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PRINTALL.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PRINTALL.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PRINTALL.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(56)))));
            this.PRINTALL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PRINTALL.ForeColor = System.Drawing.Color.White;
            this.PRINTALL.Image = global::Apotek.Properties.Resources.icons8_print_24px;
            this.PRINTALL.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PRINTALL.Location = new System.Drawing.Point(773, 17);
            this.PRINTALL.Name = "PRINTALL";
            this.PRINTALL.Size = new System.Drawing.Size(40, 30);
            this.PRINTALL.TabIndex = 15;
            this.PRINTALL.Click += new System.EventHandler(this.PRINTALL_Click);
            // 
            // SEARCH
            // 
            this.SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SEARCH.BorderRadius = 15;
            this.SEARCH.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SEARCH.DefaultText = "";
            this.SEARCH.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SEARCH.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SEARCH.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SEARCH.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SEARCH.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SEARCH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SEARCH.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SEARCH.IconRight = global::Apotek.Properties.Resources.search;
            this.SEARCH.Location = new System.Drawing.Point(819, 17);
            this.SEARCH.Name = "SEARCH";
            this.SEARCH.PasswordChar = '\0';
            this.SEARCH.PlaceholderText = "Cari Data";
            this.SEARCH.SelectedText = "";
            this.SEARCH.Size = new System.Drawing.Size(164, 30);
            this.SEARCH.TabIndex = 2;
            this.SEARCH.TextChanged += new System.EventHandler(this.SEARCH_TextChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No Faktur";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Pesanan Dibuat";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nama";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Alamat";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Jumlah Barang";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Total Harga";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Keuntungan";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Tgl Pengambilan";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 20F;
            this.Column7.HeaderText = "";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.FillWeight = 20F;
            this.Column10.HeaderText = "";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // DeleteColumn
            // 
            this.DeleteColumn.FillWeight = 20F;
            this.DeleteColumn.HeaderText = "";
            this.DeleteColumn.Name = "DeleteColumn";
            this.DeleteColumn.ReadOnly = true;
            // 
            // FrmLaporan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 698);
            this.Controls.Add(this.PRINTALL);
            this.Controls.Add(this.lbl_TOTALKEUNTUNGAN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_TOTALPENJUALAN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_TGL);
            this.Controls.Add(this.lbl_TOTALBARANG);
            this.Controls.Add(this.ENDDATE);
            this.Controls.Add(this.STARTDATE);
            this.Controls.Add(this.SEARCH);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLaporan";
            this.Text = "FrmLaporan";
            this.Load += new System.EventHandler(this.FrmLaporan_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Guna.UI2.WinForms.Guna2TextBox SEARCH;
        private Guna.UI2.WinForms.Guna2DateTimePicker STARTDATE;
        private Guna.UI2.WinForms.Guna2DateTimePicker ENDDATE;
        private System.Windows.Forms.Label lbl_TOTALBARANG;
        private Guna.UI2.WinForms.Guna2DataGridView dgv;
        private System.Windows.Forms.Label lbl_TGL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_TOTALKEUNTUNGAN;
        private System.Windows.Forms.Label lbl_TOTALPENJUALAN;
        private Guna.UI2.WinForms.Guna2Button PRINTALL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewImageColumn Column7;
        private System.Windows.Forms.DataGridViewImageColumn Column10;
        private System.Windows.Forms.DataGridViewImageColumn DeleteColumn;
    }
}