using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.GtkSharp
{
    public class GtkSharpWindow : DriverWindow
    {
        Gtk.Window window;
        Gtk.Fixed fixchild;

        public GtkSharpWindow(Widget shellobject, string caption)
            : base(shellobject)
        {
            window = new Gtk.Window(caption);
            GtkSharpDriver.InitWidget(window, shellobject);
            window.ResizeChecked += new EventHandler(EventResizeChecked);
            window.Hidden += new EventHandler(EventClosed);

            //window.WindowPosition = Gtk.WindowPosition.Mouse;

            fixchild = new Gtk.Fixed();
            fixchild.Show();
            window.Add(fixchild);

            window.DeleteEvent += new Gtk.DeleteEventHandler(window_DeleteEvent);
            window.KeyPressEvent += new Gtk.KeyPressEventHandler(window_KeyPressEvent);
        }

        [GLib.ConnectBefore]
        void window_KeyPressEvent(object o, Gtk.KeyPressEventArgs args)
        {
            KeyData k = GtkSharpKeys.DecodeKey(args.Event);
            if (k.KeyCode == KeyCode.Unknown)
                return; //dont send event of unknown key

            bool block = ((Window)ShellObject).OnRawKeyDown(new GuppyKeyArgs(ShellObject, k));
            if (block)
                args.RetVal = true;

            /*var ed=new EventData(ShellObject, EventID.KeyDown, false) { i1 = k };
            ed.Send();*/
        }

        void EventResizeChecked(object sender, EventArgs e)
        {
            fixchild.SetSizeRequest(1, 1);
            ((Window)ShellObject).OnResized();

            
        }




        void window_DeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            if (AutoDispose)
            {
                window.Hide();
                if (window.Modal)
                {
                    /*never destroy modal windows automagically*/
                }
                else
                {
                    window.Destroy();
                }
                args.RetVal = true; //we handled this event ourselves
            }
            else //dont autodispose
            {
                window.Hide();  //just hide the window
                args.RetVal = true; //we handled this event
            }
        }

        Gdk.Window GdkWindow
        {
            get
            {
                if (window.GdkWindow == null)
                    window.Realize();
                if (window.GdkWindow == null)
                    throw new Exception("Failed to get gdk window");
                return window.GdkWindow;
            }
        }



        void EventClosed(object sender, EventArgs e)
        {

            ((Window)ShellObject).OnClosed();

        }

        public override Size2i ClientSize
        {
            get
            {
                int w, h;
                window.GetSize(out w, out h);
                return new Size2i(w, h);
            }
            set
            {
                window.Resize(value.Width, value.Height);
            }
        }

        public override void Show()
        {

            Response = null;
            Gtk.Window parent = GtkSharpDriver.GetForegroundWindow();

            //Size = ShellObject.LayoutInfo.Size;

            window.TransientFor = null;
            window.Modal = false;

            SolveWindowPosition(parent);

            window.Show();
        }

        public override string Caption
        {
            get
            {
                return window.Title;
            }
            set
            {
                window.Title = value;
            }
        }

        public override object NativeObject
        {
            get { return window; }
        }

        public override void Detach(DriverWidget chld)
        {
            //TODO: this does not work, shy??
            Gtk.Widget child = chld.NativeObject as Gtk.Widget;
            if (fixchild != null && child != null)
            {
                fixchild.Remove(child);
            }
        }

        
        public override void Append(DriverWidget dw)
        {
            Gtk.Widget w = dw.NativeObject as Gtk.Widget;
            if (w != null)
                fixchild.Put(w, 0, 0);

        }

        public override object ShowModal()
        {

            Response = null;
            Gtk.Window parent = GtkSharpDriver.GetForegroundWindow();

            window.TransientFor = parent;
            window.Modal = true;

            SolveWindowPosition(parent);

            window.Show();

            while (window.Visible)
            {
                Gtk.Application.RunIteration(true);
            }

            return Response;
        }

        private void SolveWindowPosition(Gtk.Window parent)
        {
            int x, y, cw, ch, pw, ph, px, py;
            PositionMode wp = (ShellObject as Window).PositionMode;
            //if center parent and parent is null, center screen instead
            if (parent == null && wp == PositionMode.CenterParent)
                wp = PositionMode.CenterScreen;


            switch (wp)
            {
                case PositionMode.Manual:
                    //HACK: we have to move the window to the position it already is for some stupid gtk-reason
                    //an GetPosition doesnt work (in windows at least)
                    int wx, wy, w, h, d;
                    window.GdkWindow.GetGeometry(out wx, out wy, out w, out h, out d);
                    Margin m = GetDecorationSize();
                    window.Move(wx - m.Left, wy - m.Top);
                    return; //done already
                case PositionMode.CenterParent:
                    parent.GetSize(out pw, out ph);
                    parent.GetPosition(out px, out py);
                    window.GetSize(out cw, out ch);
                    var c = window.WindowPosition;
                    x = px + pw / 2 - cw / 2;
                    y = py + ph / 2 - ch / 2;
                    window.Move(x, y);
                    break;
                case PositionMode.CenterScreen:
                    Gdk.Screen scr = window.Screen; // ?? Gdk.Screen.Default
                    window.GetSize(out cw, out ch);
                    x = scr.Width / 2 - cw / 2;
                    y = scr.Height / 2 - ch / 2;
                    window.Move(x, y);
                    break;
                case PositionMode.MouseCursor:
                    Gdk.Screen scr2 = window.Screen;
                    scr2.Display.GetPointer(out x, out y);
                    window.Move(x, y);
                    break;
                default:
                    throw new Exception("GtkSharp driver does not know how to position screen at " + wp.ToString());
            }
        }

        public override Size2i Size
        {
            get
            {
                int w, h;
                window.GetSize(out w, out h); //returns the size of the client in stupid gtk
                Margin m = GetDecorationSize(); //so append windows decoration sizes
                return new Size2i(w + m.Horizontal, h + m.Vertical);
            }
            set
            {
                Margin m = GetDecorationSize();
                window.Resize(value.Width - m.Horizontal, value.Height - m.Vertical);
            }
        }

        public override void Close(object response)
        {
            this.Response = response;

            if (window.Modal)
            {
                //dont destroy modal dialogs automatically
                window.Hide();
            }
            else
            {
                if (AutoDispose)
                {
                    window.Destroy();
                }
                else
                    window.Hide();
            }
        }

        public override void SetMinSize(int width, int height)
        {
            Gdk.Geometry g = new Gdk.Geometry();
            var dec = GetDecorationSize();
            g.MinWidth = width - dec.Horizontal; //note: gtks size functions are in client area
            g.MinHeight = height - dec.Vertical;
            window.SetGeometryHints(window, g, Gdk.WindowHints.MinSize);
        }

        public override Margin GetDecorationSize()
        {

            var gdkw = GdkWindow;

            int x, y, fx, fy, dx, dy;
            gdkw.GetOrigin(out x, out y);
            gdkw.GetRootOrigin(out fx, out fy);
            dx = x - fx;
            dy = y - fy;
            Margin res = new Margin(dx, dy, dx, dx);
            return res;
        }

        public override DecorationFlags DecorationFlags
        {
            get
            {
                DecorationFlags res = 0;
                if (window.Resizable)
                    res |= DecorationFlags.Resizable;

                return res;
            }
            set
            {
                // window.Resizable = (value & DecorationFlags.Resizable) != 0;
                Gdk.WMFunction func = Gdk.WMFunction.Move | Gdk.WMFunction.Close;
                Gdk.WMDecoration decor = Gdk.WMDecoration.Border | Gdk.WMDecoration.Title | Gdk.WMDecoration.Menu; //have to have menu for close button to work/be visible

                if ((value & DecorationFlags.MaxButton) != 0)
                {
                    decor |= Gdk.WMDecoration.Maximize;
                    func |= Gdk.WMFunction.Maximize;
                }

                if ((value & DecorationFlags.MinButton) != 0)
                {
                    decor |= Gdk.WMDecoration.Minimize;
                    func |= Gdk.WMFunction.Minimize;
                }

                if ((value & DecorationFlags.Resizable) != 0)
                {
                    func |= Gdk.WMFunction.Resize;
                    decor |= Gdk.WMDecoration.Resizeh;
                }


                var gdkw = GdkWindow;
                gdkw.Functions = func;
                gdkw.SetDecorations(decor);

            }
        }

        public override void Maximize()
        {
            window.Maximize();
        }

        public override void Minimize()
        {
            window.Iconify();
        }

        public override Point2i Position
        {
            get
            {
                int x, y, w, h, d;
                window.GdkWindow.GetGeometry(out x, out y, out w, out h, out d);
                Margin m = GetDecorationSize();
                return new Point2i(x - m.Left, y - m.Top);
            }
            set
            {
                //Margin m = GetDecorationSize();
                window.Move(value.X, value.Y);
            }
        }


    }
}
