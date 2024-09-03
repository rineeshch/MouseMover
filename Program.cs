using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HibaRineeshApp
{
    class Program
    {
        // Import necessary functions from user32.dll
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        const int SM_CXSCREEN = 0;
        const int SM_CYSCREEN = 1;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint KEYEVENTF_KEYDOWN = 0x0000;
        const uint KEYEVENTF_KEYUP = 0x0002;

        static void Main(string[] args)
        {
            // Get the screen dimensions
            int screenWidth = GetSystemMetrics(SM_CXSCREEN);
            int screenHeight = GetSystemMetrics(SM_CYSCREEN);
            Random rnd = new Random();

            // Define the vertical position (middle of the screen)
            int y = screenHeight / 2;

            // Define the movement range for the mouse (left-right area)
            int leftBoundary = screenWidth / 3;
            int rightBoundary = 2 * screenWidth / 3;

            while (true)
            {
                // Random horizontal mouse movement within the defined range
                int x = rnd.Next(leftBoundary, rightBoundary);
                SetCursorPos(x, y);

                // Occasionally click the mouse
                if (rnd.Next(10) < 3) // 30% chance to click
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, UIntPtr.Zero);
                    mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, UIntPtr.Zero);
                }

                // Occasionally press a key (e.g., Shift, Ctrl, or Alt)
                if (rnd.Next(10) < 3) // 30% chance to press a key
                {
                    byte[] keys = { 0x10, 0x11, 0x12 }; // Shift, Ctrl, Alt
                    byte key = keys[rnd.Next(keys.Length)];
                    keybd_event(key, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero); // Key down
                    keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);   // Key up
                }

                // Pause for a random time between 5 and 15 seconds
                Thread.Sleep(rnd.Next(5000, 15000));
            }
        }
    }
}
