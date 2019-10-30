using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class Valuator : Widget
    {

        double minval = 0.0;
        double maxval = 1.0;

        public Valuator(CompositeWidget parent,bool vertical)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateValuator(this,vertical));
        }

        DriverValuator DriverValuator
        {
            get { return DriverObject as DriverValuator; }
        }

        virtual public double Value
        {
            //internally, driver wants value 0.0-1.0 so we reconpute them
            get
            {
                return DriverValuator.Value * (maxval - minval) + minval;
            }
            set
            {
                double delta = (maxval - minval); //protect from invalid settings
                if (delta <= 1e-6)
                    DriverValuator.Value = 0.0;
                else
                {
                    value = (value - minval) / delta;   //recompute value for drivers 0.0-1.0
                    if (value < 0.0) value = 0.0;
                    if (value > 1.0) value = 1.0;
                    DriverValuator.Value = value;
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

