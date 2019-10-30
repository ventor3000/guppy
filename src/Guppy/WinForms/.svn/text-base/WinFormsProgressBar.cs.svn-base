using System;
using System.Collections.Generic;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
    public class WinFormsProgressBar : DriverProgressBar
    {
        private System.Windows.Forms.ProgressBar progbar;

        public WinFormsProgressBar(Widget shellobject)
            : base(shellobject)
        {
            progbar = new System.Windows.Forms.ProgressBar();
            progbar.Tag = shellobject; //map-back from native control to guppy object

            progbar.Minimum = 0;
            progbar.Maximum = 100;

            //progbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            progbar.MarqueeAnimationSpeed = 1000;
        }

        public override double Value
        {
            get
            {
                return (double)progbar.Value / 100.0;
            }
            set
            {
                progbar.Value = (int)(value * 100.0);
            }
        }


        public override object NativeObject
        {
            get { return progbar; }
        }

        public override Size2i GetNaturalSize()
        {
            return new Size2i(120, 25);
        }



    }
}
