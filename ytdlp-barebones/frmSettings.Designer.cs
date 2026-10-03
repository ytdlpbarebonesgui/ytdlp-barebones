namespace ytdlp_barebones
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            cbautoupdate = new CheckBox();
            tabControl1 = new TabControl();
            tabGeneral = new TabPage();
            cmbthemes = new ComboBox();
            label8 = new Label();
            cbdownsecautocom = new CheckBox();
            cbfileautocom = new CheckBox();
            cburlautocom = new CheckBox();
            cboutputautocom = new CheckBox();
            label4 = new Label();
            cblocationrequired = new CheckBox();
            cburlrequired = new CheckBox();
            label3 = new Label();
            cbclearurldownload = new CheckBox();
            tabAdvanced = new TabPage();
            cbmonoaudiomode = new CheckBox();
            cbforcekeyframesatcuts = new CheckBox();
            label6 = new Label();
            cbsaveformlocation = new CheckBox();
            cbsaveformsize = new CheckBox();
            cbonlyoneinstance = new CheckBox();
            label7 = new Label();
            tabAbout = new TabPage();
            label5 = new Label();
            btngithubytdlpbarebones = new Button();
            btndiscord = new Button();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            tabControl1.SuspendLayout();
            tabGeneral.SuspendLayout();
            tabAdvanced.SuspendLayout();
            tabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // cbautoupdate
            // 
            cbautoupdate.AutoSize = true;
            cbautoupdate.Checked = true;
            cbautoupdate.CheckState = CheckState.Checked;
            cbautoupdate.Cursor = Cursors.Hand;
            cbautoupdate.Location = new Point(305, 66);
            cbautoupdate.Name = "cbautoupdate";
            cbautoupdate.Size = new Size(268, 29);
            cbautoupdate.TabIndex = 11;
            cbautoupdate.Text = "&Automatic Updates (Start-Up)";
            cbautoupdate.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabGeneral);
            tabControl1.Controls.Add(tabAdvanced);
            tabControl1.Controls.Add(tabAbout);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(682, 373);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 13;
            // 
            // tabGeneral
            // 
            tabGeneral.BackColor = Color.DimGray;
            tabGeneral.Controls.Add(cmbthemes);
            tabGeneral.Controls.Add(label8);
            tabGeneral.Controls.Add(cbdownsecautocom);
            tabGeneral.Controls.Add(cbfileautocom);
            tabGeneral.Controls.Add(cburlautocom);
            tabGeneral.Controls.Add(cboutputautocom);
            tabGeneral.Controls.Add(label4);
            tabGeneral.Controls.Add(cblocationrequired);
            tabGeneral.Controls.Add(cburlrequired);
            tabGeneral.Controls.Add(label3);
            tabGeneral.Controls.Add(cbclearurldownload);
            tabGeneral.Controls.Add(cbautoupdate);
            tabGeneral.ForeColor = Color.White;
            tabGeneral.Location = new Point(4, 34);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(3);
            tabGeneral.Size = new Size(674, 335);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            // 
            // cmbthemes
            // 
            cmbthemes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbthemes.BackColor = Color.FromArgb(64, 64, 64);
            cmbthemes.Cursor = Cursors.Hand;
            cmbthemes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbthemes.FlatStyle = FlatStyle.Flat;
            cmbthemes.ForeColor = Color.White;
            cmbthemes.FormattingEnabled = true;
            cmbthemes.Items.AddRange(new object[] { "System Default", "Light", "Dark" });
            cmbthemes.Location = new Point(82, 16);
            cmbthemes.Name = "cmbthemes";
            cmbthemes.Size = new Size(232, 33);
            cmbthemes.TabIndex = 34;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(7, 19);
            label8.Name = "label8";
            label8.Size = new Size(69, 25);
            label8.TabIndex = 33;
            label8.Text = "Theme:";
            // 
            // cbdownsecautocom
            // 
            cbdownsecautocom.AutoSize = true;
            cbdownsecautocom.Checked = true;
            cbdownsecautocom.CheckState = CheckState.Checked;
            cbdownsecautocom.Cursor = Cursors.Hand;
            cbdownsecautocom.Location = new Point(305, 221);
            cbdownsecautocom.Name = "cbdownsecautocom";
            cbdownsecautocom.Size = new Size(248, 29);
            cbdownsecautocom.TabIndex = 30;
            cbdownsecautocom.Text = "&Download Sections Option";
            cbdownsecautocom.UseVisualStyleBackColor = true;
            // 
            // cbfileautocom
            // 
            cbfileautocom.AutoSize = true;
            cbfileautocom.Checked = true;
            cbfileautocom.CheckState = CheckState.Checked;
            cbfileautocom.Cursor = Cursors.Hand;
            cbfileautocom.Location = new Point(7, 221);
            cbfileautocom.Name = "cbfileautocom";
            cbfileautocom.Size = new Size(121, 29);
            cbfileautocom.TabIndex = 29;
            cbfileautocom.Text = "&File Option";
            cbfileautocom.UseVisualStyleBackColor = true;
            // 
            // cburlautocom
            // 
            cburlautocom.AutoSize = true;
            cburlautocom.Checked = true;
            cburlautocom.CheckState = CheckState.Checked;
            cburlautocom.Cursor = Cursors.Hand;
            cburlautocom.Location = new Point(7, 186);
            cburlautocom.Name = "cburlautocom";
            cburlautocom.Size = new Size(126, 29);
            cburlautocom.TabIndex = 28;
            cburlautocom.Text = "&URL Option";
            cburlautocom.UseVisualStyleBackColor = true;
            // 
            // cboutputautocom
            // 
            cboutputautocom.AutoSize = true;
            cboutputautocom.Checked = true;
            cboutputautocom.CheckState = CheckState.Checked;
            cboutputautocom.Cursor = Cursors.Hand;
            cboutputautocom.Location = new Point(305, 186);
            cboutputautocom.Name = "cboutputautocom";
            cboutputautocom.Size = new Size(241, 29);
            cboutputautocom.TabIndex = 27;
            cboutputautocom.Text = "&Location/Directory Option";
            cboutputautocom.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 158);
            label4.Name = "label4";
            label4.Size = new Size(84, 25);
            label4.TabIndex = 26;
            label4.Text = "Auto-fills";
            // 
            // cblocationrequired
            // 
            cblocationrequired.AutoSize = true;
            cblocationrequired.Checked = true;
            cblocationrequired.CheckState = CheckState.Checked;
            cblocationrequired.Cursor = Cursors.Hand;
            cblocationrequired.Location = new Point(305, 126);
            cblocationrequired.Name = "cblocationrequired";
            cblocationrequired.Size = new Size(294, 29);
            cblocationrequired.TabIndex = 24;
            cblocationrequired.Text = "&Location/Directory Path Required";
            cblocationrequired.UseVisualStyleBackColor = true;
            // 
            // cburlrequired
            // 
            cburlrequired.AutoSize = true;
            cburlrequired.Checked = true;
            cburlrequired.CheckState = CheckState.Checked;
            cburlrequired.Cursor = Cursors.Hand;
            cburlrequired.Location = new Point(7, 126);
            cburlrequired.Name = "cburlrequired";
            cburlrequired.Size = new Size(140, 29);
            cburlrequired.TabIndex = 23;
            cburlrequired.Text = "&URL Required";
            cburlrequired.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 98);
            label3.Name = "label3";
            label3.Size = new Size(98, 25);
            label3.TabIndex = 22;
            label3.Text = "Validations";
            // 
            // cbclearurldownload
            // 
            cbclearurldownload.AutoSize = true;
            cbclearurldownload.Cursor = Cursors.Hand;
            cbclearurldownload.Location = new Point(6, 66);
            cbclearurldownload.Name = "cbclearurldownload";
            cbclearurldownload.Size = new Size(280, 29);
            cbclearurldownload.TabIndex = 14;
            cbclearurldownload.Text = "&Clear All URLs When Download";
            cbclearurldownload.UseVisualStyleBackColor = true;
            // 
            // tabAdvanced
            // 
            tabAdvanced.BackColor = Color.DimGray;
            tabAdvanced.Controls.Add(cbmonoaudiomode);
            tabAdvanced.Controls.Add(cbforcekeyframesatcuts);
            tabAdvanced.Controls.Add(label6);
            tabAdvanced.Controls.Add(cbsaveformlocation);
            tabAdvanced.Controls.Add(cbsaveformsize);
            tabAdvanced.Controls.Add(cbonlyoneinstance);
            tabAdvanced.Controls.Add(label7);
            tabAdvanced.Location = new Point(4, 34);
            tabAdvanced.Name = "tabAdvanced";
            tabAdvanced.Size = new Size(674, 335);
            tabAdvanced.TabIndex = 2;
            tabAdvanced.Text = "Advanced";
            // 
            // cbmonoaudiomode
            // 
            cbmonoaudiomode.AutoSize = true;
            cbmonoaudiomode.Cursor = Cursors.Hand;
            cbmonoaudiomode.Location = new Point(305, 143);
            cbmonoaudiomode.Name = "cbmonoaudiomode";
            cbmonoaudiomode.Size = new Size(187, 29);
            cbmonoaudiomode.TabIndex = 35;
            cbmonoaudiomode.Text = "&Mono Audio Mode";
            cbmonoaudiomode.UseVisualStyleBackColor = true;
            // 
            // cbforcekeyframesatcuts
            // 
            cbforcekeyframesatcuts.AutoSize = true;
            cbforcekeyframesatcuts.Checked = true;
            cbforcekeyframesatcuts.CheckState = CheckState.Checked;
            cbforcekeyframesatcuts.Cursor = Cursors.Hand;
            cbforcekeyframesatcuts.Location = new Point(6, 143);
            cbforcekeyframesatcuts.Name = "cbforcekeyframesatcuts";
            cbforcekeyframesatcuts.Size = new Size(224, 29);
            cbforcekeyframesatcuts.TabIndex = 34;
            cbforcekeyframesatcuts.Text = "&Force Keyframes at Cuts";
            cbforcekeyframesatcuts.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 115);
            label6.Name = "label6";
            label6.Size = new Size(212, 25);
            label6.TabIndex = 33;
            label6.Text = "yt-dlp's Auxiliary Options";
            // 
            // cbsaveformlocation
            // 
            cbsaveformlocation.AutoSize = true;
            cbsaveformlocation.Cursor = Cursors.Hand;
            cbsaveformlocation.Location = new Point(6, 83);
            cbsaveformlocation.Name = "cbsaveformlocation";
            cbsaveformlocation.Size = new Size(260, 29);
            cbsaveformlocation.TabIndex = 29;
            cbsaveformlocation.Text = "&Save/Load Window Location";
            cbsaveformlocation.UseVisualStyleBackColor = true;
            // 
            // cbsaveformsize
            // 
            cbsaveformsize.AutoSize = true;
            cbsaveformsize.Checked = true;
            cbsaveformsize.CheckState = CheckState.Checked;
            cbsaveformsize.Cursor = Cursors.Hand;
            cbsaveformsize.Location = new Point(6, 53);
            cbsaveformsize.Name = "cbsaveformsize";
            cbsaveformsize.Size = new Size(224, 29);
            cbsaveformsize.TabIndex = 28;
            cbsaveformsize.Text = "&Save/Load Window Size";
            cbsaveformsize.UseVisualStyleBackColor = true;
            // 
            // cbonlyoneinstance
            // 
            cbonlyoneinstance.AutoSize = true;
            cbonlyoneinstance.Checked = true;
            cbonlyoneinstance.CheckState = CheckState.Checked;
            cbonlyoneinstance.Cursor = Cursors.Hand;
            cbonlyoneinstance.Location = new Point(6, 23);
            cbonlyoneinstance.Name = "cbonlyoneinstance";
            cbonlyoneinstance.Size = new Size(208, 29);
            cbonlyoneinstance.TabIndex = 27;
            cbonlyoneinstance.Text = "&Run only one instance";
            cbonlyoneinstance.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 0);
            label7.Name = "label7";
            label7.Size = new Size(151, 25);
            label7.TabIndex = 26;
            label7.Text = "Window Behavior";
            // 
            // tabAbout
            // 
            tabAbout.BackColor = Color.DimGray;
            tabAbout.Controls.Add(label5);
            tabAbout.Controls.Add(btngithubytdlpbarebones);
            tabAbout.Controls.Add(btndiscord);
            tabAbout.Controls.Add(label2);
            tabAbout.Controls.Add(label1);
            tabAbout.Controls.Add(pictureBox1);
            tabAbout.Location = new Point(4, 34);
            tabAbout.Name = "tabAbout";
            tabAbout.Padding = new Padding(3);
            tabAbout.Size = new Size(674, 335);
            tabAbout.TabIndex = 1;
            tabAbout.Text = "About";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.ForeColor = Color.Red;
            label5.Location = new Point(137, 33);
            label5.Name = "label5";
            label5.Size = new Size(159, 25);
            label5.TabIndex = 10;
            label5.Text = "Created by SyberG";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btngithubytdlpbarebones
            // 
            btngithubytdlpbarebones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btngithubytdlpbarebones.BackColor = Color.White;
            btngithubytdlpbarebones.Cursor = Cursors.Hand;
            btngithubytdlpbarebones.FlatAppearance.MouseDownBackColor = Color.Gray;
            btngithubytdlpbarebones.FlatAppearance.MouseOverBackColor = Color.Silver;
            btngithubytdlpbarebones.FlatStyle = FlatStyle.Flat;
            btngithubytdlpbarebones.Font = new Font("Cascadia Code", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btngithubytdlpbarebones.Image = Properties.Resources.github;
            btngithubytdlpbarebones.Location = new Point(70, 271);
            btngithubytdlpbarebones.Name = "btngithubytdlpbarebones";
            btngithubytdlpbarebones.Size = new Size(58, 58);
            btngithubytdlpbarebones.TabIndex = 9;
            btngithubytdlpbarebones.TextAlign = ContentAlignment.MiddleRight;
            btngithubytdlpbarebones.TextImageRelation = TextImageRelation.ImageBeforeText;
            btngithubytdlpbarebones.UseVisualStyleBackColor = false;
            // 
            // btndiscord
            // 
            btndiscord.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btndiscord.BackColor = Color.White;
            btndiscord.Cursor = Cursors.Hand;
            btndiscord.FlatAppearance.MouseDownBackColor = Color.Gray;
            btndiscord.FlatAppearance.MouseOverBackColor = Color.Silver;
            btndiscord.FlatStyle = FlatStyle.Flat;
            btndiscord.Font = new Font("Cascadia Code", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btndiscord.Image = Properties.Resources.discord;
            btndiscord.Location = new Point(6, 271);
            btndiscord.Name = "btndiscord";
            btndiscord.Size = new Size(58, 58);
            btndiscord.TabIndex = 8;
            btndiscord.TextAlign = ContentAlignment.MiddleRight;
            btndiscord.TextImageRelation = TextImageRelation.ImageBeforeText;
            btndiscord.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Location = new Point(137, 58);
            label2.Name = "label2";
            label2.Size = new Size(531, 271);
            label2.TabIndex = 7;
            label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Mono", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(137, 6);
            label1.Name = "label1";
            label1.Size = new Size(264, 27);
            label1.TabIndex = 6;
            label1.Text = "YT-DLP Barebones v1.0";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.yt_dlp_barebones_app_icon_v2;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(6, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 125);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(682, 373);
            Controls.Add(tabControl1);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSettings";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            tabControl1.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabGeneral.PerformLayout();
            tabAdvanced.ResumeLayout(false);
            tabAdvanced.PerformLayout();
            tabAbout.ResumeLayout(false);
            tabAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private CheckBox cbautoupdate;
        private TabControl tabControl1;
        private TabPage tabGeneral;
        private TabPage tabAbout;
        private Button btngithubytdlpbarebones;
        private Button btndiscord;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label5;
        private CheckBox cbclearurldownload;
        private CheckBox cblocationrequired;
        private CheckBox cburlrequired;
        private Label label3;
        private CheckBox cbfileautocom;
        private CheckBox cburlautocom;
        private CheckBox cboutputautocom;
        private Label label4;
        private CheckBox cbdownsecautocom;
        private Label label8;
        private ComboBox cmbthemes;
        private TabPage tabAdvanced;
        private CheckBox cbforcekeyframesatcuts;
        private Label label6;
        private CheckBox cbsaveformlocation;
        private CheckBox cbsaveformsize;
        private CheckBox cbonlyoneinstance;
        private Label label7;
        private CheckBox cbmonoaudiomode;
    }
}