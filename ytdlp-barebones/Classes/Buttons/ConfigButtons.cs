namespace ytdlp_barebones.Helpers.Buttons
{
    public static class ConfigButtons
    {
        #region frmMain
        /// Handles the Save Config button click from frmMain.
        /// Opens frmSaveConfig if the yt-dlp command textbox is not empty,
        /// then invokes the provided callback.
        /// <param name="ytDlpCommand">The content of the yt-dlp command textbox.</param>
        /// <param name="afterSaveCallback">Callback to run after closing frmSaveConfig.</param>
        /// <param name="owner">Owner form for the modal dialog.</param>
        public static void SaveConfigFromMain(string ytDlpCommand, Action afterSaveCallback, Form owner)
        {
            frmSaveConfig saveConfigForm = new frmSaveConfig(ytDlpCommand);
            saveConfigForm.ShowDialog(owner);
            afterSaveCallback?.Invoke();
        }

        /// Handles the Config Manage button click from frmMain.
        /// Opens frmConfigManager if at least one config file exists,
        /// then invokes the provided callback.
        /// <param name="cmbLoadConfig">The combobox holding the configs.</param>
        /// <param name="cmbConfigCategory">The combobox holding the category.</param>
        /// <param name="afterManageCallback">Callback to run after closing frmConfigManager.</param>
        public static void ConfigManage(ComboBox cmbLoadConfig, ComboBox cmbConfigCategory, Action afterManageCallback)
        {
            string configFolderPath = Path.Combine(Application.StartupPath, "config");

            if (Directory.GetFiles(configFolderPath, "*.txt", SearchOption.AllDirectories).Length == 0)
            {
                MessageBox.Show("No configurations are saved for the manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Optionally, the form can refresh directories prior to this call.
            string rememberedCategory = cmbConfigCategory.SelectedItem?.ToString() ?? "";
            string rememberedConfig = cmbLoadConfig.SelectedItem?.ToString() ?? "";
            frmConfigManager configManager = new frmConfigManager(rememberedCategory, rememberedConfig);
            configManager.ShowDialog();
            afterManageCallback?.Invoke();
        }
        #endregion

        #region frmSaveConfig
        /// Handles the Save button click from frmSaveConfig.
        /// Validates input, saves the configuration file, shows a success message,
        /// and closes the form if successful.
        /// <param name="txtName">Textbox containing the config name.</param>
        /// <param name="cmbEditConfigCategory">Combobox with an optional category folder.</param>
        /// <param name="txtYtDlpCommand">Textbox containing the yt-dlp command.</param>
        /// <param name="currentForm">The current form (frmSaveConfig) to be closed on success.</param>
        public static void SaveConfigFile(TextBox txtName, ComboBox cmbEditConfigCategory, TextBox txtYtDlpCommand, Form currentForm)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFolderPath = Path.Combine(appDirectory, "config");

            // If config subfolder doesn't exist, create one
            Directory.CreateDirectory(configFolderPath);

            string configFileName = txtName.Text.Trim() + ".txt";
            string targetFolder = configFolderPath;
            string categoryText = cmbEditConfigCategory.Text.Trim();

            if (!string.IsNullOrEmpty(categoryText))
            {
                targetFolder = Path.Combine(configFolderPath, categoryText);

                // If the category folder name doesn't exist, then create a subfolder
                Directory.CreateDirectory(targetFolder);
            }

            string configFilePath = Path.Combine(targetFolder, configFileName);

            if (File.Exists(configFilePath))
                MessageBox.Show("A config file with the same name already exists in the targeted category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string commandText = txtYtDlpCommand.Text;
                File.WriteAllText(configFilePath, commandText);
                MessageBox.Show("Config file saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentForm.Close();
            }
        }
        #endregion

        #region frmConfigManager
        /// Handles updating an existing configuration from frmConfigManager.
        /// <param name="selectedConfig">The originally selected configuration name.</param>
        /// <param name="newYtDlpCommand">The new yt-dlp command text.</param>
        /// <param name="newConfigName">The new configuration name.</param>
        /// <param name="newCategory">The new category (optional subfolder).</param>
        /// <param name="cmbLoadConfig">The combobox listing configurations; its SelectedItem will be updated.</param>
        /// <param name="refreshConfigCallback">Callback to refresh the config list (e.g. ConfigList.RefreshConfigList).</param>
        public static void UpdateConfig(
            string selectedConfig,
            string newYtDlpCommand,
            string newConfigName,
            string newCategory,
            ComboBox cmbLoadConfig,
            Action refreshConfigCallback)
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string configFolderPath = Path.Combine(appDirectory, "config");

                // Locate the file by searching in all subdirectories.
                string[] matchingFiles = Directory.GetFiles(configFolderPath, selectedConfig + ".txt", SearchOption.AllDirectories);
                if (matchingFiles.Length == 0)
                {
                    MessageBox.Show("The selected configuration file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string currentConfigFilePath = matchingFiles[0];
                string currentConfigContent = File.ReadAllText(currentConfigFilePath).Trim();

                // Get the current directory (could be a subfolder) and determine the target folder.
                string currentDirectory = Path.GetDirectoryName(currentConfigFilePath);
                string targetDirectory = !string.IsNullOrWhiteSpace(newCategory)
                                            ? Path.Combine(configFolderPath, newCategory)
                                            : configFolderPath;

                // Determine if the configuration name is changing.
                bool renameFile = !selectedConfig.Equals(newConfigName, StringComparison.OrdinalIgnoreCase);
                string targetName = renameFile ? newConfigName : selectedConfig;
                string targetFilePath = Path.Combine(targetDirectory, targetName + ".txt");

                // Check if nothing has changed.
                bool sameCommand = currentConfigContent.Equals(newYtDlpCommand.Trim(), StringComparison.Ordinal);
                bool sameName = !renameFile;
                bool sameFolder = currentDirectory.Equals(targetDirectory, StringComparison.OrdinalIgnoreCase);
                if (sameCommand && sameName && sameFolder)
                {
                    MessageBox.Show("No changes are made.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Build a confirmation message with relative paths.
                string beforeRelativePath = Path.GetRelativePath(configFolderPath, currentConfigFilePath);
                string afterRelativePath = Path.GetRelativePath(configFolderPath, targetFilePath);
                string infoMessage =
                    $"Are you sure you want to update '{selectedConfig}'?\n\n" +
                    "Before:\n" +
                    $"Path: {beforeRelativePath}\n" +
                    (renameFile ? $"Name: {selectedConfig}\n" : "") +
                    $"{currentConfigContent}\n\n" +
                    "After:\n" +
                    $"Path: {afterRelativePath}\n" +
                    (renameFile ? $"Name: {newConfigName}\n" : "") +
                    newYtDlpCommand;
                DialogResult confirmResult = MessageBox.Show(infoMessage, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult != DialogResult.Yes)
                    return;

                // Overwrite the file with the new command.
                File.WriteAllText(currentConfigFilePath, newYtDlpCommand);

                // If the file should be moved or renamed.
                if (!currentConfigFilePath.Equals(targetFilePath, StringComparison.OrdinalIgnoreCase))
                {
                    if (File.Exists(targetFilePath))
                    {
                        MessageBox.Show("A configuration file with the same name already exists in the target folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Directory.CreateDirectory(targetDirectory);
                    File.Move(currentConfigFilePath, targetFilePath);
                }

                MessageBox.Show("Configuration updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshConfigCallback?.Invoke();
                cmbLoadConfig.SelectedItem = targetName;
            }

        /// Handles deleting a configuration from frmConfigManager.
        /// <param name="selectedConfig">The configuration name to delete.</param>
        /// <param name="cmbLoadConfig">The combobox listing configurations.</param>
        /// <param name="cmbConfigCategory">The combobox for category selection.</param>
        /// <param name="currentForm">The current frmConfigManager (to close if no configs remain).</param>
        /// <param name="refreshConfigCallback">Callback to refresh the configuration list.</param>
        public static void DeleteConfig(
            string selectedConfig,
            ComboBox cmbLoadConfig,
            ComboBox cmbConfigCategory,
            Form currentForm,
            Action refreshConfigCallback)
            {
                if (string.IsNullOrEmpty(selectedConfig))
                    return;

                string confirmationMessage = $"Are you sure you want to delete the configuration \"{selectedConfig}\"?";
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string configFolderPath = Path.Combine(appDirectory, "config");
                string[] matchingFiles = Directory.GetFiles(configFolderPath, selectedConfig + ".txt", SearchOption.AllDirectories);

                if (matchingFiles.Length > 0)
                {
                    string fileToDelete = matchingFiles[0];
                    string fileDirectory = Path.GetDirectoryName(fileToDelete);
                    File.Delete(fileToDelete);
                    MessageBox.Show("Configuration deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // If the deleted file was in a subfolder, attempt to remove the category folder if empty.
                    if (!string.IsNullOrEmpty(fileDirectory) &&
                        !fileDirectory.Equals(configFolderPath, StringComparison.OrdinalIgnoreCase))
                    {
                        string[] remainingFiles = Directory.GetFiles(fileDirectory, "*.txt", SearchOption.TopDirectoryOnly);
                        if (remainingFiles.Length == 0)
                        {
                            bool deletionSucceeded = false;
                            try
                            {
                                Directory.Delete(fileDirectory, true);
                                deletionSucceeded = !Directory.Exists(fileDirectory);
                            }
                            catch (IOException)
                            {
                                deletionSucceeded = !Directory.Exists(fileDirectory);
                                if (!deletionSucceeded)
                                    MessageBox.Show("The category folder is in use by another process but it'll get deleted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                deletionSucceeded = !Directory.Exists(fileDirectory);
                                if (!deletionSucceeded)
                                    MessageBox.Show("Access denied when attempting to delete the category folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (deletionSucceeded)
                                MessageBox.Show("This cateory no longer has configurations, deleting the file and the category.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The selected configuration file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                refreshConfigCallback?.Invoke();

                // Check if any configuration files remain.
                string[] allFiles = Directory.GetFiles(configFolderPath, "*.txt", SearchOption.AllDirectories);
                if (allFiles.Length == 0)
                {
                    MessageBox.Show("No configurations left. Closing the Configuration Manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentForm.Close();
                    return;
                }
                else
                {
                    if (cmbConfigCategory.Items.Count > 0)
                        cmbConfigCategory.SelectedIndex = 0;
                    if (cmbLoadConfig.Items.Count > 0)
                        cmbLoadConfig.SelectedIndex = 0;
                }
            }

        /// Handles downloading a configuration file from frmConfigManager.
        /// Uses the current yt-dlp command text (instead of reading from file) for the download.
        /// <param name="selectedConfig">The configuration name to download.</param>
        /// <param name="ytDlpCommand">The yt-dlp command text to download.</param>
        public static void DownloadConfig(string selectedConfig, string ytDlpCommand)
        {
            string warningMessage =
                "NOTE: If the configuration contains placeholders (like <dir>, <url>, etc.), " +
                "you must clear them out using a text editor before using this configuration.\n\n" +
                $"Do you want to download the '{selectedConfig}' configuration file?";
            DialogResult result = MessageBox.Show(warningMessage, "Confirm Download", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string saveDirectory = folderDialog.SelectedPath;
                    string configFileName = selectedConfig + ".config";
                    string configFilePathToSave = Path.Combine(saveDirectory, configFileName);

                    File.WriteAllText(configFilePathToSave, ytDlpCommand);
                    MessageBox.Show("Configuration file downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
    }
}
