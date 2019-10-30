using GuppyGUI.AbstractDriver;
using System;

namespace GuppyGUI
{

    public delegate void EventCallback();
    public delegate void ActionCallback();

    public abstract class Widget:IDisposable
    {

        public CompositeWidget Parent;
        internal LayoutInfo LayoutInfo = new LayoutInfo();
        public object Tag;

        public bool? ExpandX;
        public bool? ExpandY;
        public bool? UniformWidth;
        public bool? UniformHeight;
        public LayoutAlign? Align;

        public Size2i Size = Size2i.Natural;

        internal object DriverObject;

        public Widget()
        {

        }


        virtual protected void AttachDriverObject(CompositeWidget parent, DriverWidget w)
        {
            //use this function from inheriting constructor with driver object
            DriverObject = w;
           // this.Parent = parent;
            if(parent!=null)
                parent.Children.Append(this);
        }


        

        virtual public void CalcLayoutInfoRecursive(ref bool anyexpandx, ref bool anyexpandy) //default behaviour, should be overriden by composites
        {

            Size2i MinSize = CalcNaturalSize();
            LayoutInfo.ExpandX = ExpandX ?? (Parent == null ? false : Parent.ChildrenExpandX);
            LayoutInfo.ExpandY = ExpandY ?? (Parent == null ? false : Parent.ChildrenExpandY);
            LayoutInfo.UniformWidth = UniformWidth ?? (Parent == null ? false : Parent.ChildrenUniformWidth);
            LayoutInfo.UniformHeight = UniformHeight ?? (Parent == null ? false : Parent.ChildrenUniformHeight);
            LayoutInfo.Align = Align ?? (Parent == null ? GuppyGUI.LayoutAlign.Left : Parent.ChildrenAlign);

            if (LayoutInfo.ExpandX) anyexpandx = true;
            if (LayoutInfo.ExpandY) anyexpandy = true;

            int liw, lih;
            

            if (Size.Width != Guppy.Natural) liw = Size.Width;
            else liw = MinSize.Width;

            if (Size.Height != Guppy.Natural) lih = Size.Height;
            else lih = MinSize.Height;
            LayoutInfo.Size = new Size2i(liw, lih);

        }

        virtual protected Size2i CalcNaturalSize()
        {
            DriverWidget w = (DriverObject as DriverWidget);
            if (w == null)
                return Size2i.Natural;
            return w.GetNaturalSize();
        }


        virtual public void CalcPositionsRecursive(int left, int top, int width, int height)
        {
            DriverWidget w = DriverObject as DriverWidget;
            if (w != null)
                w.Place(left, top, width, height);
        }

        /// <summary>
        /// Returns the window this widget resides in or null if none.
        /// </summary>
        virtual public Window Window
        {
            get
            {
                Widget cur = this;
                while (cur != null && !(cur is Window))
                    cur = cur.Parent;
                return cur as Window;
            }
        }



        virtual public string Tip
        {
            set
            {
                var d = DriverObject as DriverWidget;
                if (d != null) d.Tip = value;
            }
        }

        virtual public bool Visible
        {
            set
            {
                var d = DriverObject as DriverWidget;
                if (d != null) d.Visible = value;
            }
            get
            {
                var d = DriverObject as DriverWidget;
                if (d != null) return d.Visible;
                return false;
            }
        }

        virtual public bool Enabled
        {
            set
            {
                var d = DriverObject as DriverWidget;
                if (d != null) d.Enabled = value;
            }
            get
            {
                var d = DriverObject as DriverWidget;
                if (d != null) return d.Enabled;
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the height _request_ for this widget. The layout algorith might
        /// choose a diffrent actual height.
        /// </summary>
        virtual public int Height
        {

            get
            {
                return Size.Height;
            }
            set
            {
                Size = new Size2i(Size.Width, value);
            }
        }

        /// <summary>
        /// Gets or sets the width _request_ for this widget. The layout algorith might
        /// choose a diffrent actual width.
        /// </summary>
        virtual public int Width
        {

            get
            {
                return Size.Width;
            }
            set
            {
                Size = new Size2i(value,Size.Height);
            }
        }


        #region EVENTS

        virtual public event GuppyKeyHandler EvKeyDown;
        virtual internal bool OnKeyDown(GuppyKeyArgs e)
        {
            if (EvKeyDown != null) EvKeyDown(e);
            return e.Block;
        }

        virtual public event GuppyKeyHandler EvKeyUp;
        virtual internal bool OnKeyUp(GuppyKeyArgs e)
        {
            if (EvKeyUp != null) EvKeyUp(e);
            return e.Block;
        }

        #endregion

        public virtual void Dispose()
        {
            

            if (DriverObject != null)
            {
                Guppy.Driver.DefaultDispose(DriverObject);
            }
            else
            {
                //Driver object is null, but it might be a composite widget so
                //dispose each child individually
                CompositeWidget cw = this as CompositeWidget;
                if (cw != null)
                {
                    while (cw.Children.Count>0)
                        cw.Children.Detach(cw.Children[cw.Children.Count - 1]);
                }
            }

            if (Parent != null)
                Parent.Children.Detach(this); //detach to avoid refresing problems in layout manager

        }
    }

    /// <summary>
    /// This class contains info. computed in the layout process, ie. no static or pre-set data.
    /// </summary>
    public class LayoutInfo
    {
        public bool ExpandX;
        public bool ExpandY;
        public bool UniformWidth;
        public bool UniformHeight;
        public Size2i Size=Size2i.Natural;   //the actual size of this object (before expand). It can be altered by user size and by UniforWidth/Height flags
        public LayoutAlign Align; //how control is placed in minor direction of parent container
    }




}
