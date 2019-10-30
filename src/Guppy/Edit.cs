using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public class Edit : Widget
    {
        public Edit(CompositeWidget parent)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateEdit(this));
        }

        private DriverEdit DriverEdit
        {
            get { return DriverObject as DriverEdit; }
        }

        virtual public string Text
        {
            get { return DriverEdit.Text; }
            set { DriverEdit.Text = value; }
        }

        virtual public void Append(string txt)
        {
            DriverEdit.Append(txt);
        }

        virtual public void AppendLine(string txt) //TODO: maybe multiline append aware?
        {
            Append(txt);
            Append("\n");
        }

        virtual public void Clear()
        {
            DriverEdit.Clear();
        }


        virtual public void SelectAll()
        {
            DriverEdit.SelectAll();
        }

        virtual public bool ReadOnly
        {
            get {
                return DriverEdit.ReadOnly;
            }
            set {
                DriverEdit.ReadOnly=value;
            }
        }

       

        #region EVENTS

        virtual public event GuppyEventHandler EvChanged;
        virtual public bool OnChanged()
        {
            var e = new GuppyEventArgs(this);
            if (EvChanged != null) EvChanged(e);
            return e.Block;
        }

        virtual public event GuppyEventHandler EvLeave;
        virtual public bool OnLeave()
        {
            var e = new GuppyEventArgs(this);
            if (EvLeave != null) EvLeave(e);
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
