using ytdlp_barebones.Helpers.Display;

namespace ytdlp_barebones
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            InitializeUI();
        }

        // Methods
        // Unified handler: retrieves setter from Tag and applies
        private void CheckBox_CheckedChangedInternal(object sender, EventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is Action<bool> setter)
            {
                setter(cb.Checked);
                Settings.Default.Save();
            }
        }
        // Method for social media buttons to open URLs
        private void OpenUrl(string url)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        // Used to initialize checkboxes and it's setting equivalent, tooltip display, & unified event handler for all checkboxes
        private void InitializeUI()
        {
            // One array for checkboxes, settings getter/setter, and tooltip
            var items = new (CheckBox cb, Func<bool> getter, Action<bool> setter, string tooltip)[]
            {
                // General
                (cbautoupdate,
                    () => Settings.Default.AutoCheckUpdates,
                    v => Settings.Default.AutoCheckUpdates = v,
                    "Automatically update yt-dlp every time the app is opened."),
                (cbclearurldownload,
                    () => Settings.Default.ClearURLsDownload,
                    v => Settings.Default.ClearURLsDownload = v,
                    "Clear all URLs when the download process starts."),
                // (Validations)
                (cburlrequired,
                    () => Settings.Default.URLRequired,
                    v => Settings.Default.URLRequired = v,
                    "Requires you to paste URL(s).\r\nDisabling this is not recommended, unless for debugging purposes."),
                (cblocationrequired,
                    () => Settings.Default.LocationRequired,
                    v => Settings.Default.LocationRequired = v,
                    "Requires you to select a location path." +
                    "\r\nIf disabled and no path is selected/path doesn't exist, then it defaults to your Downloads folder." +
                    "\r\nFor debugging without the output option, you must also disable Location/Directory Option."),
                // (Auto-fills)
                (cboutputautocom,
                    () => Settings.Default.OutputAutoComplete,
                    v => Settings.Default.OutputAutoComplete = v,
                    "When <dir> isn't placed.\r\nIt places the complete output option at the end of command (-o \"<dir>\\%(title).50s [%(id)s].%(ext)s\")."),
                (cburlautocom,
                    () => Settings.Default.URLAutoComplete,
                    v => Settings.Default.URLAutoComplete = v,
                    "When <url> isn't placed. Places the URL or '-a urls.txt' at the end."),
                (cbfileautocom,
                    () => Settings.Default.FileAutoComplete,
                    v => Settings.Default.FileAutoComplete = v,
                    "Places '--cookies \"<file>\"' or '--netrc \"<file>\"' at start of the command.\r\nDoesn't work for miscellaneous files."),
                (cbdownsecautocom,
                    () => Settings.Default.DownSecAutoComplete,
                    v => Settings.Default.DownSecAutoComplete = v,
                    "Places all download section timestamps provided.\r\nUnless the placeholder '<downsec-###>', which works regardless if enabled/disabled."),
                // (yt-dlp's Auxiliary Options)
                (cbforcekeyframesatcuts,
                    () => Settings.Default.ForceKeyframesAtCuts,
                    v => Settings.Default.ForceKeyframesAtCuts = v,
                    "--force-keyframes-at-cuts" +
                    "\r\nAccurate section cuts (force keyframes at cuts)." +
                    "\r\nIt may fix glitch effect after downloading video by section."),
                (cbmonoaudiomode,
                    () => Settings.Default.MonoAudioMode,
                    v => Settings.Default.MonoAudioMode = v,
                    "--postprocessor-args \"-ac 1\" / --ppa \"-ac 1\"" +
                    "\r\nConverts audio to mono (1 channel). It will not replace inputted postprocessor-arg options, it'll add '-ac 1' on it." +
                    "\r\nWorks best for audio downloads, video downloads requires recoding." +
                    "\r\nReduces file size, but volume and quality may be affected."),

                // Advanced
                (cbonlyoneinstance,
                    () => Settings.Default.OnlyOneInstance,
                    v => Settings.Default.OnlyOneInstance = v,
                    "If enabled, opening the app again will bring the existing instance instead of opening a duplicate.\r\nDisabling this is not recommended and might cause unexpected issues to the app."),
                (cbsaveformsize,
                    () => Settings.Default.SaveLoadFormSize,
                    v => Settings.Default.SaveLoadFormSize = v,
                    "Saves/loads the size of the window automatically."),
                (cbsaveformlocation,
                    () => Settings.Default.SaveLoadFormLocation,
                    v => Settings.Default.SaveLoadFormLocation = v,
                    "Saves/loads the location of the window automatically."),
            };

            // Apply everything in one loop
            var tooltips = new Dictionary<Control, string>();
            foreach (var item in items)
            {
                var cb = item.cb;
                cb.CheckedChanged -= CheckBox_CheckedChangedInternal; // avoid duplicate subscription
                cb.Checked = item.getter();
                cb.Tag = item.setter; // store setter for later
                cb.CheckedChanged += CheckBox_CheckedChangedInternal;

                tooltips[cb] = item.tooltip;
            }

            // Set tooltips in one go using your helper
            TooltipInitializer.SetTooltips(tooltips);

            // About buttons
            tooltips[btndiscord] = "Discord";
            tooltips[btngithubytdlpbarebones] = "GitHub (ytdlpbarebones)";
            TooltipInitializer.SetTooltips(tooltips);

            // Other Events
            // (Buttons)
            // uses OpenUrl method
            btndiscord.Click += (s, e) => OpenUrl("https://discord.com/users/663648701239132170");
            btngithubytdlpbarebones.Click += (s, e) => OpenUrl("https://github.com/ytdlpbarebonesgui");

            // (Form)
            // Wire load and closing events for theme handling
            Load += (s, e) =>
            {
                // Ensure Theme setting has a default and apply theme to this form
                Theme.Load(this);

                // Set combobox to current theme value (or System Default)
                string current = string.IsNullOrWhiteSpace(Settings.Default.Theme) ? Theme.SystemDefault : Settings.Default.Theme;
                var match = cmbthemes.Items.Cast<object>().FirstOrDefault(i => i.ToString().Equals(current, StringComparison.OrdinalIgnoreCase));
                if (match != null)
                    cmbthemes.SelectedItem = match;
                else
                    cmbthemes.SelectedItem = Theme.SystemDefault;
            };

            // (Combobox)
            cmbthemes.SelectedIndexChanged += (s, e) =>
            {
                // Get selected theme and current saved theme
                string selected = cmbthemes.SelectedItem?.ToString() ?? Theme.SystemDefault;
                // Get old theme for comparison
                string old = string.IsNullOrWhiteSpace(Settings.Default.Theme)
                    ? Theme.SystemDefault
                    : Settings.Default.Theme;

                // Save updated theme if changed
                if (!string.Equals(old, selected, StringComparison.OrdinalIgnoreCase))
                {
                    Settings.Default.Theme = selected;
                    Settings.Default.Save();
                }

                // Apply theme to this settings form
                Theme.Load(this);

                // Apply theme to main form if effective theme changed
                string oldEff = Theme.GetEffectiveThemeName(old);
                string newEff = Theme.GetEffectiveThemeName(selected);

                if (!string.Equals(oldEff, newEff, StringComparison.OrdinalIgnoreCase))
                {
                    // Refresh main form theme
                    var main = Application.OpenForms.OfType<frmMain>().FirstOrDefault();
                    if (main != null)
                    {
                        Theme.Load(main);
                    }
                }
            };
        }
    }
}
