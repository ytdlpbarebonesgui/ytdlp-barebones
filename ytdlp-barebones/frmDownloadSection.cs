using ytdlp_barebones.Helpers.Display;
using ytdlp_barebones.Helpers.Events;

namespace ytdlp_barebones
{
    public partial class frmDownloadSection : Form
    {
        // Add mode helpers: public properties to be read by frmMain after ShowDialog()
        public string StartTime { get; private set; } = "";
        public string EndTime { get; private set; } = "";

        // Edit mode helpers
        public bool IsEditMode { get; private set; } = false;
        public string OriginalStart { get; private set; } = "";
        public string OriginalEnd { get; private set; } = "";
        public string OriginalTimeNum { get; private set; } = "";

        public frmDownloadSection()
        {
            InitializeComponent();
            IntializeEvents();
        }

        // Methods
        // (For Edit) Use this to prefill the form when editing.
        public void LoadForEdit(string timeNum, string start, string end)
        {
            IsEditMode = true;
            OriginalTimeNum = timeNum ?? "";
            OriginalStart = start ?? "";
            OriginalEnd = end ?? "";

            // Change form title + button text + image
            this.Text = "Update a Download Section";
            btnadd.Text = "Update";
            btnadd.Image = Properties.Resources.pencil;

            // Fill start hh:mm:ss
            if (!string.IsNullOrEmpty(OriginalStart) && OriginalStart.Contains(":"))
            {
                var parts = OriginalStart.Split(':');
                if (parts.Length >= 3)
                {
                    txtstarttimehh.Text = parts[0];
                    txtstarttimemm.Text = parts[1];
                    txtstarttimess.Text = parts[2];
                }
            }
            else
            {
                txtstarttimehh.Text = txtstarttimemm.Text = txtstarttimess.Text = "";
            }

            // Fill end - could be "inf"
            if (!string.IsNullOrEmpty(OriginalEnd) && OriginalEnd.Equals("inf", StringComparison.OrdinalIgnoreCase))
            {
                cbendtimeinf.Checked = true;
                txtendtimehh.Text = txtendtimemm.Text = txtendtimess.Text = "";
            }
            else if (!string.IsNullOrEmpty(OriginalEnd) && OriginalEnd.Contains(":"))
            {
                cbendtimeinf.Checked = false;
                var parts = OriginalEnd.Split(':');
                if (parts.Length >= 3)
                {
                    txtendtimehh.Text = parts[0];
                    txtendtimemm.Text = parts[1];
                    txtendtimess.Text = parts[2];
                }
            }
            else
            {
                cbendtimeinf.Checked = false;
                txtendtimehh.Text = txtendtimemm.Text = txtendtimess.Text = "";
            }
        }

        // Events
        private void IntializeEvents()
        {
            // Form load equivalent
            Load += (s,e) =>
            {
                Theme.Load(this); // Apply theme
                btncancel.Select();
            };

            // (Attach KeyPress and TextChanged handlers for all 6 boxes)
            // Start time
            txtstarttimehh.KeyPress += KeyEvents.TimeandNumbersOnly;
            txtstarttimemm.KeyPress += KeyEvents.TimeandNumbersOnly;
            txtstarttimess.KeyPress += KeyEvents.TimeandNumbersOnly;
            txtstarttimehh.TextChanged += (s, ev) => KeyEvents.TimeandNumbersOnly(txtstarttimehh);
            txtstarttimemm.TextChanged += (s, ev) => KeyEvents.TimeandNumbersOnly(txtstarttimemm);
            txtstarttimess.TextChanged += (s, ev) => KeyEvents.TimeandNumbersOnly(txtstarttimess);
            // End time
            txtendtimehh.KeyPress += KeyEvents.TimeandNumbersOnly;
            txtendtimemm.KeyPress += KeyEvents.TimeandNumbersOnly;
            txtendtimess.KeyPress += KeyEvents.TimeandNumbersOnly;
            txtendtimehh.TextChanged += (s, ev) => KeyEvents.TimeandNumbersOnly(txtendtimehh);
            txtendtimemm.TextChanged += (s, ev) => KeyEvents.TimeandNumbersOnly(txtendtimemm);
            txtendtimess.TextChanged += (s, ev) => KeyEvents.TimeandNumbersOnly(txtendtimess);

            // (Buttons)
            btnadd.Click += (s,e) => 
            {
                // Helper used only at add/update-time to produce final two-digit strings.
                // Note: pads single-digit by adding a leading zero => "5" -> "05".
                string NormalizeForAdd(TextBox tb, bool isMinuteOrSecond)
                {
                    string t = tb?.Text?.Trim() ?? "";

                    if (t.Length == 0) return "00";        // convert null/empty -> "00"
                    if (t.Length == 1) t = "0" + t;       // pad single-digit -> leading zero (5 -> 05)
                    if (t.Length > 2) t = t.Substring(t.Length - 2); // use last two digits if pasted

                    if (isMinuteOrSecond)
                    {
                        if (int.TryParse(t, out int v) && v > 59) return "59";
                    }
                    return t;
                }

                // Normalize start
                string shh = NormalizeForAdd(txtstarttimehh, false);
                string smm = NormalizeForAdd(txtstarttimemm, true);
                string sss = NormalizeForAdd(txtstarttimess, true);

                // End (unless cbendtimeinf is checked)
                bool endIgnored = cbendtimeinf.Checked;
                string ehh = "00", emm = "00", ess = "00";
                if (!endIgnored)
                {
                    ehh = NormalizeForAdd(txtendtimehh, false);
                    emm = NormalizeForAdd(txtendtimemm, true);
                    ess = NormalizeForAdd(txtendtimess, true);
                }

                string startTimeStr = $"{shh}:{smm}:{sss}";
                // When end is ignored, use the literal "inf"
                string endTimeStr = endIgnored ? "inf" : $"{ehh}:{emm}:{ess}";

                // Validate start <= end (only if end is present / not 'inf')
                if (!endIgnored)
                {
                    if (!TimeSpan.TryParse(startTimeStr, out TimeSpan tsStart) || !TimeSpan.TryParse(endTimeStr, out TimeSpan tsEnd))
                    {
                        MessageBox.Show("Invalid time format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (tsStart > tsEnd)
                    {
                        MessageBox.Show("Start Time can't be later than End Time", "Invalid Time Range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // If edit mode and no change made, show error and remain open
                if (IsEditMode)
                {
                    if (string.Equals(OriginalStart, startTimeStr, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(OriginalEnd, endTimeStr, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("No changes are made.", "No Changes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Confirm update (show from -> to)
                    string confirmDisplay =
                        $"From:\r\n{OriginalStart}-{OriginalEnd}\r\n\r\nTo:\r\n{startTimeStr}-{endTimeStr}";

                    var dr = MessageBox.Show($"Are you sure you want to update the download section?\r\n{confirmDisplay}", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr != DialogResult.Yes) return;
                }

                // Set public props and close OK
                StartTime = startTimeStr;
                EndTime = endTimeStr; // "inf" if cbendtimeinf checked
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            btncancel.Click += (s, e) => Close();
            // (Check Changed)
            cbendtimeinf.CheckedChanged += (s,e) => txtendtimess.Enabled = txtendtimemm.Enabled = txtendtimehh.Enabled = !cbendtimeinf.Checked;
        }
    }
}
