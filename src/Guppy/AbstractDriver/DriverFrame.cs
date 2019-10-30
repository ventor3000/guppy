
namespace GuppyGUI.AbstractDriver
{
	
	public abstract class DriverFrame:DriverCompositeWidget
	{
		public DriverFrame(Widget shellobject):base(shellobject)
		{
			
		}

    public abstract string Caption { get; set; }
	}
}
