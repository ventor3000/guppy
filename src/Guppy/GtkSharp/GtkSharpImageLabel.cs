using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.GtkSharp
{
  class GtkSharpImageLabel:DriverImageLabel
  {
    Gtk.Image image;
    

    public GtkSharpImageLabel(Widget shellobject, Image img)
      : base(shellobject)
    {
     // this.image = GtkSharpDriver.ImageToGtkImage(img);
      Gdk.Pixbuf pb = GtkSharpDriver.ImageToGtkPixbuf(img);
      this.image = new Gtk.Image(pb);

      GtkSharpDriver.InitWidget(this.image, shellobject);

      this.image.Show();
    }

    public override Image Image
    {
      set {
        Gdk.Pixbuf pb=GtkSharpDriver.ImageToGtkPixbuf(value);
        image.Pixbuf = pb;
      }

    }


    public override object NativeObject
    {
      get { return image; }
    }

    public override Size2i GetNaturalSize()
    {
      if (image.Pixbuf == null)
        return new Size2i(16, 16);
      return GtkSharpDriver.DefaultGetNaturalSize(image);
    }


  }
}
