using System;
using GuppyGUI.AbstractDriver;
using Gtk;
using System.Collections.Generic;



namespace GuppyGUI.GtkSharp
{

    public class GtkSharpDriver : Driver
    {
        public override void Open()
        {

            Gtk.Application.Init();
            GLib.ExceptionManager.UnhandledException += new GLib.UnhandledExceptionHandler(ExceptionManager_UnhandledException);

        }

        void ExceptionManager_UnhandledException(GLib.UnhandledExceptionArgs args)
        {
            string msg = "Unhandled unknown exception";
            Exception exc = args.ExceptionObject as Exception;
            if (exc != null)
            {
                msg = "Outer exception:\n" + exc.Message + "\n";
                if (exc.InnerException != null)
                {
                    msg += "Inner exception:\n" + exc.InnerException.Message + "\n";
                }
            }
            Guppy.Message("UNHANDLED EXCEPTION", "An unhandled fatal exception occured in the program:\n" + msg);
            args.ExitApplication = false;
        }

        public override void Run()
        {
            Gtk.Application.Run();
        }

        public override void Quit()
        {
            Gtk.Application.Quit();

        }

        public override void Message(string title, string msg)
        {
            Gtk.MessageDialog md = new Gtk.MessageDialog(
                GetForegroundWindow(),
                Gtk.DialogFlags.Modal,
                Gtk.MessageType.Other,
                Gtk.ButtonsType.Ok,
                msg
            );

            md.Title = title;
            md.Run();
            md.Destroy();
        }

        internal static Gtk.Window GetForegroundWindow()
        {
            foreach (Gtk.Window w in Gtk.Window.ListToplevels())
                if (w.HasToplevelFocus)
                    return w;
            return null;
        }


        public override DriverToggle CreateToggle(Widget shellobject, string caption, bool isbutton)
        {
            return new GtkSharpToggle(shellobject, caption, isbutton);
        }


        internal static Size2i DefaultGetNaturalSize(Gtk.Widget wi)
        {

            wi.SetSizeRequest(-1, -1);  //=unset, request natural size
            var req = wi.SizeRequest();
            return new Size2i(req.Width, req.Height);
        }


        public override DriverChoice CreateChoice(Widget shellobject, params object[] entries)
        {
            return new GtkSharpChoice(shellobject, entries);
        }

        public override DriverSeparator CreateSeparator(Widget shellobject, bool vertical)
        {
            return new GtkSharpSeparator(shellobject, vertical);
        }

        public override DriverWindow CreateWindow(Widget shellobject, string caption)
        {
            return new GtkSharpWindow(shellobject, caption);
        }

        public override DriverButton CreateButton(Widget shellobject, string caption)
        {
            return new GtkSharpButton(shellobject, caption);
        }

        public override DriverLabel CreateLabel(Widget shellobject, string caption)
        {
            return new GtkSharpLabel(shellobject, caption);
        }

        public override DriverEdit CreateEdit(Widget shellobject)
        {
            return new GtkSharpEdit(shellobject);
        }

        public override DriverFrame CreateFrame(Widget shellobject, string caption,bool border) //TODO: handle border flag
        {
            return new GtkSharpFrame(shellobject, caption);
        }

        public override DriverImage CreateImage(System.IO.Stream src)
        {
            return new GtkSharpImage(src);
        }

        internal static Gtk.Image ImageToGtkImage(Image img)
        {
            if (img == null) return null;
            GtkSharpImage gi = img.DriverObject as GtkSharpImage;
            if (gi != null)
            {
                Gtk.Image res = gi.CreateGtkImage();
                if (res != null)
                    return res;
            }

            return null;
        }

        internal static Gdk.Pixbuf ImageToGtkPixbuf(Image img)
        {
            if (img == null) return null;
            GtkSharpImage gi = img.DriverObject as GtkSharpImage;
            if (gi != null)
                return gi.pixbuf;
            return null;
        }

        public override DriverImageLabel CreateSlide(Widget shellobject, Image image)
        {
            return new GtkSharpImageLabel(shellobject, image);
        }

        public override DriverListBox CreateListBox(Widget shellobject, params object[] items)
        {
            throw new NotImplementedException("Listbox not implemented in GTK sharp driver");
        }

        public override DriverMemo CreateMemo(Widget shellobject)
        {
            return new GtkSharpMemo(shellobject);
        }

        public override DriverSplitter CreateSplitter(Widget shellobject, bool vertical)
        {
            throw new NotImplementedException("Splitter not implemented in GTK sharp driver");
        }


        public override DriverSplitterPanel CreateSplitterPanel(Widget shellobject, object driverobject)
        {
            throw new NotImplementedException("SplitterPanel not implemented in GTK sharp driver");
        }

        public override DriverRadioButton CreateRadioButton(Widget shellobject, string caption)
        {
            throw new NotImplementedException("Radio button not implemented in GTK sharp driver");
        }

        public override DriverProgressBar CreateProgressBar(Widget shellobject)
        {
            throw new NotImplementedException("Progress bar not implemented in GTK sharp driver");
        }

        public override DriverValuator CreateValuator(Widget shellobject, bool vertical)
        {
            throw new NotImplementedException("Valuator not implemented in GTK sharp driver");
        }

        public override DriverTabs CreateTabs(Widget shellobject)
        {
            throw new NotImplementedException("tabs not implemented in gtk driver");
        }

        public override DriverTabPage CreateTabPage(Widget shellobject, string caption)
        {
            throw new NotImplementedException("tabpage not implemented in gtk driver");
        }

        public override Widget Focus
        {
            get
            {
                Gtk.Window gw=GetForegroundWindow();
                if (gw == null)
                    return null;
                var gwi = gw.Focus;
                if (gwi == null)
                    return null;

                if (gwi.Data.ContainsKey("GUPPYSHELL"))
                {
                    Widget guppyw = gwi.Data["GUPPYSHELL"] as GuppyGUI.Widget;
                    return guppyw;
                }

                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        internal static void InitWidget(Gtk.Widget wi,GuppyGUI.Widget shellobj)
        {
            if (wi != null)
            {
                wi.Data["GUPPYSHELL"] = shellobj;
            }
        }

        public override DriverPopupMenu CreatePopupMenu(Widget shellobject)
        {
            throw new NotImplementedException("popup menu not implemented in gtk driver");
        }

        public override DriverMenuItem CreateMenuItem(Widget shellobject, string Caption, Image image, MenuFlags flags)
        {
            throw new NotImplementedException("menuitem not implemented in gtk driver");
        }


        public override DriverChoiceEdit CreateChoiceEdit(Widget shellobject, params object[] items)
        {
            throw new NotImplementedException("choiceedit not implemented in gtk driver");
        }

        public override void Wait(bool block)
        {
            throw new NotImplementedException();
        }



       

        #region DEFAULT_BEHAVIOUR

        public override void DefaultSetTooltip(object target, string tiptext)
        {
            Gtk.Widget gw = target as Gtk.Widget;
            if (gw != null)
            {
                gw.TooltipText = tiptext;
            }
        }

        public override bool DefaultGetVisible(object target)
        {
            Gtk.Widget gw = target as Gtk.Widget;
            if (gw != null)
            {
                return gw.Visible;
            }
            throw new Exception("DefaultGetVisible in GTK driver do not support the sent object");
        }

        public override void DefaultSetVisible(object target, bool isvisible)
        {
            Gtk.Widget gw = target as Gtk.Widget;
            if (gw != null)
            {
                gw.Visible = isvisible;
                return;
            }
            throw new Exception("DefaultGetVisible in GTK driver do not support the sent object");
        }

        public override bool DefaultGetEnabled(object target)
        {
            throw new NotImplementedException();

        }

        public override void DefaultSetEnabled(object target, bool isvisible)
        {
            throw new NotImplementedException();

        }

        public override void DefaultPlace(object target, int x, int y, int w, int h)
        {

            Gtk.Widget wi = target as Gtk.Widget;
            if (wi == null)
                return;

            Gtk.Fixed fix = wi.Parent as Gtk.Fixed;
            if (fix != null)
            {
                fix.Move(wi, x, y);
                wi.SetSizeRequest(w, h);
            }
        }

        public override void DefaultDetach(object target, object chld)
        {
            Gtk.Container parent = target as Gtk.Container;
            Gtk.Widget child = chld as Gtk.Widget;
            if (parent != null && child != null)
            {
                parent.Remove(child);
            }
        }

        public override void DefaultDispose(object target)
        {
            /*IDisposable disp = target as IDisposable;
            if (disp != null)
                disp.Dispose();*/

            DriverWidget dw = target as DriverWidget;
            if (dw != null)
            {

                Gtk.Widget w = dw.NativeObject as Gtk.Widget;
                if (w != null)
                {
                    w.Destroy();
                    w.Dispose();
                }
            }
            else
            {
                IDisposable di = target as IDisposable;
                if (di != null)
                    di.Dispose();
            }
        }

        #endregion

    }
}



