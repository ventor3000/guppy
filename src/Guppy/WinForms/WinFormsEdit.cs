using GuppyGUI.AbstractDriver;
using System.Windows.Forms;


namespace GuppyGUI.WinForms
{
    public class WinFormsEdit : DriverEdit
    {
        TextBox textbox;

        public WinFormsEdit(Widget shellobject)
            : base(shellobject)
        {

            textbox = new TextBox(); // new System.Windows.Forms.TextBox();
            textbox.Tag = shellobject; //map-back from native control to guppy object

            textbox.TextChanged += delegate { ((Edit)ShellObject).OnChanged(); };
            textbox.Leave += delegate { ((Edit)ShellObject).OnLeave(); };
            textbox.Enter += delegate { ((Edit)ShellObject).OnEnter(); };

            //manually set colors to avoid grayed out when in read-only mode
            textbox.BackColor = System.Drawing.SystemColors.Window;
            textbox.ForeColor = System.Drawing.SystemColors.WindowText;
        }

        /*
        void textbox_TextChanged(object sender, System.EventArgs e)
        {
          new EventData(ShellObject, EventID.Changed, true).Send();
        }*/


        public override Size2i GetNaturalSize()
        {
            if (textbox.Multiline)
            {
                return new Size2i(50, 50);
            }
            else
            {
                System.Drawing.Size s = textbox.PreferredSize;
                s.Width = GuppyGUI.Guppy.DefaultEditWidth;
                return WinFormsDriver.ConvertSize(s);
            }

        }




        public override object NativeObject { get { return textbox; } }


        public override string Text
        {
            get
            {
                return textbox.Text;
            }
            set
            {
                textbox.Text = value;
            }
        }

        public override void Append(string txt)
        {
            textbox.AppendText(txt);
        }

        public override void Clear()
        {
            textbox.Clear();
        }

        public override bool Enabled
        {
            get
            {
                return textbox.Enabled;
            }
            set
            {
                textbox.Enabled = value;
            }
        }

        public override void SelectAll()
        {
            textbox.SelectAll();
        }

        public override bool ReadOnly
        {
            get
            {
                return textbox.ReadOnly;
            }
            set
            {
                textbox.ReadOnly = value;
            }
        }

    }




}
