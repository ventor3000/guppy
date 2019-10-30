
namespace GuppyGUI.AbstractDriver
{
    public abstract class DriverMemo : DriverWidget
    {
        public DriverMemo(Widget shellobject)
            : base(shellobject)
        {

        }

        public abstract string Text { get; set; }
        public abstract void Append(string text);
        public abstract void Clear();
        public abstract Point2i CaretPosition { get; }
        public abstract bool ReadOnly { get; set; }
        public abstract void AppendLine(string text);
        public abstract void SelectAll();
        public abstract bool AcceptsTabs { get; set; }

    }
}
