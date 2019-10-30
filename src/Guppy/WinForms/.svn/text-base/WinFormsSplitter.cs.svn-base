using GuppyGUI.AbstractDriver;
using System;


namespace GuppyGUI.WinForms
{
    public class WinFormsSplitter : DriverSplitter
    {
        System.Windows.Forms.SplitContainer split;

        bool block_changed_events=false;
        bool vertical;

        public WinFormsSplitter(Widget shellobject, bool vertical)
            : base(shellobject)
        {
            split = new System.Windows.Forms.SplitContainer();
            split.Tag = shellobject; //map-back from native control to guppy object

            //vertical in windows forms means the opposite of in gtk. We use vertical/horizontal for layout direction
            //of the children, and not the splitterbar direction (as in gtk)
            split.Orientation = vertical ? System.Windows.Forms.Orientation.Horizontal : System.Windows.Forms.Orientation.Vertical;
            split.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            child1 = new SplitterPanel(shellobject as CompositeWidget, split.Panel1);
            child2 = new SplitterPanel(shellobject as CompositeWidget, split.Panel2);

            split.SplitterMoved += delegate { if(!block_changed_events) ((Splitter)ShellObject).OnChanged(); };

            this.vertical = vertical;
        }

       


        public override object NativeObject
        {
            get { return split; }
        }

        bool firstlayout = true;

        public override void Place(int x, int y, int w, int h)
        {
            try
            {
                block_changed_events = true;

                split.SetBounds(x, y, w, h);

                // Margin decor1 = (Child1.DriverObject as WinFormsSplitterPanel).GetDecorationSize();
                // Margin decor2 = (Child2.DriverObject as WinFormsSplitterPanel).GetDecorationSize();

                if (firstlayout)
                {
                    UpdateConstraints();
                    firstlayout = false;
                }

            }
            finally
            {
                block_changed_events = false;
            }
        }

        private void UpdateConstraints()
        {
            if (vertical)
            {
                split.SplitterDistance = Child1.LayoutInfo.Size.Height; // -decor1.Horizontal;
                split.Panel1MinSize = Child1.LayoutInfo.Size.Height;
                split.Panel2MinSize = Child2.LayoutInfo.Size.Height;
            }
            else
            {
                split.SplitterDistance = Child1.LayoutInfo.Size.Width; // -decor1.Horizontal;
                split.Panel1MinSize = Child1.LayoutInfo.Size.Width;
                split.Panel2MinSize = Child2.LayoutInfo.Size.Width;
            }

            //resolve which panels should be fixed or not,
            //if both children expands, no child panel is fixed
            //if none expands (which is an error) child1 is fixed
            int exp = vertical ?
                ((Child1.LayoutInfo.ExpandY ? 1 : 0) | (Child2.LayoutInfo.ExpandY ? 2 : 0)) :
                ((Child1.LayoutInfo.ExpandX ? 1 : 0) | (Child2.LayoutInfo.ExpandX ? 2 : 0));
            System.Windows.Forms.FixedPanel[] pans = { 
                        System.Windows.Forms.FixedPanel.Panel1, 
                        System.Windows.Forms.FixedPanel.Panel2, 
                        System.Windows.Forms.FixedPanel.Panel1, 
                        System.Windows.Forms.FixedPanel.None 
                    };
            split.FixedPanel = pans[exp];


        }

        public override void Append(DriverWidget dw)
        {
            //append for splitter does nothing, we have to use one of its children
        }

        public override Margin GetDecorationSize()
        {
            int mw = split.Size.Width - split.ClientSize.Width;
            int mh = split.Size.Height - split.ClientSize.Height;

            return new Margin(mw / 2 , mh / 2, mw  / 2, mh / 2);
        }

        public override int SplitterWidth
        {
            get
            {
                return split.SplitterWidth;
            }
            set
            {
                split.SplitterWidth = value;
            }
        }

    }

    public class WinFormsSplitterPanel : DriverSplitterPanel
    {
        System.Windows.Forms.Panel panel;

        public WinFormsSplitterPanel(Widget shellobject, object panelobject)
            : base(shellobject)
        {
            this.panel = panelobject as System.Windows.Forms.Panel;
            this.panel.Tag = shellobject; //map-back from native control to guppy object

            if (this.panel == null)
                throw new Exception("Error creating panel for splitter");
        }

        public override void Append(DriverWidget dw)
        {
            System.Windows.Forms.Control ctrl = dw.NativeObject as System.Windows.Forms.Control;
            if (ctrl != null)
                panel.Controls.Add(ctrl);
        }


        public override object NativeObject
        {
            get { return panel; }
        }


        /*
        public override Size2i ClientSize
        {
            get { return WinFormsDriver.ConvertSize(panel.ClientSize); }
        }*/

        public override Size2i Size
        {
            get { return WinFormsDriver.ConvertSize(panel.Size); }
        }

        public override Margin GetDecorationSize()
        {
            System.Drawing.Size siz = System.Drawing.Size.Empty;
            if (panel.BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
                siz = System.Windows.Forms.SystemInformation.BorderSize;
            else if (panel.BorderStyle == System.Windows.Forms.BorderStyle.Fixed3D)
                siz = System.Windows.Forms.SystemInformation.Border3DSize;
            return new Margin(siz.Width, siz.Height, siz.Width, siz.Height);
        }

    }
}
