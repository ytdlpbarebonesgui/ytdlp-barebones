using ytdlp_barebones.Helpers.Display;

namespace ytdlp_barebones.Helpers.Lists
{
    public class ConfigList
    {
        // Fields
        private ComboBox cmbconfigcategory;
        private ComboBox cmbloadconfig;
        private TextBox txtytdlpcommand;
        private ComboBox cmbeditconfigcategory; // This is used only in frmConfigManager
        private bool isRefreshing = false; // add this field to ConfigList
        private bool isUpdatingEditConfig = false; // Flag to prevent recursion

        // Constructor
        // The cmbeditconfigcategory parameter is optional (frmMain won’t pass it)
        public ConfigList(ComboBox cmbconfigcategory, ComboBox cmbloadconfig, TextBox txtytdlpcommand, ComboBox cmbeditconfigcategory = null)
        {
            this.cmbconfigcategory = cmbconfigcategory;
            this.cmbloadconfig = cmbloadconfig;
            this.txtytdlpcommand = txtytdlpcommand;
            this.cmbeditconfigcategory = cmbeditconfigcategory;
        }

        // Public properties
        // Provide a public property for the flag so the form can check it
        public bool IsRefreshing { get { return isRefreshing; } }
        // Helper method: To reduce repetition of IsRefreshing method
        public bool IsProcessing() { return IsRefreshing; }

        // Public Methods
        // (Refresh Methods)
        // Refresh the config category combobox (cmbconfigcategory)
        public void RefreshConfigCategoryList()
        {
            isRefreshing = true; // Signal refresh in progress

            // Remember the current category (defaulting to "all" if nothing is selected)
            string currentCategory = cmbconfigcategory.SelectedItem?.ToString() ?? "all";

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFolderPath = Path.Combine(appDirectory, "config");

            // If config subfolder doesn't exist, create one
            Directory.CreateDirectory(configFolderPath);

            cmbconfigcategory.BeginUpdate();
            cmbconfigcategory.Items.Clear(); // Clear existing items
            cmbconfigcategory.Items.Add("all"); // Add "all" as the first item

            // Get first-level subdirectories inside "config"
            string[] subFolders = Directory.GetDirectories(configFolderPath);
            foreach (string folder in subFolders)
            {
                // Check if the folder contains any .txt files (config files) at the top level.
                string[] configFiles = Directory.GetFiles(folder, "*.txt", SearchOption.TopDirectoryOnly);
                if (configFiles.Length == 0)
                {
                    // The folder is empty; delete it and skip adding it.
                    try
                    {
                        Directory.Delete(folder, true);
                    }
                    catch (Exception)
                    {
                        // Ignoring exception errors for deletion
                    }
                    continue;
                }

                string folderName = Path.GetFileName(folder);
                if (!cmbconfigcategory.Items.Contains(folderName)) cmbconfigcategory.Items.Add(folderName);
            }

            // Re-select previously chosen category if it still exists.
            int indexToSelect = cmbconfigcategory.Items.IndexOf(currentCategory);
            cmbconfigcategory.SelectedIndex = indexToSelect >= 0 ? indexToSelect : 0;

            cmbconfigcategory.EndUpdate();
            ComboBoxHelper.ExpandComboBoxDropDownWidth(cmbconfigcategory);

            isRefreshing = false;
        }
        // RefreshConfigList to use for the file names.
        public void RefreshConfigList(bool addDefaultItem = false, bool selectDefaultIndex = false)
        {
            // Refresh category list first.
            if (cmbconfigcategory != null) RefreshConfigCategoryList();

            // Clear and update cmbloadconfig.
            cmbloadconfig.Items.Clear();
            // Add "-- Load Configuration --" if addDefaultItem is set to true
            if (addDefaultItem && cmbconfigcategory.SelectedItem == "all") cmbloadconfig.Items.Add("-- Load Configuration --");

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFolderPath = Path.Combine(appDirectory, "config");

            string selectedCategory = "all";
            if (cmbconfigcategory != null && cmbconfigcategory.SelectedItem != null) selectedCategory = cmbconfigcategory.SelectedItem.ToString();

            string[] configFiles = new string[0];
            if (Directory.Exists(configFolderPath))
            {
                if (selectedCategory.Equals("all", StringComparison.OrdinalIgnoreCase))
                    configFiles = Directory.GetFiles(configFolderPath, "*.txt", SearchOption.AllDirectories); // Get all files in config and category subfolders.
                else
                {
                    string specificFolderPath = Path.Combine(configFolderPath, selectedCategory);
                    if (Directory.Exists(specificFolderPath)) configFiles = Directory.GetFiles(specificFolderPath, "*.txt", SearchOption.TopDirectoryOnly);
                }

                // Collect file names without extension.
                List<string> configNames = new List<string>();
                foreach (string configFile in configFiles)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(configFile);
                    configNames.Add(fileNameWithoutExtension);
                }

                // Sort using our custom comparer.
                configNames.Sort(new ComboBoxHelper.CustomConfigComparer());

                // Add sorted items to the combo box.
                foreach (var name in configNames) cmbloadconfig.Items.Add(name);
            }

            ComboBoxHelper.ExpandComboBoxDropDownWidth(cmbloadconfig);

            // cmbeditconfigcategory: mirror the category list from cmbconfigcategory, EXCLUDING "all".
            if (cmbeditconfigcategory != null && cmbconfigcategory != null)
            {
                cmbeditconfigcategory.BeginUpdate();
                cmbeditconfigcategory.Items.Clear();
                foreach (var item in cmbconfigcategory.Items)
                {
                    string categoryItem = item.ToString();
                    if (!categoryItem.Equals("all", StringComparison.OrdinalIgnoreCase)) cmbeditconfigcategory.Items.Add(categoryItem); // Skip "all"
                }
                // Set selected index of cmbeditconfigcategory equal to cmbconfigcategory (if valid).
                int selectedIndex = cmbconfigcategory.SelectedIndex;
                if (selectedIndex > 0)
                    cmbeditconfigcategory.SelectedIndex = selectedIndex - 1; // Prevent selecting "all"
                else
                    cmbeditconfigcategory.SelectedIndex = 0; // Default to first valid folder

                cmbeditconfigcategory.EndUpdate();
                ComboBoxHelper.ExpandComboBoxDropDownWidth(cmbeditconfigcategory);
            }

            // Auto-select the first item in cmbloadconfig if needed.
            if (selectDefaultIndex && cmbloadconfig.Items.Count > 0) cmbloadconfig.SelectedIndex = 0;
        }
        // (Configuration Selection/Update Methods)
        // This method finds the sub-subfolder where the selected file is located and updates cmbconfigcategory
        public void AutoSelectEditConfigForConfig()
        {
            if (isUpdatingEditConfig) return; // Prevent recursion
            isUpdatingEditConfig = true; // Set flag to prevent recursive updates

            // Get the selected config name
            string selectedConfig = cmbloadconfig.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedConfig))
            {
                isUpdatingEditConfig = false;
                return;
            }

            // Get the application's directory and config folder path
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFolderPath = Path.Combine(appDirectory, "config");

            // Find the selected config file's actual path
            string[] matchingFiles = Directory.GetFiles(configFolderPath, selectedConfig + ".txt", SearchOption.AllDirectories);
            if (matchingFiles.Length == 0)
            {
                isUpdatingEditConfig = false;
                return; // File doesn't exist, do nothing
            }

            string configFilePath = matchingFiles[0];

            // Determine which subfolder it belongs to
            string parentFolder = Path.GetDirectoryName(configFilePath);
            if (parentFolder == null)
            {
                isUpdatingEditConfig = false;
                return;
            }

            // Get relative path (e.g., "mp3", "mp4" if inside sub-subfolders)
            string relativePath = parentFolder.Replace(configFolderPath + Path.DirectorySeparatorChar, "");

            // If the file is in the "config" root folder, clear selection in cmbeditconfigcategory
            if (parentFolder == configFolderPath)
            {
                cmbeditconfigcategory.SelectedIndex = -1; // No category selected (blank)
                isUpdatingEditConfig = false;
                return;
            }

            // Find and select the correct subfolder in cmbeditconfigcategory
            for (int i = 0; i < cmbeditconfigcategory.Items.Count; i++)
            {
                if (cmbeditconfigcategory.Items[i].ToString().Equals(relativePath, StringComparison.OrdinalIgnoreCase))
                {
                    if (cmbeditconfigcategory.SelectedIndex != i) cmbeditconfigcategory.SelectedIndex = i; // Only update if it's different
                    isUpdatingEditConfig = false;
                    return;
                }
            }

            isUpdatingEditConfig = false; // Reset flag after execution
        }
        // CheckConfig to correctly find the config file
        // Bsed on the current category.
        public void CheckConfig(ComboBox cmbloadconfig, ComboBox cmbconfigcategory, TextBox txtytdlpcommand)
        {
            if (cmbloadconfig.SelectedItem != null)
            {
                string selectedConfig = cmbloadconfig.SelectedItem.ToString();
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string configFolderPath = Path.Combine(appDirectory, "config");

                string configFilePath = "";
                if (cmbconfigcategory != null && cmbconfigcategory.SelectedItem != null &&
                    !cmbconfigcategory.SelectedItem.ToString().Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    string specificFolderPath = Path.Combine(configFolderPath, cmbconfigcategory.SelectedItem.ToString());
                    configFilePath = Path.Combine(specificFolderPath, selectedConfig + ".txt");
                }
                else
                {
                    // Search in the base folder first.
                    string basePath = Path.Combine(configFolderPath, selectedConfig + ".txt");
                    if (File.Exists(basePath))
                        configFilePath = basePath;
                    else
                    {
                        // Search in subdirectories if not found in the base folder.
                        string[] files = Directory.GetFiles(configFolderPath, selectedConfig + ".txt", SearchOption.AllDirectories);
                        if (files.Length > 0)
                            configFilePath = files[0];
                    }
                }

                if (File.Exists(configFilePath))
                {
                    string[] configLines = ComboBoxHelper.ReadAllLinesSafely(configFilePath);
                    txtytdlpcommand.Clear();
                    if (configLines.Length > 0)
                        txtytdlpcommand.Text = configLines[0];
                }
                else if (selectedConfig == "-- Load Configuration --")
                    txtytdlpcommand.Clear();
                else
                {
                    MessageBox.Show("The selected configuration file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbloadconfig.Items.Remove(selectedConfig);
                    cmbloadconfig.SelectedIndex = 0;
                }
            }
        }
        public void RememberConfig()
        {
            string selectedConfig = cmbloadconfig.SelectedItem?.ToString();
            // Remember the category string (defaulting to "all" if nothing is selected)
            string rememberedCategory = (cmbconfigcategory?.SelectedItem?.ToString() ?? "all").Trim();

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFolderPath = Path.Combine(appDirectory, "config");
            string configFilePath = string.Empty;

            // If a specific category is applied (i.e. not "all"), check in that subfolder.
            if (!string.IsNullOrEmpty(rememberedCategory) &&
                !rememberedCategory.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                string specificFolderPath = Path.Combine(configFolderPath, rememberedCategory);
                configFilePath = Path.Combine(specificFolderPath, selectedConfig + ".txt");
            }
            else
            {
                // First, check in the base folder.
                string basePath = Path.Combine(configFolderPath, selectedConfig + ".txt");
                if (File.Exists(basePath))
                    configFilePath = basePath;
                else
                {
                    // Search in all subdirectories if not found in the base folder.
                    string[] files = Directory.GetFiles(configFolderPath, selectedConfig + ".txt", SearchOption.AllDirectories);
                    if (files.Length > 0) configFilePath = files[0];
                }
            }

            // Refresh the configuration list.
            RefreshConfigList(addDefaultItem: true);

            // Re-select the remembered category in cmbconfigcategory if it exists.
            if (cmbconfigcategory != null)
            {
                foreach (var item in cmbconfigcategory.Items)
                {
                    if (item.ToString().Equals(rememberedCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        cmbconfigcategory.SelectedItem = item;
                        break;
                    }
                }
            }

            // If the config file exists (or the default item is selected), re-select the configuration.
            if (File.Exists(configFilePath) || selectedConfig == "-- Load Configuration --")
                cmbloadconfig.SelectedItem = selectedConfig;
            else
                cmbloadconfig.SelectedIndex = 0;
        }
    }
}