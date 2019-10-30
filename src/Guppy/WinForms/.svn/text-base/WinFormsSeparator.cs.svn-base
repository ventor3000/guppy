using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
  public class WinFormsSeparator:DriverSeparator
  {
    System.Windows.Forms.Label seplabel;
    public readonly bool vertical;
    const int septhickness = 3;

    public WinFormsSeparator(Widget shellobject, bool vertical)
      : base(shellobject)
    {

      this.vertical = vertical;

      seplabel = new System.Windows.Forms.Label();
      seplabel.Tag = shellobject; //map-back from native control to guppy object

      seplabel.AutoSize = false;
      seplabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      if (vertical)
      {
        seplabel.Width = septhickness;
        seplabel.Height = 16;
      }
      else
      {
        seplabel.Height = septhickness;
        seplabel.Width = 16;
      }

      
    }
    

    
    public override object NativeObject
    {
      get { return seplabel; }
    }

    public override Size2i GetNaturalSize()
    {
      if (vertical)
        return WinFormsDriver.ConvertSize(new System.Drawing.Size(septhickness, 16));
      else
        return WinFormsDriver.ConvertSize(new System.Drawing.Size(16, septhickness));
    }

    public override void Place(int x, int y, int w, int h)
    {
      if (vertical)
        seplabel.SetBounds(x, y, septhickness, h);
      else
        seplabel.SetBounds(x, y, w, septhickness);
    }

  }
}
