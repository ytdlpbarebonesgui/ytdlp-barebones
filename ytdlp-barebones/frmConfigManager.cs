using ytdlp_barebones.Helpers.Buttons;
using ytdlp_barebones.Helpers.Display;
using ytdlp_barebones.Helpers.Events;
using ytdlp_barebones.Helpers.Events.TextEvents;
using ytdlp_barebones.Helpers.Lists;

namespace ytdlp_barebones
{
    public partial class frmConfigManager : Form
    {
        private ConfigList ConfigList;
        private string rememberedCategory;
        private string rememberedConfig;

        public frmConfigManager(string rememberedcategory, string rememberedConfig)
        {
            InitializeComponent();
            this.rememberedCategory = rememberedcategory;
            this.rememberedConfig = rememberedConfig;
            ConfigList = new ConfigList(cmbconfigcategory, cmbloadconfig, txtytdlpcommand, cmbeditconfigcategory);
            InitializeEvents();
        }

        //Load
        private void frmConfigManager_Load(object sender, EventArgs e)
        {
            // Apply the selected theme to the form.
            Theme.Load(this);

            // If a refresh is in progress, don't process the event.
            if (ConfigList.IsProcessing()) return;

            // Refresh both comboboxes (the category and config lists)
            ConfigList.RefreshConfigList(addDefaultItem: false, selectDefaultIndex: false);

            // Automatically select the remembered category in cmbconfigcategory (if available)
            if (!string.IsNullOrEmpty(rememberedCategory))
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

            // If the remembered config is the default option "-- Load Configuration --"
            // or is empty, then default cmbloadconfig to index 0.
            if (string.IsNullOrEmpty(rememberedConfig) || rememberedConfig.Equals("-- Load Configuration --", StringComparison.OrdinalIgnoreCase))
            {
                if (cmbloadconfig.Items.Count > 0) cmbloadconfig.SelectedIndex = 0;
            }
            else
            {
                // Otherwise, try to select the remembered config in cmbloadconfig.
                bool found = false;
                foreach (var item in cmbloadconfig.Items)
                {
                    if (item.ToString().Equals(rememberedConfig, StringComparison.OrdinalIgnoreCase))
                    {
                        cmbloadconfig.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
                if (!found && cmbloadconfig.Items.Count > 0) cmbloadconfig.SelectedIndex = 0;
            }

            // Immediately clear the selection so that nothing is highlighted.
            cmbeditconfigcategory.SelectionStart = 0;
            cmbeditconfigcategory.SelectionLength = 0;
        }

        //Events
        private void InitializeEvents()
        {
            // (Buttons)
            //uses ConfigButtons.cs as helper
            btnupdate.Click += (s, e) =>
            {
                ConfigButtons.UpdateConfig(
                    cmbloadconfig.SelectedItem?.ToString(),
                    txtytdlpcommand.Text,
                    txtname.Text.Trim(),
                    cmbeditconfigcategory.Text.Trim(),
                    cmbloadconfig,
                    () => { ConfigList.RefreshConfigList(); }  // Refresh callback
                );
            };
            btndelete.Click += (s, e) =>
            {
                ConfigButtons.DeleteConfig(
                    cmbloadconfig.SelectedItem?.ToString(),
                    cmbloadconfig,
                    cmbconfigcategory,
                    this,
                    () => { ConfigList.RefreshConfigList(); }  // Refresh callback
                );
            };
            btndownload2config.Click += (s, e) =>
            {
                ConfigButtons.DownloadConfig(
                    cmbloadconfig.SelectedItem?.ToString(),
                    txtytdlpcommand.Text
                );
            };
            // (Key Presses)
            txtname.KeyPress += KeyEvents.InvalidChars;
            txtytdlpcommand.KeyPress += KeyEvents.PreventNewLine;
            // (Text Changed)
            txtytdlpcommand.TextChanged += (s,e) =>
            {
                // Cast sender to TextBox.
                if (s is TextBox txtBox)
                    TextValidationEvents.ValidateYtDlpCommandText(txtBox); // Pass the textbox and the event handler method itself.

                // Always check for nulls after syntax correction
                TextValidationEvents.ValidateNull(txtytdlpcommand, txtname, btndownload2config, btnupdate);
            };
            // Always check for nulls after syntax correction
            txtname.TextChanged += (s,e) => TextValidationEvents.ValidateNull(txtytdlpcommand, txtname, btndownload2config, btnupdate);
            // (Selected Index Changed)
            cmbloadconfig.SelectedIndexChanged += (s,e) =>
            {
                txtname.Clear();
                txtname.Text = cmbloadconfig.Text;

                // Passes the UI controls (ComboBoxes and TextBox) explicitly to CheckConfig 
                ConfigList.CheckConfig(cmbloadconfig, cmbconfigcategory, txtytdlpcommand);

                // Automatically set cmbconfigcategory based on where the file is located
                ConfigList.AutoSelectEditConfigForConfig();
            };
            cmbconfigcategory.SelectedIndexChanged += (s, e) =>
            {
                if (ConfigList.IsProcessing()) return;
                // Refresh config list and auto-select the first config file.
                ConfigList.RefreshConfigList(addDefaultItem: false, selectDefaultIndex: true);
            };
        }
    }
}
