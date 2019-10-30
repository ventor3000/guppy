using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class ChoiceEdit:Widget
    {
        public ChoiceEdit(CompositeWidget parent,params object[] items)
        {
            AttachDriverObject(parent,Guppy.Driver.CreateChoiceEdit(this,items));
        }

        DriverChoiceEdit DriverEdit { get { return DriverObject as DriverChoiceEdit; } }

        virtual public object SelectedObject
        {
            get { return DriverEdit[SelectedIndex]; }
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

                //no object match found but we have special case for
                //combo edit=> try to map string to objects ToString:
                string str = value as string;
                if (str != null)
                {
                    for (int l = 0; l < Count; l++)
                    {
                        string objstr = this[l].ToString();
                        if (objstr != null && objstr==str)
                        {
                            SelectedIndex = l;
                            return;
                        }
                    }
                }
                

                SelectedIndex = -1; //object do not match any, set to empty
            }
        }

        virtual public void Append(object obj)
        {
            DriverEdit.Append(obj);
        }

        virtual public int RemoveIndex(int index)
        {
            return DriverEdit.RemoveIndex(index);
        }

        virtual public void Clear()
        {
            DriverEdit.Clear();
        }

        virtual public int DropDownCount
        {
            get { return DriverEdit.DropDownCount; }
            set { DriverEdit.DropDownCount = value; }
        }

        virtual public string Text
        {
            get { return DriverEdit.Text; }
            set { DriverEdit.Text = value; }
        }


        virtual public int SelectedIndex
        {
            get { return DriverEdit.SelectedIndex; }
            set { DriverEdit.SelectedIndex = value; }
        }

        virtual public object this[int index]
        {
            get { return DriverEdit[index]; }
        }


        virtual public int Count
        {
            get
            {
                return DriverEdit.Count;
            }
        }
    }
}
