namespace ytdlp_barebones
{
    partial class frmEditFile
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
            cbedit = new CheckBox();
            txtfilecontent = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtfileandfolder = new TextBox();
            txtrename = new TextBox();
            btnupdate = new Button();
            btncancel = new Button();
            cmbfilefolder = new ComboBox();
            SuspendLayout();
            // 
            // cbedit
            // 
            cbedit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbedit.AutoSize = true;
            cbedit.Cursor = Cursors.Hand;
            cbedit.Location = new Point(12, 382);
            cbedit.Name = "cbedit";
            cbedit.Size = new Size(398, 29);
            cbedit.TabIndex = 12;
            cbedit.Text = "&Edit Content (Not Recommended for Cookies)";
            cbedit.UseVisualStyleBackColor = true;
            // 
            // txtfilecontent
            // 
            txtfilecontent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtfilecontent.BackColor = Color.FromArgb(64, 64, 64);
            txtfilecontent.BorderStyle = BorderStyle.FixedSingle;
            txtfilecontent.Font = new Font("Cascadia Code", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtfilecontent.ForeColor = Color.White;
            txtfilecontent.Location = new Point(12, 88);
            txtfilecontent.Multiline = true;
            txtfilecontent.Name = "txtfilecontent";
            txtfilecontent.PasswordChar = '*';
            txtfilecontent.ReadOnly = true;
            txtfilecontent.ScrollBars = ScrollBars.Both;
            txtfilecontent.Size = new Size(718, 279);
            txtfilecontent.TabIndex = 11;
            txtfilecontent.WordWrap = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 51);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 10;
            label2.Text = "Change To:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 14);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 9;
            label1.Text = "File Name:";
            // 
            // txtfileandfolder
            // 
            txtfileandfolder.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtfileandfolder.BackColor = Color.FromArgb(64, 64, 64);
            txtfileandfolder.BorderStyle = BorderStyle.FixedSingle;
            txtfileandfolder.Enabled = false;
            txtfileandfolder.ForeColor = Color.White;
            txtfileandfolder.Location = new Point(117, 12);
            txtfileandfolder.Name = "txtfileandfolder";
            txtfileandfolder.Size = new Size(613, 31);
            txtfileandfolder.TabIndex = 13;
            // 
            // txtrename
            // 
            txtrename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtrename.BackColor = Color.FromArgb(64, 64, 64);
            txtrename.BorderStyle = BorderStyle.FixedSingle;
            txtrename.ForeColor = Color.White;
            txtrename.Location = new Point(268, 49);
            txtrename.Name = "txtrename";
            txtrename.Size = new Size(462, 31);
            txtrename.TabIndex = 14;
            // 
            // btnupdate
            // 
            btnupdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnupdate.Cursor = Cursors.Hand;
            btnupdate.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnupdate.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnupdate.FlatStyle = FlatStyle.Flat;
            btnupdate.Image = Properties.Resources.save;
            btnupdate.Location = new Point(484, 373);
            btnupdate.Name = "btnupdate";
            btnupdate.Size = new Size(120, 40);
            btnupdate.TabIndex = 15;
            btnupdate.Text = "&Update";
            btnupdate.TextAlign = ContentAlignment.MiddleRight;
            btnupdate.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnupdate.UseVisualStyleBackColor = true;
            // 
            // btncancel
            // 
            btncancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btncancel.Cursor = Cursors.Hand;
            btncancel.FlatAppearance.MouseDownBackColor = Color.Gray;
            btncancel.FlatAppearance.MouseOverBackColor = Color.Silver;
            btncancel.FlatStyle = FlatStyle.Flat;
            btncancel.Image = Properties.Resources.close;
            btncancel.Location = new Point(610, 373);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(120, 40);
            btncancel.TabIndex = 16;
            btncancel.Text = "&Cancel";
            btncancel.TextAlign = ContentAlignment.MiddleRight;
            btncancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btncancel.UseVisualStyleBackColor = true;
            // 
            // cmbfilefolder
            // 
            cmbfilefolder.BackColor = Color.FromArgb(64, 64, 64);
            cmbfilefolder.Cursor = Cursors.Hand;
            cmbfilefolder.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbfilefolder.FlatStyle = FlatStyle.Flat;
            cmbfilefolder.ForeColor = Color.White;
            cmbfilefolder.FormattingEnabled = true;
            cmbfilefolder.Items.AddRange(new object[] { "cookies", "auth", "misc" });
            cmbfilefolder.Location = new Point(117, 49);
            cmbfilefolder.Name = "cmbfilefolder";
            cmbfilefolder.Size = new Size(145, 33);
            cmbfilefolder.TabIndex = 17;
            // 
            // frmEditFile
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            ClientSize = new Size(742, 423);
            Controls.Add(cmbfilefolder);
            Controls.Add(btncancel);
            Controls.Add(btnupdate);
            Controls.Add(txtrename);
            Controls.Add(txtfileandfolder);
            Controls.Add(cbedit);
            Controls.Add(txtfilecontent);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmEditFile";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Edit File";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox cbedit;
        private TextBox txtfilecontent;
        private Label label2;
        private Label label1;
        private TextBox txtfileandfolder;
        private TextBox txtrename;
        private Button btnupdate;
        private Button btncancel;
        private ComboBox cmbfilefolder;
    }
}