using GuppyGUI.AbstractDriver;
using System;

namespace GuppyGUI.GtkSharp
{
    public class GtkSharpFrame : DriverFrame
    {
        Gtk.Frame frame;
        Gtk.Fixed fixchild;

        public GtkSharpFrame(Widget shellobject, string caption)
            : base(shellobject)
        {
            frame = new Gtk.Frame(caption);
            GtkSharpDriver.InitWidget(frame, shellobject);
            frame.Show();

            fixchild = new Gtk.Fixed();
            frame.Add(fixchild);
            fixchild.Show();
        }


        public override string Caption
        {
            get
            {
                return frame.Label;
            }
            set
            {
                frame.Label = value;
            }
        }

        public override object NativeObject
        {
            get { return frame; }
        }




        Gdk.Window GdkWindow
        {
            get
            {
                if (frame.GdkWindow == null)
                    frame.Realize();
                if (frame.GdkWindow == null)
                    throw new Exception("Failed to get gdk window");
                return frame.GdkWindow;
            }
        }


        int StringHeight(Gtk.Widget w, string text)
        {
            int wi, he;
            Pango.Layout lo = w.CreatePangoLayout(text);
            lo.GetPixelSize(out wi, out he);
            lo.Dispose();
            return he;
        }


        public override Margin GetDecorationSize()
        {

            int txth = StringHeight(frame, Caption + "W");
            int xx = frame.Style.Xthickness;
            int yy = frame.Style.Ythickness;

            return new Margin(xx, txth, xx, yy);
        }

        public override void Append(DriverWidget dw)
        {

            Gtk.Widget w = dw.NativeObject as Gtk.Widget;
            if (w != null)
                fixchild.Put(w, 0, 0);
        }

    }
}

