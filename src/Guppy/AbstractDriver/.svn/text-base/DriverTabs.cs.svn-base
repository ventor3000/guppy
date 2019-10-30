using System;
using System.Collections.Generic;
using System.Text;

namespace GuppyGUI.AbstractDriver
{
    public abstract class DriverTabs : DriverCompositeWidget
    {
        public DriverTabs(Widget shellobject)
            : base(shellobject)
        {

        }

        public abstract bool ShowTabs { set; }

        public abstract int SelectedIndex { get; set; }
        public abstract int Count { get; }
        public abstract int RemoveIndex(int index);
    }

    public abstract class DriverTabPage:DriverCompositeWidget
    {
        public DriverTabPage(Widget shellobject)
            : base(shellobject)
        {

        }

        public abstract Size2i Size { get; }

        public abstract string Caption { get; set; }

    }

    

   
    


}
