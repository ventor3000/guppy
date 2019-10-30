using System;
using System.Runtime.InteropServices;
using System.Security;

namespace GuppyGUI.WindowsSpecific
{
    public static class WinAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool PeekMessage(out Message message, IntPtr handle, uint filterMin, uint filterMax, uint flags);
        [DllImport("user32.dll")]
        static extern IntPtr DispatchMessage([In] ref Message lpmsg);
        [DllImport("user32.dll")]
        static extern bool TranslateMessage([In] ref Message lpMsg);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        internal static void HandleMessage(bool blocking)
        {
            Message msg;

            if (!blocking && !PeekMessage(out msg, IntPtr.Zero, 0, 0, 1)) //1=PM_NOREMOVE, do not remove message
                return; //not blocking and no message available

            if (GetMessage(out msg, IntPtr.Zero, 0, 0))
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        }

    }
}
