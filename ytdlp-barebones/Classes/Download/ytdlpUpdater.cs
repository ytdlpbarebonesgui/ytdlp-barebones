using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

namespace ytdlp_barebones.Helpers.Download
{
    public static class ytdlpUpdater
    {
        // Helper method to run a process in hidden mode and return its standard output.
        private static string RunHiddenProcess(string fileName, string arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd().Trim();
            }
        }

        // Returns the installed version by running yt-dlp.exe with --version.
        public static string GetYtDlpVersion(string ytDlpPath)
        {
            return RunHiddenProcess(ytDlpPath, "--version");
        }

        // Checks if ffmpeg is installed by silently running "ffmpeg -version".
        // Returns the first line of the version output, or "Not Installed!" if ffmpeg isn't found.
        public static string GetFfmpegVersion()
        {
            try
            {
                string output = RunHiddenProcess("ffmpeg", "-version");
                return !string.IsNullOrWhiteSpace(output)
                    ? output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)[0] // Return only the first line (e.g. "ffmpeg version ...")
                    : "ffmpeg is Not Installed!";
            }
            catch (Exception)
            {
                return "ffmpeg is Not Installed!";
            }
        }

        // Asynchronously fetches the latest version from GitHub.
        public static async Task<string> GetLatestYtDlpVersion()
        {
            // Use HttpClient to get the latest release info from GitHub API.
            using (HttpClient client = new HttpClient())
            {
                // GitHub API URL for the latest release of yt-dlp.
                string versionUrl = "https://api.github.com/repos/yt-dlp/yt-dlp/releases/latest";
                // GitHub API requires a User-Agent header.
                client.DefaultRequestHeaders.UserAgent.ParseAdd("request");

                // Send GET request and get the response as a string.
                string response = await client.GetStringAsync(versionUrl);

                // Parse the JSON response to extract the tag_name.
                using JsonDocument doc = JsonDocument.Parse(response);
                // Get the root element of the JSON document.
                JsonElement root = doc.RootElement;

                // get the tag_name property, which contains the version string.
                if (root.TryGetProperty("tag_name", out JsonElement tagNameElement))
                {
                    // Return the version string.
                    return tagNameElement.GetString()!;
                }

                return "";
            }
        }

        // Runs the built‑in update command for yt‑dlp.
        public static void UpdateYtDlp(string ytDlpPath) => RunHiddenProcess(ytDlpPath, "-U"); // Here, the output is ignored.

        // Checks if yt-dlp.exe exists; if not, downloads it. Otherwise,
        // Checks for updates and performs an update if needed (only if AutoCheckUpdates is true).
        // All blocking operations are run on background threads so that the app remains responsive.
        // Returns a log string with status messages.
        public static async Task<string> EnsureLatestYtDlpAsync(string ytDlpPath, bool forceUpdate = false)
        {
            StringBuilder log = new StringBuilder();
            // URL to download the latest yt-dlp.exe from GitHub.
            string downloadUrl = "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe";

            if (!File.Exists(ytDlpPath))
            {
                // If yt-dlp.exe is missing, try to download it regardless of AutoCheckUpdates.
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        byte[] fileData = await client.GetByteArrayAsync(downloadUrl);
                        await File.WriteAllBytesAsync(ytDlpPath, fileData);
                        log.AppendLine("yt-dlp Download Complete!");
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = "Error downloading yt-dlp.exe: " + ex.Message +
                                          "\nThe application is unusable without both yt-dlp.exe and an internet connection. " +
                                          "Please check your connection and reopen the app.";
                        MessageBox.Show(errorMsg, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                }
            }
            else
            {
                // Check for updates if forceUpdate is true or if AutoCheckUpdates is enabled.
                if (forceUpdate || Settings.Default.AutoCheckUpdates)
                {
                    try
                    {
                        // Get the current version.
                        string currentVersion = await Task.Run(() => GetYtDlpVersion(ytDlpPath));
                        string latestVersion = "";

                        bool networkUp = NetworkInterface.GetIsNetworkAvailable();
                        if (!networkUp)
                        {
                            log.AppendLine("No internet connection. Cannot process checking updates. Reopen the app to automatically check updates again.");
                            latestVersion = currentVersion;
                        }
                        else
                        {
                            try
                            {
                                // Attempt to get the latest version.
                                latestVersion = await GetLatestYtDlpVersion();
                            }
                            catch (HttpRequestException ex)
                            {
                                // Handle GitHub rate limit (403) separately to inform user accurately.
                                if (ex.Message.Contains("403") || ex.Message.Contains("rate limit"))
                                    log.AppendLine("GitHub rate limit exceeded. Cannot check for updates right now. Wait for about an hour.");
                                else // Handle general network-related errors like no connection, DNS failure, timeout, etc.
                                    log.AppendLine("Network error while checking for updates: " + ex.Message);

                                latestVersion = currentVersion;
                            }
                            // Catch all other unexpected exceptions (ex. parsing, internal logic errors).
                            catch (Exception ex)
                            {
                                log.AppendLine("Unexpected error during update check: " + ex.Message);
                                latestVersion = currentVersion;
                            }
                            // let other exceptions escape so you can catch bad parsing/etc.
                        }

                        // If the current version is not equal to the latest version, update.
                        if (currentVersion != latestVersion)
                        {
                            await Task.Run(() => UpdateYtDlp(ytDlpPath));
                            log.AppendLine("yt-dlp Updated to Latest Version!");
                        }
                        else
                            log.AppendLine("yt-dlp.exe Detected and Up-to-Date!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error checking for updates: " + ex.Message);
                    }
                }
                // If neither force update nor AutoCheckUpdates, no update check is performed.
            }

            // Tasks to check yt-dlp and ffmpeg version
            var ytDlpVersionTask = Task.Run(() => GetYtDlpVersion(ytDlpPath));
            var ffmpegVersionTask = Task.Run(() => GetFfmpegVersion());
            // Run both at the same time for lesser loading time
            await Task.WhenAll(ytDlpVersionTask, ffmpegVersionTask);

            // Display the current version after processing.
            string installedVersion = ytDlpVersionTask.Result;
            string ffmpegVersion = ffmpegVersionTask.Result;

            log.AppendLine($"yt-dlp version: {installedVersion}");
            log.Append(ffmpegVersion);

            return log.ToString();
        }
    }
}