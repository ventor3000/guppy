using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class ProgressBar:Widget
    {

        double minval = 0.0;
        double maxval = 1.0;

        public ProgressBar(CompositeWidget parent)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateProgressBar(this));
        }

        DriverProgressBar DriverBar
        {
            get { return DriverObject as DriverProgressBar; }
        }

        virtual public double Value
        {
            //internally, driver wants value 0.0-1.0 so we reconpute them
            get
            {
                return DriverBar.Value * (maxval - minval) + minval;
            }
            set
            {
                double delta=(maxval - minval); //protect from invalid settings
                if(delta<=1e-6)
                    DriverBar.Value=0.0;
                else {
                    value = (value - minval) / delta;   //recompute value for drivers 0.0-1.0
                    if (value < 0.0) value = 0.0;
                    if (value > 1.0) value = 1.0;
                        DriverBar.Value = value;
                }
            }
        }

        virtual public double Max
        {
            get { return maxval; }
            set { maxval = value; }
        }

        virtual public double Min
        {
            get { return minval; }
            set { minval = value; }
        }

    }
}
