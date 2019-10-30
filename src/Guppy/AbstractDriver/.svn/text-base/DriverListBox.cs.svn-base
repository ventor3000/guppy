
namespace GuppyGUI.AbstractDriver
{
    public abstract class DriverListBox : DriverWidget
    {
        public DriverListBox(Widget shellobject)
            : base(shellobject)
        {

        }

        public abstract int Append(object item);
        public abstract int Count { get; }
        public abstract int SelectedIndex { get; set; }
        public abstract void Clear();
        public abstract int RemoveIndex(int index);
        public abstract object this[int index]{get;}
        
    }
}
