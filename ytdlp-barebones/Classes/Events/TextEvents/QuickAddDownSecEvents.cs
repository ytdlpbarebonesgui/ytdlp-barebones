using System.Media;
using System.Text.RegularExpressions;
using ytdlp_barebones.Helpers.Buttons.Optional.DownloadSection;

namespace ytdlp_barebones.Helpers.Events.TextEvents
{
    public static class QuickAddDownSecEvents
    {
        // (Events)
        // KeyPress: allow only digits, '-', ':', control chars. Prevent second dash on manual typing and block extra ':' per side.
        public static void CheckTwoTimestamps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(sender is TextBox tb)) return;

            // Allow control keys (Backspace, Delete, arrow keys delivered as control here)
            if (char.IsControl(e.KeyChar))
                return;

            char c = e.KeyChar;
            int caret = tb.SelectionStart;
            int dashIndex = tb.Text.IndexOf('-');
            bool hasDash = dashIndex >= 0;
            bool typingRightSide = hasDash && caret > dashIndex;

            // If right side already contains 'inf' at its start, do not accept any character input
            // (only allow control keys which were already returned above).
            if (typingRightSide)
            {
                string right = (dashIndex + 1 <= tb.Text.Length - 1) ? tb.Text.Substring(dashIndex + 1) : "";
                if (!string.IsNullOrEmpty(right) && right.StartsWith("inf", StringComparison.OrdinalIgnoreCase))
                {
                    // If user selected the 'inf' text and is typing, allow replacement via selection,
                    // otherwise block all character input.
                    if (tb.SelectionLength == 0)
                    {
                        SystemSounds.Beep.Play();
                        e.Handled = true;
                        return;
                    }
                    // else allow typed character to replace the selected text (will still be sanitized by TextChanged)
                }
            }

            // Allow 'i'/'I' as **insert inf** only when caret is on right side AFTER dash (and swallow the key).
            if (c == 'i' || c == 'I')
            {
                if (hasDash && caret > dashIndex)
                {
                    InsertInfAtCaretIfAppropriate(tb);
                    e.Handled = true;
                    return;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    e.Handled = true;
                    return;
                }
            }

            // Only allow digits, dash, colon from here on.
            if (!(char.IsDigit(c) || c == '-' || c == ':'))
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
                return;
            }

            // Prevent second dash
            if (c == '-')
            {
                if (tb.Text.Contains('-'))
                {
                    SystemSounds.Beep.Play();
                    e.Handled = true;
                    return;
                }
            }

            // Limit ':' count on the side where the caret is
            string sideText;
            if (!hasDash)
                sideText = tb.Text;
            else
                sideText = typingRightSide ? tb.Text.Substring(dashIndex + 1) : tb.Text.Substring(0, dashIndex);

            int colonCount = sideText.Count(ch => ch == ':');
            if (c == ':' && colonCount >= 3)
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }
        // KeyDown: handle paste (Ctrl+V). If paste contains not exactly one dash -> reject + clear and play sound.
        // Otherwise sanitize pasted text and insert it.
        public static void CheckTwoTimestamps_KeyDown(object sender, KeyEventArgs e, DataGridView dgv, Button btnClearAll)
        {
            if (!(sender is TextBox tb)) return;

            // Handle Backspace to remove 'inf' as a whole when caret is inside or right after it
            if (e.KeyCode == Keys.Back)
            {
                int caret = tb.SelectionStart;
                string text = tb.Text ?? "";
                int dashIndex = text.IndexOf('-');
                if (dashIndex >= 0)
                {
                    int infStart = dashIndex + 1;
                    if (infStart < text.Length)
                    {
                        string right = text.Substring(infStart);
                        if (right.StartsWith("inf", StringComparison.OrdinalIgnoreCase))
                        {
                            // If caret is anywhere within the 'inf' token or immediately after it, delete the whole token
                            int infEndPos = infStart + 3; // one past 'f'
                            if (caret >= infStart && caret <= infEndPos)
                            {
                                // Remove 'inf' and position caret at infStart
                                tb.Text = text.Substring(0, infStart) + text.Substring(infEndPos);
                                tb.SelectionStart = infStart;
                                e.SuppressKeyPress = true;
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                }
            }

            // Handle Paste
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (!Clipboard.ContainsText()) return;
                string clip = Clipboard.GetText().Trim();

                // SIngle-line paste guard
                // If the textbox already contains a dash AND the clipboard text also contains a dash,
                // then reject single-line pastes. (Still allow multi-line paste below.)
                if (!clip.Contains("\n") && !clip.Contains("\r") && tb.Text.Contains('-') && clip.Contains('-'))
                {
                    SystemSounds.Beep.Play();
                    e.SuppressKeyPress = true;
                    return;
                }

                // Split lines
                var lines = clip.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 1)
                {
                    // Multi-line paste
                    e.SuppressKeyPress = true;

                    List<string> rejected = new List<string>();

                    foreach (string raw in lines)
                    {
                        string line = raw.Trim();
                        if (string.IsNullOrEmpty(line)) continue;

                        if (Regex.Matches(line, "-").Count != 1)
                        {
                            rejected.Add(line);
                            continue;
                        }

                        string clean = SanitizeQuickPaste(line);
                        if (string.IsNullOrEmpty(clean) || Regex.Matches(clean, "-").Count != 1)
                        {
                            rejected.Add(line);
                            continue;
                        }

                        // Try to add directly into grid
                        if (!QuickDownloadSectionButtons.TryQuickAddToGrid(clean, dgv, btnClearAll, out string errMsg))
                            rejected.Add(line + (errMsg != null ? $" ({errMsg})" : ""));
                    }


                    // Show rejected lines (if any)
                    if (rejected.Count > 0)
                    {
                        string msg = "The following timestamps are rejected:\n" + string.Join("\n", rejected);
                        MessageBox.Show(msg, "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Unused but might be needed in the future: Clear textbox after multi-line paste
                    // tb.Clear();

                    return;
                }
                else
                {
                    // Single-line paste
                    string single = lines[0];

                    // Does it contain a dash?
                    if (single.Contains("-"))
                    {
                        // Guard: textbox already has dash AND pasted text has dash → reject
                        if (tb.Text.Contains('-'))
                        {
                            SystemSounds.Beep.Play();
                            e.SuppressKeyPress = true;
                            return;
                        }

                        // (existing logic for one dash goes here)
                        int dashCount = Regex.Matches(single, "-").Count;
                        if (dashCount != 1)
                        {
                            SystemSounds.Beep.Play();
                            e.SuppressKeyPress = true;
                            tb.Clear();
                            return;
                        }

                        string clean = SanitizeQuickPaste(single);
                        if (string.IsNullOrEmpty(clean) || Regex.Matches(clean, "-").Count != 1)
                        {
                            SystemSounds.Beep.Play();
                            e.SuppressKeyPress = true;
                            tb.Clear();
                            return;
                        }

                        e.SuppressKeyPress = true;
                        tb.SelectedText = clean;
                    }
                    else
                    {
                        // Single-line paste without dash → just paste raw
                        e.SuppressKeyPress = true;
                        tb.SelectedText = single;
                    }
                }
            }
        }
        // TextChanged: auto-strip illegal characters typed (letters etc.) while preserving 'inf' on right side.
        public static void CheckTwoTimestamps_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox tb)) return;

            // Keep caret position as best as possible
            int caret = tb.SelectionStart;
            string text = tb.Text ?? "";

            // If no dash yet, left side only; allow digits, colons, '*' (asterisk used to represent 00:00:00)
            if (!text.Contains('-'))
            {
                string left = Regex.Replace(text, @"[^0-9:\*]", "");
                if (left != text)
                {
                    SystemSounds.Beep.Play();
                    tb.Text = left;
                    tb.SelectionStart = Math.Min(caret - 1, tb.Text.Length);
                }
                return;
            }

            // Has dash; split
            var parts = text.Split(new[] { '-' }, 2);
            string leftSide = parts[0];
            string rightSide = parts.Length > 1 ? parts[1] : "";

            // sanitize left: allow digits and ':' and '*' only
            string cleanLeft = Regex.Replace(leftSide, @"[^0-9:\*]", "");
            // sanitize right: allow digits and ':' and letters 'inf' (case-insensitive)
            string cleanRightTmp = Regex.Replace(rightSide, @"[^0-9:infINF]", "");
            // Normalize 'INF' case to lower-case 'inf'
            string cleanRight = Regex.Replace(cleanRightTmp, "(?i)inf", m => m.Value.ToLower());

            // Remove partial 'in'/'i' when backspaced is handled naturally — but ensure we don't leave 'in' alone
            if (cleanRight.Length > 0 && cleanRight != cleanRightTmp)
            {
                // user typed some disallowed letters before full 'inf' -> beep
                SystemSounds.Beep.Play();
            }

            string combined = cleanLeft + "-" + cleanRight;
            if (combined != text)
            {
                int newCaret = Math.Max(0, Math.Min(combined.Length, caret - (text.Length - combined.Length)));
                tb.Text = combined;
                tb.SelectionStart = newCaret;
            }
        }
        // (Methods)
        // Sanitize pasted quick text while keeping allowed tokens and minimal formatting.
        private static string SanitizeQuickPaste(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";

            input = input.Trim();
            // remove spaces
            input = Regex.Replace(input, @"\s+", "");

            // ensure exactly one dash - caller already checked this
            var parts = input.Split(new[] { '-' }, 2);
            string left = parts[0];
            string right = parts.Length > 1 ? parts[1] : "";

            // Left side: allow digits, ':' or a single '*' (convert later)
            left = left == "*" ? "*" : Regex.Replace(left, @"[^0-9:]", "");

            // Right side: allow digits, ':', or 'inf' (case-insensitive)
            if (Regex.IsMatch(right, @"^(?i:inf)$"))
            {
                right = "inf";
            }
            else
            {
                right = Regex.Replace(right, @"[^0-9:]", "");
            }

            return left + "-" + right;
        }
        // Called by the small "i" button: if caret is currently inside or after the right side (end time) and end is empty, insert 'inf'
        public static void InsertInfAtCaretIfAppropriate(TextBox tb)
        {
            if (tb == null) return;
            string text = tb.Text ?? "";
            int dash = text.IndexOf('-');
            if (dash < 0)
            {
                // no dash -> append "-inf"
                if (!text.EndsWith("-"))
                    tb.Text = text + "-inf";
                else
                    tb.Text = text + "inf";
                tb.SelectionStart = tb.Text.Length;
                return;
            }

            string right = dash + 1 <= text.Length - 1 ? text.Substring(dash + 1) : "";
            if (string.IsNullOrEmpty(right))
            {
                tb.Text = text + "inf";
                tb.SelectionStart = tb.Text.Length;
            }
            else if (!right.Equals("inf", StringComparison.OrdinalIgnoreCase))
            {
                // replace whatever is at right with 'inf'
                tb.Text = text.Substring(0, dash + 1) + "inf";
                tb.SelectionStart = tb.Text.Length;
            }
        }
    }
}
