using System.Diagnostics;
using ytdlp_barebones.Helpers.Lists;

public static class DirectoryButtons
{
    /// Opens a FolderBrowserDialog to add a new directory. 
    /// If the chosen directory is valid and not already added, it appends the directory to the file and refreshes the list.
    public static void AddDirectory(string filePath, Action refreshDirectoryList)
    {
        while (true)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    DirectoryList.AddDirectories(new[] { folderDialog.SelectedPath }, filePath, refreshDirectoryList);
                    break;
                }
                else
                    break; // user canceled
            }
        }
    }

    /// Removes the selected directory from the file after confirmation.
    /// <param name="filePath">The file path where the directories are stored.</param>
    /// <param name="listBox">The ListBox control containing the directories.</param>
    /// <param name="refreshDirectoryList">Callback to refresh the UI list.</param>
    public static void RemoveDirectory(string filePath, ListBox listBox, Action refreshDirectoryList)
    {
        if (listBox.SelectedItem != null)
        {
            string selectedDirectory = listBox.SelectedItem.ToString();
            var result = MessageBox.Show(
                $"Are you sure you want to remove the '{selectedDirectory}' path? The original folder path won't be removed.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var directories = File.Exists(filePath)
                    ? File.ReadAllLines(filePath).ToList()
                    : new List<string>();

                directories.Remove(selectedDirectory);
                File.WriteAllLines(filePath, directories);
                MessageBox.Show("Location path deleted successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshDirectoryList?.Invoke();
            }
        }
    }

    /// <summary>
    /// Opens the selected directory in Windows Explorer.
    /// </summary>
    /// <param name="listBox">The ListBox control containing the directories.</param>
    public static void OpenDirectory(ListBox listBox)
    {
        if (listBox.SelectedItem != null)
        {
            string selectedDirectory = listBox.SelectedItem.ToString().TrimEnd('\\');
            if (Directory.Exists(selectedDirectory))
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = selectedDirectory,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
        }
    }

    // Static lock object to synchronize file access across for MoveDirectory void.
    private static readonly object _fileLock = new object();

    /// <summary>
    /// Move up and down the selected directory path.
    /// Direction should be -1 for up, +1 for down.
    /// </summary>
    /// <param name="filePath">The file path where the directories are stored.</param>
    /// <param name="listBox">The ListBox control containing the directories.</param>
    /// <param name="direction">The direction where the directory should go</param>
    public static void MoveDirectory(string filePath, ListBox listBox, int direction)
    {
        int selectedIndex = listBox.SelectedIndex;
        int newIndex = selectedIndex + direction;

        // Check bounds for the new index.
        if (newIndex < 0 || newIndex >= listBox.Items.Count)
        {
            string message = direction < 0 ?
                "The selected directory is already at the top." :
                "The selected directory is already at the bottom.";
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Swap the items.
        object currentItem = listBox.Items[selectedIndex];
        listBox.Items[selectedIndex] = listBox.Items[newIndex];
        listBox.Items[newIndex] = currentItem;

        // Save updated list order to file.
        List<string> directoryList = new List<string>();
        foreach (var item in listBox.Items) directoryList.Add(item.ToString());

        // Use a lock to prevent concurrent file access.
        lock (_fileLock) File.WriteAllLines(filePath, directoryList);

        // Update the selected index.
        listBox.SelectedIndex = newIndex;
        listBox.Focus();
    }
}
