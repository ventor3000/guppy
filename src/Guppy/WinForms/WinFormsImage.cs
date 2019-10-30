using System.IO;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
  internal class WinFormsImage:DriverImage
  {
    System.Drawing.Image image;

    public WinFormsImage(Stream src)
    {
      image=System.Drawing.Image.FromStream(src);
    }

    public override int Width
    {
      get { return image.Width; }
    }

    public override int Height
    {
      get { return image.Height; }
    }


    public override object NativeObject
    {
      get { return image; }
    }
    


  }
}
