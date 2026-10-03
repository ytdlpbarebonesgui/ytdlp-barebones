using System.Text.RegularExpressions;
using ytdlp_barebones.Helpers.Display;

namespace ytdlp_barebones.Helpers.Lists
{
    public static class DirectoryList
    {
        // Fields & Properties
        // Static field to hold the ListBox reference.
        private static ListBox _listBox;
        // Static field to store the previously selected directory.
        private static string previousSelectedDirectory = null;
        // Static property for the file path.
        public static string DirectoriesFilePath { get; } = Path.Combine(Application.StartupPath, "directories.txt");
        
        // Initialization
        // Call this method (e.g., in frmMain's constructor) to assign the ListBox.
        public static void Initialize(ListBox listBox) => _listBox = listBox;

        // Public Methods
        #region Refresh & Updates
        // Refreshes the directory list from the file and updates the ListBox.
        public static void RefreshDirectoryList()
        {
            if (_listBox == null)
                return; // or throw an exception

            _listBox.Items.Clear();

            // Create the file if it doesn't exist.
            if (!File.Exists(DirectoriesFilePath)) File.WriteAllText(DirectoriesFilePath, string.Empty);

            if (File.Exists(DirectoriesFilePath))
            {
                // Read all non-empty lines.
                var lines = File.ReadAllLines(DirectoriesFilePath)
                                .Where(line => !string.IsNullOrWhiteSpace(line));

                List<string> cleanedLines = new List<string>();

                foreach (var line in lines)
                {
                    // Trim the line.
                    string cleanedLine = line.Trim();

                    // Remove trailing backslash if present.
                    if (cleanedLine.EndsWith("\\"))
                        cleanedLine = cleanedLine.TrimEnd('\\');

                    // Split the line using a regular expression (e.g., if multiple drive paths exist).
                    string[] segments = Regex.Split(cleanedLine, @"(?=[A-Z]:)");
                    foreach (var segment in segments)
                    {
                        string seg = segment.Trim();
                        if (!string.IsNullOrEmpty(seg)) cleanedLines.Add(seg);
                    }
                }

                // Overwrite the file with cleaned data.
                File.WriteAllLines(DirectoriesFilePath, cleanedLines);

                // Populate the ListBox.
                _listBox.Items.AddRange(cleanedLines.ToArray());
            }

            // Focus on the listbox
            // The code that avoids selecting any element in frmMain
            _listBox.Focus();
        }
        // Checks and updates the previously selected directory.
        /// <param name="gbdirectories">The GroupBox control to update with the directory selection status.</param>
        public static void CheckPreviousSelectedDirectory(GroupBox gbdirectories)
        {
            if (_listBox == null)
                return;

            if (previousSelectedDirectory != null)
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(basePath, "directories.txt");

                // Ensure extended-length path prefix.
                if (!filePath.StartsWith(@"\\?\"))
                    filePath = @"\\?\" + filePath;

                if (File.Exists(filePath))
                {
                    string[] directories = File.ReadAllLines(filePath);
                    bool existsInFile = directories.Contains(previousSelectedDirectory);

                    // Remove the previous selection if it's no longer in the file.
                    if (!existsInFile && _listBox.SelectedItem?.ToString() != previousSelectedDirectory)
                        _listBox.Items.Remove(previousSelectedDirectory);
                }
            }

            // Update the previous selection based on the current ListBox selection.
            if (_listBox.SelectedItem != null)
            {
                string currentSelection = _listBox.SelectedItem.ToString();
                // If the item is already selected, deselect it.
                if (currentSelection == previousSelectedDirectory)
                {
                    _listBox.SelectedIndex = -1;
                    previousSelectedDirectory = null;
                    ValidateIndex(gbdirectories, false, null);
                    return;
                }

                previousSelectedDirectory = currentSelection;
                // Update the GroupBox based on whether the directory exists.
                ValidateIndex(gbdirectories, Directory.Exists(previousSelectedDirectory), null);
            }
            else
            {
                previousSelectedDirectory = null;
                ValidateIndex(gbdirectories, false, null);
            }
        }
        // Update the GroupBox's text and color and button states.
        public static void ValidateIndex(GroupBox groupBox, bool isValidOnDisk, Action<bool> onValidationChanged)
        {
            const string baseText = "2. Location/Directory path(s)";

            // Update GroupBox text and color based on validity
            groupBox.Text = isValidOnDisk ? baseText : baseText + " (select one) 🦴";
            groupBox.ForeColor = isValidOnDisk ? (Settings.Default.Theme == "Light" ? Color.Black : Color.White) : Color.Red;

            // Call the delegate to enable/disable buttons
            onValidationChanged?.Invoke(isValidOnDisk);
        }
        #endregion
        #region Adding Directories
        /// Adds one or more directories (used by drag-and-drop or manual picker).
        public static void AddDirectories(IEnumerable<string> directories, string directoriesFilePath, Action refreshDirectoryList)
        {
            HandleDirectories(directories, directoriesFilePath, refreshDirectoryList);
        }
        /// <summary>
        /// Handles adding directories from both FolderBrowserDialog and drag-n-drop.
        /// - Normalizes input (trims quotes/spaces, removes trailing slashes).
        /// - Rejects invalid paths, non-existent dirs, duplicates, and app’s own directory.
        /// - Collects accepted and rejected directories separately.
        /// - Appends accepted directories to the file and refreshes the UI list.
        /// - Shows two message boxes in order:
        ///     1. Error box for rejected directories (if any).
        ///     2. Info box for accepted directories (if any).
        /// </summary>
        private static void HandleDirectories(IEnumerable<string> directories, string filePath, Action refreshDirectoryList)
        {
            List<string> accepted = new List<string>();
            List<string> rejected = new List<string>();

            // Normalize: read existing dirs
            string[] existingDirectories = File.Exists(filePath)
                ? File.ReadAllLines(filePath)
                : Array.Empty<string>();

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);

            foreach (string dir in directories)
            {
                string candidate = dir?.Trim('"', ' ').TrimEnd(Path.DirectorySeparatorChar);
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    rejected.Add($"'{dir}' (Empty/Invalid)");
                    continue;
                }

                if (!Directory.Exists(candidate))
                {
                    rejected.Add($"'{candidate}' (Does not exist)");
                    continue;
                }

                if (candidate.Equals(appDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    rejected.Add($"'{candidate}' (App’s own directory)");
                    continue;
                }

                if (existingDirectories.Contains(candidate, StringComparer.OrdinalIgnoreCase))
                {
                    rejected.Add($"'{candidate}' (Duplicate)");
                    continue;
                }

                // Passed all checks
                File.AppendAllText(filePath, Environment.NewLine + candidate);
                accepted.Add(candidate);
            }

            // Refresh UI if anything was added
            if (accepted.Any())
                refreshDirectoryList?.Invoke();

            // Show rejected first (if any)
            if (rejected.Any())
            {
                string rejectedMsg = "These path(s) were rejected:" + Environment.NewLine +
                                     string.Join(Environment.NewLine, rejected.Select(d => "- " + d));
                MessageBox.Show(rejectedMsg, "Rejected Directories", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Then show accepted (if any)
            if (accepted.Any())
            {
                string acceptedMsg = "New location path(s) added successfully!" + Environment.NewLine +
                                     string.Join(Environment.NewLine, accepted.Select(d => "- \'" + d + "\'"));
                MessageBox.Show(acceptedMsg, "Accepted Directories", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}