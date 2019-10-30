using System;
using GuppyGUI.AbstractDriver;


namespace GuppyGUI.WinForms
{
    public class WinFormsRadioButton:DriverRadioButton
    {
        System.Windows.Forms.RadioButton radiobtn;

        public WinFormsRadioButton(Widget shellobject, string caption):base(shellobject)
        {
            radiobtn = new System.Windows.Forms.RadioButton();
            radiobtn.Tag = shellobject; //map-back from native control to guppy object

            radiobtn.Text = caption;

            radiobtn.CheckedChanged += delegate { ((RadioButton)ShellObject).OnChanged(); };
            radiobtn.Enter += delegate { ((RadioButton)ShellObject).OnEnter(); };
        }


        public override object NativeObject
        {
            get { return radiobtn; }
        }

        public override Size2i GetNaturalSize()
        {
            return WinFormsDriver.ConvertSize(radiobtn.PreferredSize); // GetPreferredSize(Size.Empty);
        }


        public override bool Checked
        {
            get
            {
                return radiobtn.Checked;
            }
            set
            {
                radiobtn.Checked = value;
            }
        }

        public override bool Enabled
        {
            get
            {
                return radiobtn.Enabled;
            }
            set
            {
                radiobtn.Enabled = value;
            }
        }

        public override string Caption
        {
            get
            {
                return radiobtn.Text;
            }
            set
            {
                radiobtn.Text = value;
            }
        }

        
    }
}
