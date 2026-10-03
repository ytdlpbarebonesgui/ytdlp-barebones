using System.ComponentModel;
using ytdlp_barebones.Helpers.Buttons.Optional;
using ytdlp_barebones.Helpers.Display;
using ytdlp_barebones.Helpers.Events;
using ytdlp_barebones.Helpers.Events.TextEvents;

namespace ytdlp_barebones
{
    public partial class frmEditExec : Form
    {
        private string initialExec;
        private readonly string originalName;     // null when creating new
        private readonly bool isEditMode;

        // Callback that ExecButtons.EditExec will set so main form can refresh the list
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action OnUpdateSuccess { get; set; }

        // Constructors
        // Add mode
        public frmEditExec(string execCommand) : this(execCommand, null) { }
        // Edit mode (supply original filename without extension)
        public frmEditExec(string execCommand, string originalName)
        {
            InitializeComponent();
            initialExec = execCommand ?? "";
            this.originalName = string.IsNullOrWhiteSpace(originalName) ? null : originalName;
            this.isEditMode = !string.IsNullOrWhiteSpace(this.originalName);
        }

        // Load
        private void frmEditExec_Load(object sender, EventArgs e)
        {
            InitializeEvents();

            // Apply theme
            Theme.Load(this);

            // Prefill exec text
            txtexec.Text = initialExec;

            if (isEditMode)
            {
                // Put the current filename into txtname so user can rename if desired
                txtname.Text = originalName;

                // Update UI for edit mode
                this.Text = "Update your Exec Command";
                groupBox1.Text = "Update name of your current exec command";
                btnsave.Text = "Update";
                try
                {
                    btnsave.Image = Properties.Resources.pencil; // ensure pencil resource exists
                }
                catch
                {
                    // ignore missing resource
                }
            }

            // Initial validation call (if you want validation to run immediately)
            TextValidationEvents.ValidateNull(txtexec, txtname, downloadBtn: null, updateBtn: btnsave);
        }

        // Events
        private void InitializeEvents()
        {
            // (Buttons)
            btnsave.Click += (s, e) =>
            {
                // Call update — pass original name and original content
                if (isEditMode)
                    ExecButtons.UpdateExecFile(txtname, txtexec, this, originalName, initialExec, OnUpdateSuccess);
                else
                    ExecButtons.SaveExecFile(txtname, txtexec, this); // Save new exec
            };
            btncancel.Click += (s, e) => Close();
            // (Key Press)
            txtname.KeyPress += KeyEvents.InvalidChars;
            // (Text Changed)
            // Always check for nulls after syntax correction
            txtexec.TextChanged += (s, e) => TextValidationEvents.ValidateNull(txtexec, txtname, downloadBtn: null, updateBtn: btnsave);
            txtname.TextChanged += (s, e) => TextValidationEvents.ValidateNull(txtexec, txtname, downloadBtn: null, updateBtn: btnsave);
        }
    }
}
