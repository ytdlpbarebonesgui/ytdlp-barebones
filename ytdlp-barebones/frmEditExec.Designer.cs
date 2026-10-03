namespace ytdlp_barebones
{
    partial class frmEditExec
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
            groupBox1 = new GroupBox();
            txtname = new TextBox();
            txtexec = new TextBox();
            btncancel = new Button();
            btnsave = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtname);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(508, 64);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Name for your new exec command:";
            // 
            // txtname
            // 
            txtname.BackColor = Color.FromArgb(64, 64, 64);
            txtname.BorderStyle = BorderStyle.FixedSingle;
            txtname.Dock = DockStyle.Fill;
            txtname.ForeColor = Color.White;
            txtname.Location = new Point(3, 27);
            txtname.Name = "txtname";
            txtname.Size = new Size(502, 31);
            txtname.TabIndex = 0;
            // 
            // txtexec
            // 
            txtexec.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtexec.BackColor = Color.FromArgb(64, 64, 64);
            txtexec.BorderStyle = BorderStyle.FixedSingle;
            txtexec.ForeColor = Color.White;
            txtexec.Location = new Point(12, 82);
            txtexec.Multiline = true;
            txtexec.Name = "txtexec";
            txtexec.ScrollBars = ScrollBars.Vertical;
            txtexec.Size = new Size(508, 193);
            txtexec.TabIndex = 2;
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
            btncancel.Location = new Point(400, 281);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(120, 40);
            btncancel.TabIndex = 5;
            btncancel.Text = "&Cancel";
            btncancel.TextAlign = ContentAlignment.MiddleRight;
            btncancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btncancel.UseVisualStyleBackColor = true;
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
            btnsave.Location = new Point(274, 281);
            btnsave.Name = "btnsave";
            btnsave.Size = new Size(120, 40);
            btnsave.TabIndex = 4;
            btnsave.Text = "&Save";
            btnsave.TextAlign = ContentAlignment.MiddleRight;
            btnsave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnsave.UseVisualStyleBackColor = true;
            // 
            // frmEditExec
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            ClientSize = new Size(532, 333);
            Controls.Add(btncancel);
            Controls.Add(btnsave);
            Controls.Add(txtexec);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmEditExec";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Save a Exec Command";
            Load += frmEditExec_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtname;
        private TextBox txtexec;
        private Button btncancel;
        private Button btnsave;
    }
}