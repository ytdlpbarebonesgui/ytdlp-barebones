using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ytdlp_barebones
{
    internal static class Program
    {
        private const string MutexName = "ytdlp_barebones_mutex";
        private const int SW_RESTORE = 9;

        // Define FLASHWINFO structure.
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        // Flags for FlashWindowEx.
        public const UInt32 FLASHW_STOP = 0;
        public const UInt32 FLASHW_CAPTION = 1;
        public const UInt32 FLASHW_TRAY = 2;
        public const UInt32 FLASHW_ALL = FLASHW_CAPTION | FLASHW_TRAY;
        public const UInt32 FLASHW_TIMER = 4;
        public const UInt32 FLASHW_TIMERNOFG = 12;

        // Import necessary functions.
        [DllImport("user32.dll")]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        static void Main()
        {
            bool createdNew;

            // Attempt to create a mutex.
            using (Mutex mutex = new Mutex(true, MutexName, out createdNew))
            {
                if (!createdNew && Settings.Default.OnlyOneInstance)
                {
                    // If another instance is running, bring its window to the front and flash.
                    BringExistingInstanceToFront();
                    return;
                }

                ApplicationConfiguration.Initialize();

                // Run the app
                Application.Run(new frmMain());
            }
        }

        // Method
        // Function to bring the existing instance to the front and flash its taskbar button.
        private static void BringExistingInstanceToFront()
        {
            Process current = Process.GetCurrentProcess();
            // Look for other processes with the same name.
            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    IntPtr hWnd = process.MainWindowHandle;
                    if (hWnd != IntPtr.Zero)
                    {
                        // Restore the window if it is minimized.
                        ShowWindow(hWnd, SW_RESTORE);
                        // Bring the window to the foreground.
                        SetForegroundWindow(hWnd);

                        // Prepare the FLASHWINFO structure.
                        FLASHWINFO fwInfo = new FLASHWINFO
                        {
                            cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO))),
                            hwnd = hWnd,
                            dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG,
                            uCount = UInt32.MaxValue, // Flash until window gets focus.
                            dwTimeout = 0
                        };

                        FlashWindowEx(ref fwInfo);
                        break;
                    }
                }
            }
        }
    }
}