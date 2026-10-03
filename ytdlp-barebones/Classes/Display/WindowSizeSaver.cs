namespace ytdlp_barebones.Helpers.Display
{
    public static class WindowSizeSaver
    {
        /// <summary>
        /// Saves size and location based on the current state of the form.
        /// This method should be called when the window is closing,
        /// or when its state changes (e.g. maximized/unmaximized).
        /// </summary>
        public static void Save(Form form)
        {
            // Save the form size if the setting is enabled.
            if (Settings.Default.SaveLoadFormSize)
            {
                if (form.WindowState == FormWindowState.Normal)
                {
                    Settings.Default.MainFormSize = form.Size;
                }
                else
                {
                    // When minimized or maximized, use the RestoreBounds.
                    Settings.Default.MainFormSize = form.RestoreBounds.Size;
                }
            }

            // Save the form location if the setting is enabled.
            if (Settings.Default.SaveLoadFormLocation)
            {
                string locationStr = "";
                if (form.WindowState == FormWindowState.Normal)
                {
                    locationStr = $"{form.Location.X}, {form.Location.Y}";
                }
                else
                {
                    locationStr = $"{form.RestoreBounds.Location.X}, {form.RestoreBounds.Location.Y}";
                }
                Settings.Default.MainFormLocation = locationStr;
            }

            // Save the current WindowState regardless.
            Settings.Default.MainFormState = form.WindowState.ToString();
            Settings.Default.Save();
        }

        /// <summary>
        /// Loads the saved size, location, and window state into the form.
        /// Location is only applied if saving is enabled and the saved value is valid.
        /// If the saved position causes the form to be partially or fully off-screen, it is automatically adjusted to stay within the screen's working area.
        /// If location saving is disabled, the form is manually centered on the screen.
        /// </summary>
        public static void Load(Form form)
        {
            if (Settings.Default.SaveLoadFormSize)
            {
                form.Size = Settings.Default.MainFormSize;
            }

            var workingArea = Screen.PrimaryScreen.WorkingArea;

            if (Settings.Default.SaveLoadFormLocation)
            {
                string savedLocation = Settings.Default.MainFormLocation;
                if (!string.IsNullOrEmpty(savedLocation))
                {
                    var parts = savedLocation.Split(',');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int x) &&
                        int.TryParse(parts[1].Trim(), out int y))
                    {
                        var proposedBounds = new Rectangle(x, y, form.Width, form.Height);

                        // Check if the entire form is within the working area
                        if (workingArea.Contains(proposedBounds))
                        {
                            form.Location = new Point(x, y);
                        }
                        else
                        {
                            // Adjust to keep the form fully visible
                            x = Math.Max(workingArea.Left, x);
                            y = Math.Max(workingArea.Top, y);
                            x = Math.Min(workingArea.Right - form.Width, x);
                            y = Math.Min(workingArea.Bottom - form.Height, y);
                            form.Location = new Point(x, y);
                        }
                    }
                }
            }
            else
            {
                // Center the form if location saving is disabled
                // CenterScreen StartPosition doesn't work
                int centerX = workingArea.X + ((workingArea.Width - form.Width) / 2);
                int centerY = workingArea.Y + ((workingArea.Height - form.Height) / 2);
                form.Location = new Point(centerX, centerY);
            }

            if (Enum.TryParse(Settings.Default.MainFormState, out FormWindowState state))
            {
                form.WindowState = state;
            }
        }

        /// <summary>
        /// Optionally, save the location and size immediately when the form is minimized.
        /// This ensures that closing, maximizing, and unmaximizing events are all captured.
        /// </summary>
        public static void SaveOnMinimized(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized)
            {
                if (Settings.Default.SaveLoadFormSize)
                {
                    Settings.Default.MainFormSize = form.RestoreBounds.Size;
                }
                if (Settings.Default.SaveLoadFormLocation)
                {
                    Settings.Default.MainFormLocation =
                        $"{form.RestoreBounds.Location.X}, {form.RestoreBounds.Location.Y}";
                }
                // Force state to Normal so the app reopens in a normal state.
                Settings.Default.MainFormState = FormWindowState.Normal.ToString();
                Settings.Default.Save();
            }
        }
    }
}
