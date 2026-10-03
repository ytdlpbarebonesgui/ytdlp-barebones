using System.Text;
using System.Text.RegularExpressions;

namespace ytdlp_barebones.Helpers.Events
{
    public static class KeyEvents
    {
        // Prevents you from entering invalid characters (for file names)
        public static void InvalidChars(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Get the invalid file name characters from Windows.
                char[] invalidChars = Path.GetInvalidFileNameChars();

                // Check if the entered character is invalid (but allow backspace and space)
                if (Array.Exists(invalidChars, c => c == e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ') e.Handled = true;
            }
        }

        // Prevents the new line character from being entered (for single-line textboxes)
        public static void PreventNewLine(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) e.Handled = true;
        }

        // Handles paste operations for a URL textbox (for URLs)
        public static async Task PasteURL(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (sender is TextBox textBox)
                {
                    // Allow a small delay to let the paste complete.
                    await Task.Delay(50);

                    #region ASCII Conversion
                    // Get current text (with pasted content included)
                    string raw = textBox.Text;

                    // Convert to ASCII (drops fancy Unicode chars)
                    string ascii = Encoding.ASCII.GetString(
                        Encoding.ASCII.GetBytes(raw)
                    );

                    // Strip zero-width / invisible chars
                    ascii = Regex.Replace(ascii, @"[\u200B-\u200D\uFEFF]", string.Empty);

                    // Assign cleaned text
                    textBox.Text = ascii.Trim();
                    #endregion

                    // Append a newline after the pasted content.
                    textBox.Text += Environment.NewLine;

                    // Move the caret to the end and scroll to it.
                    textBox.SelectionStart = textBox.Text.Length;
                    textBox.ScrollToCaret();
                }
            }
        }

        // Numbers only for frmNewDownloadSection form - KeyPress handler
        public static void TimeandNumbersOnly(object sender, KeyPressEventArgs e)
        {
            // Allow digits and backspace only
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
        // Normalizes and enforces limits for the textbox (call from TextChanged or Leave)
        // - caps mm/ss values to 59
        public static void TimeandNumbersOnly(TextBox tb)
        {
            if (tb == null) return;
            string name = tb.Name?.ToLowerInvariant() ?? "";
            string txt = tb.Text ?? "";

            // Keep empty — caller (btnadd) will convert empty -> "00"
            if (string.IsNullOrWhiteSpace(txt))
            {
                tb.Text = "";
                tb.SelectionStart = tb.Text.Length;
                return;
            }

            // Remove any non-digits (in case of paste)
            var digitsOnly = "";
            foreach (char c in txt)
                if (char.IsDigit(c)) digitsOnly += c;

            if (digitsOnly.Length == 0)
            {
                tb.Text = "";
                tb.SelectionStart = tb.Text.Length;
                return;
            }

            // If more than 2 digits, truncate to last two (reasonable default)
            if (digitsOnly.Length > 2) digitsOnly = digitsOnly.Substring(digitsOnly.Length - 2);

            // If this is a minutes or seconds box, cap at 59
            if (name.EndsWith("mm") || name.EndsWith("ss"))
            {
                if (int.TryParse(digitsOnly, out int val))
                {
                    if (val > 59) digitsOnly = "59";
                }
            }

            tb.Text = digitsOnly;
            tb.SelectionStart = tb.Text.Length;
        }

        // Exec
        // Prevents pasting multi-line text; replaces newlines with spaces
        public static void NormalizePasteToSingleLine(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsText())
                {
                    string text = Clipboard.GetText();
                    if (text.Contains("\r") || text.Contains("\n"))
                    {
                        e.SuppressKeyPress = true; // Block default paste
                        if (sender is TextBox txt)
                        {
                            txt.SelectedText = text.Replace("\r", " ").Replace("\n", " ");
                        }
                    }
                }
            }
        }
    }
}
