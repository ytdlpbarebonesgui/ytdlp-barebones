namespace ytdlp_barebones
{
    partial class frmSaveConfig
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
            groupBox1 = new GroupBox();
            cmbeditconfigcategory = new ComboBox();
            txtname = new TextBox();
            txtytdlpcommand = new TextBox();
            btnsave = new Button();
            btncancel = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbeditconfigcategory);
            groupBox1.Controls.Add(txtname);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(578, 69);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Set-up a category (optional) and name for your new configuration:";
            // 
            // cmbeditconfigcategory
            // 
            cmbeditconfigcategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbeditconfigcategory.BackColor = Color.FromArgb(64, 64, 64);
            cmbeditconfigcategory.Cursor = Cursors.Hand;
            cmbeditconfigcategory.FlatStyle = FlatStyle.Flat;
            cmbeditconfigcategory.ForeColor = Color.Black;
            cmbeditconfigcategory.FormattingEnabled = true;
            cmbeditconfigcategory.Location = new Point(6, 30);
            cmbeditconfigcategory.Name = "cmbeditconfigcategory";
            cmbeditconfigcategory.Size = new Size(117, 33);
            cmbeditconfigcategory.TabIndex = 11;
            // 
            // txtname
            // 
            txtname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtname.BackColor = Color.FromArgb(64, 64, 64);
            txtname.BorderStyle = BorderStyle.FixedSingle;
            txtname.ForeColor = Color.White;
            txtname.Location = new Point(129, 31);
            txtname.Name = "txtname";
            txtname.Size = new Size(443, 31);
            txtname.TabIndex = 0;
            // 
            // txtytdlpcommand
            // 
            txtytdlpcommand.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtytdlpcommand.BackColor = Color.FromArgb(64, 64, 64);
            txtytdlpcommand.BorderStyle = BorderStyle.FixedSingle;
            txtytdlpcommand.ForeColor = Color.White;
            txtytdlpcommand.Location = new Point(12, 87);
            txtytdlpcommand.Multiline = true;
            txtytdlpcommand.Name = "txtytdlpcommand";
            txtytdlpcommand.ScrollBars = ScrollBars.Vertical;
            txtytdlpcommand.Size = new Size(578, 174);
            txtytdlpcommand.TabIndex = 1;
            // 
            // btnsave
            // 
            btnsave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnsave.Cursor = Cursors.Hand;
            btnsave.Enabled = false;
            btnsave.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnsave.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnsave.FlatStyle = FlatStyle.Flat;
            btnsave.ForeColor = Color.White;
            btnsave.Image = Properties.Resources.save;
            btnsave.Location = new Point(344, 267);
            btnsave.Name = "btnsave";
            btnsave.Size = new Size(120, 40);
            btnsave.TabIndex = 2;
            btnsave.Text = "&Save";
            btnsave.TextAlign = ContentAlignment.MiddleRight;
            btnsave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnsave.UseVisualStyleBackColor = true;
            // 
            // btncancel
            // 
            btncancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btncancel.Cursor = Cursors.Hand;
            btncancel.FlatAppearance.MouseDownBackColor = Color.Gray;
            btncancel.FlatAppearance.MouseOverBackColor = Color.Silver;
            btncancel.FlatStyle = FlatStyle.Flat;
            btncancel.ForeColor = Color.White;
            btncancel.Image = Properties.Resources.close;
            btncancel.Location = new Point(470, 267);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(120, 40);
            btncancel.TabIndex = 3;
            btncancel.Text = "&Cancel";
            btncancel.TextAlign = ContentAlignment.MiddleRight;
            btncancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btncancel.UseVisualStyleBackColor = true;
            // 
            // frmSaveConfig
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            ClientSize = new Size(602, 313);
            Controls.Add(btncancel);
            Controls.Add(btnsave);
            Controls.Add(txtytdlpcommand);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSaveConfig";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Save New Configuration";
            Load += frmSaveConfig_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtname;
        private TextBox txtytdlpcommand;
        private Button btnsave;
        private Button btncancel;
        private ComboBox cmbeditconfigcategory;
    }
}