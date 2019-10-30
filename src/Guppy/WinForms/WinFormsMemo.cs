using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
    public class WinFormsMemo : DriverMemo
    {
        System.Windows.Forms.TextBox memo;

        public WinFormsMemo(Widget shellobject)
            : base(shellobject)
        {
            memo = new System.Windows.Forms.TextBox();
            memo.Tag = shellobject; //map-back from native control to guppy object

            memo.Multiline = true;

            memo.TextChanged += delegate { ((Memo)ShellObject).OnChanged(); };

            //manually set colors to avoid grayed out when in read-only mode
            memo.BackColor = System.Drawing.SystemColors.Window;
            memo.ForeColor = System.Drawing.SystemColors.WindowText;
        }

        public override void Append(string text)
        {
            memo.AppendText(text);
        }

        public override void Clear()
        {
            memo.Text = "";
        }


        public override object NativeObject
        {
            get { return memo; }
        }

        public override Size2i GetNaturalSize()
        {
            return new Size2i(Guppy.DefaultEditWidth, Guppy.DefaultEditWidth);
        }


        public override Point2i CaretPosition
        {
            get
            {
                int line = memo.GetLineFromCharIndex(memo.SelectionStart);
                int col = memo.SelectionStart - memo.GetFirstCharIndexFromLine(line);
                return new Point2i(col, line);
            }
        }

        public override bool ReadOnly
        {
            get
            {
                return memo.ReadOnly;
            }
            set
            {
                memo.ReadOnly = value;
            }
        }

        public override void AppendLine(string text)
        {
            memo.AppendText(text + "\n");
        }

        public override string Text
        {
            get
            {
                return memo.Text;
            }
            set
            {
                memo.Text = value;
            }
        }

        public override void SelectAll()
        {
            memo.SelectAll();
        }

        public override bool AcceptsTabs
        {
            get
            {
                return memo.AcceptsTab;
            }
            set
            {
                memo.AcceptsTab = value;
            }
        }
    }
}
