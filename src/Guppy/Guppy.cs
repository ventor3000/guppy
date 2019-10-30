using GuppyGUI.AbstractDriver;
using System;


namespace GuppyGUI
{



    public enum DriverMode
    {
        Auto = 0,
        WinForms = 1,
        GtkSharp = 2
    }

    public static class Guppy
    {
        private static Driver drv;
        public static Driver Driver { get { return drv; } }
        private static bool isopen = false; //to protect from multiple open:s

        public static int DefaultGap = 4;
        public static int DefaultMargin = 8;
        public static int DefaultEditWidth = 75;
        public const int Natural = -1; //used for Size2i to signal 'use natural size' for width and/or height

        private static Window mainwindow;

        public static void Open()
        {
            Open(DriverMode.Auto);
        }

        public static void Open(DriverMode driver)
        {

            bool _64bit = IntPtr.Size == 8;

            if (isopen)
                return;

            PlatformID platid = System.Environment.OSVersion.Platform;

            if (platid.ToString().StartsWith("Win"))
            {
                //in windows gtk or winforms are both valid
                if (driver == DriverMode.WinForms || driver == DriverMode.Auto)
                    drv = new GuppyGUI.WinForms.WinFormsDriver();
                else if (driver == DriverMode.GtkSharp)
                {
                    if (_64bit)
                        throw new Exception("You are trying to initalize Guppy with the gtk sharp driver in 64 bit mode. This is not possible, please change driver or change compilation target to x86");

                    drv = new GuppyGUI.GtkSharp.GtkSharpDriver();
                }
            }
            else
            { //for now: run GtkSharp driver on all other platforms
                drv = new GuppyGUI.GtkSharp.GtkSharpDriver();
            }

            drv.Open();
            isopen = true;
        }


        public static void Run(Window mainwindow)
        {
            

            Guppy.mainwindow = mainwindow;
            mainwindow.Show();
            mainwindow.EvClosed += delegate { drv.Quit(); };
            drv.Run();
        }


        public static void Message(string title, string msg)
        {
            Guppy.Driver.Message(title, msg);
        }

        public static Widget Focus
        {
            get
            {
                return Guppy.Driver.Focus;
            }
            set
            {
                Guppy.Driver.Focus = value;
            }
        }

        public static void Wait(bool block)
        {
            Driver.Wait(block);
        }

        public static Window MainWindow
        {
            get { return mainwindow; }
        }

        public static string ShortcutName(KeyData kdata)
        {
            return KeyData.ShortcutName(kdata);
        }

        

    }
}
