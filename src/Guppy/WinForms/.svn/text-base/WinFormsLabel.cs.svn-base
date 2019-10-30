using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
    public class WinFormsLabel:DriverLabel
    {
        System.Windows.Forms.Label label;

        public WinFormsLabel(Widget shellobject, string caption):base(shellobject)
        {
            label = new System.Windows.Forms.Label();
            label.Tag = shellobject; //map-back from native control to guppy object

            label.Text = caption;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label.AutoSize = false;

            label.Click += delegate { ((Label)ShellObject).OnClicked(); };
        }

        public override string Caption
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text=value;
            }
        }

        public override Size2i GetNaturalSize()
        {
          return WinFormsDriver.ConvertSize(label.PreferredSize); // label.GetPreferredSize(Size.Empty);
        }


        public override object NativeObject { get { return label; } }



    }
}
