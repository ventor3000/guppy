
using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{
    public class Memo : Widget
    {
        public Memo(CompositeWidget parent)
        {
            AttachDriverObject(parent, Guppy.Driver.CreateMemo(this));
        }

        private DriverMemo DriverMemo
        {
            get { return DriverObject as DriverMemo; }
        }

        virtual public void Append(string text)
        {
            DriverMemo.Append(text);
        }

        virtual public void Clear()
        {
            DriverMemo.Clear();
        }

        virtual public Point2i CaretPosition { get { return DriverMemo.CaretPosition; } }

        virtual public void AppendLine(string text)
        {
            DriverMemo.AppendLine(text);
        }

        virtual public bool ReadOnly
        {
            get { return DriverMemo.ReadOnly; }
            set { DriverMemo.ReadOnly = value; }
        }

        virtual public string Value
        {
            get { return DriverMemo.Text; }
            set { DriverMemo.Text = value; }
        }

        virtual public void SelectAll()
        {
            DriverMemo.SelectAll();
        }

        virtual public bool AcceptsTabs
        {
            get { return DriverMemo.AcceptsTabs; }
            set { DriverMemo.AcceptsTabs = value; }
        }

        #region EVENTS

        virtual public event GuppyEventHandler EvChanged;
        virtual public bool OnChanged()
        {
            var e = new GuppyEventArgs(this);
            if (EvChanged != null) EvChanged(e);
            return e.Block;
        }

        #endregion

    }
}
