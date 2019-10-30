
using System;
using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{
    public class Tabs:CompositeWidget
    {
        bool eventblock = false;

        public Tabs(CompositeWidget parent)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateTabs(this));
        }


        private DriverTabs DriverTabs { get { return DriverObject as DriverTabs; } }


        public override void CalcLayoutInfoRecursive(ref bool anyexpandx, ref bool anyexpandy)
        {
            int maxw=0, maxh=0;
            foreach (TabPage w in Children)
            {
                

                w.CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
                maxw = Math.Max(maxw, w.LayoutInfo.Size.Width);
                maxh = Math.Max(maxh, w.LayoutInfo.Size.Height);
            }

            base.CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);

            //add decor size to tabs, dont care for margin since it makes no sense in
            //a tab control anyway (use the tabpages to apply margin)
            Margin decor = DriverTabs.GetDecorationSize();
            LayoutInfo.Size = new Size2i(maxw+decor.Horizontal, maxh+decor.Vertical);


           
        }

        public override void CalcPositionsRecursive(int left, int top, int width, int height)
        {
            //base.CalcPositionsRecursive(left, top, width, height);

            DriverTabs.Place(left, top, width, height);

            foreach (Widget w in Children)
            {
                TabPage tp = w as TabPage;
                DriverTabPage dtp = w.DriverObject as DriverTabPage;
                if (dtp != null)
                {
                    var siz = dtp.Size;
                    w.CalcPositionsRecursive(0, 0, siz.Width, siz.Height);
                }

            }
           
        }

        virtual public bool ShowTabs { set { DriverTabs.ShowTabs = false; } }

        virtual public int SelectedIndex
        {
            get
            {
                return DriverTabs.SelectedIndex;
            }
            set
            {
                try
                {
                    eventblock = true;
                    DriverTabs.SelectedIndex = value;
                }
                finally
                {
                    eventblock = false;
                }
            }
        }

        virtual public int Count
        {
            get {return DriverTabs.Count;}
        }

        virtual public int RemoveIndex(int index)
        {
            return DriverTabs.RemoveIndex(index);
        }

        virtual public TabPage SelectedPage
        {
            get
            {
                if (SelectedIndex < 0) return null;
                return Children[SelectedIndex] as TabPage;
            }
            set
            {
                int idx = Children.IndexOf(value);
                if (idx < 0) return;
                SelectedIndex = idx;

            }
        }

        #region EVENTS
        virtual public event GuppyEventHandler EvChanged;
        virtual internal void OnChanged() { if (!eventblock && EvChanged != null) EvChanged(new GuppyEventArgs(this)); }
        #endregion
    }

    public class TabPage : CompositeWidget
    {
        public TabPage(Tabs parent,string caption)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateTabPage(this, caption));
        }

        internal DriverTabPage DriverPage { get { return DriverObject as DriverTabPage; } }

        virtual public string Caption
        {
            get { return DriverPage.Caption; }
            set { DriverPage.Caption = value; }
        }
    }

    
}
