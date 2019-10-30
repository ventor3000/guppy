
namespace GuppyGUI.AbstractDriver
{
  public abstract class DriverImageLabel:DriverWidget
  {
    public DriverImageLabel(Widget shellobject)
      : base(shellobject)
    {
    }

    public abstract Image Image {set;}
    
  }
}
