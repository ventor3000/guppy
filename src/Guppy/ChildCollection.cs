using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class ChildCollection:IEnumerable<Widget>
    {
        List<Widget> items=new List<Widget>();
        CompositeWidget owner;

        public ChildCollection(CompositeWidget owner)
        {
            this.owner = owner;
        }

        private void PhysicalAppend(DriverCompositeWidget parent, Widget child)
        {
            if (parent == null)
                return;

            if (child.DriverObject == null)
            {
                //no driver object to append, but it might be a composite container so apply recursivly
                CompositeWidget cw = child as CompositeWidget;
                if (cw != null)
                {
                    foreach(Widget wi in cw.Children) 
                        PhysicalAppend(parent,wi);
                }
            }
            else
            {
                DriverWidget ch = child.DriverObject as DriverWidget;
                if (ch != null)
                    parent.Append(ch);
            }
        }

        public void Append(Widget w)
        {
            //adds a child to this array and atatches it physically to the driver object
            if (w != null)
            {
               /* DriverCompositeWidget physdrvobj = owner.PhysicalParentDriverObject;
                if (physdrvobj != null)
                {
                    if (w.DriverObject == null)
                    {
                        //a non-physical control, if composite, append each child
                        CompositeWidget cw = w as CompositeWidget;
                        if (cw != null)
                        {
                            foreach (Widget childwi in cw.Children)
                                Append(childwi);
                        }
                    }
                    else
                    {
                        DriverWidget child = w.DriverObject as DriverWidget;
                        if (child != null)
                            physdrvobj.Append(child);
                    }
                }*/
                PhysicalAppend(owner.PhysicalParentDriverObject, w);
                items.Add(w);
                w.Parent = owner;
            }
        }

        private void PhysicalDetach(DriverCompositeWidget parent, Widget child)
        {
            if (parent == null)
                return;

            if (child.DriverObject == null)
            {
                //no driver object to detach, but it might be a composite container so apply recursivly
                CompositeWidget cw = child as CompositeWidget;
                if (cw != null)
                {
                    foreach (Widget wi in cw.Children)
                        PhysicalDetach(parent, wi);
                }
            }
            else
            {
                DriverWidget ch = child.DriverObject as DriverWidget;
                if (ch != null)
                    parent.Detach(ch);
            }
        }

        public Widget Detach(Widget w)
        {
            //removes a child from this array and detaches it physically from the driver object
            if (w != null)
            {
                /*DriverCompositeWidget physdrvobj = owner.PhysicalParentDriverObject;
                if (physdrvobj != null)
                {
                    DriverWidget child = w.DriverObject as DriverWidget;
                    if (child != null)
                        physdrvobj.Detach(child);
                }*/

                PhysicalDetach(owner.PhysicalParentDriverObject, w);
                items.Remove(w);
                w.Parent = null;
            }

            return w;
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }


        public Widget this[int i]
        {
            get
            {
                return items[i];
            }
        }

        public int IndexOf(Widget w)
        {
            return items.IndexOf(w);
        }


        public IEnumerator<Widget> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
