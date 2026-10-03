using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ytdlp_barebones.Helpers.Lists;
using ytdlp_barebones.Helpers.Buttons.Optional;

namespace ytdlp_barebones.Helpers.Events
{
    internal class DragEvents
    {
        #region lbdirectories
        public static void DirectoryDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        public static void DirectoryDragDrop(object sender, DragEventArgs e, string filePath, Action refreshDirectoryList)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                DirectoryList.AddDirectories(paths.Where(Directory.Exists), filePath, refreshDirectoryList);
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = e.Data.GetData(DataFormats.Text) as string;
                if (!string.IsNullOrWhiteSpace(text))
                {
                    string[] lines = text
                        .Replace("\r\n", "\n")
                        .Replace("\r", "\n")
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    DirectoryList.AddDirectories(lines, filePath, refreshDirectoryList);
                }
            }
        }
        #endregion
        #region cmbfilelist
        // Handles DragEnter for file list ComboBoxes
        public static void FileDragEnter(object sender, DragEventArgs e)
        {
            // Check for file drop or text data
            if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // Handles DragDrop for file list ComboBoxes
        public static void FileDragDrop(object sender, DragEventArgs e)
        {
            // Ensure sender is a ComboBox
            if (!(sender is ComboBox combo)) return;

            // If file drop, move files into app folders and refresh
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Get dropped file paths
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                FileButtons.ImportFiles(paths, () => FileList.RefreshFileList());

                // Also update the ComboBox text area to list the files with '- ' prefix each
                if (paths != null && paths.Length > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var p in paths)
                    {
                        sb.AppendLine("- " + Path.GetFileName(p));
                    }
                    // Show in the combobox tooltip or text? ComboBox doesn't support multi-line text; set as SelectedItem if exists
                    // We'll set the Text property so user can see the list when dropdown closed.
                    combo.Text = sb.ToString().TrimEnd();
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = e.Data.GetData(DataFormats.Text) as string;
                if (string.IsNullOrWhiteSpace(text)) return;

                // Normalize newlines and split into lines
                var lines = text.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n')
                    .Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                // If lines look like file paths, try to import files
                var existingPaths = lines.Where(File.Exists).ToArray();
                if (existingPaths.Length > 0)
                {
                    FileButtons.ImportFiles(existingPaths, () => FileList.RefreshFileList());
                }

                // Format lines with '- ' prefix and set to ComboBox.Text
                var sb = new StringBuilder();
                foreach (var l in lines)
                    sb.AppendLine("- " + l);

                combo.Text = sb.ToString().TrimEnd();
            }
        }
        #endregion
        #region txturls
        // Handles DragEnter for URL textboxes
        public static void URLDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Text)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }
        // Handles DragDrop for URL textboxes
        public static void URLDragDrop(object sender, DragEventArgs e)
        {
            if (sender is TextBox textBox && e.Data.GetDataPresent(DataFormats.Text))
            {
                string droppedText = (string)e.Data.GetData(DataFormats.Text);

                // Normalize line breaks to \n only
                droppedText = droppedText.Replace("\r\n", "\n").Replace("\r", "\n");

                // Remove non-ASCII and control characters (keep printable ASCII + whitespace)
                droppedText = new string(droppedText
                    .Where(c => c >= 32 && c <= 126 || c == '\n' || c == '\t')
                    .ToArray());

                // Trim each line, remove empty ones
                var lines = droppedText
                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim());

                string cleaned = string.Join(Environment.NewLine, lines);

                // Append cleaned text with a trailing newline
                if (!string.IsNullOrWhiteSpace(cleaned))
                {
                    if (textBox.Text.Length > 0 && !textBox.Text.EndsWith(Environment.NewLine))
                        textBox.AppendText(Environment.NewLine);

                    textBox.AppendText(cleaned + Environment.NewLine);
                }
            }
        }
        #endregion
    }
}