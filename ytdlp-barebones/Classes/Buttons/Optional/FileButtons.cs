using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ytdlp_barebones.Helpers.Buttons.Optional
{
    public static class FileButtons
    {
        // frmMain
        /// Handles importing a file.
        /// Opens an OpenFileDialog, determines the destination subfolder based on file content,
        /// moves the file (replacing any existing one), and then invokes a refresh callback.
        /// <param name="refreshFileListCallback">Callback to refresh the file list.</param>
        public static void ImportFile(Action refreshFileListCallback)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text files (*.txt;*.netrc)|*.txt;*.netrc";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string sourceFilePath = ofd.FileName;
                    string fileContent;
                    using (StreamReader reader = new StreamReader(sourceFilePath)) fileContent = reader.ReadToEnd();

                    // Determine subfolder based on file content.
                    string[] cookieKeywords = { "Netscape HTTP Cookie File", "Cookie File" };
                    string[] authKeywords = { "username ", "password ", "machine ", "oauth2 " };

                    string subfolder = cookieKeywords.Any(keyword => fileContent.Contains(keyword))
                        ? "cookies"
                        : authKeywords.Any(keyword => fileContent.Contains(keyword))
                            ? "auth"
                            : "misc";

                    string destDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subfolder);

                    // If directory doesn't exist, create one
                    Directory.CreateDirectory(destDir);

                    string destFilePath = Path.Combine(destDir, Path.GetFileName(sourceFilePath));

                    // Prevent duplicates by name and extension across all managed subfolders
                    string[] allSubfolders = new[] { "cookies", "auth", "misc" };
                    string sourceFileName = Path.GetFileName(sourceFilePath);
                    bool duplicateExists = allSubfolders
                        .Select(sf => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sf, sourceFileName))
                        .Any(p => File.Exists(p));

                    if (duplicateExists)
                    {
                        MessageBox.Show($"A file named '{sourceFileName}' already exists in the application folders. Import skipped.", "Duplicate File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        if (File.Exists(destFilePath))
                        {
                            File.Delete(destFilePath);
                            File.Move(sourceFilePath, destFilePath);
                            MessageBox.Show("File replaced successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            File.Move(sourceFilePath, destFilePath);
                            MessageBox.Show($"File imported to \"{subfolder}\" successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error importing file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    refreshFileListCallback?.Invoke();
                }
            }
        }

        /// <summary>
        /// Imports multiple files by path. Determines subfolder for each file based on its content
        /// (cookies/auth/misc), moves the file to the application folder and shows a summary message.
        /// </summary>
        public static void ImportFiles(IEnumerable<string> paths, Action refreshFileListCallback)
        {
            if (paths == null) return;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var cookieKeywords = new[] { "Netscape HTTP Cookie File", "Cookie File" };
            var authKeywords = new[] { "username ", "password ", "machine ", "oauth2 " };

            var imported = new List<string>();
            var failed = new List<string>();

            foreach (var p in paths)
            {
                try
                {
                    if (!File.Exists(p))
                    {
                        failed.Add(p);
                        continue;
                    }

                    string fileContent;
                    using (var reader = new StreamReader(p)) fileContent = reader.ReadToEnd();

                    string subfolder = cookieKeywords.Any(keyword => fileContent.Contains(keyword))
                        ? "cookies"
                        : authKeywords.Any(keyword => fileContent.Contains(keyword))
                            ? "auth"
                            : "misc";

                    string destDir = Path.Combine(baseDir, subfolder);
                    Directory.CreateDirectory(destDir);

                    string fileName = Path.GetFileName(p);

                    // Prevent duplicates across managed subfolders
                    string[] allSubfolders = new[] { "cookies", "auth", "misc" };
                    bool duplicateExists = allSubfolders
                        .Select(sf => Path.Combine(baseDir, sf, fileName))
                        .Any(fp => File.Exists(fp));

                    if (duplicateExists)
                    {
                        failed.Add(p); // treat as failed due to duplicate
                        continue;
                    }

                    string destFilePath = Path.Combine(destDir, fileName);
                    File.Move(p, destFilePath);
                    imported.Add(Path.GetFileName(destFilePath));
                }
                catch
                {
                    failed.Add(p);
                }
            }

            // Refresh list
            refreshFileListCallback?.Invoke();

            // Build summary message
            var sb = new System.Text.StringBuilder();
            if (imported.Count > 0)
            {
                sb.AppendLine("Imported files:");
                foreach (var f in imported)
                    sb.AppendLine("- " + f);
            }
            if (failed.Count > 0)
            {
                if (sb.Length > 0) sb.AppendLine();
                sb.AppendLine("Failed to import:");
                foreach (var f in failed)
                    sb.AppendLine("- " + f);
            }

            if (sb.Length > 0)
                MessageBox.Show(sb.ToString(), "Import Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// Handles removal of a file.
        /// Retrieves the selected file from the provided ComboBox,
        /// confirms deletion, deletes the file (and optionally its folder if empty),
        /// then invokes a refresh callback.
        /// <param name="cmbfilelist">ComboBox with files (e.g. "cookies\\file.ext").</param>
        /// <param name="refreshFileListCallback">Callback to refresh the file list.</param>
        public static void RemoveFile(ComboBox cmbfilelist, Action refreshFileListCallback)
        {
            if (cmbfilelist.SelectedIndex > 0)
            {
                string selectedItem = cmbfilelist.SelectedItem.ToString();
                string[] parts = selectedItem.Split('\\');
                string displayFileName = parts.Length > 1 ? parts[1] : selectedItem;

                DialogResult dr = MessageBox.Show($"Are you sure you want to delete '{displayFileName}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    string subfolder = parts[0];
                    string fileName = parts.Length > 1 ? parts[1] : "";
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subfolder, fileName);
                    try
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            MessageBox.Show($"File '{displayFileName}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("The selected file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    refreshFileListCallback?.Invoke();
                }
            }
        }

        /// Handles editing a file.
        /// Opens the frmEditFile form with the selected file and, after closing,
        /// invokes the callback with the updated file string.
        /// <param name="cmbfilelist">ComboBox containing the files.</param>
        /// <param name="owner">Owner form to show the edit form modally.</param>
        /// <param name="updateFileCallback">Callback to update file info after editing.</param>
        public static void EditFile(ComboBox cmbfilelist, Form owner, Action<string> updateFileCallback)
        {
            if ((cmbfilelist).SelectedIndex > 0)
            {
                string selectedItem = cmbfilelist.SelectedItem.ToString();
                frmEditFile editFileForm = new frmEditFile(selectedItem);
                editFileForm.ShowDialog(owner);
                updateFileCallback?.Invoke(editFileForm.UpdatedFileString);
            }
        }

        /// Handles updating a file in frmEditFile.
        /// Validates inputs, confirms changes, writes the new content (and renames/moves the file if needed),
        /// and then invokes a callback with the new file string.
        /// <param name="originalFullString">The original full string (subfolder\filename) of the file.</param>
        /// <param name="originalFileContent">The original content of the file.</param>
        /// <param name="newSubfolder">The new subfolder selected.</param>
        /// <param name="newFileName">The new file name.</param>
        /// <param name="newFileContent">The updated file content.</param>
        /// <param name="filePath">The original file's full path.</param>
        /// <param name="currentForm">The current frmEditFile instance (to be closed on success).</param>
        /// <param name="updateFileCallback">Callback to update the file string (e.g. in frmMain).</param>
        public static void UpdateFile(string originalFullString, string originalFileContent, string newSubfolder, string newFileName, string newFileContent, string filePath, Form currentForm, Action<string> updateFileCallback)
        {
            string newFullString = newSubfolder + "\\" + newFileName;
            bool isNameChanged = !newFullString.Equals(originalFullString, StringComparison.OrdinalIgnoreCase);
            bool isContentChanged = !newFileContent.Equals(originalFileContent, StringComparison.Ordinal);
            if (!isNameChanged && !isContentChanged)
            {
                MessageBox.Show("No changes are made.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string confirmMessage = $"Are you sure you want to update \"{originalFullString}\" ";
            if (isNameChanged && isContentChanged)
                confirmMessage += $"to \"{newFullString}\" and its content?";
            else if (isNameChanged)
                confirmMessage += $"to \"{newFullString}\"?";
            else if (isContentChanged)
                confirmMessage += "'s content?";

            if (MessageBox.Show(confirmMessage, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string newFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newSubfolder, newFileName);
            try
            {
                string[] newFileLines = newFileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                File.WriteAllLines(newFilePath, newFileLines);

                if (!newFilePath.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }
                MessageBox.Show("File updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateFileCallback?.Invoke(newFullString);
                currentForm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
