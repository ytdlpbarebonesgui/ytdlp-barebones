namespace ytdlp_barebones.Helpers.Display
{
    public partial class ToastNotification : Form
    {
        private System.Windows.Forms.Timer appearTimer;
        private System.Windows.Forms.Timer disappearTimer;
        private System.Windows.Forms.Timer autoCloseTimer;
        private Label messageLabel;

        public ToastNotification(string text)
        {
            // Basic form styling
            // No border, no taskbar icon, always on top
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.Black;
            Cursor = Cursors.Hand;

            Opacity = 0.0; // start hidden for fade-in

            // Apply theme
            Theme.Load(this);

            // Label
            messageLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11),
                MaximumSize = new Size(450, 0),
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(15)
            };

            // Theme class must set colors, but fallback if not
            if (messageLabel.ForeColor.IsEmpty) messageLabel.ForeColor = Color.Gray;

            Controls.Add(messageLabel);
            messageLabel.Location = new Point(0, 0);
            // Set size based on label
            Size = new Size(messageLabel.Width, messageLabel.Height);

            // Position at bottom center above taskbar
            PositionToast();

            // Timer (auto close)
            // Fade-in timer
            appearTimer = new System.Windows.Forms.Timer();
            appearTimer.Interval = 15;
            appearTimer.Tick += FadeIn;

            // Fade-out timer
            disappearTimer = new System.Windows.Forms.Timer();
            disappearTimer.Interval = 15;
            disappearTimer.Tick += FadeOut;

            // Auto close (after fade-in)
            autoCloseTimer = new System.Windows.Forms.Timer();
            autoCloseTimer.Interval = 8000; // show for 8 seconds
            autoCloseTimer.Tick += (s, e) =>
            {
                autoCloseTimer.Stop();
                disappearTimer.Start();
            };

            // Click-to-dismiss
            // Attach click event to both form and label
            Click += (s, e) => TriggerImmediateClose();
            messageLabel.Click += (s, e) => TriggerImmediateClose();
        }

        // --- PUBLIC METHODS ---
        // The final method to show a toast notification with the message
        public static void Show(string message)
        {
            ToastNotification toast = new ToastNotification(message);
            toast.Show();
        }


        // --- PPRIVATE & PROTECTED METHODS ---
        // Position the toast at the bottom center of the screen
        private void PositionToast()
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2; // center horizontally
            int y = Screen.PrimaryScreen.WorkingArea.Height - Height - 20; // 20px above taskbar
            Location = new Point(x, y); // set location
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e); // call base method
            appearTimer.Start(); // start fade-in
        }

        // Fade in smoothly
        private void FadeIn(object sender, EventArgs e)
        {
            // Increase opacity
            if (Opacity < 1.0)
                Opacity += 0.05;
            else // fully visible
            {
                Opacity = 1.0;
                appearTimer.Stop();
                autoCloseTimer.Start(); // start countdown after fully visible
            }
        }

        // Fade out smoothly
        private void FadeOut(object sender, EventArgs e)
        {
            // Decrease opacity
            if (Opacity > 0.0)
                Opacity -= 0.05;
            else // fully hidden
            {
                Opacity = 0.0;
                disappearTimer.Stop();
                Close();
            }
        }

        // Handle click to close immediately
        private void TriggerImmediateClose()
        {
            // Stop all other timers
            appearTimer.Stop();
            autoCloseTimer.Stop();

            // If already fading out, let it continue
            if (!disappearTimer.Enabled)
                disappearTimer.Start();
        }

    }
}
