using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{

    public enum PositionMode
    {
        Auto,     //=use center parent first time window is shown, then set to manual
        Manual,   //=use window as is / manual position
        CenterParent, //center the window that is closest parent to this one. If no such window, center on screen
        CenterScreen,  //center on the current screen
        MouseCursor,  //at mouse cursor
    }


    public enum SizeMode
    {
        Auto, //=use natural size first time window is shown, then manual
        Manual //=use widgets Size property, which of width and height can be Natural(-1)
    }

    public enum DecorationFlags
    {
        DialogFrame = 0,
        Resizable = 1,
        MinButton = 2,
        MaxButton = 4,
        TaskBarButton = 8, //TODO: implement in gtk
        Icon = 16,  //TODO: implement in gtk
    }

    public class Window : CompositeWidget
    {

        PositionMode windowposition = PositionMode.Auto;
        SizeMode windowsize = SizeMode.Auto;

        public Window(string caption)
        {
            AttachDriverObject(null, Guppy.Driver.CreateWindow(this, caption));
            EvResized += delegate { UpdateLayout(); };
            
        }


        virtual public string Caption
        {
            get
            {
                return DriverWindow.Caption;
            }
            set
            {
                DriverWindow.Caption = value;
            }
        }


        virtual public void Show()
        {
            InternalShow(false);
        }

        virtual public DecorationFlags DecorationFlags
        {
            get
            {
                return DriverWindow.DecorationFlags;
            }
            set
            {
                DriverWindow.DecorationFlags = value;
            }
        }

        virtual public DriverWindow DriverWindow
        {
            get { return DriverObject as DriverWindow; }
        }

        virtual public object ShowModal()
        {
            return InternalShow(true);
        }

        private object InternalShow(bool modal)
        {
            bool autopos = false;
            object res = "";

            if (windowposition == PositionMode.Auto)
            {
                autopos = true;
                windowposition = PositionMode.CenterParent; //use this time
            }

            bool anyexpandx = false, anyexpandy = false;
            CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
            CalcPositionsRecursive(0, 0, LayoutInfo.Size.Width, LayoutInfo.Size.Height);

            UpdateMinSize();  //figure out how user is allowed to resize this window

            if (windowsize == SizeMode.Auto)
            {
                UpdateSize(); //set physical size
                windowsize = SizeMode.Manual;
            }
            else
            {
                //manual, use window as is
            }

            if (modal)
                res = DriverWindow.ShowModal();
            else
                DriverWindow.Show();

            if (autopos)
                windowposition = PositionMode.Manual; //use manual next time form is shown



            return res;
        }

        private void UpdateSize()
        {
            int he = Size.Height;
            int wi = Size.Width;
            Size2i natural = LayoutInfo.Size;
            if (he == Guppy.Natural) he = natural.Height;
            if (wi == Guppy.Natural) wi = natural.Width;

            DriverWindow.Size = new Size2i(wi, he);
        }

        private void UpdateMinSize()
        {
            if (Shrink)
                DriverWindow.SetMinSize(0, 0);
            else
            {

                DriverWindow.SetMinSize(LayoutInfo.Size.Width, LayoutInfo.Size.Height);
            }
        }

        virtual protected void UpdateLayout()
        {
            Size2i s = DriverWindow.Size;

            bool anyexpandx = false, anyexpandy = false;
            CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
            CalcPositionsRecursive(0, 0, s.Width, s.Height);
        }

        public override void Refresh()
        {
            /*Size2i s = DriverWindow.Size;

            bool anyexpandx = false, anyexpandy = false;
            CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
            CalcPositionsRecursive(0, 0, s.Width, s.Height);*/

          

            UpdateLayout();
            UpdateSize();

            //DriverWindow.Size = new Size2i(LayoutInfo.Size.Width, LayoutInfo.Size.Height);
        }



        virtual public Size2i Bounds
        {
            get { return DriverWindow.Size; }
            set { DriverWindow.Size = value; }
        }

        virtual public PositionMode PositionMode
        {
            get
            {
                return windowposition;
            }
            set
            {
                windowposition = value;
            }
        }

        virtual public SizeMode SizeMode
        {
            get
            {
                return windowsize;
            }
            set
            {
                windowsize = value;
            }
        }


        virtual public void Close(object response)
        {
            DriverWindow.Close(response);
        }



        virtual public bool AutoDispose
        {
            get { return DriverWindow.AutoDispose; }
            set { DriverWindow.AutoDispose = value; }
        }

        protected bool shrink = false;
        virtual public bool Shrink //TODO: test if this really works anymore
        {
            get { return shrink; }
            set { if (value != shrink) { shrink = value; UpdateMinSize(); } }
        }

        virtual public void Maximize()
        {
            DriverWindow.Maximize();
        }

        virtual public void Minimize()
        {
            DriverWindow.Minimize();
        }

        virtual public Point2i Position
        {
            get
            {
                return DriverWindow.Position;
            }
            set
            {
                DriverWindow.Position = value;
                PositionMode = PositionMode.Manual;
            }
        }


        #region EVENTS

        virtual public event EventHandler EvResized;
        Size2i oldsize = Size2i.Empty;
        virtual internal void OnResized()
        {
            if (EvResized != null)
            {
                Size2i cursize = DriverWindow.ClientSize;
                if (cursize != oldsize)
                { //protect from multiple resize events from some toolkits (ie. gtk)
                    EvResized(this, new EventArgs());
                    oldsize = cursize;
                }
            }
        }

        virtual public event GuppyEventHandler EvClosed;
        virtual internal bool OnClosed()
        {
            GuppyEventArgs e = new GuppyEventArgs(this);
            if (EvClosed != null)
                EvClosed(e);
            return e.Block;
        }


        virtual public event GuppyEventHandler EvClosing;
        virtual internal bool OnClosing()
        {
            GuppyEventArgs e = new GuppyEventArgs(this);
            if (EvClosing != null) EvClosing(e);
            return e.Block;
        }

        virtual public event GuppyKeyHandler EvRawKeyDown;
        virtual internal bool OnRawKeyDown(GuppyKeyArgs e)
        {
            if (EvRawKeyDown != null) EvRawKeyDown(e);

            if (e.Block)
                return true; //goodbye event

            Widget focused = Guppy.Focus;
            GuppyKeyArgs focusedargs = new GuppyKeyArgs(focused, e.Key);
            if(focused!=null) 
                focused.OnKeyDown(focusedargs);

            return focusedargs.Block;
        }

        virtual public event GuppyKeyHandler EvRawKeyUp;
        virtual internal bool OnRawKeyUp(GuppyKeyArgs e)
        {
           /* if (EvRawKeyUp != null) EvRawKeyUp(e);
            return e.Block;*/

            if (EvRawKeyUp != null) EvRawKeyUp(e);

            if (e.Block)
                return true; //goodbye event

            Widget focused = Guppy.Focus;
            if (focused != null)
                focused.OnKeyUp(e);

            return e.Block;
        }
        
        virtual public event GuppyEventHandler EvShowing;
        virtual internal bool OnShowing()
        {
            GuppyEventArgs e = new GuppyEventArgs(this);
            if (EvShowing != null) EvShowing(e);
            return e.Block;
        }

        virtual public event GuppyEventHandler EvShowed;
        virtual internal bool OnShowed()
        {
            GuppyEventArgs e = new GuppyEventArgs(this);
            if (EvShowed != null) EvShowed(e);
            return e.Block;
        }

        

        #endregion
    }
}
