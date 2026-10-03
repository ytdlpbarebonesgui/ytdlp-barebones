using System.Media;
using System.Text.RegularExpressions;

namespace ytdlp_barebones.Helpers.Buttons.Optional.DownloadSection
{
    internal class QuickDownloadSectionButtons
    {
        // Quick Add Download Section
        /// <summary>
        /// Quick-add a download section from a single textbox string like "00:01:00-00:05:00" or "*-00:05:00" or "90-120" or "00:10-inf"
        /// Adds a row to dgv (timeNum, start, end). Plays Asterisk on success.
        /// </summary>
        public static void QuickAddDownloadSection(TextBox quickTextBox, string quickText, DataGridView dgv, Button btnClearAll)
        {
            if (quickTextBox == null) throw new ArgumentNullException(nameof(quickTextBox));
            if (string.IsNullOrWhiteSpace(quickText)) return;
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));

            string t = quickText.Trim();
            // Ensure exactly one dash
            if (Regex.Matches(t, "-").Count != 1)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Invalid format: must contain exactly one '-' between start and end.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Try to parse
            if (!TryParseQuickRange(t, out string startHms, out string endHms, out string errMsg))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(errMsg ?? "Invalid time format.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Duplicate check
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                string existingStart = row.Cells["startTime"]?.Value?.ToString() ?? "";
                string existingEnd = row.Cells["endTime"]?.Value?.ToString() ?? "";
                if (existingStart == startHms && existingEnd == endHms)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("This start-end time section already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // limit check
            int newIndex = dgv.Rows.Count + 1;
            if (newIndex > 999)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Cannot add more than 999 download sections.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgv.Rows.Add(newIndex.ToString("D3"), startHms, endHms);
            if (btnClearAll != null) btnClearAll.Enabled = true;

            // Scroll to the last row
            if (dgv.Rows.Count > 0)
                dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - 1;

            SystemSounds.Asterisk.Play();

            try
            {
                quickTextBox.Clear();
                quickTextBox.Focus();
            }
            catch { /* ignore any focus-related issues */ }
        }
        // Try parse quick input into canonical start/end hh:mm:ss (end may be "inf")
        private static bool TryParseQuickRange(string input, out string startHms, out string endHms, out string error)
        {
            startHms = endHms = null;
            error = null;

            input = input.Trim();

            // Split only on the first dash (range separator)
            var parts = input.Split(new[] { '-' }, 2);
            if (parts.Length != 2) { error = "Must contain one dash separating start and end."; return false; }

            string left = parts[0]; string right = parts[1];

            // Convert left
            if (string.IsNullOrEmpty(left))
                startHms = "00:00:00";
            else if (!TryConvertTokenToHms(left, out startHms, out error))
                return false;

            // Convert right (allow 'inf' or empty -> inf)
            if (string.IsNullOrWhiteSpace(right) || right.Equals("inf", StringComparison.OrdinalIgnoreCase))
                endHms = "inf";
            else if (!TryConvertTokenToHms(right, out endHms, out error))
                return false;

            // Validate times (start <= end unless end is inf)
            if (!endHms.Equals("inf", StringComparison.OrdinalIgnoreCase))
            {
                if (!TimeSpan.TryParse(startHms, out TimeSpan tsStart) || !TimeSpan.TryParse(endHms, out TimeSpan tsEnd))
                {
                    error = "Failed parsing times.";
                    return false;
                }
                if (tsStart > tsEnd)
                {
                    error = "Start Time can't be later than End Time.";
                    return false;
                }

                // Ensure consistent padding between start and end (2-digit vs 3-digit hours)
                int startHours = int.Parse(startHms.Split(':')[0]);
                int endHours = int.Parse(endHms.Split(':')[0]);
                int width = (endHours >= 100) ? 3 : 2;

                startHms = startHours.ToString(width == 3 ? "D3" : "D2") + startHms.Substring(startHms.IndexOf(':'));
                endHms = endHours.ToString(width == 3 ? "D3" : "D2") + endHms.Substring(endHms.IndexOf(':'));
            }

            return true;
        }
        // Token converter: accepts plain seconds or colonized parts. Returns hh:mm:ss or hhh:mm:ss (max 999:59:59)
        private static bool TryConvertTokenToHms(string token, out string hms, out string error)
        {
            hms = null;
            error = null;
            if (string.IsNullOrWhiteSpace(token)) { hms = "00:00:00"; return true; }

            token = token.Trim();

            // Remove stray dashes if any survived
            if (token.StartsWith("-") || token.EndsWith("-"))
                token = token.Trim('-').Trim();

            // If token contains non-digit/colon, invalid here (TextEvents should have sanitized)
            if (!Regex.IsMatch(token, @"^[0-9:]+$"))
            {
                error = "Invalid characters in time token.";
                return false;
            }

            // If token is a plain integer (no ':') treat as seconds
            if (!token.Contains(':'))
            {
                if (!long.TryParse(token, out long secs))
                {
                    error = "Invalid seconds value.";
                    return false;
                }
                if (secs < 0) { error = "Negative time not allowed."; return false; }
                if (secs > 3599999) secs = 3599999; // cap
                hms = SecondsToHms(secs);
                return true;
            }

            // Has colons: split and map from right: seconds, minutes, hours
            var parts = token.Split(':').Select(p => p.Trim()).ToArray();
            if (parts.Length > 4) { error = "Too many ':' in token."; return false; }

            int days = 0, hours = 0, minutes = 0, seconds = 0;
            try
            {
                if (parts.Length >= 1 && !string.IsNullOrEmpty(parts[parts.Length - 1]))
                    seconds = int.Parse(parts[parts.Length - 1]);
                if (parts.Length >= 2 && !string.IsNullOrEmpty(parts[parts.Length - 2]))
                    minutes = int.Parse(parts[parts.Length - 2]);
                if (parts.Length >= 3 && !string.IsNullOrEmpty(parts[parts.Length - 3]))
                    hours = int.Parse(parts[parts.Length - 3]);
                if (parts.Length == 4 && !string.IsNullOrEmpty(parts[0]))
                    days = int.Parse(parts[0]);
            }
            catch
            {
                error = "Failed parsing time parts.";
                return false;
            }

            // Convert days to hours
            long totalHours = (long)days * 24 + hours;

            // Normalize into total seconds instead of clamping
            long totalSeconds = totalHours * 3600 + (long)minutes * 60 + seconds;
            if (totalSeconds > 3599999) totalSeconds = 3599999; // cap

            hms = SecondsToHms(totalSeconds);
            return true;
        }
        private static string SecondsToHms(long secs)
        {
            long hours = secs / 3600;
            long minutes = secs % 3600 / 60;
            long seconds = secs % 60;

            // Always at least 2 digits, but expand to 3 when hours >= 100
            string hourFormat = hours >= 100 ? $"{hours:D3}" : $"{hours:D2}";
            return $"{hourFormat}:{minutes:D2}:{seconds:D2}";
        }
        // For CheckTwoTimestamps_KeyDown from TextEvents.cs
        public static bool TryQuickAddSilent(string quickText, out string errMsg)
        {
            errMsg = null;
            if (!TryParseQuickRange(quickText, out string startHms, out string endHms, out errMsg))
                return false;

            // Skip duplicate/limit checks here if you only want parsing validation
            // OR call the same logic as QuickAddDownloadSection but skip MessageBox
            return true;
        }
        public static bool TryQuickAddToGrid(string quickText, DataGridView dgv, Button btnClearAll, out string errMsg)
        {
            errMsg = null;
            if (string.IsNullOrWhiteSpace(quickText)) { errMsg = "Empty text."; return false; }
            if (dgv == null) { errMsg = "No DataGridView."; return false; }

            // parse
            if (!TryParseQuickRange(quickText, out string startHms, out string endHms, out errMsg))
                return false;

            // duplicate check
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                string existingStart = row.Cells["startTime"]?.Value?.ToString() ?? "";
                string existingEnd = row.Cells["endTime"]?.Value?.ToString() ?? "";
                if (existingStart == startHms && existingEnd == endHms)
                {
                    errMsg = "Duplicate.";
                    return false;
                }
            }

            // limit check
            int newIndex = dgv.Rows.Count + 1;
            if (newIndex > 999)
            {
                errMsg = "Limit reached.";
                return false;
            }

            dgv.Rows.Add(newIndex.ToString("D3"), startHms, endHms);
            if (btnClearAll != null) btnClearAll.Enabled = true;

            // Scroll to the last row
            if (dgv.Rows.Count > 0)
                dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - 1;

            return true;
        }
    }
}
