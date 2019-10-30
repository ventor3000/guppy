using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class Label : Widget
    {
        public Label(CompositeWidget parent, string caption)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateLabel(this, caption));
        }


        private DriverLabel DriverLabel
        {
            get { return DriverObject as DriverLabel; }
        }



        virtual public string Caption
        {
            get
            {
                return DriverLabel.Caption;
            }
            set
            {
                DriverLabel.Caption = value;
            }
        }


        #region EVENTS
        virtual public event GuppyEventHandler EvClicked;
        virtual internal bool OnClicked()
        {
            var e = new GuppyEventArgs(this);
            if (EvClicked != null) EvClicked(e);
            return e.Block;
        }

        #endregion


    }
}
