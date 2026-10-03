using ytdlp_barebones.Helpers.Buttons.Optional;
using ytdlp_barebones.Helpers.Display;
using ytdlp_barebones.Helpers.Events;
using ytdlp_barebones.Helpers.Events.TextEvents;

namespace ytdlp_barebones
{
    public partial class frmEditFile : Form
    {
        // Store the original full string (e.g. "cookies\filename.ext")
        private string originalFullString;
        // Store the file's original content.
        private string originalFileContent;
        // Store the actual file path.
        private string filePath;
        // Public property to return either the updated or original file string.
        public string UpdatedFileString;

        public frmEditFile(string selectedItem)
        {
            InitializeComponent();
            originalFullString = selectedItem;
            // Initialize UpdatedFileString to the original value.
            UpdatedFileString = originalFullString;
            InitializeEvents();
        }

        // Events
        private void InitializeEvents()
        {
            // (Form)
            Load += (s, e) =>
            {
                // Apply theme
                Theme.Load(this);

                // Select the textbox 'txtfileandfolder'
                txtfileandfolder.Select();

                // Display the full string in txtfileandfolder.
                txtfileandfolder.Text = originalFullString;

                // Split the string to get subfolder and file name.
                string[] parts = originalFullString.Split('\\');
                if (parts.Length < 2)
                {
                    MessageBox.Show("Invalid file path format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                string subfolder = parts[0];
                string fileName = parts[1];

                // Set the cmbfilefolder combo box to the subfolder.
                cmbfilefolder.SelectedItem = subfolder;

                // Display the file name in txtrename.
                txtrename.Text = fileName;

                // Build the full file path.
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subfolder, fileName);
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("The file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // Read the file content line-by-line and join with proper newlines.
                string[] lines = File.ReadAllLines(filePath);
                originalFileContent = string.Join(Environment.NewLine, lines);
                txtfilecontent.Text = originalFileContent;
            };

            // (Buttons)
            // Update button uses the FileButtons.cs helper
            btnupdate.Click += (s, e) =>
            {
                FileButtons.UpdateFile(
                    originalFullString,
                    originalFileContent,
                    cmbfilefolder.SelectedItem.ToString(),
                    txtrename.Text,
                    txtfilecontent.Text,
                    filePath,
                    this,
                    (updatedFileString) => { /* For example, update frmMain selection */ }
                );
            };
            btncancel.Click += (s, e) => Close();
            // (Check Changed)
            cbedit.CheckedChanged += (s, e) =>
            {
                bool isEnabled = cbedit.Checked;
                txtfilecontent.ReadOnly = !isEnabled;

                // Toggle password visibility
                txtfilecontent.PasswordChar = isEnabled ? '\0' : '*';
            };
            // (Key Press)
            txtrename.KeyPress += KeyEvents.InvalidChars;
            // (Text changed)
            // Always check for nulls after syntax correction
            txtrename.TextChanged += (s, e) => TextValidationEvents.ValidateNull(txtfilecontent, txtrename, downloadBtn: null, updateBtn: btnupdate);
            txtfilecontent.TextChanged += (s, e) => TextValidationEvents.ValidateNull(txtfilecontent, txtrename, downloadBtn: null, updateBtn: btnupdate);
        }
    }
}