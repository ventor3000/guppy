
using GuppyGUI.AbstractDriver;
namespace GuppyGUI.WinForms
{
  public class WinFormsImageLabel:DriverImageLabel
  {
    System.Windows.Forms.Label label;

    public WinFormsImageLabel(Widget shellobject, Image img):base(shellobject)
    {
      label = new System.Windows.Forms.Label();
      label.Tag = shellobject; //map-back from native control to guppy object

      label.Image = WinFormsDriver.ImageToWinFormsImage(img);
    }

    public override Image Image
    {
      set { label.Image=WinFormsDriver.ImageToWinFormsImage(value); }
    }


    public override object NativeObject
    {
      get { return label; }
    }

    public override Size2i GetNaturalSize()
    {
      if (label.Image == null)
        return new Size2i(16, 16);
      else
        return new Size2i(label.Image.Width, label.Image.Height);
    }


  }
}
