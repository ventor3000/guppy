
using GuppyGUI.AbstractDriver;
using System;
namespace GuppyGUI
{
    public class Splitter : CompositeWidget
    {
        public Splitter(CompositeWidget parent, bool vertical)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateSplitter(this, vertical));
            this.Vertical = vertical;

            EvChanged += new GuppyEventHandler(Splitter_EvChanged);
        }

        void Splitter_EvChanged(GuppyEventArgs e)
        {
            Child1.Update();
            Child2.Update();
        }

        DriverSplitter DriverSplitter { get { return DriverObject as DriverSplitter; } }

        virtual public SplitterPanel Child1 { get { return DriverSplitter.Child1; } }
        virtual public SplitterPanel Child2 { get { return DriverSplitter.Child2; } }

        public override void CalcLayoutInfoRecursive(ref bool anyexpandx, ref bool anyexpandy)
        {

            Child1.CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
            Child2.CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
            base.CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);

            if (!Vertical)
            {
                LayoutInfo.Size = new Size2i(
                  Child1.LayoutInfo.Size.Width + Child2.LayoutInfo.Size.Width + DriverSplitter.SplitterWidth,
                  Math.Max(Child1.LayoutInfo.Size.Height, Child2.LayoutInfo.Size.Height)
                );
            }
            else
            {
                LayoutInfo.Size = new Size2i(
                  Math.Max(Child1.LayoutInfo.Size.Width, Child2.LayoutInfo.Size.Width),
                  Child1.LayoutInfo.Size.Height + Child2.LayoutInfo.Size.Height + DriverSplitter.SplitterWidth
                );
            }
        }

        public override void CalcPositionsRecursive(int left, int top, int width, int height)
        {
            //base.CalcPositionsRecursive(left, top, width, height);

            DriverSplitter.Place(left, top, width, height);
            Size2i siz1 = /*Child1.LayoutInfo.Size;*/ (Child1.DriverObject as DriverSplitterPanel).Size;
            Size2i siz2 = /*Child2.LayoutInfo.Size;*/  (Child2.DriverObject as DriverSplitterPanel).Size;
            Child1.CalcPositionsRecursive(0, 0, siz1.Width, siz1.Height);
            Child2.CalcPositionsRecursive(0, 0, siz2.Width, siz2.Height);
        }


        #region EVENTS
        virtual public event GuppyEventHandler EvChanged;
        virtual internal bool OnChanged()
        {
            var e = new GuppyEventArgs(this);
            if (EvChanged != null) EvChanged(e);
            return e.Block;
        }
        #endregion

    }
}
