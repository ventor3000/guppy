using GuppyGUI.AbstractDriver;

namespace GuppyGUI.GtkSharp
{
  public class GtkSharpSeparator:DriverSeparator
  {
    Gtk.Separator sep;

    public GtkSharpSeparator(Widget shellobject, bool vertical)
      : base(shellobject)
    {
      if (vertical)
        sep = new Gtk.VSeparator();
      else
        sep = new Gtk.HSeparator();

      GtkSharpDriver.InitWidget(sep, shellobject);

      sep.Show();
    }



   
    public override object NativeObject
    {
      get { return sep; }
    }

    public override Size2i GetNaturalSize()
    {
      var res=GtkSharpDriver.DefaultGetNaturalSize(sep);
      return res;
    }


  }
}
