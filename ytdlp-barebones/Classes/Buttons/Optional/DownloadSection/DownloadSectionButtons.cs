namespace ytdlp_barebones.Helpers.Buttons.Optional.DownloadSection
{
    internal class DownloadSectionButtons
    {
        // Download Section
        // Add a download section (opens frmDownloadSection). Keeps looping if duplicate found until cancel.
        public static void AddDownloadSection(DataGridView dgv, IWin32Window owner, Button btnClearAll)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));

            // Check limit (disallow > 999 rows)
            int currentRows = dgv.Rows.Count;
            if (currentRows >= 999)
            {
                MessageBox.Show("You have reached the maximum number of download sections (999). Cannot add more.",
                                "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            while (true)
            {
                using (var dlg = new frmDownloadSection())
                {
                    var result = dlg.ShowDialog(owner);
                    if (result != DialogResult.OK)
                        return; // user cancelled -> stop

                    string startTime = dlg.StartTime ?? "";
                    string endTime = dlg.EndTime ?? "";

                    // Duplicate check
                    bool duplicate = false;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string existingStart = row.Cells["startTime"]?.Value?.ToString() ?? row.Cells[1].Value?.ToString() ?? "";
                        string existingEnd = row.Cells["endTime"]?.Value?.ToString() ?? row.Cells[2].Value?.ToString() ?? "";

                        if (existingStart == startTime && existingEnd == endTime)
                        {
                            duplicate = true;
                            break;
                        }
                    }

                    if (duplicate)
                    {
                        MessageBox.Show("This start-end time section already exists. Please try again.",
                                        "Duplicate Section", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // loop again -> re-open dialog
                        continue;
                    }

                    // Determine new timeNum as 3-digit number
                    int newIndex = dgv.Rows.Count + 1;
                    if (newIndex > 999)
                    {
                        MessageBox.Show("Cannot add more than 999 rows.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string timeNum = newIndex.ToString("D3"); // 001, 002, ...

                    // Add row to grid (timeNum, startTime, endTime)
                    dgv.Rows.Add(timeNum, startTime, endTime);

                    // enable clear-all button (there is now at least one row)
                    if (btnClearAll != null) btnClearAll.Enabled = true;

                    return; // success -> exit
                }
            }
        }
        // Edit the selected download section (opens frmDownloadSection in edit mode)
        public static void EditDownloadSection(DataGridView dgv, IWin32Window owner)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));

            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select exactly one download section to edit.", "Edit Section", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgv.SelectedRows[0];
            if (selectedRow == null || selectedRow.IsNewRow) return;

            string originalTimeNum = selectedRow.Cells["timeNum"]?.Value?.ToString() ?? "";
            string originalStart = selectedRow.Cells["startTime"]?.Value?.ToString() ?? "";
            string originalEnd = selectedRow.Cells["endTime"]?.Value?.ToString() ?? "";

            while (true)
            {
                using (var dlg = new frmDownloadSection())
                {
                    // Preload for edit
                    dlg.LoadForEdit(originalTimeNum, originalStart, originalEnd);

                    var result = dlg.ShowDialog(owner);
                    if (result != DialogResult.OK)
                        return; // user cancelled

                    string newStart = dlg.StartTime ?? "";
                    string newEnd = dlg.EndTime ?? "";

                    // Check duplicate: find any other row that already has the same start+end
                    bool duplicateFound = false;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string rowTimeNum = row.Cells["timeNum"]?.Value?.ToString() ?? "";
                        string rowStart = row.Cells["startTime"]?.Value?.ToString() ?? "";
                        string rowEnd = row.Cells["endTime"]?.Value?.ToString() ?? "";

                        // Skip the row being edited
                        if (string.Equals(rowTimeNum, originalTimeNum, StringComparison.OrdinalIgnoreCase)) continue;

                        if (string.Equals(rowStart, newStart, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(rowEnd, newEnd, StringComparison.OrdinalIgnoreCase))
                        {
                            duplicateFound = true;
                            break;
                        }
                    }

                    if (duplicateFound)
                    {
                        MessageBox.Show("Another section already uses those start/end times. Please choose different times.", "Duplicate Section", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // loop again so user can edit
                        continue;
                    }

                    // Find the row that still matches the original timeNum (in case rows were modified/deleted)
                    DataGridViewRow targetRow = null;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string rowTimeNum = row.Cells["timeNum"]?.Value?.ToString() ?? "";
                        if (string.Equals(rowTimeNum, originalTimeNum, StringComparison.OrdinalIgnoreCase))
                        {
                            targetRow = row;
                            break;
                        }
                    }

                    if (targetRow == null)
                    {
                        MessageBox.Show($"The row with number {originalTimeNum} no longer exists. Edit aborted.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Apply update in-place
                    targetRow.Cells["startTime"].Value = newStart;
                    targetRow.Cells["endTime"].Value = newEnd;

                    return; // success -> exit
                }
            }
        }
        // Remove selected download section and renumber later rows (keeps sequence continuous)
        public static void RemoveDownloadSection(DataGridView dgv, Button btnEdit, Button btnRemove, Button btnClearAll)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));

            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select exactly one download section to remove.", "Remove Section", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sel = dgv.SelectedRows[0];
            if (sel.IsNewRow) return;

            string timeNum = sel.Cells["timeNum"]?.Value?.ToString() ?? sel.Cells[0].Value?.ToString() ?? "";
            string start = sel.Cells["startTime"]?.Value?.ToString() ?? sel.Cells[1].Value?.ToString() ?? "";
            string end = sel.Cells["endTime"]?.Value?.ToString() ?? sel.Cells[2].Value?.ToString() ?? "";

            // Parse selected timeNum to integer
            if (!int.TryParse(timeNum, out int removedNum))
            {
                // Fallback: if parsing fails, just remove this row and exit
                dgv.Rows.Remove(sel);
            }
            else
            {
                // Remove the selected row first
                dgv.Rows.Remove(sel);

                // Decrement timeNum for rows with number > removedNum
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    string tnStr = row.Cells["timeNum"]?.Value?.ToString() ?? row.Cells[0].Value?.ToString() ?? "";
                    if (!int.TryParse(tnStr, out int tn)) continue;

                    if (tn > removedNum)
                    {
                        int newNum = tn - 1;
                        row.Cells["timeNum"].Value = newNum.ToString("D3"); // keep 3-digit format
                    }
                }
            }

            // Tidy up selection and button states
            dgv.ClearSelection();
            if (btnEdit != null) btnEdit.Enabled = false;
            if (btnRemove != null) btnRemove.Enabled = false;
            if (btnClearAll != null) btnClearAll.Enabled = dgv.Rows.Count > 0;
        }
        // Clear all download sections after confirmation.
        public static void ClearAllDownloadSections(DataGridView dgv, Button btnEdit, Button btnRemove, Button btnClearAll)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));

            var dr = MessageBox.Show("This will remove all added download sections. Are you sure you want to continue?", "Clear All Sections", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes) return;

            dgv.Rows.Clear();
            dgv.ClearSelection();

            if (btnClearAll != null) btnClearAll.Enabled = false;
            if (btnEdit != null) btnEdit.Enabled = false;
            if (btnRemove != null) btnRemove.Enabled = false;
        }
    }
}