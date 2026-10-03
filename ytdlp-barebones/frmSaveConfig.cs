using ytdlp_barebones.Helpers.Buttons;
using ytdlp_barebones.Helpers.Display;
using ytdlp_barebones.Helpers.Events;
using ytdlp_barebones.Helpers.Events.TextEvents;

namespace ytdlp_barebones
{
    public partial class frmSaveConfig : Form
    {
        private string ytdlpcommand;

        public frmSaveConfig(string ytdlpcommand)
        {
            InitializeComponent();
            this.ytdlpcommand = ytdlpcommand;
            InitializeEvents();
        }

        // Load
        private void frmSaveConfig_Load(object sender, EventArgs e)
        {
            // Apply the theme
            Theme.Load(this);

            // Set the command text.
            txtytdlpcommand.Text = ytdlpcommand;

            // Get the app and config folder paths.
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFolderPath = Path.Combine(appDirectory, "config");

            // If config subfolder doesn't exist, create the subfolder
            Directory.CreateDirectory(configFolderPath);

            // Begin update to prevent flicker during item population.
            cmbeditconfigcategory.BeginUpdate();
            cmbeditconfigcategory.Items.Clear();

            // Get subfolders from the config folder.
            string[] subFolders = Directory.GetDirectories(configFolderPath);
            foreach (string folder in subFolders)
            {
                // Check if the folder contains any .txt config files.
                string[] configFiles = Directory.GetFiles(folder, "*.txt", SearchOption.TopDirectoryOnly);
                if (configFiles.Length == 0)
                {
                    // If no config files are found, delete the folder.
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

                // Add the folder name to the combobox if it isn't already added.
                string folderName = Path.GetFileName(folder);
                if (!cmbeditconfigcategory.Items.Contains(folderName)) cmbeditconfigcategory.Items.Add(folderName);
            }
            cmbeditconfigcategory.EndUpdate();

            // Optionally adjust the dropdown width for better display.
            ComboBoxHelper.ExpandComboBoxDropDownWidth(cmbeditconfigcategory);

            // Explicitly leave the selection blank.
            cmbeditconfigcategory.SelectedIndex = -1;
            cmbeditconfigcategory.Text = "";
        }

        // Events
        private void InitializeEvents()
        {
            // (Buttons)
            // Save buttons uses the ConfigButtons.cs helper
            btnsave.Click += (s, e) => ConfigButtons.SaveConfigFile(txtname, cmbeditconfigcategory, txtytdlpcommand, this);
            btncancel.Click += (s, e) => Close();
            // (Key Presses)
            txtname.KeyPress += KeyEvents.InvalidChars;
            txtytdlpcommand.KeyPress += KeyEvents.PreventNewLine;
            // (Text Changed)
            txtytdlpcommand.TextChanged += (s, e) =>
            {
                if (s is TextBox txtBox)
                    TextValidationEvents.ValidateYtDlpCommandText(txtBox); // Pass the textbox and the event handler method itself.

                // Always check for nulls after syntax correction
                TextValidationEvents.ValidateNull(txtytdlpcommand, txtname, downloadBtn: null, updateBtn: btnsave);
            };
            // Always check for nulls after syntax correction
            txtname.TextChanged += (s, e) => TextValidationEvents.ValidateNull(txtytdlpcommand, txtname, downloadBtn: null, updateBtn: btnsave);
        }
    }
}
