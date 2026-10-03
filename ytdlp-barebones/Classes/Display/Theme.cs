using Microsoft.Win32;

namespace ytdlp_barebones.Helpers.Display
{
    public static class Theme
    {
        private const string SystemDefaultName = "System Default";

        private class ThemeColors
        {
            public Color FormBack;
            public Color FormFore;
            public Color ControlBack;
            public Color ControlFore;
            public Color TextBoxBack;
            public Color TextBoxFore;
            public Color ButtonBack;
            public Color ButtonFore;
            public Color DataGridBack;
            public Color DataGridFore;
            public Color Highlight;
        }

        private static ThemeColors Dark => new ThemeColors
        {
            FormBack = Color.FromArgb(32, 32, 32), // main form background
            FormFore = Color.FromArgb(240, 240, 240),
            ControlBack = Color.FromArgb(45, 45, 48),
            ControlFore = Color.FromArgb(240, 240, 240),
            TextBoxBack = Color.FromArgb(28, 28, 28),
            TextBoxFore = Color.FromArgb(240, 240, 240),
            ButtonBack = Color.FromArgb(50, 50, 50),
            ButtonFore = Color.FromArgb(245, 245, 245),
            DataGridBack = Color.FromArgb(40, 40, 40),
            DataGridFore = Color.FromArgb(235, 235, 235),
            Highlight = Color.FromArgb(0, 122, 204)
        };

        private static ThemeColors Light => new ThemeColors
        {
            FormBack = Color.FromArgb(245, 245, 245),
            FormFore = Color.FromArgb(20, 20, 20),
            ControlBack = Color.FromArgb(255, 255, 255),
            ControlFore = Color.FromArgb(20, 20, 20),
            TextBoxBack = Color.FromArgb(255, 255, 255),
            TextBoxFore = Color.FromArgb(20, 20, 20),
            ButtonBack = Color.FromArgb(240, 240, 240),
            ButtonFore = Color.FromArgb(20, 20, 20),
            DataGridBack = Color.FromArgb(255, 255, 255),
            DataGridFore = Color.FromArgb(20, 20, 20),
            Highlight = Color.FromArgb(0, 120, 215)
        };

        // Public: load theme for a form and its child controls
        public static void Load(Form form)
        {
            try
            {
                // Ensure settings value exists
                if (string.IsNullOrWhiteSpace(Settings.Default.Theme))
                {
                    Settings.Default.Theme = SystemDefaultName;
                    Settings.Default.Save();
                }

                // Determine effective theme (Light/Dark)
                bool isDark = ResolveEffectiveIsDark(Settings.Default.Theme);
                var colors = isDark ? Dark : Light;

                ApplyToForm(form, colors);
            }
            catch
            {
                // Swallow exceptions to avoid breaking form load
            }
        }

        // Determines whether a theme string means dark (resolves "System Default")
        public static bool ResolveEffectiveIsDark(string themeName)
        {
            if (string.IsNullOrWhiteSpace(themeName) || themeName.Equals(SystemDefaultName, StringComparison.OrdinalIgnoreCase))
            {
                // Try to detect system app theme (AppsUseLightTheme == 0 means dark)
                try
                {
                    using var rk = Registry.CurrentUser.OpenSubKey(@"Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize");
                    if (rk != null)
                    {
                        var v = rk.GetValue("AppsUseLightTheme");
                        if (v is int intVal)
                        {
                            return intVal == 0;
                        }
                    }
                }
                catch
                {
                    // fallback to light
                    return false;
                }

                return false; // default light
            }

            // else compare specific names
            return themeName.Equals("Dark", StringComparison.OrdinalIgnoreCase);
        }

        private static void ApplyToForm(Form form, ThemeColors colors)
        {
            if (form == null) return;

            form.BackColor = colors.FormBack;
            form.ForeColor = colors.FormFore;

            // Apply to top-level controls
            foreach (Control c in form.Controls)
            {
                ApplyToControlRecursive(c, colors);
            }

            // Special: some controls like MenuStrip, StatusStrip
            foreach (var strip in form.Controls.OfType<MenuStrip>())
            {
                strip.BackColor = colors.ControlBack;
                strip.ForeColor = colors.ControlFore;
            }

            foreach (var strip in form.Controls.OfType<StatusStrip>())
            {
                strip.BackColor = colors.ControlBack;
                strip.ForeColor = colors.ControlFore;
            }

            // Force repaint
            form.Invalidate(true);
        }

        private static void ApplyToControlRecursive(Control c, ThemeColors colors)
        {
            if (c == null) return;

            // Skip download button on main form
            if (c.Name.Equals("btndownload", StringComparison.OrdinalIgnoreCase))
                return;

            switch (c)
            {
                case GroupBox gb:
                    // Preserve labels that are intentionally red (visual requirement)
                    gb.BackColor = colors.ControlBack;
                    if (!gb.ForeColor.Equals(Color.Red))
                    {
                        gb.ForeColor = colors.ControlFore;
                    }
                    break;
                case Label lb:
                    if (!lb.ForeColor.Equals(Color.Red))
                    {
                        lb.ForeColor = colors.ControlFore;
                    }
                    break;
                case TextBox tb:
                    tb.BackColor = colors.TextBoxBack;
                    tb.ForeColor = colors.TextBoxFore;
                    break;
                case RichTextBox rtb:
                    rtb.BackColor = colors.TextBoxBack;
                    rtb.ForeColor = colors.TextBoxFore;
                    break;
                case ComboBox cb:
                    cb.ForeColor = colors.TextBoxFore;
                    if (cb.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        cb.BackColor = colors.ButtonBack;
                    }
                    else
                    {
                        cb.BackColor = colors.TextBoxBack;
                    }
                    break;
                case ListBox lbx:
                    lbx.BackColor = colors.TextBoxBack;
                    lbx.ForeColor = colors.TextBoxFore;
                    break;
                case DataGridView dgv:
                    dgv.BackgroundColor = colors.DataGridBack;
                    dgv.ForeColor = colors.DataGridFore;
                    dgv.DefaultCellStyle.BackColor = colors.DataGridBack;
                    dgv.DefaultCellStyle.ForeColor = colors.DataGridFore;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = colors.ControlBack;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = colors.ControlFore;
                    dgv.RowHeadersDefaultCellStyle.BackColor = colors.ControlBack;
                    dgv.RowHeadersDefaultCellStyle.ForeColor = colors.ControlFore;
                    break;
                case Button btn:
                    // Only style if button uses flat style (flat UI), otherwise leave default styling
                    try
                    {
                        var flat = btn.FlatStyle == FlatStyle.Flat || btn.FlatStyle == FlatStyle.Popup;
                        if (flat)
                        {
                            btn.BackColor = colors.ButtonBack;
                            btn.ForeColor = colors.ButtonFore;
                            btn.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                    break;
                case CheckBox chk:
                    // Determine background based on parent container
                    Control parent = chk.Parent;
                    // Checkbox background should match parent container
                    if (parent is Form)
                        chk.BackColor = colors.FormBack;
                    else
                        chk.BackColor = colors.ControlBack;
                    break;
                case Panel p:
                    p.BackColor = colors.ControlBack;
                    p.ForeColor = colors.ControlFore;
                    break;
                default:
                    try
                    {
                        // Generic controls
                        c.BackColor = colors.ControlBack;
                        c.ForeColor = colors.ControlFore;
                    }
                    catch { }
                    break;
            }

            // Recurse
            foreach (Control child in c.Controls)
            {
                ApplyToControlRecursive(child, colors);
            }
        }

        // Helper: get effective theme name for comparison
        public static string GetEffectiveThemeName(string themeName)
        {
            bool isDark = ResolveEffectiveIsDark(themeName);
            return isDark ? "Dark" : "Light";
        }

        public static string SystemDefault => SystemDefaultName;
    }
}