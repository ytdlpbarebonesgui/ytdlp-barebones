using System.Text.RegularExpressions;

namespace ytdlp_barebones.Helpers.Events.TextEvents
{
    public static class TextValidationEvents
    {
        /// <summary>
        /// Validates the text of a yt-dlp command textbox.  
        /// - Disallows typing/pasting "yt-dlp" at the start (the app handles it internally).  
        /// - Strips out pasted URLs (http/https).  
        /// - Shows an error message to guide the user.  
        /// Uses <see cref="TextBox.Tag"/> as a flag to prevent recursive calls during text updates.  
        /// </summary>
        /// <param name="commandBox">The textbox control containing the yt-dlp command.</param>
        public static void ValidateYtDlpCommandText(TextBox commandBox)
        {
            // Prevent infinite recursion.
            // If the textbox is already being validated, exit early.
            if (commandBox.Tag is bool isValidating && isValidating)
                return;

            try
            {
                // Mark the textbox as "currently validating"
                commandBox.Tag = true;

                string currentText = commandBox.Text;

                // Disallow "yt-dlp" prefix
                if (currentText.StartsWith("yt-dlp"))
                {
                    // Remove the prefix and clean up extra spaces
                    commandBox.Text = currentText.Substring(6).TrimStart();
                    commandBox.SelectionStart = commandBox.Text.Length;

                    // Notify the user: redundant prefix
                    MessageBox.Show(
                        "Do not put 'yt-dlp' at the start of the command.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    // Refresh local copy of text after modification
                    currentText = commandBox.Text;
                }

                // Disallow pasted URLs (http/https URLs)
                string urlPattern = @"(http://|https://)\S*";
                MatchCollection matches = Regex.Matches(currentText, urlPattern);

                if (matches.Count > 0)
                {
                    // Strip all detected URLs from the text
                    foreach (Match match in matches)
                        currentText = currentText.Replace(match.Value, "");

                    // Default error message
                    string errorMessage = "Do not put URLs in the yt-dlp command textbox.";

                    // If inside frmMain, give a more specific instruction
                    Form parentForm = commandBox.FindForm();
                    if (parentForm != null && parentForm.Name == "frmMain")
                        errorMessage = "Do not put URLs in the yt-dlp command textbox.\n" +
                                       "Put it in the URL textbox instead, and use the '<url>' placeholder shortcut.";

                    // Notify the user: wrong input type
                    MessageBox.Show(
                        errorMessage,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    // Apply sanitized text
                    commandBox.Text = currentText.Trim();
                    commandBox.SelectionStart = commandBox.Text.Length;
                }
            }
            finally
            {
                // Always reset the "validating" flag so future changes can be processed
                commandBox.Tag = false;
            }
        }
        /// Validates and normalizes the contents of a URL textbox:
        /// - Prevents stray newlines when textbox is empty or whitespace-only.
        /// - Removes duplicate URLs across pasted, dragged, or typed entries.
        /// - Shows a combined error message if any duplicates are removed.
        /// - Preserves a single trailing blank line only if the user pressed Enter after the last URL.
        /// - Updates the associated GroupBox with URL count and color feedback.
        /// - Keeps the caret positioned at the end of the textbox after validation.
        public static void ValidateURLText(TextBox txturls, GroupBox gbUrl)
        {
            // If textbox is only whitespace, reset it to empty and stop
            // This'll lock the textbox from having stray newlines
            if (string.IsNullOrWhiteSpace(txturls.Text))
            {
                txturls.Text = string.Empty;
                txturls.SelectionStart = 0; // caret at first line
                return;
            }

            // Split by lines but keep blanks for end-check
            var rawLines = txturls.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(l => l.TrimEnd()) // trim right side only so Enter stays visible
                .ToList();

            HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            List<string> accepted = new List<string>();
            List<string> rejected = new List<string>();

            foreach (var line in rawLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (seen.Contains(line))
                {
                    rejected.Add(line);
                    continue;
                }
                seen.Add(line);
                accepted.Add(line);
            }

            // Show duplicates once
            if (rejected.Count > 0)
            {
                string message = "The following URL(s) were rejected as duplicates:\n" +
                                 string.Join(Environment.NewLine, rejected.Select(l => "\'" + l + "\'"));
                MessageBox.Show(message, "Duplicate URLs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Allow exactly one trailing blank line if user ended with Enter
            bool endsWithBlank = rawLines.Count > 0 && string.IsNullOrWhiteSpace(rawLines.Last());

            string newText = string.Join(Environment.NewLine, accepted) +
                             (endsWithBlank ? Environment.NewLine : "");

            if (txturls.Text != newText)
            {
                txturls.Text = newText;
                txturls.SelectionStart = txturls.Text.Length;
                txturls.ScrollToCaret();
            }

            // Regex pattern for URL detection
            string pattern = @"\b((https?://)?([\w-]+\.)+[\w-]+(/[^\s]*)?)\b";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // Modify GroupBox color based on URL presence
            // Count valid URLs
            int urlCount = accepted.Count(line => regex.IsMatch(line));
            // Update GroupBox text and color based on URL count
            string plural = urlCount == 1 ? "" : "s";
            // Set text: count of URLs or placeholder if none
            gbUrl.Text = urlCount > 0 ? $"3. URL(s) to download ({urlCount} URL{plural} pasted)" : "3. URL(s) to download 🦴";
            // Set color: black/white if valid URLs exist, red otherwise
            gbUrl.ForeColor = urlCount > 0 ? (Settings.Default.Theme == "Light" ? Color.Black : Color.White) : Color.Red;
        }


        /// Checks for non-empty commandBox and (optionally) nameBox,  
        /// then enables/disables the two buttons:
        /// - downloadBtn → commandBox non-empty  
        /// - updateBtn  → commandBox & nameBox both non-empty  
        public static void ValidateNull(TextBox commandBox, TextBox nameBox, Button downloadBtn, Button updateBtn)
        {
            // Check if commandBox has valid text
            bool isCommandValid = !string.IsNullOrWhiteSpace(commandBox.Text);
            bool isNameValid = nameBox == null || !string.IsNullOrWhiteSpace(nameBox.Text);

            // Enable/disable buttons based on validation
            if (downloadBtn != null)
                downloadBtn.Enabled = isCommandValid;
            if (updateBtn != null)
                updateBtn.Enabled = isCommandValid && isNameValid;
        }
    }
}
