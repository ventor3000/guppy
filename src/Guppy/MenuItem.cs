
using System;
using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{
    public class MenuItem:CompositeWidget
    {

        public MenuItem(CompositeWidget menu, string caption, Image image, MenuFlags flags)
        {
            if (menu!=null && !(menu is PopupMenu) && !(menu is MenuItem))
                throw new Exception("MenuItem:s can only have menus/popup and other menuitems as parent");
            AttachDriverObject(menu, Guppy.Driver.CreateMenuItem(this, caption, image, flags));
        }

        public MenuItem(CompositeWidget parent, string caption)
            : this(parent, caption, null, MenuFlags.None)
        {
        }

        DriverMenuItem DriverItem { get { return DriverObject as DriverMenuItem; } }


        virtual public void Clear()
        {
            DriverItem.Clear();
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
