namespace ytdlp_barebones
{
    partial class frmMain
    {
        
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        
        ///  Clean up any resources being used.
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


        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            gbytdlpconfig = new GroupBox();
            cmbconfigcategory = new ComboBox();
            btnconfigmanage = new Button();
            txtytdlpcommand = new TextBox();
            btnsaveconfig = new Button();
            cmbloadconfig = new ComboBox();
            btndownload = new Button();
            btnopenappdir = new Button();
            gbfiles = new GroupBox();
            btneditfile = new Button();
            cmbfilelist = new ComboBox();
            btnremovefile = new Button();
            btnimportfile = new Button();
            btnsettings = new Button();
            btncheckupdates = new Button();
            tabMain = new TabControl();
            tabGeneral = new TabPage();
            splitcontainerDirandURL = new SplitContainer();
            gbdirectories = new GroupBox();
            btnmovedowndir = new Button();
            btnmoveupdir = new Button();
            btnopendir = new Button();
            btnremovedir = new Button();
            btnadddir = new Button();
            lbdirectories = new ListBox();
            gburl = new GroupBox();
            txturls = new TextBox();
            tabOptional = new TabPage();
            gbexec = new GroupBox();
            btneditexec = new Button();
            btndeleteexec = new Button();
            btnsaveexec = new Button();
            cmbexec = new ComboBox();
            txtexec = new TextBox();
            gbdownloadsections = new GroupBox();
            btnquickadddownloadsections = new Button();
            txtquickdownloadsections = new TextBox();
            btnclearalldownloadsections = new Button();
            btneditdownloadsection = new Button();
            btnremovedownloadsection = new Button();
            btnadddownloadsection = new Button();
            dgvdownloadsections = new DataGridView();
            timeNum = new DataGridViewTextBoxColumn();
            startTime = new DataGridViewTextBoxColumn();
            endTime = new DataGridViewTextBoxColumn();
            panelBottomButtons = new Panel();
            gbytdlpconfig.SuspendLayout();
            gbfiles.SuspendLayout();
            tabMain.SuspendLayout();
            tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitcontainerDirandURL).BeginInit();
            splitcontainerDirandURL.Panel1.SuspendLayout();
            splitcontainerDirandURL.Panel2.SuspendLayout();
            splitcontainerDirandURL.SuspendLayout();
            gbdirectories.SuspendLayout();
            gburl.SuspendLayout();
            tabOptional.SuspendLayout();
            gbexec.SuspendLayout();
            gbdownloadsections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvdownloadsections).BeginInit();
            panelBottomButtons.SuspendLayout();
            SuspendLayout();
            // 
            // gbytdlpconfig
            // 
            gbytdlpconfig.Controls.Add(cmbconfigcategory);
            gbytdlpconfig.Controls.Add(btnconfigmanage);
            gbytdlpconfig.Controls.Add(txtytdlpcommand);
            gbytdlpconfig.Controls.Add(btnsaveconfig);
            gbytdlpconfig.Controls.Add(cmbloadconfig);
            gbytdlpconfig.Dock = DockStyle.Top;
            gbytdlpconfig.ForeColor = Color.White;
            gbytdlpconfig.Location = new Point(10, 10);
            gbytdlpconfig.Name = "gbytdlpconfig";
            gbytdlpconfig.Size = new Size(854, 204);
            gbytdlpconfig.TabIndex = 0;
            gbytdlpconfig.TabStop = false;
            gbytdlpconfig.Text = "1. YT-DLP Command and Configuration";
            // 
            // cmbconfigcategory
            // 
            cmbconfigcategory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbconfigcategory.BackColor = Color.FromArgb(64, 64, 64);
            cmbconfigcategory.Cursor = Cursors.Hand;
            cmbconfigcategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbconfigcategory.FlatStyle = FlatStyle.Flat;
            cmbconfigcategory.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbconfigcategory.ForeColor = Color.White;
            cmbconfigcategory.FormattingEnabled = true;
            cmbconfigcategory.Location = new Point(6, 130);
            cmbconfigcategory.Name = "cmbconfigcategory";
            cmbconfigcategory.Size = new Size(207, 33);
            cmbconfigcategory.TabIndex = 5;
            // 
            // btnconfigmanage
            // 
            btnconfigmanage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnconfigmanage.Cursor = Cursors.Hand;
            btnconfigmanage.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnconfigmanage.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnconfigmanage.FlatStyle = FlatStyle.Flat;
            btnconfigmanage.Image = Properties.Resources.briefcase;
            btnconfigmanage.Location = new Point(799, 169);
            btnconfigmanage.Name = "btnconfigmanage";
            btnconfigmanage.Size = new Size(49, 29);
            btnconfigmanage.TabIndex = 4;
            btnconfigmanage.TextAlign = ContentAlignment.MiddleRight;
            btnconfigmanage.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnconfigmanage.UseVisualStyleBackColor = true;
            // 
            // txtytdlpcommand
            // 
            txtytdlpcommand.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtytdlpcommand.BackColor = Color.FromArgb(64, 64, 64);
            txtytdlpcommand.BorderStyle = BorderStyle.FixedSingle;
            txtytdlpcommand.ForeColor = Color.White;
            txtytdlpcommand.Location = new Point(6, 26);
            txtytdlpcommand.Multiline = true;
            txtytdlpcommand.Name = "txtytdlpcommand";
            txtytdlpcommand.PlaceholderText = "--options -o \"<dir>\\%(title).50s [%(id)s].%(ext)s\" <url>\r\n(do not put yt-dlp at the beginning)";
            txtytdlpcommand.ScrollBars = ScrollBars.Vertical;
            txtytdlpcommand.Size = new Size(842, 98);
            txtytdlpcommand.TabIndex = 0;
            // 
            // btnsaveconfig
            // 
            btnsaveconfig.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnsaveconfig.Cursor = Cursors.Hand;
            btnsaveconfig.Enabled = false;
            btnsaveconfig.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnsaveconfig.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnsaveconfig.FlatStyle = FlatStyle.Flat;
            btnsaveconfig.Image = Properties.Resources.save;
            btnsaveconfig.Location = new Point(744, 169);
            btnsaveconfig.Name = "btnsaveconfig";
            btnsaveconfig.Size = new Size(49, 29);
            btnsaveconfig.TabIndex = 3;
            btnsaveconfig.TextAlign = ContentAlignment.MiddleRight;
            btnsaveconfig.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnsaveconfig.UseVisualStyleBackColor = true;
            // 
            // cmbloadconfig
            // 
            cmbloadconfig.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbloadconfig.BackColor = Color.FromArgb(64, 64, 64);
            cmbloadconfig.Cursor = Cursors.Hand;
            cmbloadconfig.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbloadconfig.FlatStyle = FlatStyle.Flat;
            cmbloadconfig.ForeColor = Color.White;
            cmbloadconfig.FormattingEnabled = true;
            cmbloadconfig.Location = new Point(219, 130);
            cmbloadconfig.Name = "cmbloadconfig";
            cmbloadconfig.Size = new Size(629, 33);
            cmbloadconfig.TabIndex = 0;
            // 
            // btndownload
            // 
            btndownload.BackColor = Color.FromArgb(0, 192, 0);
            btndownload.Cursor = Cursors.Hand;
            btndownload.Dock = DockStyle.Fill;
            btndownload.FlatAppearance.BorderSize = 0;
            btndownload.FlatStyle = FlatStyle.Flat;
            btndownload.Font = new Font("Segoe UI Variable Display", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btndownload.ForeColor = Color.White;
            btndownload.Location = new Point(100, 0);
            btndownload.Name = "btndownload";
            btndownload.Size = new Size(732, 40);
            btndownload.TabIndex = 4;
            btndownload.Text = "&Download Now!";
            btndownload.TextImageRelation = TextImageRelation.ImageBeforeText;
            btndownload.UseVisualStyleBackColor = false;
            // 
            // btnopenappdir
            // 
            btnopenappdir.BackColor = Color.Transparent;
            btnopenappdir.Cursor = Cursors.Hand;
            btnopenappdir.Dock = DockStyle.Left;
            btnopenappdir.FlatAppearance.BorderSize = 0;
            btnopenappdir.FlatStyle = FlatStyle.Flat;
            btnopenappdir.Font = new Font("Segoe UI Variable Small", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnopenappdir.ForeColor = SystemColors.ControlText;
            btnopenappdir.Image = Properties.Resources.folder;
            btnopenappdir.Location = new Point(0, 0);
            btnopenappdir.Name = "btnopenappdir";
            btnopenappdir.Size = new Size(50, 40);
            btnopenappdir.TabIndex = 6;
            btnopenappdir.TextAlign = ContentAlignment.MiddleRight;
            btnopenappdir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnopenappdir.UseVisualStyleBackColor = false;
            // 
            // gbfiles
            // 
            gbfiles.Controls.Add(btneditfile);
            gbfiles.Controls.Add(cmbfilelist);
            gbfiles.Controls.Add(btnremovefile);
            gbfiles.Controls.Add(btnimportfile);
            gbfiles.Dock = DockStyle.Top;
            gbfiles.ForeColor = Color.White;
            gbfiles.Location = new Point(10, 10);
            gbfiles.Name = "gbfiles";
            gbfiles.Size = new Size(854, 100);
            gbfiles.TabIndex = 4;
            gbfiles.TabStop = false;
            gbfiles.Text = "Files (for cookies, login, etc.)";
            // 
            // btneditfile
            // 
            btneditfile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btneditfile.Cursor = Cursors.Hand;
            btneditfile.Enabled = false;
            btneditfile.FlatAppearance.MouseDownBackColor = Color.Gray;
            btneditfile.FlatAppearance.MouseOverBackColor = Color.Silver;
            btneditfile.FlatStyle = FlatStyle.Flat;
            btneditfile.Image = Properties.Resources.pencil;
            btneditfile.Location = new Point(799, 65);
            btneditfile.Name = "btneditfile";
            btneditfile.Size = new Size(49, 29);
            btneditfile.TabIndex = 4;
            btneditfile.TextAlign = ContentAlignment.MiddleRight;
            btneditfile.TextImageRelation = TextImageRelation.ImageBeforeText;
            btneditfile.UseVisualStyleBackColor = true;
            // 
            // cmbfilelist
            // 
            cmbfilelist.AllowDrop = true;
            cmbfilelist.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbfilelist.BackColor = Color.FromArgb(64, 64, 64);
            cmbfilelist.Cursor = Cursors.Hand;
            cmbfilelist.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbfilelist.FlatStyle = FlatStyle.Flat;
            cmbfilelist.ForeColor = Color.White;
            cmbfilelist.FormattingEnabled = true;
            cmbfilelist.Location = new Point(6, 26);
            cmbfilelist.Name = "cmbfilelist";
            cmbfilelist.Size = new Size(842, 33);
            cmbfilelist.TabIndex = 6;
            // 
            // btnremovefile
            // 
            btnremovefile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnremovefile.Cursor = Cursors.Hand;
            btnremovefile.Enabled = false;
            btnremovefile.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnremovefile.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnremovefile.FlatStyle = FlatStyle.Flat;
            btnremovefile.Image = Properties.Resources.close;
            btnremovefile.Location = new Point(744, 65);
            btnremovefile.Name = "btnremovefile";
            btnremovefile.Size = new Size(49, 29);
            btnremovefile.TabIndex = 5;
            btnremovefile.TextAlign = ContentAlignment.MiddleRight;
            btnremovefile.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnremovefile.UseVisualStyleBackColor = true;
            // 
            // btnimportfile
            // 
            btnimportfile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnimportfile.Cursor = Cursors.Hand;
            btnimportfile.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnimportfile.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnimportfile.FlatStyle = FlatStyle.Flat;
            btnimportfile.Image = Properties.Resources.plus;
            btnimportfile.Location = new Point(689, 65);
            btnimportfile.Name = "btnimportfile";
            btnimportfile.Size = new Size(49, 29);
            btnimportfile.TabIndex = 4;
            btnimportfile.TextAlign = ContentAlignment.MiddleRight;
            btnimportfile.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnimportfile.UseVisualStyleBackColor = true;
            // 
            // btnsettings
            // 
            btnsettings.BackColor = Color.Transparent;
            btnsettings.Cursor = Cursors.Hand;
            btnsettings.Dock = DockStyle.Left;
            btnsettings.FlatAppearance.BorderSize = 0;
            btnsettings.FlatStyle = FlatStyle.Flat;
            btnsettings.Font = new Font("Segoe UI Variable Small", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnsettings.ForeColor = SystemColors.ControlText;
            btnsettings.Image = Properties.Resources.cogwheel;
            btnsettings.Location = new Point(50, 0);
            btnsettings.Name = "btnsettings";
            btnsettings.Size = new Size(50, 40);
            btnsettings.TabIndex = 7;
            btnsettings.TextAlign = ContentAlignment.MiddleRight;
            btnsettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnsettings.UseVisualStyleBackColor = false;
            // 
            // btncheckupdates
            // 
            btncheckupdates.BackColor = Color.Transparent;
            btncheckupdates.Cursor = Cursors.Hand;
            btncheckupdates.Dock = DockStyle.Right;
            btncheckupdates.FlatAppearance.BorderSize = 0;
            btncheckupdates.FlatStyle = FlatStyle.Flat;
            btncheckupdates.Font = new Font("Segoe UI Variable Small", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btncheckupdates.ForeColor = SystemColors.ControlText;
            btncheckupdates.Image = (Image)resources.GetObject("btncheckupdates.Image");
            btncheckupdates.Location = new Point(832, 0);
            btncheckupdates.Name = "btncheckupdates";
            btncheckupdates.Size = new Size(50, 40);
            btncheckupdates.TabIndex = 8;
            btncheckupdates.TextAlign = ContentAlignment.MiddleRight;
            btncheckupdates.TextImageRelation = TextImageRelation.ImageBeforeText;
            btncheckupdates.UseVisualStyleBackColor = false;
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabGeneral);
            tabMain.Controls.Add(tabOptional);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(882, 513);
            tabMain.SizeMode = TabSizeMode.Fixed;
            tabMain.TabIndex = 9;
            // 
            // tabGeneral
            // 
            tabGeneral.BackColor = Color.FromArgb(30, 30, 30);
            tabGeneral.Controls.Add(splitcontainerDirandURL);
            tabGeneral.Controls.Add(gbytdlpconfig);
            tabGeneral.ForeColor = Color.White;
            tabGeneral.Location = new Point(4, 34);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(10);
            tabGeneral.Size = new Size(874, 475);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            // 
            // splitcontainerDirandURL
            // 
            splitcontainerDirandURL.Dock = DockStyle.Fill;
            splitcontainerDirandURL.Location = new Point(10, 214);
            splitcontainerDirandURL.Name = "splitcontainerDirandURL";
            // 
            // splitcontainerDirandURL.Panel1
            // 
            splitcontainerDirandURL.Panel1.Controls.Add(gbdirectories);
            splitcontainerDirandURL.Panel1MinSize = 350;
            // 
            // splitcontainerDirandURL.Panel2
            // 
            splitcontainerDirandURL.Panel2.Controls.Add(gburl);
            splitcontainerDirandURL.Panel2MinSize = 350;
            splitcontainerDirandURL.Size = new Size(854, 251);
            splitcontainerDirandURL.SplitterDistance = 427;
            splitcontainerDirandURL.TabIndex = 1;
            // 
            // gbdirectories
            // 
            gbdirectories.Controls.Add(btnmovedowndir);
            gbdirectories.Controls.Add(btnmoveupdir);
            gbdirectories.Controls.Add(btnopendir);
            gbdirectories.Controls.Add(btnremovedir);
            gbdirectories.Controls.Add(btnadddir);
            gbdirectories.Controls.Add(lbdirectories);
            gbdirectories.Dock = DockStyle.Fill;
            gbdirectories.ForeColor = Color.Red;
            gbdirectories.Location = new Point(0, 0);
            gbdirectories.Name = "gbdirectories";
            gbdirectories.Size = new Size(427, 251);
            gbdirectories.TabIndex = 3;
            gbdirectories.TabStop = false;
            gbdirectories.Text = "2. Location/Directory path(s) (select one) \U0001f9b4";
            // 
            // btnmovedowndir
            // 
            btnmovedowndir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnmovedowndir.Cursor = Cursors.Hand;
            btnmovedowndir.Enabled = false;
            btnmovedowndir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnmovedowndir.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnmovedowndir.FlatStyle = FlatStyle.Flat;
            btnmovedowndir.ForeColor = Color.White;
            btnmovedowndir.Image = Properties.Resources.arrows__2_;
            btnmovedowndir.Location = new Point(61, 216);
            btnmovedowndir.Name = "btnmovedowndir";
            btnmovedowndir.Size = new Size(49, 29);
            btnmovedowndir.TabIndex = 6;
            btnmovedowndir.TextAlign = ContentAlignment.MiddleRight;
            btnmovedowndir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnmovedowndir.UseVisualStyleBackColor = true;
            // 
            // btnmoveupdir
            // 
            btnmoveupdir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnmoveupdir.Cursor = Cursors.Hand;
            btnmoveupdir.Enabled = false;
            btnmoveupdir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnmoveupdir.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnmoveupdir.FlatStyle = FlatStyle.Flat;
            btnmoveupdir.ForeColor = Color.White;
            btnmoveupdir.Image = Properties.Resources.arrows__1_;
            btnmoveupdir.Location = new Point(6, 216);
            btnmoveupdir.Name = "btnmoveupdir";
            btnmoveupdir.Size = new Size(49, 29);
            btnmoveupdir.TabIndex = 5;
            btnmoveupdir.TextAlign = ContentAlignment.MiddleRight;
            btnmoveupdir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnmoveupdir.UseVisualStyleBackColor = true;
            // 
            // btnopendir
            // 
            btnopendir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnopendir.Cursor = Cursors.Hand;
            btnopendir.Enabled = false;
            btnopendir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnopendir.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnopendir.FlatStyle = FlatStyle.Flat;
            btnopendir.ForeColor = Color.White;
            btnopendir.Image = Properties.Resources.folder;
            btnopendir.Location = new Point(372, 216);
            btnopendir.Name = "btnopendir";
            btnopendir.Size = new Size(49, 29);
            btnopendir.TabIndex = 3;
            btnopendir.TextAlign = ContentAlignment.MiddleRight;
            btnopendir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnopendir.UseVisualStyleBackColor = true;
            // 
            // btnremovedir
            // 
            btnremovedir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnremovedir.Cursor = Cursors.Hand;
            btnremovedir.Enabled = false;
            btnremovedir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnremovedir.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnremovedir.FlatStyle = FlatStyle.Flat;
            btnremovedir.ForeColor = Color.White;
            btnremovedir.Image = Properties.Resources.close;
            btnremovedir.Location = new Point(317, 216);
            btnremovedir.Name = "btnremovedir";
            btnremovedir.Size = new Size(49, 29);
            btnremovedir.TabIndex = 2;
            btnremovedir.TextAlign = ContentAlignment.MiddleRight;
            btnremovedir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnremovedir.UseVisualStyleBackColor = true;
            // 
            // btnadddir
            // 
            btnadddir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnadddir.Cursor = Cursors.Hand;
            btnadddir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnadddir.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnadddir.FlatStyle = FlatStyle.Flat;
            btnadddir.ForeColor = Color.White;
            btnadddir.Image = Properties.Resources.plus;
            btnadddir.Location = new Point(262, 216);
            btnadddir.Name = "btnadddir";
            btnadddir.Size = new Size(49, 29);
            btnadddir.TabIndex = 1;
            btnadddir.TextAlign = ContentAlignment.MiddleRight;
            btnadddir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnadddir.UseVisualStyleBackColor = true;
            // 
            // lbdirectories
            // 
            lbdirectories.AllowDrop = true;
            lbdirectories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbdirectories.BackColor = Color.FromArgb(64, 64, 64);
            lbdirectories.BorderStyle = BorderStyle.None;
            lbdirectories.Cursor = Cursors.Hand;
            lbdirectories.ForeColor = Color.White;
            lbdirectories.FormattingEnabled = true;
            lbdirectories.HorizontalScrollbar = true;
            lbdirectories.Location = new Point(6, 27);
            lbdirectories.Name = "lbdirectories";
            lbdirectories.ScrollAlwaysVisible = true;
            lbdirectories.Size = new Size(415, 175);
            lbdirectories.TabIndex = 0;
            // 
            // gburl
            // 
            gburl.Controls.Add(txturls);
            gburl.Dock = DockStyle.Fill;
            gburl.ForeColor = Color.Red;
            gburl.Location = new Point(0, 0);
            gburl.Name = "gburl";
            gburl.Size = new Size(423, 251);
            gburl.TabIndex = 4;
            gburl.TabStop = false;
            gburl.Text = "3. URL(s) to download \U0001f9b4";
            // 
            // txturls
            // 
            txturls.AllowDrop = true;
            txturls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txturls.BackColor = Color.FromArgb(64, 64, 64);
            txturls.BorderStyle = BorderStyle.FixedSingle;
            txturls.ForeColor = Color.White;
            txturls.Location = new Point(6, 30);
            txturls.Multiline = true;
            txturls.Name = "txturls";
            txturls.PlaceholderText = "https://www.example.com/video\r\nhttps://www.example.com/video\r\nhttps://www.example.com/video";
            txturls.ScrollBars = ScrollBars.Vertical;
            txturls.Size = new Size(411, 215);
            txturls.TabIndex = 1;
            // 
            // tabOptional
            // 
            tabOptional.BackColor = Color.FromArgb(30, 30, 30);
            tabOptional.Controls.Add(gbexec);
            tabOptional.Controls.Add(gbdownloadsections);
            tabOptional.Controls.Add(gbfiles);
            tabOptional.Location = new Point(4, 34);
            tabOptional.Name = "tabOptional";
            tabOptional.Padding = new Padding(10);
            tabOptional.Size = new Size(874, 475);
            tabOptional.TabIndex = 1;
            tabOptional.Text = "Optional";
            // 
            // gbexec
            // 
            gbexec.Controls.Add(btneditexec);
            gbexec.Controls.Add(btndeleteexec);
            gbexec.Controls.Add(btnsaveexec);
            gbexec.Controls.Add(cmbexec);
            gbexec.Controls.Add(txtexec);
            gbexec.Dock = DockStyle.Fill;
            gbexec.ForeColor = Color.White;
            gbexec.Location = new Point(394, 110);
            gbexec.Name = "gbexec";
            gbexec.Size = new Size(470, 355);
            gbexec.TabIndex = 6;
            gbexec.TabStop = false;
            gbexec.Text = "Exec (Shell Command, No Restrictions ⚠️)";
            // 
            // btneditexec
            // 
            btneditexec.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btneditexec.Cursor = Cursors.Hand;
            btneditexec.Enabled = false;
            btneditexec.FlatAppearance.MouseDownBackColor = Color.Gray;
            btneditexec.FlatAppearance.MouseOverBackColor = Color.Silver;
            btneditexec.FlatStyle = FlatStyle.Flat;
            btneditexec.Image = Properties.Resources.pencil;
            btneditexec.Location = new Point(414, 320);
            btneditexec.Name = "btneditexec";
            btneditexec.Size = new Size(49, 29);
            btneditexec.TabIndex = 6;
            btneditexec.TextAlign = ContentAlignment.MiddleRight;
            btneditexec.TextImageRelation = TextImageRelation.ImageBeforeText;
            btneditexec.UseVisualStyleBackColor = true;
            // 
            // btndeleteexec
            // 
            btndeleteexec.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btndeleteexec.Cursor = Cursors.Hand;
            btndeleteexec.Enabled = false;
            btndeleteexec.FlatAppearance.MouseDownBackColor = Color.Gray;
            btndeleteexec.FlatAppearance.MouseOverBackColor = Color.Silver;
            btndeleteexec.FlatStyle = FlatStyle.Flat;
            btndeleteexec.Image = Properties.Resources.close;
            btndeleteexec.Location = new Point(359, 320);
            btndeleteexec.Name = "btndeleteexec";
            btndeleteexec.Size = new Size(49, 29);
            btndeleteexec.TabIndex = 7;
            btndeleteexec.TextAlign = ContentAlignment.MiddleRight;
            btndeleteexec.TextImageRelation = TextImageRelation.ImageBeforeText;
            btndeleteexec.UseVisualStyleBackColor = true;
            // 
            // btnsaveexec
            // 
            btnsaveexec.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnsaveexec.Cursor = Cursors.Hand;
            btnsaveexec.Enabled = false;
            btnsaveexec.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnsaveexec.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnsaveexec.FlatStyle = FlatStyle.Flat;
            btnsaveexec.Image = Properties.Resources.save;
            btnsaveexec.Location = new Point(304, 320);
            btnsaveexec.Name = "btnsaveexec";
            btnsaveexec.Size = new Size(49, 29);
            btnsaveexec.TabIndex = 4;
            btnsaveexec.TextAlign = ContentAlignment.MiddleRight;
            btnsaveexec.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnsaveexec.UseVisualStyleBackColor = true;
            // 
            // cmbexec
            // 
            cmbexec.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbexec.BackColor = Color.FromArgb(64, 64, 64);
            cmbexec.Cursor = Cursors.Hand;
            cmbexec.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbexec.FlatStyle = FlatStyle.Flat;
            cmbexec.ForeColor = Color.White;
            cmbexec.FormattingEnabled = true;
            cmbexec.Location = new Point(6, 281);
            cmbexec.Name = "cmbexec";
            cmbexec.Size = new Size(457, 33);
            cmbexec.TabIndex = 2;
            // 
            // txtexec
            // 
            txtexec.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtexec.BackColor = Color.FromArgb(64, 64, 64);
            txtexec.BorderStyle = BorderStyle.FixedSingle;
            txtexec.ForeColor = Color.White;
            txtexec.Location = new Point(6, 26);
            txtexec.Multiline = true;
            txtexec.Name = "txtexec";
            txtexec.PlaceholderText = "Type in your exec command here!";
            txtexec.ScrollBars = ScrollBars.Vertical;
            txtexec.Size = new Size(457, 249);
            txtexec.TabIndex = 1;
            // 
            // gbdownloadsections
            // 
            gbdownloadsections.BackColor = Color.FromArgb(30, 30, 30);
            gbdownloadsections.Controls.Add(btnquickadddownloadsections);
            gbdownloadsections.Controls.Add(txtquickdownloadsections);
            gbdownloadsections.Controls.Add(btnclearalldownloadsections);
            gbdownloadsections.Controls.Add(btneditdownloadsection);
            gbdownloadsections.Controls.Add(btnremovedownloadsection);
            gbdownloadsections.Controls.Add(btnadddownloadsection);
            gbdownloadsections.Controls.Add(dgvdownloadsections);
            gbdownloadsections.Dock = DockStyle.Left;
            gbdownloadsections.ForeColor = Color.White;
            gbdownloadsections.Location = new Point(10, 110);
            gbdownloadsections.Name = "gbdownloadsections";
            gbdownloadsections.Size = new Size(384, 355);
            gbdownloadsections.TabIndex = 5;
            gbdownloadsections.TabStop = false;
            gbdownloadsections.Text = "Download Sections";
            // 
            // btnquickadddownloadsections
            // 
            btnquickadddownloadsections.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnquickadddownloadsections.Cursor = Cursors.Hand;
            btnquickadddownloadsections.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnquickadddownloadsections.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnquickadddownloadsections.FlatStyle = FlatStyle.Flat;
            btnquickadddownloadsections.Image = Properties.Resources.flash;
            btnquickadddownloadsections.Location = new Point(329, 318);
            btnquickadddownloadsections.Name = "btnquickadddownloadsections";
            btnquickadddownloadsections.Size = new Size(49, 29);
            btnquickadddownloadsections.TabIndex = 9;
            btnquickadddownloadsections.TextAlign = ContentAlignment.MiddleRight;
            btnquickadddownloadsections.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnquickadddownloadsections.UseVisualStyleBackColor = true;
            // 
            // txtquickdownloadsections
            // 
            txtquickdownloadsections.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtquickdownloadsections.BackColor = Color.FromArgb(64, 64, 64);
            txtquickdownloadsections.BorderStyle = BorderStyle.FixedSingle;
            txtquickdownloadsections.ForeColor = Color.White;
            txtquickdownloadsections.Location = new Point(6, 316);
            txtquickdownloadsections.Name = "txtquickdownloadsections";
            txtquickdownloadsections.PlaceholderText = "hh:mm:ss-hh:mm:ss";
            txtquickdownloadsections.Size = new Size(317, 31);
            txtquickdownloadsections.TabIndex = 8;
            // 
            // btnclearalldownloadsections
            // 
            btnclearalldownloadsections.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnclearalldownloadsections.Cursor = Cursors.Hand;
            btnclearalldownloadsections.Enabled = false;
            btnclearalldownloadsections.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnclearalldownloadsections.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnclearalldownloadsections.FlatStyle = FlatStyle.Flat;
            btnclearalldownloadsections.Image = Properties.Resources.brush;
            btnclearalldownloadsections.Location = new Point(329, 26);
            btnclearalldownloadsections.Name = "btnclearalldownloadsections";
            btnclearalldownloadsections.Size = new Size(49, 29);
            btnclearalldownloadsections.TabIndex = 7;
            btnclearalldownloadsections.TextAlign = ContentAlignment.MiddleRight;
            btnclearalldownloadsections.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnclearalldownloadsections.UseVisualStyleBackColor = true;
            // 
            // btneditdownloadsection
            // 
            btneditdownloadsection.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btneditdownloadsection.Cursor = Cursors.Hand;
            btneditdownloadsection.Enabled = false;
            btneditdownloadsection.FlatAppearance.MouseDownBackColor = Color.Gray;
            btneditdownloadsection.FlatAppearance.MouseOverBackColor = Color.Silver;
            btneditdownloadsection.FlatStyle = FlatStyle.Flat;
            btneditdownloadsection.Image = Properties.Resources.pencil;
            btneditdownloadsection.Location = new Point(329, 248);
            btneditdownloadsection.Name = "btneditdownloadsection";
            btneditdownloadsection.Size = new Size(49, 29);
            btneditdownloadsection.TabIndex = 7;
            btneditdownloadsection.TextAlign = ContentAlignment.MiddleRight;
            btneditdownloadsection.TextImageRelation = TextImageRelation.ImageBeforeText;
            btneditdownloadsection.UseVisualStyleBackColor = true;
            // 
            // btnremovedownloadsection
            // 
            btnremovedownloadsection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnremovedownloadsection.Cursor = Cursors.Hand;
            btnremovedownloadsection.Enabled = false;
            btnremovedownloadsection.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnremovedownloadsection.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnremovedownloadsection.FlatStyle = FlatStyle.Flat;
            btnremovedownloadsection.Image = Properties.Resources.close;
            btnremovedownloadsection.Location = new Point(329, 61);
            btnremovedownloadsection.Name = "btnremovedownloadsection";
            btnremovedownloadsection.Size = new Size(49, 29);
            btnremovedownloadsection.TabIndex = 7;
            btnremovedownloadsection.TextAlign = ContentAlignment.MiddleRight;
            btnremovedownloadsection.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnremovedownloadsection.UseVisualStyleBackColor = true;
            // 
            // btnadddownloadsection
            // 
            btnadddownloadsection.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnadddownloadsection.Cursor = Cursors.Hand;
            btnadddownloadsection.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnadddownloadsection.FlatAppearance.MouseOverBackColor = Color.Silver;
            btnadddownloadsection.FlatStyle = FlatStyle.Flat;
            btnadddownloadsection.Image = Properties.Resources.plus;
            btnadddownloadsection.Location = new Point(329, 283);
            btnadddownloadsection.Name = "btnadddownloadsection";
            btnadddownloadsection.Size = new Size(49, 29);
            btnadddownloadsection.TabIndex = 7;
            btnadddownloadsection.TextAlign = ContentAlignment.MiddleRight;
            btnadddownloadsection.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnadddownloadsection.UseVisualStyleBackColor = true;
            // 
            // dgvdownloadsections
            // 
            dgvdownloadsections.AllowUserToAddRows = false;
            dgvdownloadsections.AllowUserToDeleteRows = false;
            dgvdownloadsections.AllowUserToOrderColumns = true;
            dgvdownloadsections.AllowUserToResizeColumns = false;
            dgvdownloadsections.AllowUserToResizeRows = false;
            dgvdownloadsections.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvdownloadsections.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvdownloadsections.BackgroundColor = Color.FromArgb(64, 64, 64);
            dgvdownloadsections.BorderStyle = BorderStyle.None;
            dgvdownloadsections.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvdownloadsections.Columns.AddRange(new DataGridViewColumn[] { timeNum, startTime, endTime });
            dgvdownloadsections.Location = new Point(6, 26);
            dgvdownloadsections.MultiSelect = false;
            dgvdownloadsections.Name = "dgvdownloadsections";
            dgvdownloadsections.ReadOnly = true;
            dgvdownloadsections.RowHeadersVisible = false;
            dgvdownloadsections.RowHeadersWidth = 51;
            dgvdownloadsections.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvdownloadsections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvdownloadsections.Size = new Size(317, 284);
            dgvdownloadsections.TabIndex = 0;
            // 
            // timeNum
            // 
            timeNum.FillWeight = 50F;
            timeNum.HeaderText = "#";
            timeNum.MinimumWidth = 6;
            timeNum.Name = "timeNum";
            timeNum.ReadOnly = true;
            timeNum.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // startTime
            // 
            startTime.HeaderText = "Start Time";
            startTime.MinimumWidth = 6;
            startTime.Name = "startTime";
            startTime.ReadOnly = true;
            startTime.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // endTime
            // 
            endTime.HeaderText = "End Time";
            endTime.MinimumWidth = 6;
            endTime.Name = "endTime";
            endTime.ReadOnly = true;
            endTime.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // panelBottomButtons
            // 
            panelBottomButtons.Controls.Add(btndownload);
            panelBottomButtons.Controls.Add(btnsettings);
            panelBottomButtons.Controls.Add(btncheckupdates);
            panelBottomButtons.Controls.Add(btnopenappdir);
            panelBottomButtons.Dock = DockStyle.Bottom;
            panelBottomButtons.Location = new Point(0, 513);
            panelBottomButtons.Name = "panelBottomButtons";
            panelBottomButtons.Size = new Size(882, 40);
            panelBottomButtons.TabIndex = 10;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(882, 553);
            Controls.Add(tabMain);
            Controls.Add(panelBottomButtons);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(900, 600);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YT-DLP Barebones";
            Load += frmMain_Load;
            gbytdlpconfig.ResumeLayout(false);
            gbytdlpconfig.PerformLayout();
            gbfiles.ResumeLayout(false);
            tabMain.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            splitcontainerDirandURL.Panel1.ResumeLayout(false);
            splitcontainerDirandURL.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitcontainerDirandURL).EndInit();
            splitcontainerDirandURL.ResumeLayout(false);
            gbdirectories.ResumeLayout(false);
            gburl.ResumeLayout(false);
            gburl.PerformLayout();
            tabOptional.ResumeLayout(false);
            gbexec.ResumeLayout(false);
            gbexec.PerformLayout();
            gbdownloadsections.ResumeLayout(false);
            gbdownloadsections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvdownloadsections).EndInit();
            panelBottomButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbytdlpconfig;
        private TextBox txtytdlpcommand;
        private ComboBox cmbloadconfig;
        private Button btnconfigmanage;
        private Button btnsaveconfig;
        private Button btndownload;
        private Button btnopenappdir;
        private GroupBox gbfiles;
        private Button btnimportfile;
        private Button btnremovefile;
        private ComboBox cmbfilelist;
        private Button btneditfile;
        private Button btnsettings;
        private ComboBox cmbconfigcategory;
        private Button btncheckupdates;
        private TabControl tabMain;
        private TabPage tabGeneral;
        private TabPage tabOptional;
        private GroupBox gbdownloadsections;
        private DataGridView dgvdownloadsections;
        private Button btnadddownloadsection;
        private Button btneditdownloadsection;
        private Button btnremovedownloadsection;
        private Button btnclearalldownloadsections;
        private GroupBox gbexec;
        private Button btnsaveexec;
        private ComboBox cmbexec;
        private TextBox txtexec;
        private Button btneditexec;
        private Button btndeleteexec;
        private TextBox txtquickdownloadsections;
        private Button btnquickadddownloadsections;
        private Panel panelBottomButtons;
        private SplitContainer splitcontainerDirandURL;
        private GroupBox gbdirectories;
        private Button btnmovedowndir;
        private Button btnmoveupdir;
        private Button btnopendir;
        private Button btnremovedir;
        private Button btnadddir;
        private ListBox lbdirectories;
        private GroupBox gburl;
        private TextBox txturls;
        private DataGridViewTextBoxColumn timeNum;
        private DataGridViewTextBoxColumn startTime;
        private DataGridViewTextBoxColumn endTime;
    }
}
