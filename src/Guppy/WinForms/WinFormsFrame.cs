using System;
using GuppyGUI.AbstractDriver;
using System.Windows.Forms;

namespace GuppyGUI.WinForms
{

    public class WinFormsFrame : DriverFrame
    {
        //System.Windows.Forms.GroupBox groupbox;
        System.Windows.Forms.Control frame; //can be panel or GroupBox

        public WinFormsFrame(Widget shellobject, string caption,bool border)
            : base(shellobject)
        {

            if (!border)
                frame = new Panel();
            else
                frame = new System.Windows.Forms.GroupBox();
            frame.Tag = shellobject; //map-back from native control to guppy object

            frame.Text = caption;
        }

        public override object NativeObject
        {
            get
            {
                return frame;
            }
        }

        public override string Caption
        {
            get
            {
                return frame.Text;
            }
            set
            {
                frame.Text = value;
            }
        }


        public override Size2i GetNaturalSize()
        {
            throw new Exception("Natural size should not be called for composite widgets");
        }



        public override void Append(DriverWidget dw)
        {
            System.Windows.Forms.Control ctrl = dw.NativeObject as System.Windows.Forms.Control;
            if (ctrl != null)
                frame.Controls.Add(ctrl);
        }

        public override Margin GetDecorationSize()
        {
            if (frame is GroupBox)
            {
                int xb = SystemInformation.Border3DSize.Width;
                int yb = SystemInformation.Border3DSize.Height;
                int fh = frame.Font.Height;
                return new Margin(xb, fh + yb, xb, yb);
            }
            else 
                return Margin.Empty; //borderless panel => 0 margin
        }

        public override Point2i GetClientOrigin()
        {
            if (frame is GroupBox)
            {
                int xb = SystemInformation.Border3DSize.Width;
                int yb = SystemInformation.Border3DSize.Height;
                int fh = frame.Font.Height;

                var res = new Point2i(xb, fh + yb);
                return res;
            }
            else
            {
                return Point2i.Empty; //borderless frame has origin @ 0,0
            }
        }

    }
}
