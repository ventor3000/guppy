
using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{
    public class PopupMenu:CompositeWidget
    {
        public PopupMenu()
        {
            AttachDriverObject(null, Guppy.Driver.CreatePopupMenu(this));
        }

        DriverPopupMenu DriverPopup {get {return DriverObject as DriverPopupMenu;}}

        virtual public void Show()
        {
            DriverPopup.Show();
        }

        /*virtual public void Clear()
        {
            Children.Clear();
            DriverPopup.Clear();
        }*/ //TODO: implement this in some way Children.Clear?


        #region EVENTS
        virtual public event GuppyEventHandler EvShowing;
        virtual internal bool OnShowing()
        {
            var e = new GuppyEventArgs(this);
            if (EvShowing != null) EvShowing(e);
            return e.Block;
        }

        #endregion
       
    }

    

    [System.Flags]
    public enum MenuFlags {
        None=0,
        Separator=1,
        Checkable=2,
        Checked=3
    }
}
