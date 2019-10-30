using GuppyGUI.AbstractDriver;
using System;

namespace GuppyGUI.WinForms
{

    public class WinFormsPopupMenu:DriverPopupMenu
    {
        System.Windows.Forms.ContextMenuStrip strip;

        public WinFormsPopupMenu(Widget shellobject)
            : base(shellobject)
        {
            strip = new System.Windows.Forms.ContextMenuStrip();
            strip.Tag = shellobject; //map-back from native control to guppy object

            strip.Opening += (s,e)=> { 
                //M$ prepopulates event with cancel=true if menu is empty which is a problem
                //if we populate it OnShowing, we solve this by setting e.Cancel always
                bool block=((PopupMenu)ShellObject).OnShowing();
                e.Cancel = block;            
            };
        }

        public override void Show()
        {
            strip.Show(System.Windows.Forms.Cursor.Position);
        }
        
        public override object NativeObject
        {
            get { return strip; }
        }

        public override Size2i GetNaturalSize()
        {
            return Size2i.Empty; //not valid for this widget
        }


        public override void Clear()
        {
            strip.Items.Clear();
        }


        /*public override MenuItem Add(string caption, Image image, MenuFlags flags)
        {
            System.Windows.Forms.ToolStripItem item=null;
            if ((flags & MenuFlags.Separator) != 0)
                item = new System.Windows.Forms.ToolStripSeparator();
            else
            {
                System.Windows.Forms.ToolStripMenuItem mi = new System.Windows.Forms.ToolStripMenuItem();
                item = mi;
                mi.Image = WinFormsDriver.ImageToWinFormsImage(image);
                mi.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;

                //parse shortkey text
                if (caption == null) caption = "";
                string[] capts=caption.Split('\t');
                if(capts.Length>0)
                    mi.Text = capts[0];
                if (capts.Length > 1)
                    mi.ShortcutKeyDisplayString = capts[1];

                if ((flags & MenuFlags.Checkable) != 0)
                {
                    mi.CheckOnClick = true;
                    mi.CheckState = (((flags & MenuFlags.Checked) != 0) ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked);
                }

                strip.Items.Add(mi);
            }
            
            return new MenuItem(item, caption, image, flags);
        }*/

        

        public override void Append(DriverWidget dw)
        {
            
            WinFormsMenuItem mi = dw as WinFormsMenuItem;
            if (mi == null) throw new Exception("Trying to append invalid object to popup menu:" + dw.ToString());

            var item=dw.NativeObject as System.Windows.Forms.ToolStripItem;

            

            strip. Items.Add(item);
        }

        public override void Detach(DriverWidget child)
        {
            WinFormsMenuItem mi = child as WinFormsMenuItem;
            if (mi == null) throw new Exception("Trying to detach invalid object to popup menu:" + child.ToString());

            var item = child.NativeObject as System.Windows.Forms.ToolStripItem;

            strip.Items.Remove(item);
        }

        public override bool Visible
        {
            get
            {
                return strip.Visible;
            }
            set
            {
                strip.Visible = value;
            }
        }

        public override bool Enabled
        {
            get
            {
                return strip.Enabled;
            }
            set
            {
                strip.Enabled = value;
            }
        }


    }
}
