
using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{

    public class Frame : CompositeWidget
    {
        public Frame(CompositeWidget parent, string caption,bool border)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateFrame(this, caption,border));

            if (border)
                Margin = new Margin(Guppy.DefaultMargin);
            else
                Margin = Margin.Empty;
        }
        
        private DriverFrame DriverFrame { get { return DriverObject as DriverFrame; } }

        virtual public string Caption { get { return DriverFrame.Caption; } set { DriverFrame.Caption = value; } }

    }
}
