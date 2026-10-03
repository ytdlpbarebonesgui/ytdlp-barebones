namespace ytdlp_barebones
{
    partial class frmConfigManager
    {
        
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        
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


        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbloadconfig = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            txtname = new TextBox();
            txtytdlpcommand = new TextBox();
            btnupdate = new Button();
            btndownload2config = new Button();
            btndelete = new Button();
            cmbconfigcategory = new ComboBox();
            cmbeditconfigcategory = new ComboBox();
            SuspendLayout();
            // 
            // cmbloadconfig
            // 
            cmbloadconfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbloadconfig.BackColor = Color.FromArgb(64, 64, 64);
            cmbloadconfig.Cursor = Cursors.Hand;
            cmbloadconfig.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbloadconfig.FlatStyle = FlatStyle.Flat;
            cmbloadconfig.ForeColor = Color.White;
            cmbloadconfig.FormattingEnabled = true;
            cmbloadconfig.Location = new Point(320, 12);
            cmbloadconfig.Name = "cmbloadconfig";
            cmbloadconfig.Size = new Size(450, 33);
            cmbloadconfig.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(131, 25);
            label1.TabIndex = 1;
            label1.Text = "Select a config:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 55);
            label2.Name = "label2";
            label2.Size = new Size(98, 25);
            label2.TabIndex = 2;
            label2.Text = "Change to:";
            // 
            // txtname
            // 
            txtname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtname.BackColor = Color.Black;
            txtname.BorderStyle = BorderStyle.FixedSingle;
            txtname.ForeColor = Color.White;
            txtname.Location = new Point(320, 53);
            txtname.Name = "txtname";
            txtname.Size = new Size(450, 31);
            txtname.TabIndex = 3;
            // 
            // txtytdlpcommand
            // 
            txtytdlpcommand.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtytdlpcommand.BackColor = Color.FromArgb(64, 64, 64);
            txtytdlpcommand.BorderStyle = BorderStyle.None;
            txtytdlpcommand.ForeColor = Color.White;
            txtytdlpcommand.Location = new Point(12, 90);
            txtytdlpcommand.Multiline = true;
            txtytdlpcommand.Name = "txtytdlpcommand";
            txtytdlpcommand.ScrollBars = ScrollBars.Vertical;
            txtytdlpcommand.Size = new Size(758, 165);
            txtytdlpcommand.TabIndex = 4;
            // 
            // btnupdate
            // 
            btnupdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnupdate.Cursor = Cursors.Hand;
            btnupdate.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnupdate.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnupdate.FlatStyle = FlatStyle.Flat;
            btnupdate.Image = Properties.Resources.save;
            btnupdate.Location = new Point(524, 261);
            btnupdate.Name = "btnupdate";
            btnupdate.Size = new Size(120, 40);
            btnupdate.TabIndex = 5;
            btnupdate.Text = "&Update";
            btnupdate.TextAlign = ContentAlignment.MiddleRight;
            btnupdate.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnupdate.UseVisualStyleBackColor = true;
            // 
            // btndownload2config
            // 
            btndownload2config.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btndownload2config.Cursor = Cursors.Hand;
            btndownload2config.FlatAppearance.MouseDownBackColor = Color.Gray;
            btndownload2config.FlatAppearance.MouseOverBackColor = Color.Silver;
            btndownload2config.FlatStyle = FlatStyle.Flat;
            btndownload2config.Image = Properties.Resources.circle;
            btndownload2config.Location = new Point(398, 261);
            btndownload2config.Name = "btndownload2config";
            btndownload2config.Size = new Size(120, 40);
            btndownload2config.TabIndex = 6;
            btndownload2config.Text = "&Download";
            btndownload2config.TextAlign = ContentAlignment.MiddleRight;
            btndownload2config.TextImageRelation = TextImageRelation.ImageBeforeText;
            btndownload2config.UseVisualStyleBackColor = true;
            // 
            // btndelete
            // 
            btndelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btndelete.Cursor = Cursors.Hand;
            btndelete.FlatAppearance.MouseDownBackColor = Color.Gray;
            btndelete.FlatAppearance.MouseOverBackColor = Color.Silver;
            btndelete.FlatStyle = FlatStyle.Flat;
            btndelete.Image = Properties.Resources.close;
            btndelete.Location = new Point(650, 261);
            btndelete.Name = "btndelete";
            btndelete.Size = new Size(120, 40);
            btndelete.TabIndex = 7;
            btndelete.Text = "&Delete";
            btndelete.TextAlign = ContentAlignment.MiddleRight;
            btndelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btndelete.UseVisualStyleBackColor = true;
            // 
            // cmbconfigcategory
            // 
            cmbconfigcategory.BackColor = Color.FromArgb(64, 64, 64);
            cmbconfigcategory.Cursor = Cursors.Hand;
            cmbconfigcategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbconfigcategory.FlatStyle = FlatStyle.Flat;
            cmbconfigcategory.ForeColor = Color.White;
            cmbconfigcategory.FormattingEnabled = true;
            cmbconfigcategory.Location = new Point(149, 12);
            cmbconfigcategory.Name = "cmbconfigcategory";
            cmbconfigcategory.Size = new Size(165, 33);
            cmbconfigcategory.TabIndex = 9;
            // 
            // cmbeditconfigcategory
            // 
            cmbeditconfigcategory.BackColor = Color.Black;
            cmbeditconfigcategory.Cursor = Cursors.Hand;
            cmbeditconfigcategory.FlatStyle = FlatStyle.Flat;
            cmbeditconfigcategory.ForeColor = Color.White;
            cmbeditconfigcategory.FormattingEnabled = true;
            cmbeditconfigcategory.Location = new Point(149, 51);
            cmbeditconfigcategory.Name = "cmbeditconfigcategory";
            cmbeditconfigcategory.Size = new Size(165, 33);
            cmbeditconfigcategory.TabIndex = 10;
            // 
            // frmConfigManager
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            ClientSize = new Size(782, 313);
            Controls.Add(cmbeditconfigcategory);
            Controls.Add(cmbconfigcategory);
            Controls.Add(btndelete);
            Controls.Add(btndownload2config);
            Controls.Add(btnupdate);
            Controls.Add(txtytdlpcommand);
            Controls.Add(txtname);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbloadconfig);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmConfigManager";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuration Manager";
            Load += frmConfigManager_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbloadconfig;
        private Label label1;
        private Label label2;
        private TextBox txtname;
        private TextBox txtytdlpcommand;
        private Button btnupdate;
        private Button btndownload2config;
        private Button btndelete;
        private ComboBox cmbconfigcategory;
        private ComboBox cmbeditconfigcategory;
    }
}