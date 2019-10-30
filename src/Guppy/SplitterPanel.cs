
using GuppyGUI.AbstractDriver;
namespace GuppyGUI
{
  public class SplitterPanel:CompositeWidget
  {

    internal SplitterPanel(CompositeWidget parent,object driverobject)//special class cannot be constructed from outside thus internal
    {
      AttachDriverObject(parent,Guppy.Driver.CreateSplitterPanel(this,driverobject)); 
    }

    public DriverSplitterPanel DriverPanel {get {return DriverObject as DriverSplitterPanel;}}

    /*public Size2i ClientSize { get { return DriverPanel.ClientSize; } }*/

    virtual public void Update()
    {
        Size2i s = DriverPanel.Size;
        bool anyexpandx = false, anyexpandy = false;
        CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
        CalcPositionsRecursive(0, 0, s.Width, s.Height);
    }

  }
}
