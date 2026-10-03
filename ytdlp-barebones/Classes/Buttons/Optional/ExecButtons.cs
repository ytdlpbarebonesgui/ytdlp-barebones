using ytdlp_barebones.Helpers.Display;

namespace ytdlp_barebones.Helpers.Buttons.Optional
{
    public static class ExecButtons
    {
        // frmMain
        /// Handles the Save Exec button click from frmMain.
        /// Opens frmSaveExec if the exec textbox is not empty,
        /// then invokes the provided callback.
        /// <param name="execCommand">The content of the exec textbox.</param>
        /// <param name="afterSaveCallback">Callback to run after closing frmSaveExec.</param>
        /// <param name="owner">Owner form for the modal dialog.</param>
        public static void SaveExecFromMain(string execCommand, Action afterSaveCallback, Form owner)
        {
            frmEditExec saveExecForm = new frmEditExec(execCommand);
            saveExecForm.ShowDialog(owner);
            afterSaveCallback?.Invoke();
        }

        // frmEditExec
        /// <summary>
        /// Handles the Save button click from frmSaveExec.
        /// Validates input, saves the exec file, shows a success message,
        /// and closes the form if successful.
        /// </summary>
        /// <param name="txtName">Textbox containing the exec name.</param>
        /// <param name="txtExec">Textbox containing the exec command.</param>
        /// <param name="currentForm">The current form (frmSaveExec) to be closed on success.</param>
        public static void SaveExecFile(TextBox txtName, TextBox txtExec, Form currentForm)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string execFolderPath = Path.Combine(appDirectory, "exec");

            // If exec subfolder doesn't exist, create one
            Directory.CreateDirectory(execFolderPath);

            string execFileName = txtName.Text.Trim() + ".txt";
            string execFilePath = Path.Combine(execFolderPath, execFileName);

            if (File.Exists(execFilePath))
            {
                MessageBox.Show("An exec file with the same name already exists.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string execCommand = txtExec.Text.Trim();
                File.WriteAllText(execFilePath, execCommand);

                MessageBox.Show("Exec file saved successfully.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                currentForm.Close();
            }
        }

        public static void EditExecFromMain(ComboBox cmbExec, Action afterSaveCallback, Form owner)
        {
            if (cmbExec == null || cmbExec.SelectedItem == null) return;

            string selectedName = cmbExec.SelectedItem.ToString();
            if (selectedName.Equals("-- Load Exec --", StringComparison.OrdinalIgnoreCase)) return;

            string execFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exec");
            string path = Path.Combine(execFolder, selectedName + ".txt");

            string content = "";
            if (File.Exists(path))
            {
                try
                {
                    // Use the safe reader if available; fall back to File.ReadAllLines if something goes wrong.
                    content = ComboBoxHelper.ReadAllLinesSafely(path).FirstOrDefault() ?? "";
                }
                catch
                {
                    try { content = File.ReadAllLines(path).FirstOrDefault() ?? ""; } catch { content = ""; }
                }
            }

            var frm = new frmEditExec(content, selectedName)
            {
                OnUpdateSuccess = afterSaveCallback
            };
            frm.ShowDialog(owner);
        }

        /// <summary>
        /// Updates an existing exec file (rename and/or update content).
        /// </summary>
        /// <param name="txtName">Textbox with the (possibly new) name (no extension).</param>
        /// <param name="txtExec">Textbox with the new exec command.</param>
        /// <param name="currentForm">The modal form to close on success.</param>
        /// <param name="originalName">Original filename (without extension) being edited.</param>
        /// <param name="originalContent">Original content of the exec file (for confirmation message).</param>
        /// <param name="refreshCallback">Optional action invoked after a successful update (e.g., to refresh combobox).</param>
        public static void UpdateExecFile(TextBox txtName, TextBox txtExec, Form currentForm, string originalName, string originalContent, Action refreshCallback = null)
        {
            if (txtName == null || txtExec == null || string.IsNullOrWhiteSpace(originalName))
                return;

            string newName = txtName.Text.Trim();
            string newContent = txtExec.Text.Trim();

            if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newContent))
            {
                MessageBox.Show("Exec name and command cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string execFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exec");
            Directory.CreateDirectory(execFolder);

            string oldPath = Path.Combine(execFolder, originalName + ".txt");
            string newPath = Path.Combine(execFolder, newName + ".txt");

            // Confirm with the user (show old and new)
            var dr = MessageBox.Show(
                "Are you want to update this exec file?\r\n\r\n" +
                "Current:\r\n" + originalName + "\r\n" + originalContent + "\r\n\r\n" +
                "New:\r\n" + newName + "\r\n" + newContent,
                "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                // If renaming and target exists, delete target first (overwrite semantics)
                if (!string.Equals(oldPath, newPath, StringComparison.OrdinalIgnoreCase) && File.Exists(newPath))
                {
                    File.Delete(newPath);
                }

                // Write the new content (this will create the file if it doesn't exist)
                File.WriteAllText(newPath, newContent);

                // If the file was renamed, remove the old file if it still exists and paths differ
                if (!string.Equals(oldPath, newPath, StringComparison.OrdinalIgnoreCase) && File.Exists(oldPath))
                {
                    try { File.Delete(oldPath); } catch { /* ignore deletion errors */ }
                }

                MessageBox.Show("Exec file updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Invoke optional refresh callback (e.g., ExecList.RefreshExecList)
                refreshCallback?.Invoke();

                // Close the edit form
                currentForm?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating exec file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Deletes the currently selected exec file.
        /// </summary>
        public static void DeleteExec(ComboBox cmbExec, Action refreshCallback)
        {
            if (cmbExec?.SelectedItem == null) return;

            string selected = cmbExec.SelectedItem.ToString();
            if (selected.Equals("-- Load Exec --", StringComparison.OrdinalIgnoreCase))
                return;

            string execFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exec");
            string filePath = Path.Combine(execFolder, selected + ".txt");

            if (File.Exists(filePath))
            {
                DialogResult confirm = MessageBox.Show(
                    $"Are you sure you want to delete the '{selected}' exec file?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    File.Delete(filePath);
                    refreshCallback?.Invoke();
                }
            }
            else
            {
                MessageBox.Show("Exec file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                refreshCallback?.Invoke();
            }
        }
    }
}
