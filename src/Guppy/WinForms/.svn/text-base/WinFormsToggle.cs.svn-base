using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
    public class WinFormsToggle : DriverToggle
    {
        CustomCheckbox checkbox;

        public WinFormsToggle(Widget shellobject, string caption, bool isbutton)
            : base(shellobject)
        {
            checkbox = new CustomCheckbox();
            checkbox.Text = caption;
            checkbox.CheckedChanged += delegate { ((Toggle)shellobject).OnChanged(); };
            checkbox.Enter += delegate { ((Toggle)ShellObject).OnEnter(); };
            checkbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;

            if (isbutton)
            {
                checkbox.Appearance = System.Windows.Forms.Appearance.Button;
                checkbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        public override string Caption
        {
            get
            {
                return checkbox.Text;
            }
            set
            {
                checkbox.Text = value;
            }
        }

        public override Size2i GetNaturalSize()
        {
            Size2i res = WinFormsDriver.ConvertSize(checkbox.PreferredSize);
            return res;

            //return WinFormsDriver.ConvertSize(checkbox.PreferredSize); // GetPreferredSize(Size.Empty);
        }


        public override object NativeObject { get { return checkbox; } }


        public override bool Checked
        {
            get
            {
                return checkbox.Checked;
            }
            set
            {
                try
                {

                    checkbox.Checked = value;
                }
                finally
                {
                }
            }
        }

        public override bool Enabled
        {
            get
            {
                return checkbox.Enabled;
            }
            set
            {
                checkbox.Enabled = value;
            }
        }

        public override bool CanFocus
        {
            get
            {
                return checkbox.FocusEnabled;
            }
            set
            {
                checkbox.FocusEnabled = value;
            }
        }


        public override Image Image
        {
            set
            {
                checkbox.Image = WinFormsDriver.ImageToWinFormsImage(value);
            }
        }


        //we need to override winforms chackbox to be able to solve the 'CanFocus' property

        private class CustomCheckbox : System.Windows.Forms.CheckBox
        {



            public CustomCheckbox()
            {

            }


            internal bool FocusEnabled //needed to be able to shut of focus
            {
                get
                {
                    return this.GetStyle(System.Windows.Forms.ControlStyles.Selectable);
                }
                set
                {
                    this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, value);
                }
            }
        }

        



    }
}
