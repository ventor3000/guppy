using GuppyGUI.AbstractDriver;
using System;

namespace GuppyGUI.WinForms
{
    public class WinFormsTabs:DriverTabs
    {
        System.Windows.Forms.TabControl tabctrl;
        static System.Drawing.Size orgitemsize;    //remember original setting of item size here if we switch back and forth form ShowTabs

        public WinFormsTabs(Widget shellobject):base(shellobject)
        {
            tabctrl = new System.Windows.Forms.TabControl();
            tabctrl.Tag = shellobject; //map-back from native control to guppy object

            if(orgitemsize==null)
                orgitemsize=tabctrl.ItemSize;
            tabctrl.ShowToolTips = true; //allow for tab tooltips

            tabctrl.Selected+=delegate { ((Tabs)ShellObject).OnChanged(); };
            
            
        }

        public override void Append(DriverWidget dw)
        {
            DriverTabPage page = dw as DriverTabPage;
            if (page==null)
                throw new Exception("Only tabpages can have tab control as parent");
            WinFormsTabPage wpage = page as WinFormsTabPage;

            tabctrl.TabPages.Add(dw.NativeObject as System.Windows.Forms.TabPage);
        }


        public override object NativeObject
        {
            get { return tabctrl; }
        }


        public override Margin GetDecorationSize()
        {
            var dr=tabctrl.DisplayRectangle;
            var siz=tabctrl.Size;
            return new Margin(dr.Left, dr.Top, siz.Width - dr.Right, siz.Height - dr.Bottom);
        }

        public override bool ShowTabs
        {
            set {
                if (value)
                {
                    tabctrl.ItemSize = orgitemsize;
                    tabctrl.Appearance = System.Windows.Forms.TabAppearance.Normal;
                    tabctrl.SizeMode = System.Windows.Forms.TabSizeMode.Normal;
                }
                else
                {
                    tabctrl.Appearance=System.Windows.Forms.TabAppearance.Buttons;
                    tabctrl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
                    tabctrl.ItemSize = new System.Drawing.Size(0, 1);
                }
            }
        }

        public override int Count
        {
            get { return tabctrl.TabPages.Count; }
        }

        public override int SelectedIndex
        {
            get
            {
                return tabctrl.SelectedIndex;
            }
            set
            {
                tabctrl.SelectedIndex = value;
            }
        }

        public override int RemoveIndex(int index)
        {
            int cnt = Count;

            if (index < 0 || index >= cnt)
                return -1;
            tabctrl.TabPages.RemoveAt(index);
            if (index == cnt - 1)
                return cnt - 2;
            return index;
        }
    }

    public class WinFormsTabPage : DriverTabPage
    {
        System.Windows.Forms.TabPage tabpage;

        public WinFormsTabPage(Widget shellobject, string caption)
            : base(shellobject)
        {
            tabpage = new System.Windows.Forms.TabPage(caption);
            tabpage.Tag = shellobject; //map-back from native control to guppy object

            tabpage.UseVisualStyleBackColor = true;
        }

        public override void Append(DriverWidget dw)
        {
            System.Windows.Forms.Control ctrl = dw.NativeObject as System.Windows.Forms.Control;
            if (ctrl != null)
                tabpage.Controls.Add(ctrl);
        }


        public override object NativeObject
        {
            get { return tabpage; }
        }


        public override Size2i Size
        {
            get { return WinFormsDriver.ConvertSize(tabpage.Size); }
        }


        public override Size2i GetNaturalSize()
        {
            return new Size2i(16, 16);
        }


        public override string Caption
        {
            get
            {
                return tabpage.Text;
            }
            set
            {
                tabpage.Text = value;
            }
        }
    }
}
