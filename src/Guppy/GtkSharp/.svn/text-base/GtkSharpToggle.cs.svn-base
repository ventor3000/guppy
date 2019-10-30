using GuppyGUI.AbstractDriver;

namespace GuppyGUI.GtkSharp
{
	public class GtkSharpToggle:DriverToggle
	{
		Gtk.ToggleButton togglebtn;
		private bool sendclickevent = true;  //used to get rid of click event when setting checked in code

		public GtkSharpToggle(Widget shellobject, string caption,bool isbutton)
			: base(shellobject)
		{

			if (isbutton)
				togglebtn = new Gtk.ToggleButton(caption);
			else //checkbox
				togglebtn = new Gtk.CheckButton(caption);

            GtkSharpDriver.InitWidget(togglebtn, shellobject);

			togglebtn.Show();

			togglebtn.Clicked += delegate { if(sendclickevent) ((Toggle)shellobject).OnChanged(); };
		}


		public override string Caption
		{
			get
			{
				return togglebtn.Label;
			}
			set
			{
				togglebtn.Label = value;
			}
		}

		public override object NativeObject
		{
			get { return togglebtn; }
		}

		public override Size2i GetNaturalSize()
		{
			return GtkSharpDriver.DefaultGetNaturalSize(togglebtn);
		}



		public override bool Checked
		{
			get
			{
				return togglebtn.Active;
			}
			set
			{
				try
				{
					sendclickevent = false;
					togglebtn.Active = value;
				}
				finally
				{
					sendclickevent = true;
				}
			}
		}

        public override bool Enabled
        {
            get
            {
                return togglebtn.Active;
            }
            set
            {
                togglebtn.Active = value;
            }
        }


        public override bool CanFocus
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override Image Image
        {
            set
            {
                togglebtn.Image = GtkSharpDriver.ImageToGtkImage(value);
            }
        }
		
	}
}
