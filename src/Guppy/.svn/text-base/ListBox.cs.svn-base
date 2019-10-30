using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class ListBox : Widget
    {       
        bool eventblock = false;

        public ListBox(CompositeWidget parent, params object[] items)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateListBox(this, items));
            SelectedIndex = 0; //Select first item if any
        }

        private DriverListBox DriverListBox
        {
            get { return DriverObject as DriverListBox; }
        }

        virtual public int Append(object item)
        {
            int res = DriverListBox.Append(item);
            if (res == 0)
                SelectedIndex = 0;
            return res;
        }

        virtual public int SelectedIndex
        {
            get { return DriverListBox.SelectedIndex; }
            set
            {
                try
                {
                    eventblock = true;
                    DriverListBox.SelectedIndex = value;
                }
                finally
                {
                    eventblock = false;
                }
            }
        }

        virtual public int Count
        {
            get { return DriverListBox.Count; }
        }

        virtual public void Clear()
        {
            DriverListBox.Clear();
        }

        virtual public int RemoveIndex(int index)
        {
            return DriverListBox.RemoveIndex(index);
        }

        virtual public object SelectedObject
        {
            get { return this[SelectedIndex]; }
            set
            {
                //try to match object exactly
                for (int l = 0; l < Count; l++)
                {
                    if (this[l].Equals(value))
                    {
                        SelectedIndex = l;
                        return;
                    }
                }

                SelectedIndex = -1; //object do not match any, set to empty
            }
        }

        virtual public object this[int index]
        {
            get { return DriverListBox[index]; }
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

        virtual public event GuppyEventHandler EvDoubleClick;
        virtual internal bool OnDoubleClick()
        {
            var e = new GuppyEventArgs(this);
            if (EvDoubleClick != null) EvDoubleClick(e);
            return e.Block;
        }

        #endregion

    }
}
