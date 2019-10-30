using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{


    public class Button : Widget
    {

        private object response = null;

        public Button(CompositeWidget parent, string caption)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateButton(this, caption));
        }


        private DriverButton DriverButton
        {
            get
            {
                return DriverObject as DriverButton;
            }
        }

        /// <summary>
        /// Sets or gets the response this button gives to the window it is a child of.
        /// Setting it to null (default) disables response
        /// </summary>
        virtual public object Response
        {
            get
            {
                return response;
            }
            set
            {
                response = value;
            }
        }

        virtual public string Caption
        {
            get
            {
                return DriverButton.Caption;
            }
            set
            {
                DriverButton.Caption = value;
            }
        }

        virtual public bool Flat
        {
            get
            {
                return DriverButton.Flat;
            }
            set
            {
                DriverButton.Flat = value;
            }
        }

        virtual public bool CanFocus
        {
            get
            {
                return DriverButton.CanFocus;
            }
            set
            {
                DriverButton.CanFocus = value;

            }
        }

        virtual public Image Image
        {
            //TODO: get image???
            set
            {
                DriverButton.Image = value;
            }
        }



        virtual public bool Default
        {
            set { DriverButton.Default = value; }
        }



        #region EVENTS

        virtual public event GuppyEventHandler EvClicked;
        virtual internal bool OnClicked()
        {
            //handle buttons built-in response function
            if (response != null)
            {
                Window w = Window;
                if (w != null)
                    w.Close(response);
            }

            var e = new GuppyEventArgs(this);
            if (EvClicked != null) EvClicked(e);
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
