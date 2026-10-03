using System.Diagnostics;

namespace ytdlp_barebones.Helpers.Display
{
    public static class CMDdisplay
    {
        // Executes the provided command in an external CMD window (visible)
        // with administrator privileges. The command string is modified to include a powershell bell and pause
        // so that after executing the command the window will wait for the user to press any key.
        public static void ExecuteCommandExternal(string command)
        {
            string externalCommand = $"{command} && powershell -c \"[System.Media.SystemSounds]::Asterisk.Play()\" & pause";
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + externalCommand)
            {
                UseShellExecute = true,
                CreateNoWindow = false // Show the window.
            };

            // Start the external cmd process.
            Process.Start(processInfo);
        }

        // The internal execution and output-capturing methods were removed to simplify behavior.
    }
}