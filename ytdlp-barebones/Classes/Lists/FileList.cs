using ytdlp_barebones.Helpers.Display;

namespace ytdlp_barebones.Helpers.Lists
{
    public static class FileList
    {
        // Fields & Properties
        // Static field to hold the ComboBox reference.
        private static ComboBox _cmbfiles;
        // Base path where the app is running.
        private static readonly string _basePath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Initializes the static helper with the ComboBox control.
        /// Call this in frmMain (e.g., in the constructor) before using other methods.
        /// </summary>
        public static void Initialize(ComboBox cmbfiles) => _cmbfiles = cmbfiles;

        /// <summary>
        /// Refreshes the files list by searching for files in the "cookies", "auth", and "misc" subfolders.
        /// </summary>
        public static void RefreshFileList()
        {
            if (_cmbfiles == null)
                return;

            _cmbfiles.Items.Clear();

            // Define subfolders to search.
            string[] subfolders = { "cookies", "auth", "misc" };

            // Always add the default option.
            _cmbfiles.Items.Add("-- Select File --");

            foreach (string folder in subfolders)
            {
                string folderPath = Path.Combine(_basePath, folder);
                if (Directory.Exists(folderPath))
                {
                    // Get all files in the subfolder.
                    var files = Directory.GetFiles(folderPath);
                    foreach (var file in files)
                    {
                        // Format as "subfolder\filename.extension"
                        string fileName = Path.GetFileName(file);
                        string item = folder + "\\" + fileName;
                        _cmbfiles.Items.Add(item);
                    }
                }
            }

            // Adjust the drop-down width to fit the longest item.
            ComboBoxHelper.ExpandComboBoxDropDownWidth(_cmbfiles);

            // Select the default item.
            _cmbfiles.SelectedIndex = 0;
        }

        // Checks whether the selected file still exists.
        // If not, displays an error and resets the selection.
        public static void CheckFile(Button btnremove, Button btnedit)
        {
            // Disable buttons by default
            if (btnremove != null) btnremove.Enabled = false;
            if (btnedit != null) btnedit.Enabled = false;

            if (_cmbfiles == null) return;

            // Get selected item text
            if (_cmbfiles.SelectedItem != null)
            {
                string selectedFile = _cmbfiles.SelectedItem.ToString();

                // If default option is selected, nothing to check.
                if (selectedFile.Equals("-- Select File --", StringComparison.OrdinalIgnoreCase))
                    return;

                // Expected format: "subfolder\filename.extension"
                string[] parts = selectedFile.Split('\\');
                if (parts.Length != 2)
                {
                    MessageBox.Show("The selected file item has an invalid format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string subfolder = parts[0];
                string filename = parts[1];
                string filePath = Path.Combine(_basePath, subfolder, filename);

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("The selected file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _cmbfiles.Items.Remove(selectedFile);
                    _cmbfiles.SelectedIndex = 0; // Reset selection to default.
                    return;
                }

                // File exists, enable both
                btnremove.Enabled = btnedit.Enabled = true;
            }
        }

        // Refreshes the file list and re-selects the provided file string if it exists.
        public static void RememberFileList(string fileFullString)
        {
            RefreshFileList();

            if (!string.IsNullOrWhiteSpace(fileFullString) && fileFullString != "-- Select File --" && _cmbfiles.Items.Contains(fileFullString))
                _cmbfiles.SelectedItem = fileFullString;
            else
                _cmbfiles.SelectedIndex = 0;
        }
    }
}