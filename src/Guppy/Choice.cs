using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{
	public class Choice : Widget
	{
        private bool eventblock = false;

		public Choice (CompositeWidget parent, params object[] items)
		{
            AttachDriverObject(parent, Guppy.Driver.CreateChoice(this, items));
			/*DriverObject = Guppy.Driver.CreateChoice (this, entries);
			if (parent != null) {
				this.Parent = parent;
				parent.Append (this);
			}*/
            
            SelectedIndex = 0;  //select first item if any
		}
		
		
        private DriverChoice DriverChoice { get { return DriverObject as DriverChoice; } }

        virtual public int Append(object item)
        {
            int res=DriverChoice.Append(item);
            if (res == 0) 
                SelectedIndex = 0;
            return res;
        }

        virtual public int SelectedIndex
        {
            get { return DriverChoice.SelectedIndex; }
            set
            {

                try
                {
                    eventblock = true;
                    DriverChoice.SelectedIndex = value;
                }
                finally
                {
                    eventblock = false;
                }
            }
        }

        virtual public int Count
        {
            get { return DriverChoice.Count; }
        }


        virtual public object SelectedObject
        {
            get { return this[SelectedIndex]; }
            set {
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


        virtual public int DropDownCount
        {
            get { return DriverChoice.DropDownCount; }
            set { DriverChoice.DropDownCount = value; }
        }

        virtual public void Clear()
        {
            DriverChoice.Clear();
        }

        virtual public int RemoveIndex(int index)
        {
            return DriverChoice.RemoveIndex(index);
        }

        virtual public object this[int index]
        {
            get {
                return DriverChoice[index];
            }
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

