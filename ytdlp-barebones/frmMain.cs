using System.Diagnostics;
using System.Media;

// subfolders of Helpers
using ytdlp_barebones.Helpers.Buttons;
using ytdlp_barebones.Helpers.Buttons.Optional;
using ytdlp_barebones.Helpers.Buttons.Optional.DownloadSection;
using ytdlp_barebones.Helpers.Display;
using ytdlp_barebones.Helpers.Download;
using ytdlp_barebones.Helpers.Events;
using ytdlp_barebones.Helpers.Events.TextEvents;
using ytdlp_barebones.Helpers.Lists;

namespace ytdlp_barebones
{
    public partial class frmMain : Form
    {
        #region Variables
        // private variable for instance
        private ConfigList ConfigList;
        private ExecList ExecList;
        // directoryPath for DirectoryList
        private string directoryPath;
        // Window form state detector for frmMain_Resize
        private FormWindowState _lastWindowState;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            // Initializers for static helpers
            DirectoryList.Initialize(lbdirectories);
            FileList.Initialize(cmbfilelist);
            // Instance of ConfigList, as it is used in both frmMain and frmConfigManager.  
            // It cannot be static since each form needs its own state and UI components. 
            ConfigList = new(cmbconfigcategory, cmbloadconfig, txtytdlpcommand);
            ExecList = new ExecList(cmbexec, txtexec);

            // Load the directories.txt file from DirectoryList
            directoryPath = DirectoryList.DirectoriesFilePath;

            // Initilizers for tooltip names, events, & buttons
            InitializeTooltips();
            InitializeEvents();
            InitializeButtons();
        }

        #region Load
        // The async load event now simply calls the updater method.
        private async void frmMain_Load(object sender, EventArgs e)
        {
            // Apply theme first
            Theme.Load(this);

            // Load window size and location
            WindowSizeSaver.Load(this);

            // (Refresh)
            // Load all directories from 'directories.txt'
            DirectoryList.RefreshDirectoryList();
            // If a refresh is in progress, don't process the event.
            if (ConfigList.IsProcessing()) return;
            // Load configs from 'config' subfolder, and set the bool values to true.
            ConfigList.RefreshConfigList(addDefaultItem: true, selectDefaultIndex: true);
            // Load files from the 'cookies' and 'auth' folders for credential files.
            FileList.RefreshFileList();
            // Load files from the 'exec' folder
            ExecList.RefreshExecList();

            // run yt-dlp Updater
            await RunUpdaterAsync();
        }
        #endregion

        #region Methods
        // --- PUBLIC METHODS ---
        // Used to trigger the yt-dlp update feature (the frmMain_Load and the manual check updates button)
        public async Task RunUpdaterAsync(bool forceUpdate = false)
        {
            // Disable download and check updates button
            btndownload.Enabled = btncheckupdates.Enabled = false;

            // Enable wait cursor for entire form
            this.UseWaitCursor = true;

            // yt-dlp.exe path
            string ytDlpPath = Path.Combine(Application.StartupPath, "yt-dlp.exe");

            try
            {
                // Run the update/install process
                string updateLog = await ytdlpUpdater.EnsureLatestYtDlpAsync(ytDlpPath, forceUpdate);

                // Split lines for processing
                var lines = updateLog.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // -- Prepare notification --
                // Extract versions for message body
                string ytVersion = lines.FirstOrDefault(l => l.StartsWith("yt-dlp version")) ?? "yt-dlp version unknown";
                string ffmpegVersion = lines.FirstOrDefault(l => l.StartsWith("ffmpeg")) ?? "ffmpeg version unknown";

                // If AutoCheckUpdates is off AND not a forced update: show message ONLY
                if (!Settings.Default.AutoCheckUpdates && !forceUpdate)
                {
                    // Show only the version information
                    ToastNotification.Show($"{ytVersion}\n{ffmpegVersion}");
                }
                else
                {
                    // Normal behavior: use the status line
                    string statusLine = lines.FirstOrDefault() ?? "YT-DLP Status";
                    ToastNotification.Show($"{statusLine}\n{ytVersion}\n{ffmpegVersion}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Updater Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Restore cursor
            this.UseWaitCursor = false;

            // Re-enable buttons
            btndownload.Enabled = btncheckupdates.Enabled = true;

            // Play success sound
            SystemSounds.Asterisk.Play();
        }
        // --- PRIVATE METHODS ---
        // (For the buttons)
        // Shared logic for handling lbdirectories selection change
        private void HandleDirectoriesSelectionChanged(object sender, EventArgs e)
        {
            DirectoryList.CheckPreviousSelectedDirectory(gbdirectories); // Check previous selection

            // Figure out if there _is_ any selection at all
            bool hasSelection = lbdirectories.SelectedIndex != -1;

            string dir = hasSelection ? lbdirectories.SelectedItem.ToString() : null;
            bool reallyExists = hasSelection && Directory.Exists(dir);

            // Call the ValidateIndex, styling the GroupBox and controlling buttons
            DirectoryList.ValidateIndex(gbdirectories, reallyExists, isValidOnDisk =>
            {
                // now do exactly what you wanted:
                btnremovedir.Enabled = hasSelection;    // remove as long as something’s selected
                                                        // open the rest only if it really exists
                btnopendir.Enabled = btnmoveupdir.Enabled = btnmovedowndir.Enabled = isValidOnDisk;
            });
        }
        // Refreshes and triggers event of lbdirectories for btnadddir and btnremovedir
        private void DirRefreshTrigger(object sender, EventArgs e)
        {
            DirectoryList.RefreshDirectoryList();
            HandleDirectoriesSelectionChanged(sender, e);
        }

        #endregion

        #region Initializer Methods
        // For ToolTip display texts
        private void InitializeTooltips()
        {
            var toolTipData = new Dictionary<Control, string>
            {
                // Config
                { btnsaveconfig, "Save Current Configuration" },
                { btnconfigmanage, "Configuration Manager" },
                { cmbconfigcategory, "Configuration Categories" },
                { cmbloadconfig, "Configurations" },
                // DIrectory
                { btnadddir, "Add Path" },
                { btnremovedir, "Remove Selected Path" },
                { btnopendir, "Open Selected Path" },
                { btnmoveupdir, "Move Up" },
                { btnmovedowndir, "Move Down" },
                // Files
                { btnimportfile, "Import File" },
                { btnremovefile, "Remove Selected File" },
                { btneditfile, "Edit Selected File" },
                // Download Sections
                { btnadddownloadsection, "Add a Download Section" },
                { btneditdownloadsection, "Edit Download Section" },
                { btnremovedownloadsection, "Remove Download Section" },
                { btnclearalldownloadsections, "Clear All Download Sections" },
                { btnquickadddownloadsections, "Add a Download Section (Quicker)" },
                // Exec
                { btnsaveexec, "Save Exec Command" },
                { btndeleteexec, "Delete Exec Command File" },
                { btneditexec, "Edit Exec Command File" },
                // Misc.
                { btnsettings, "Settings" },
                { btnopenappdir, "Open App Location" },
                { btncheckupdates, "Check For Updates" }
            };
            TooltipInitializer.SetTooltips(toolTipData);
        }
        private void InitializeEvents()
        {
            // General
            // (Keys)
            txtytdlpcommand.KeyPress += KeyEvents.PreventNewLine; //prevent pressing Enter button for yt-dlp command textbox
            txturls.KeyDown += async (s, e) => await KeyEvents.PasteURL(s, e); // Ctrl+V or Paste creates new line for txturls, detect if the user presses Ctrl+V
            // (Text Changed)
            txtytdlpcommand.TextChanged += (s, e) =>
            {
                // Cast sender to TextBox.
                if (s is TextBox txtBox)
                    TextValidationEvents.ValidateYtDlpCommandText(txtBox); // pass itself

                // Always check for nulls after syntax correction
                TextValidationEvents.ValidateNull(txtytdlpcommand, null, downloadBtn: null, updateBtn: btnsaveconfig);
            };
            txturls.TextChanged += (s, e) => TextValidationEvents.ValidateURLText(txturls, gburl);
            // (Selected index changed)
            lbdirectories.SelectedIndexChanged += HandleDirectoriesSelectionChanged;
            cmbloadconfig.SelectedIndexChanged += (s, e) => ConfigList.CheckConfig(cmbloadconfig, cmbconfigcategory, txtytdlpcommand);
            cmbconfigcategory.SelectedIndexChanged += (s, e) =>
            {
                // If a refresh is in progress, don't process the event.
                if (ConfigList.IsRefreshing)
                    return;

                // Reload the configuration list using the new category.
                ConfigList.RefreshConfigList(addDefaultItem: true, selectDefaultIndex: true);
            };
            // (Drag)
            lbdirectories.DragEnter += DragEvents.DirectoryDragEnter;
            lbdirectories.DragDrop += (s, e) => DragEvents.DirectoryDragDrop(s, e, directoryPath, DirectoryList.RefreshDirectoryList);
            // File ComboBox drag/drop
            cmbfilelist.DragEnter += DragEvents.FileDragEnter;
            cmbfilelist.DragDrop += DragEvents.FileDragDrop;
            txturls.DragEnter += DragEvents.URLDragEnter;
            txturls.DragDrop += DragEvents.URLDragDrop;

            // Optional
            // (Keys)
            txtexec.KeyPress += KeyEvents.PreventNewLine;
            txtexec.KeyDown += KeyEvents.NormalizePasteToSingleLine;
            txtquickdownloadsections.KeyPress += QuickAddDownSecEvents.CheckTwoTimestamps_KeyPress;
            txtquickdownloadsections.KeyDown += (s, e) =>
            {
                QuickAddDownSecEvents.CheckTwoTimestamps_KeyDown(s, e, dgvdownloadsections, btnclearalldownloadsections);

                if (e.KeyCode == Keys.Enter)
                {
                    btnquickadddownloadsections.PerformClick();
                    e.SuppressKeyPress = true; // prevent the ding / newline
                }
            };
            // (Text Changed)
            txtexec.TextChanged += (s, e) => TextValidationEvents.ValidateNull(txtexec, null, null, btnsaveexec);
            txtquickdownloadsections.TextChanged += QuickAddDownSecEvents.CheckTwoTimestamps_TextChanged;
            // (Selected index changed)
            cmbfilelist.SelectedIndexChanged += (s, e) => FileList.CheckFile(btnremovefile, btneditfile);
            cmbexec.SelectedIndexChanged += (s, e) =>
            {
                if (ExecList.IsRefreshing) return;
                ExecList.CheckExec(cmbexec, txtexec);

                bool hasSelection = cmbexec.SelectedIndex > 0;
                btndeleteexec.Enabled = btneditexec.Enabled = hasSelection;
            };
            // (Download Section Data Grid View)
            dgvdownloadsections.SelectionChanged += (s, e) =>
            {
                bool hasSelection = dgvdownloadsections.SelectedRows.Count > 0;

                btneditdownloadsection.Enabled =
                    btnremovedownloadsection.Enabled =
                    btnclearalldownloadsections.Enabled = hasSelection;
            };

            // Form
            // (Form Resize)
            // Saving window size and location
            FormClosing += (s, e) => WindowSizeSaver.Save(this);
            ResizeEnd += (s, e) => WindowSizeSaver.Save(this);
            Resize += (s, e) =>
            {
                // Only act if the WindowState has changed.
                if (this.WindowState != _lastWindowState)
                {
                    _lastWindowState = this.WindowState;
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        WindowSizeSaver.SaveOnMinimized(this);
                    }
                }
            };
        }
        // Buttons
        // Initialize almost all buttons
        private void InitializeButtons()
        {
            var buttonActions = new (Button btn, Action action)[]
            {
                // GENERAL
                // (Add, remove, open, deselect, move up and down for directories)
                // uses the DirectoryButtons.cs helper
                // lbdirectories_SelectedIndexChanged(sender, e) forces deselection to reset button states
                (btnadddir, () => DirectoryButtons.AddDirectory(directoryPath, () => DirRefreshTrigger(null, null))),
                (btnremovedir, () => DirectoryButtons.RemoveDirectory(directoryPath, lbdirectories, () => DirRefreshTrigger(null, null))),
                (btnopendir, () => DirectoryButtons.OpenDirectory(lbdirectories)),
                (btnmoveupdir, () => DirectoryButtons.MoveDirectory(directoryPath, lbdirectories, -1)),
                (btnmovedowndir, () => DirectoryButtons.MoveDirectory(directoryPath, lbdirectories, +1)),
                // (Download)
                // uses DownloadProcess.cs as helper, and the SetDownloadControlsEnabled() method to disable elements while downloading
                (btndownload, () =>
                {
                    // Get input values.
                    string ytDlpCommand = txtytdlpcommand.Text;
                    string[] urls = txturls.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    int selectedIndex = lbdirectories.SelectedIndex;
                    string selectedDirectory = lbdirectories.SelectedItem?.ToString();
                    string urlsFilePath = "urls.txt";
                    string selectedFile = cmbfilelist.SelectedItem?.ToString();
                    var downloadSectionRanges = new List<string>();

                    // Process download sections from dgvdownloadsections
                    foreach (DataGridViewRow row in dgvdownloadsections.Rows)
                    {
                        // Skip the new row placeholder
                        if (row.IsNewRow) continue;
                        // Get timeNum, startTime, and endTime values
                        string timeNum = (row.Cells["timeNum"]?.Value?.ToString() ?? row.Cells[0]?.Value?.ToString() ?? "").Trim();
                        string start = row.Cells["startTime"]?.Value?.ToString() ?? row.Cells[1].Value?.ToString() ?? "";
                        string end = row.Cells["endTime"]?.Value?.ToString() ?? row.Cells[2].Value?.ToString() ?? "";

                        // Emergency, if start-end time are null.
                        if (string.IsNullOrWhiteSpace(start)) start = "00:00:00";
                        if (string.IsNullOrWhiteSpace(end)) end = "inf";

                        // Store as "timeNum|*hh:mm:ss-hh:mm:ss" (or *hh:mm:ss-inf)
                        downloadSectionRanges.Add($"{timeNum}|*{start}-{end}");
                    }

                    // grab exec text(if any) and pass it to PerformDownload
                    string execText = string.IsNullOrWhiteSpace(txtexec?.Text) ? null : txtexec.Text.Trim();

                    // Call the download process
                    DownloadProcess.PerformDownload(ytDlpCommand, urls, selectedIndex, selectedDirectory, urlsFilePath, selectedFile, txturls, execText, downloadSectionRanges);
                }),
                // (Config)
                // uses ConfigButtons.cs helper
                // "() => { Helper.Method(); }" means the callback after closing the form (frmSaveConfig and frmConfigManager)
                (btnsaveconfig, () => ConfigButtons.SaveConfigFromMain(txtytdlpcommand.Text, () => ConfigList.RememberConfig(), this)),
                (btnconfigmanage, () => ConfigButtons.ConfigManage(cmbloadconfig, cmbconfigcategory, () => ConfigList.RememberConfig())),

                // OPTIONALS
                // (Files)
                (btnimportfile, () => FileButtons.ImportFile(() => FileList.RefreshFileList())),
                (btnremovefile, () => FileButtons.RemoveFile(cmbfilelist, () => FileList.RefreshFileList())),
                (btneditfile, () => FileButtons.EditFile(cmbfilelist, this, updatedFile => FileList.RememberFileList(updatedFile))),
                // (Download Sections)
                (btnadddownloadsection, () => DownloadSectionButtons.AddDownloadSection(dgvdownloadsections, this, btnclearalldownloadsections)),
                (btneditdownloadsection, () => DownloadSectionButtons.EditDownloadSection(dgvdownloadsections, this)),
                (btnremovedownloadsection, () => DownloadSectionButtons.RemoveDownloadSection(dgvdownloadsections, btneditdownloadsection, btnremovedownloadsection, btnclearalldownloadsections)),
                (btnclearalldownloadsections, () => DownloadSectionButtons.ClearAllDownloadSections(dgvdownloadsections, btneditdownloadsection, btnremovedownloadsection, btnclearalldownloadsections)),
                // (Quick-Add Download Sections)
                (btnquickadddownloadsections, () => QuickDownloadSectionButtons.QuickAddDownloadSection(txtquickdownloadsections, txtquickdownloadsections.Text, dgvdownloadsections, btnclearalldownloadsections)),
                // (Exec)
                (btnsaveexec, () => ExecButtons.SaveExecFromMain(txtexec.Text, () => ExecList.RefreshExecList(), this)),
                (btneditexec, () => ExecButtons.EditExecFromMain(cmbexec, () => ExecList.RefreshExecList(true, true), this)),
                (btndeleteexec, () => ExecButtons.DeleteExec(cmbexec, () => ExecList.RefreshExecList(true, true))),

                // MISC.
                (btnopenappdir, () => Process.Start(new ProcessStartInfo { FileName = Application.StartupPath, UseShellExecute = true })), // Alternatively, use AppDomain.CurrentDomain.BaseDirectory for FileName
                (btnsettings, () => new frmSettings().ShowDialog()),
                (btncheckupdates, async () => await RunUpdaterAsync(forceUpdate: true)) // Runs yt-dlp updater forcefully
            };

            foreach (var (btn, action) in buttonActions)
            {
                btn.Click += (s, e) => action();
            }
        }
        #endregion
    }
}