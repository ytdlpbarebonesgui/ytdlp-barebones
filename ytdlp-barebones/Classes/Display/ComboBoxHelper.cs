namespace ytdlp_barebones.Helpers.Display
{
    public static class ComboBoxHelper
    {
        // Adjusts the DropDownWidth of the provided ComboBox so that all items are fully visible.
        /// <param name="combo">The ComboBox to adjust.</param>
        public static void ExpandComboBoxDropDownWidth(ComboBox combo)
        {
            if (combo == null) return;

            int maxWidth = 0;

            // Create a Graphics object to measure text.
            using (Graphics g = combo.CreateGraphics())
            {
                foreach (object item in combo.Items)
                {
                    if (item != null)
                    {
                        // Measure the width of the item using the ComboBox's font.
                        int itemWidth = (int)g.MeasureString(item.ToString(), combo.Font).Width;
                        // Extend the width of the tooltip if the item extends greater than max width
                        if (itemWidth > maxWidth) maxWidth = itemWidth;
                    }
                }
            }

            // Add extra space for the vertical scrollbar, if it appears.
            combo.DropDownWidth = maxWidth + SystemInformation.VerticalScrollBarWidth;
        }

        /// <summary>
        /// Reads all lines from a file safely with retry logic.
        /// </summary>
        public static string[] ReadAllLinesSafely(string filePath, int maxRetries = 5, int delayMs = 100)
        {
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    // Open the file with shared read/write access in case another process is writing.
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        List<string> lines = new List<string>();
                        while (!reader.EndOfStream)
                        {
                            lines.Add(reader.ReadLine());
                        }
                        return lines.ToArray();
                    }
                }
                catch (IOException)
                {
                    // Wait a bit before retrying
                    Thread.Sleep(delayMs);
                }
            }
            throw new IOException($"Unable to access {filePath} after {maxRetries} attempts.");
        }

        // Nested
        // Comparer - orders strings by category: symbols (0), numbers (1), letters (2)
        public class CustomConfigComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;

                int catX = GetCategory(x);
                int catY = GetCategory(y);
                if (catX != catY)
                {
                    return catX.CompareTo(catY);
                }
                // Same category: compare alphabetically (case-insensitive)
                return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            }

            private int GetCategory(string s)
            {
                if (string.IsNullOrEmpty(s)) return -1;
                char ch = s[0];
                if (!char.IsLetterOrDigit(ch)) return 0; // Symbols come first.
                if (char.IsDigit(ch)) return 1; // Numbers come next.
                if (char.IsLetter(ch)) return 2; // Letters last.
                return 3;
            }
        }
    }
}
