using GuppyGUI.AbstractDriver;

namespace GuppyGUI.GtkSharp
{
  public class GtkSharpImage:DriverImage
  {
   // Gtk.Image image;
    internal Gdk.Pixbuf pixbuf;
    

    public GtkSharpImage(System.IO.Stream source)
    {
      pixbuf=new Gdk.Pixbuf(source);
    }


    public override object NativeObject
    {
      get { return pixbuf; }
    }

    public override int Height
    {
      get {
        return pixbuf.Height;
      }
    }

    public override int Width
    {
      get {
        return pixbuf.Height;
      }
    }

    public Gtk.Image CreateGtkImage()
    {
      var res=new Gtk.Image(pixbuf);
      res.Show();
      return res;
    }
  }
}
