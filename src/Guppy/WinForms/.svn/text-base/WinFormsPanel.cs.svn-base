using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
  public class WinFormsPanel:DriverCompositeWidget
  {
    System.Windows.Forms.Panel panel;

    internal WinFormsPanel(Widget shellobject):base(shellobject)
    {
      panel = new System.Windows.Forms.Panel();
      panel.Tag = shellobject; //map-back from native control to guppy object
    }
    
   

    public override void Append(DriverWidget dw)
    {
      System.Windows.Forms.Control ctrl = dw.NativeObject as System.Windows.Forms.Control;
      if (ctrl != null)
        panel.Controls.Add(ctrl);
    }


    public override object NativeObject
    {
      get { return panel; }
    }


    
  }
}
