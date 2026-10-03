using ytdlp_barebones.Helpers.Display;

namespace ytdlp_barebones.Helpers.Lists
{
    public class ExecList
    {
        private readonly ComboBox _cmbExec;
        private readonly TextBox _txtExec;

        public bool IsRefreshing { get; private set; } = false;

        public ExecList(ComboBox cmbExec, TextBox txtExec)
        {
            _cmbExec = cmbExec ?? throw new ArgumentNullException(nameof(cmbExec));
            _txtExec = txtExec ?? throw new ArgumentNullException(nameof(txtExec));
        }

        /// <summary>
        /// Refreshes the exec combobox from the "exec" folder.
        /// </summary>
        public void RefreshExecList(bool addDefaultItem = true, bool selectDefaultIndex = true)
        {
            IsRefreshing = true;
            try
            {
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                string execFolder = Path.Combine(appDir, "exec");

                // Ensure folder exists
                Directory.CreateDirectory(execFolder);

                // Get filenames without extension, ordered
                var files = Directory.GetFiles(execFolder, "*.txt", SearchOption.TopDirectoryOnly)
                                     .Select(Path.GetFileNameWithoutExtension)
                                     .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                                     .ToList();

                _cmbExec.BeginUpdate();
                _cmbExec.Items.Clear();
                if (addDefaultItem)
                    _cmbExec.Items.Add("-- Load Exec --");

                foreach (var name in files)
                    _cmbExec.Items.Add(name);

                if (selectDefaultIndex)
                    _cmbExec.SelectedIndex = _cmbExec.Items.Count > 0 ? 0 : -1;

                _cmbExec.EndUpdate();

                // Auto-adjust dropdown width to fit longest item
                ComboBoxHelper.ExpandComboBoxDropDownWidth(_cmbExec);
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Loads the selected exec file content into the provided textbox.
        /// </summary>
        public void CheckExec(ComboBox cmbExec, TextBox txtExec)
        {

            if (cmbExec == null || txtExec == null) return;
            if (cmbExec.SelectedItem == null) return;

            string selected = cmbExec.SelectedItem.ToString();
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string execFolder = Path.Combine(appDir, "exec");

            // If the default placeholder is selected, clear the textbox
            if (selected.Equals("-- Load Exec --", StringComparison.OrdinalIgnoreCase))
            {
                txtExec.Clear();
                return;
            }

            string candidate = Path.Combine(execFolder, selected + ".txt");
            if (File.Exists(candidate))
            {
                string[] lines = ComboBoxHelper.ReadAllLinesSafely(candidate);
                txtExec.Clear();
                if (lines.Length > 0)
                    txtExec.Text = lines[0];
            }
            else
            {
                MessageBox.Show("The selected exec file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbExec.Items.Remove(selected);
                cmbExec.SelectedIndex = cmbExec.Items.Count > 0 ? 0 : -1;
            }
        }
    }
}
