using System;
using System.Collections.Generic;
using System.Text;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
    public class WinFormsValuator : DriverValuator
    {
        const double maxval = 1000.0;
        private readonly int preffered_thickness=-1;

        System.Windows.Forms.TrackBar trackbar;

        public WinFormsValuator(Widget shellobject, bool vertical)
            : base(shellobject)
        {
            trackbar = new System.Windows.Forms.TrackBar();
            trackbar.Tag = shellobject; //map-back from native control to guppy object

            //stupid getpreffered size doesent work well, only always returns current size so we store it here
            //discounting for the rediculous space taken into account for the tickmarks
            //we currently dont use
            if (preffered_thickness < 0)
                preffered_thickness = trackbar.Height / 2;

            trackbar.Orientation = vertical ? System.Windows.Forms.Orientation.Vertical : System.Windows.Forms.Orientation.Horizontal;
            trackbar.AutoSize = false;
            trackbar.Maximum = (int)maxval;
            trackbar.TickStyle = System.Windows.Forms.TickStyle.None;
            trackbar.Scroll += delegate { ((Valuator)shellobject).OnChanged(); };
            
            trackbar.SmallChange = (int)(maxval / 10.0);
            trackbar.LargeChange = (int)(maxval / 4);
        }

        public override double Value
        {
            get
            {
                return (double)trackbar.Value / maxval;
            }
            set
            {
                trackbar.Value = (int)(value * maxval);
            }
        }


        public override object NativeObject
        {
            get { return trackbar; }
        }

        public override Size2i GetNaturalSize()
        {
            Size2i res;
            if (trackbar.Orientation == System.Windows.Forms.Orientation.Horizontal)
                res = new Size2i(120, preffered_thickness);
            else
                res = new Size2i(preffered_thickness, 120);

            return res;
        }


    }
}
