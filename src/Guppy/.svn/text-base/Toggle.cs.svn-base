using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class Toggle : Widget
    {

        bool eventblock = false;  //used to block changed events when changed from code

        public Toggle(CompositeWidget parent, string caption, bool isbutton)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateToggle(this, caption, isbutton));
        }



        private DriverToggle DriverToggle
        {
            get { return DriverObject as DriverToggle; }
        }

        virtual public bool Checked
        {
            get { return DriverToggle.Checked; }
            set
            {
                try
                {
                    eventblock = true;
                    DriverToggle.Checked = value;
                }
                finally
                {
                    eventblock = false;
                }
            }
        }

        virtual public bool CanFocus
        {
            get { return DriverToggle.CanFocus; }
            set { DriverToggle.CanFocus = value; }
        }

        virtual public string Caption
        {
            get
            {
                return DriverToggle.Caption;
            }
            set
            {
                DriverToggle.Caption = value;
            }
        }

        virtual public Image Image
        {
            set
            {
                DriverToggle.Image = value;
            }
        }


       
        
        #region EVENTS
        virtual public event GuppyEventHandler EvChanged;
        virtual internal void OnChanged() { if (!eventblock && EvChanged != null) EvChanged(new GuppyEventArgs(this)); }

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
