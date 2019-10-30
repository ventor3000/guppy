using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class RadioButton:Widget
    {
        bool eventblock = false; //used do thrash event when changing value from code

        public RadioButton(CompositeWidget parent, string caption)
            : base()
        {
            AttachDriverObject(parent, Guppy.Driver.CreateRadioButton(this, caption));
        }

        private DriverRadioButton DriverRadioButton { get { return DriverObject as DriverRadioButton; } }


        virtual public bool Checked
        {
            get { return DriverRadioButton.Checked; }
            set {
                try
                {
                    eventblock = true;
                    DriverRadioButton.Checked = value;
                }
                finally
                {
                    eventblock = false;
                }
            }
        }



        virtual public string Caption
        {
            get { return DriverRadioButton.Caption; }
            set { DriverRadioButton.Caption = value; }
        }


        #region EVENTS
        virtual public event GuppyEventHandler EvChanged;
        virtual internal bool OnChanged()
        {
            if (eventblock)
                return false;
            var e = new GuppyEventArgs(this);
            if (EvChanged != null) EvChanged(e);
            return e.Block;
        }

        virtual public event GuppyEventHandler EvEnter;
        virtual internal bool OnEnter()
        {
            var e = new GuppyEventArgs(this);
            if (EvEnter != null) EvEnter(e);
            return e.Block;
        }

        #endregion

    }
}
