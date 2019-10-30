using GuppyGUI.AbstractDriver;

namespace GuppyGUI.GtkSharp
{
	/// <summary>
	/// Description of GtkSharpLabel.
	/// </summary>
	public class GtkSharpLabel:DriverLabel
	{
		Gtk.Label label;
		
		public GtkSharpLabel(Widget shellobject,string caption):base(shellobject)
		{
			label=new Gtk.Label(caption);
            GtkSharpDriver.InitWidget(label, shellobject);
			label.Show();
			label.SetAlignment(0.0f,0.5f);
		}

    public override string Caption
    {
			get {
				return label.Text;
			}
			set {
				label.Text=value;
			}
		}
		
		public override object NativeObject {
			get {
				return label;
			}
		}
		
		
		public override Size2i GetNaturalSize()
		{
			return GtkSharpDriver.DefaultGetNaturalSize(label);
		}
	
		

	}
}
