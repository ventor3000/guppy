
using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{
  public class ImageLabel : Widget
  {
    private Image image;

    public ImageLabel(CompositeWidget parent, Image img)
    {
      AttachDriverObject(parent, Guppy.Driver.CreateSlide(this, img));
      this.image = img;
    }

    virtual public Image Image
    {
      set
      {
        DriverLabel.Image = value;
      }
    }

    private DriverImageLabel DriverLabel
    {
      get { return DriverObject as DriverImageLabel; }
    }

  }
}
