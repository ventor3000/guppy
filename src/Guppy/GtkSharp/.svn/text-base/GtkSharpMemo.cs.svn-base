using GuppyGUI.AbstractDriver;

namespace GuppyGUI.GtkSharp
{
    public class GtkSharpMemo:DriverMemo
    {
        Gtk.TextView textview;


        public GtkSharpMemo(Widget shellobject)
            : base(shellobject)
        {
            textview = new Gtk.TextView();
            GtkSharpDriver.InitWidget(textview, shellobject);
            textview.Show();

            textview.Buffer.Changed += delegate { ((Memo)ShellObject).OnChanged(); };
            textview.AcceptsTab = false;    //dafault is to tab to next control
        }

        public override string Text
        {
            get
            {
                return textview.Buffer.Text;
            }
            set
            {
                textview.Buffer.Text = value;
            }
        }

        public override void Append(string text)
        {
            var endit=textview.Buffer.EndIter;
            textview.Buffer.Insert(ref endit, text);
        }

        public override void Clear()
        {
            textview.Buffer.Text = "";
        }

        public override Point2i CaretPosition
        {
            get {
                Gtk.TextIter it = textview.Buffer.GetIterAtMark(textview.Buffer.InsertMark);
                int lin = it.Line;
                int col = it.LineOffset;
                return new Point2i(lin, col);
            }
        }

        public override bool ReadOnly
        {
            get
            {

                return !textview.Editable;
            }
            set
            {
                textview.Editable = !value;
            }
        }

        public override void AppendLine(string text)
        {
            Append(text + "\n");
        }

        public override void SelectAll()
        {
            textview.Buffer.SelectRange(textview.Buffer.StartIter, textview.Buffer.EndIter);
        }

        public override object NativeObject
        {
            get { return textview; }
        }

        public override Size2i GetNaturalSize()
        {
            return new Size2i(Guppy.DefaultEditWidth, Guppy.DefaultEditWidth);
        }

        public override bool AcceptsTabs
        {
            get
            {
                return textview.AcceptsTab;
            }
            set
            {
                textview.AcceptsTab = value;
            }
        }
    }
}
