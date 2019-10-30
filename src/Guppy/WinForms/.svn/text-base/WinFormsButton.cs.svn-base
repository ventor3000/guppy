using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
    public class WinFormsButton : DriverButton
    {
        CustomButton button;

        public WinFormsButton(Widget shellobject, string caption)
            : base(shellobject)
        {
            button = new CustomButton();
            button.Tag = shellobject; //map-back from native control to guppy object
            button.Text = caption;
            button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;

            //Initialize events
            button.Click += delegate { button.Capture = false; ((Button)ShellObject).OnClicked(); };
            button.Enter += delegate { ((Button)ShellObject).OnEnter(); };
        }



        public override string Caption
        {
            get
            {
                return button.Text;
            }
            set
            {
                button.Text = value;
            }
        }

        public override Size2i GetNaturalSize()
        {
            System.Drawing.Size s = System.Windows.Forms.TextRenderer.MeasureText(Caption, button.Font);

            //special case:empty caption
            if (s.Height == 0)
                s.Height = System.Windows.Forms.TextRenderer.MeasureText("W", button.Font).Height;

            if (button.Image != null)
            {
                s.Height = Math.Max(s.Height, button.Image.Height);
                s.Width += button.Image.Width;
            }


            s.Width += System.Windows.Forms.SystemInformation.Border3DSize.Height * 2;
            s.Height += System.Windows.Forms.SystemInformation.Border3DSize.Width * 2;

            s.Width += 12;
            s.Height += 6;

            return WinFormsDriver.ConvertSize(s);
        }


        public override object NativeObject { get { return button; } }


        public override bool Flat
        {
            get
            {
                //return button.FlatStyle == System.Windows.Forms.FlatStyle.Flat;
                return button.Flat;
            }
            set
            {
                //button.FlatStyle = value ? System.Windows.Forms.FlatStyle.Flat : System.Windows.Forms.FlatStyle.Standard;
                button.Flat = value;

            }
        }

        public override bool CanFocus
        {
            get
            {
                return button.FocusEnabled;
            }
            set
            {
                //button.CanFocus = value;

                button.FocusEnabled = value;
            }
        }

        public override Image Image
        {
            set
            {
                var i=WinFormsDriver.ImageToWinFormsImage(value);
                button.Image = i;
                if (i == null) //hack to fix windows forms stupid imgae-handling for buttons
                    button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                else
                    button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            }
        }

        public override bool Enabled
        {
            get
            {
                return button.Enabled;
            }
            set
            {
                button.Enabled = value;
            }
        }


        private class CustomButton : System.Windows.Forms.Button
        {

            bool flat = false;

            public CustomButton()
            {
                FlatAppearance.BorderSize = 0;
            }


            public bool Flat
            {
                get
                {
                    return flat;
                }
                set
                {
                    FlatStyle = value ? System.Windows.Forms.FlatStyle.Flat : System.Windows.Forms.FlatStyle.Standard;
                    flat = value;
                }
            }



            protected override void OnMouseEnter(EventArgs e)
            {
                if (flat)
                    FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                if (flat)
                    FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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

        /// <summary>
        /// Gets or sets if this is the 'default' button in the window, ie responds on enter key and has
        /// a 'fatter' look. The button have a window as parent or grandparent for this to work.
        /// </summary>
        public override bool Default
        {
            get
            {
                Window win = ShellObject.Window;
                if (win != null)
                {
                    return (win.DriverWindow.NativeObject as System.Windows.Forms.Form).AcceptButton == button;
                }
                return false;
            }
            set
            {
                Window win = ShellObject.Window;
                if (win != null)
                {
                    (win.DriverWindow.NativeObject as System.Windows.Forms.Form).AcceptButton = value ? button : null;
                }
            }
        }

    }


}
