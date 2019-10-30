using GuppyGUI.AbstractDriver;
using System;

namespace GuppyGUI.WinForms
{
    public class WinFormsMenuItem:DriverMenuItem
    {
        System.Windows.Forms.ToolStripItem item;

        public WinFormsMenuItem(Widget shellobject,string caption, Image image, MenuFlags flags)
            :base(shellobject)
        {
            if ((flags & MenuFlags.Separator) != 0)
            {
                item = new System.Windows.Forms.ToolStripSeparator();
                item.Tag = shellobject; //map-back from native control to guppy object
                
                return; //no events for separator
            }
            else
            {
                System.Windows.Forms.ToolStripMenuItem mi = new System.Windows.Forms.ToolStripMenuItem();
                mi.Tag = shellobject; //map-back from native control to guppy object

                item = mi;
                mi.Image = WinFormsDriver.ImageToWinFormsImage(image);
                mi.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;

                //parse shortkey text
                if (caption == null) caption = "";
                string[] capts = caption.Split('\t');
                if (capts.Length > 0)
                    mi.Text = capts[0];
                if (capts.Length > 1)
                    mi.ShortcutKeyDisplayString = capts[1];

                if ((flags & MenuFlags.Checkable) != 0)
                {
                    mi.CheckOnClick = true;
                    mi.CheckState = (((flags & MenuFlags.Checked) != 0) ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked);
                }
            }
            
            item.Click += delegate { ((MenuItem)ShellObject).OnClicked(); };
        }


        public override object NativeObject
        {
            get { return item; }
        }

        public override Size2i GetNaturalSize()
        {
            return Size2i.Empty; //not valid for this widget
        }


        public override void Append(DriverWidget dw)
        {
            WinFormsMenuItem mi = dw as WinFormsMenuItem;
            if (mi == null) throw new Exception("Trying to append invalid object to popup menu:" + dw.ToString());

            var child = dw.NativeObject as System.Windows.Forms.ToolStripItem;
            var par = item as System.Windows.Forms.ToolStripMenuItem;

            if(item!=null && par!=null)
                par.DropDownItems.Add(child);
        }

        public override string Tip
        {
            set
            {
                //since this widget is not 'Control' inherited,
                //we need to set the reference another way than the default
                item.ToolTipText = value;
            }
        }

        public override bool Enabled
        {
            get
            {
                return item.Enabled;
            }
            set
            {
                item.Enabled = value;
            }
        }

        public override bool Visible
        {
            get
            {
                return item.Visible;
            }
            set
            {
                item.Visible = value;
            }
        }

        public override void Clear()
        {
            var mi = item as System.Windows.Forms.ToolStripMenuItem;
            if (mi != null)
                mi.DropDownItems.Clear();
        }
    }
}
