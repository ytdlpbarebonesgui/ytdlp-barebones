using System.Text.RegularExpressions;
using ytdlp_barebones.Helpers.Display;

namespace ytdlp_barebones.Helpers.Download
{
    public static class DownloadProcess
    {
        // PUBLIC METHODS
        // The main method that performs the download process
        public static void PerformDownload(
            string ytDlpCommand,
            string[] urls,
            int selectedIndex,
            string selectedDirectory,
            string urlsFilePath,
            string selectedFile,
            TextBox txturls,
            string execText,
            List<string> downloadSectionRanges)
        {
            // Validate inputs.
            List<string> errors = ValidationDownload(ytDlpCommand, urls, selectedIndex, selectedDirectory, downloadSectionRanges);
            if (errors.Count > 0)
            {
                string message = errors.Count == 1
                    ? errors[0]
                    : "Multiple errors detected:" + Environment.NewLine + string.Join(Environment.NewLine, errors.Select(e => "- " + e));
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Replace placeholders to build the full command.
            string fullCommand = ReplacePlaceholders(ytDlpCommand, urls, selectedDirectory, urlsFilePath, selectedFile, execText, downloadSectionRanges);

            // Ask for confirmation.
            string confirmationMessage = $"Are you sure you want to execute the yt-dlp command to download with this configuration?\n\n{fullCommand}";
            DialogResult result = MessageBox.Show(confirmationMessage, "Confirm Download", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            // Clear txturls, if ClearURLsAfterDownload is true
            if (Settings.Default.ClearURLsDownload && txturls != null)
                txturls.Clear();

            // Always open external cmd with the generated command
            CMDdisplay.ExecuteCommandExternal(fullCommand);
        }

        // Validates the download inputs.
        public static List<string> ValidationDownload(string ytDlpCommand, string[] urls, int selectedIndex, string selectedDirectory, List<string> downloadSectionRanges)
        {
            var errors = new List<string>();

            // If URLRequired and/or LocationRequired from Settings.settings is true
            if (Settings.Default.LocationRequired)
            {
                // Validate the selected directory.
                if (selectedIndex == -1)
                    errors.Add("No location path is selected.");
                else if (string.IsNullOrWhiteSpace(selectedDirectory) || !Directory.Exists(selectedDirectory))
                    errors.Add("The selected location path is invalid or does not exist.");

                // If '<dir>' is not contained
                if (!ytDlpCommand.Contains("<dir>"))
                {
                    // Check for output-related flag
                    if (ytDlpCommand.Contains(" -o ") || ytDlpCommand.Contains(" --output "))
                        errors.Add("The <dir> placeholder must be placed next to output command to avoid manual directory path typing (ex. -o \"<dir>\\yt-dlp\\Video\")");
                }
            }

            if (Settings.Default.URLRequired)
            {
                // Validate URLs. If no URLs are added on txturls
                if (urls == null || urls.Length == 0)
                {
                    errors.Add("No URL provided.");
                }
                else
                {
                    // Regex pattern to detect a functioning URL:
                    // - Optionally starts with "http://" or "https://"
                    // - Must contain at least one dot separating domain levels
                    // - Optionally followed by a path
                    string pattern = @"\b((https?://)?([\w-]+\.)+[\w-]+(/[^\s]*)?)\b";
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                    // Validate each URL in the array.
                    foreach (string url in urls)
                    {
                        if (!regex.IsMatch(url))
                        {
                            errors.Add($"Invalid URL format: {url}");
                        }
                    }
                }

                // Check if <url> is not typed in
                if (!ytDlpCommand.Contains("<url>"))
                {
                    // Check for URL-related flags
                    if (ytDlpCommand.Contains(" -a ") || ytDlpCommand.Contains(" -x ") || ytDlpCommand.Contains(" --yes-playlist "))
                        errors.Add("The <url> placeholder must be placed next to URL option (-a, -x, --yes-playlist) to avoid having to manually create a file with multiple URLs or type in the URL.");
                }
            }

            // If there are multiple download sections, require autonumber OR an output template containing .%(ext)s
            // If command already references autonumber, it's safe
            // No .%(ext)s and no autonumber -> error
            if (!Settings.Default.OutputAutoComplete && downloadSectionRanges != null && downloadSectionRanges.Count > 1 && !ytDlpCommand.Contains("autonumber") && !ytDlpCommand.Contains(".%(ext)"))
            {
                errors.Add("If there are multiple download sections to download a single video, you must include 'autonumber' on the filename output to avoid the video file from being replaced repeatedly. (Example: \"%(title)s - %(autonumber)s.%(ext)s\")");
            }

            // Return the list of errors.
            return errors;
        }

        // Processes the placeholders, writes URLs to a file if needed, and prepends "yt-dlp ".
        public static string ReplacePlaceholders(
            string ytDlpCommand,
            string[] urls,
            string selectedDirectory,
            string urlsFilePath,
            string selectedFile,
            string execText,
            List<string> downloadSectionRanges)
        {
            // If no location is selected or it doesn’t exist, then default to your Downloads folder.
            // The code on selectedDirectory dynamically detects the correct system drive (C:, D:, etc.) instead of assuming "C:\\".
            // Ensures compatibility on systems where Windows is installed on a different drive.
            if ((string.IsNullOrWhiteSpace(selectedDirectory) || !Directory.Exists(selectedDirectory)) && !Settings.Default.LocationRequired)
                selectedDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            // Process the <dir> placeholder.
            if (ytDlpCommand.Contains("<dir>"))
                ytDlpCommand = ytDlpCommand.Replace("<dir>", selectedDirectory);
            else
            {
                // If a directory path was selected and OutputAutoComplete is enabled.
                if ((!string.IsNullOrEmpty(selectedDirectory) || !Directory.Exists(selectedDirectory)) && Settings.Default.OutputAutoComplete)
                {
                    bool hasOutputFlag = ytDlpCommand.Contains(" -o ") || ytDlpCommand.Contains(" --output ");
                    if (!hasOutputFlag)
                        ytDlpCommand += " -o \"" + selectedDirectory + "\\%(title).50s [%(id)s].%(ext)s\""; // Append the output directive.
                }
            }

            // Process the <url> placeholder.
            if (urls != null)
            {
                if (ytDlpCommand.Contains("<url>"))
                {
                    if (ytDlpCommand.Contains("-a <url>"))
                        ytDlpCommand = ytDlpCommand.Replace("<url>", urlsFilePath);
                    else
                    {
                        if (urls.Length > 1)
                        {
                            File.WriteAllLines(urlsFilePath, urls);
                            ytDlpCommand = ytDlpCommand.Replace("<url>", "-a " + urlsFilePath);
                        }
                        else if (urls.Length == 1)
                            ytDlpCommand = ytDlpCommand.Replace("<url>", urls[0]);
                        else
                            ytDlpCommand = ytDlpCommand.Replace("<url>", "");
                    }
                }
                else
                {
                    bool hasURLFlag = ytDlpCommand.Contains(" -a ") || ytDlpCommand.Contains(" -x ") || ytDlpCommand.Contains(" --yes-playlist ");
                    if (!hasURLFlag && urls.Length > 0 && Settings.Default.URLAutoComplete)
                    {
                        if (urls.Length > 1)
                        {
                            File.WriteAllLines(urlsFilePath, urls);
                            ytDlpCommand += " -a " + urlsFilePath;
                        }
                        else
                            ytDlpCommand += " " + urls[0];
                    }
                }
            }
            else
                ytDlpCommand = ytDlpCommand.Replace("<url>", "");

            // Apply MonoAudioMode modifications before file-related prefixes are added.
            ytDlpCommand = ApplyMonoAudioMode(ytDlpCommand, Settings.Default.MonoAudioMode);

            // Process the file placeholders.
            bool hasFile = ytDlpCommand.Contains("<file>");
            bool hasFilecom = ytDlpCommand.Contains("<filecom>");
            bool fileSelected = !string.IsNullOrWhiteSpace(selectedFile) && !selectedFile.Equals("-- Select File --", StringComparison.OrdinalIgnoreCase);

            if (!hasFile && !hasFilecom && Settings.Default.FileAutoComplete)
            {
                if (fileSelected)
                {
                    string prefix = "";
                    if (selectedFile.StartsWith("cookies", StringComparison.OrdinalIgnoreCase))
                        prefix = "--cookies \"" + selectedFile + "\" ";

                    else if (selectedFile.StartsWith("auth", StringComparison.OrdinalIgnoreCase) && selectedFile.EndsWith(".netrc", StringComparison.OrdinalIgnoreCase))
                        prefix = "--netrc \"" + selectedFile + "\" ";

                    if (!string.IsNullOrEmpty(prefix))
                        ytDlpCommand = prefix + ytDlpCommand;
                }
            }
            else if (hasFile)
            {
                if (fileSelected)
                    ytDlpCommand = ytDlpCommand.Replace("<file>", selectedFile);
                else
                    ytDlpCommand = ytDlpCommand.Replace("<file>", "").Replace("<file> ", "");
            }
            else if (hasFilecom)
            {
                if (fileSelected)
                {
                    if (selectedFile.StartsWith("cookies", StringComparison.OrdinalIgnoreCase))
                        ytDlpCommand = ytDlpCommand.Replace("<filecom>", "--cookies \"" + selectedFile + "\"");

                    else if (selectedFile.StartsWith("auth", StringComparison.OrdinalIgnoreCase) && selectedFile.EndsWith(".netrc", StringComparison.OrdinalIgnoreCase))
                        ytDlpCommand = ytDlpCommand.Replace("<filecom>", "--netrc \"" + selectedFile + "\"");

                    else
                        ytDlpCommand = ytDlpCommand.Replace("<filecom>", "");
                }
                else
                    ytDlpCommand = ytDlpCommand.Replace("<filecom>", "").Replace("<filecom> ", "");
            }

            // Download Sections
            // Local helper that builds --download-sections arguments from a list
            string BuildDownloadSections(List<string> ranges, string singleRange = null, string baseCommand = "")
            {
                // If downloadSectionRanges is null, empty, or DownSecAutoComplete is flase => do nothing.
                // Otherwise, prepend one --download-sections "range" per entry in the list (keeps order).
                if ((!Settings.Default.DownSecAutoComplete || (ranges == null || ranges.Count <= 0))
                && string.IsNullOrEmpty(singleRange))
                    return string.Empty;

                var sb = new System.Text.StringBuilder();

                // Accurate section cuts (force keyframes at cuts)
                if (Settings.Default.ForceKeyframesAtCuts && !baseCommand.Contains("--force-keyframes-at-cuts", StringComparison.OrdinalIgnoreCase))
                    sb.Append("--force-keyframes-at-cuts ");

                // Single explicit section (placeholder case)
                if (!string.IsNullOrWhiteSpace(singleRange))
                {
                    sb.Append("--download-sections \"");
                    sb.Append(singleRange.StartsWith("*") ? singleRange : "*" + singleRange);
                    sb.Append("\" ");
                    return sb.ToString();
                }

                // Multi-section logic
                foreach (var r in ranges)
                {
                    if (string.IsNullOrWhiteSpace(r)) continue;

                    var parts = r.Split('|');
                    string safe = parts.Length == 2 ? parts[1] : r;
                    safe = safe.StartsWith("*") ? safe : "*" + safe;

                    sb.Append("--download-sections \"");
                    sb.Append(safe);
                    sb.Append("\" ");
                }
                return sb.ToString();
            }
            // Handle <downsec-#>, <downsec-##>, <downsec-###> placeholders
            var match = Regex.Match(ytDlpCommand, @"<downsec-(\d{1,3})>");
            if (match.Success)
            {
                string rawNum = match.Groups[1].Value;
                string paddedNum = rawNum.PadLeft(3, '0'); // normalize 1->001, 12->012, 123->123
                string placeholder = $"<downsec-{paddedNum}>";

                // Replace short form (<downsec-#> or <downsec-##>) with normalized one
                ytDlpCommand = Regex.Replace(ytDlpCommand, @"<downsec-(\d{1,3})>", placeholder);

                // Find if a matching timeNum exists in downloadSectionRanges
                string selectedRange = null;
                foreach (var r in downloadSectionRanges ?? new List<string>())
                {
                    // Each range should be tagged with its timeNum like "002|*00:01:00-00:05:00"
                    // (you’d generate this when populating downloadSectionRanges in frmMain)
                    var parts = r.Split('|');
                    if (parts.Length == 2 && parts[0] == paddedNum)
                    {
                        selectedRange = parts[1]; // "*hh:mm:ss-hh:mm:ss"
                        break;
                    }
                }

                // Always build via helper (pass current command so it can check for duplicates)
                ytDlpCommand = ytDlpCommand.Replace(placeholder,
                    BuildDownloadSections(downloadSectionRanges, selectedRange, ytDlpCommand));
            }
            else // No placeholder
            {
                // No placeholder -> multi-section logic as usual
                ytDlpCommand = BuildDownloadSections(downloadSectionRanges, null, ytDlpCommand) + ytDlpCommand;
            }

            // If multiple sections and no 'autonumber' present, inject autonumber into any ".%(ext)s" templates.
            // If there's no ".%(ext)", ValidationDownload will have already added an error and prevented the run.
            if (downloadSectionRanges != null && downloadSectionRanges.Count > 1 && ytDlpCommand.IndexOf("autonumber", StringComparison.OrdinalIgnoreCase) < 0)
            {
                const string extToken = ".%(ext)";
                if (ytDlpCommand.Contains(extToken))
                {
                    // Insert a readable separator plus autonumber before the extension token.
                    // Example: "%(title)s.%(ext)s" -> "%(title)s - %(autonumber)s.%(ext)s"
                    ytDlpCommand = ytDlpCommand.Replace(extToken, " - %(autonumber)s" + extToken);
                }
            }

            // Exec
            // Prepare exec flag (escape any internal double-quotes)
            if (!string.IsNullOrWhiteSpace(execText))
            {
                string escaped = execText.Replace("\"", "\\\"");
                ytDlpCommand = $"--exec \"{escaped}\" " + ytDlpCommand;
            }

            // Prepend "yt-dlp " before the command.
            string fullCommand = "yt-dlp " + ytDlpCommand;
            return fullCommand;
        }

        // PRIVATE METHODS
        // For ReplacePlaceholders
        /// <summary>
        /// Enforces mono audio mode in the provided yt-dlp command according to rules:
        /// - If monoEnabled is false, returns command unchanged.
        /// - If "-ac 1" already exists, do nothing.
        /// - Replace any "-ac X" with "-ac 1".
        /// - If --postprocessor-args / --ppa exists, ensure the quoted value contains exactly one "-ac 1" (replace or prepend).
        /// - If no postprocessor args exist, prepend a single "--postprocessor-args \"-ac 1\" " at the start of the command.
        /// - Never leave duplicate -ac entries or multiple --postprocessor-args flags.
        /// </summary>
        private static string ApplyMonoAudioMode(string ytDlpCommand, bool monoEnabled)
        {
            if (!monoEnabled || string.IsNullOrWhiteSpace(ytDlpCommand))
                return ytDlpCommand;

            // Quick check: if there is already a literal "-ac 1" (word boundary), do nothing.
            var ac1Regex = new Regex(@"\b-ac\s+1\b", RegexOptions.IgnoreCase);
            if (ac1Regex.IsMatch(ytDlpCommand))
                return ytDlpCommand; // Rule 1: already set

            // Regex to find any -ac <digits>
            var acAnyRegex = new Regex(@"\b-ac\s+(\d+)\b", RegexOptions.IgnoreCase);

            // For video recoding
            // Handle --merge-output-format -> --recode-video when safe ---
            // Replace --merge-output-format X with --recode-video X for common video containers
            // but only when it's safe (no conflicting flags like --audio-format, --remux-video, --keep-video or existing --recode-video)
            var mergeRegex = new Regex(@"--merge-output-format(?:\s*=\s*|\s+)(?<fmt>[\w\d]+)", RegexOptions.IgnoreCase);
            var mm = mergeRegex.Match(ytDlpCommand);
            if (mm.Success)
            {
                string fmt = mm.Groups["fmt"].Value.ToLowerInvariant();
                var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "mp4", "mkv", "webm", "flv", "mov", "avi", "mpeg" };
                if (allowed.Contains(fmt))
                {
                    // If there are flags that indicate user already handling re-encode/remux or audio-format, skip replacement
                    bool hasAudioFormat = Regex.IsMatch(ytDlpCommand, @"\b--audio-format\b", RegexOptions.IgnoreCase);
                    bool hasRemuxOrKeep = Regex.IsMatch(ytDlpCommand, @"\b--remux-video\b|\b--keep-video\b", RegexOptions.IgnoreCase);
                    bool hasRecode = Regex.IsMatch(ytDlpCommand, @"\b--recode-video\b", RegexOptions.IgnoreCase);

                    if (!hasAudioFormat && !hasRemuxOrKeep && !hasRecode)
                    {
                        // Perform replacement: use --recode-video <fmt>
                        ytDlpCommand = mergeRegex.Replace(ytDlpCommand, "--recode-video " + fmt);
                    }
                }
                // else: non-standard container -> leave as-is (do not replace)
            }

            // Regex to find all --postprocessor-args or --ppa occurrences with quoted value (single or double quotes)
            var ppaRegex = new Regex(@"(?:--postprocessor-args|--ppa)(?:\s*=\s*|\s+)(?:\""?([^\""]+)\""?|'?([^''']+)')", RegexOptions.IgnoreCase);
            var ppaMatches = ppaRegex.Matches(ytDlpCommand);

            if (ppaMatches.Count > 0)
            {
                // Combine all inner values (in case there are multiple flags) into one and produce a single --postprocessor-args
                var inners = new List<string>();
                foreach (Match m in ppaMatches)
                {
                    string inner = m.Groups[1].Success ? m.Groups[1].Value : m.Groups[2].Value;
                    inners.Add(inner);
                }

                // Remove all existing --postprocessor-args / --ppa flags from the ytdlp command
                ytDlpCommand = ppaRegex.Replace(ytDlpCommand, "");

                // Combine inner values preserving order and spacing
                string combinedInner = string.Join(" ", inners.Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => i.Trim()));

                if (acAnyRegex.IsMatch(combinedInner))
                {
                    // Replace any -ac X inside combinedInner with -ac 1
                    combinedInner = acAnyRegex.Replace(combinedInner, "-ac 1");

                    // Collapse duplicate "-ac 1" occurrences to one
                    combinedInner = Regex.Replace(combinedInner, @"(?:\b-ac\s+1\b)(?:[\s\S]*?\b-ac\s+1\b)+", "-ac 1", RegexOptions.IgnoreCase);

                    // A more conservative collapse: remove subsequent occurrences of "-ac 1"
                    // (Ensure only one -ac 1 remains)
                    int firstIndex = Regex.Match(combinedInner, @"\b-ac\s+1\b", RegexOptions.IgnoreCase).Index;
                    // Remove any other -ac 1 occurrences
                    combinedInner = Regex.Replace(combinedInner, @"\b-ac\s+1\b", m =>
                    {
                        if (m.Index == firstIndex) return m.Value;
                        return ""; // remove
                    }, RegexOptions.IgnoreCase);
                }
                else
                {
                    // No -ac inside existing ppa value -> prepend single -ac 1
                    combinedInner = "-ac 1 " + combinedInner;
                }

                // Normalize whitespace
                combinedInner = Regex.Replace(combinedInner.Trim(), @"\s{2,}", " ");

                // Prepend the single consolidated --postprocessor-args at the start of the ytdlp Command
                string ppaSegment = "--postprocessor-args \"" + combinedInner + "\" ";
                ytDlpCommand = ppaSegment + ytDlpCommand;

                // After consolidating PPA, ensure there are no stray -ac <num> elsewhere: remove them but keep the one inside our ppa
                // Strategy: temporarily protect the ppaSegment, remove other -ac entries, then restore.
                string marker = "___PPA_PROTECT_MARKER___";
                ytDlpCommand = ytDlpCommand.Replace(ppaSegment, marker);

                // Remove any other -ac <digits>
                ytDlpCommand = acAnyRegex.Replace(ytDlpCommand, "");

                // Collapse multiple spaces
                ytDlpCommand = Regex.Replace(ytDlpCommand, @"\s{2,}", " ").Trim();

                // Restore ppaSegment at start
                ytDlpCommand = ppaSegment + ytDlpCommand.Replace(marker, "");

                return ytDlpCommand.Trim();
            }

            // No existing --postprocessor-args/--ppa found.
            // If there are any -ac <digits> occurrences elsewhere, replace them with a single -ac 1 (Rule 2)
            if (acAnyRegex.IsMatch(ytDlpCommand))
            {
                // Replace all -ac <num> with a marker
                const string AC_MARKER = "___AC_MARKER___";
                ytDlpCommand = acAnyRegex.Replace(ytDlpCommand, AC_MARKER);

                // Replace first marker with -ac 1 and remove others
                if (ytDlpCommand.Contains(AC_MARKER))
                {
                    ytDlpCommand = Regex.Replace(ytDlpCommand, Regex.Escape(AC_MARKER), "-ac 1", RegexOptions.None, TimeSpan.FromSeconds(1));
                    // Remove any additional markers
                    ytDlpCommand = ytDlpCommand.Replace(AC_MARKER, "");
                }

                // Clean extra spaces
                ytDlpCommand = Regex.Replace(ytDlpCommand, @"\s{2,}", " ").Trim();

                return ytDlpCommand;
            }

            // No -ac anywhere and no ppa -> prepend a single --postprocessor-args "-ac 1" at the start
            string segment = "--postprocessor-args \"-ac 1\" ";
            ytDlpCommand = segment + ytDlpCommand;

            // Normalize whitespace and return
            ytDlpCommand = Regex.Replace(ytDlpCommand, @"\s{2,}", " ").Trim();
            return ytDlpCommand;
        }
    }
}