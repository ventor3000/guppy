
namespace GuppyGUI.AbstractDriver
{
  public abstract class DriverWindow : DriverCompositeWidget
  {

    public DriverWindow(Widget shellobject)
      : base(shellobject)
    {
      AutoDispose = true;
    }

    public abstract Size2i ClientSize { get; set; }
    public abstract Size2i Size { get; set; }
    public abstract object ShowModal();
    public abstract void Show();
    public abstract void Close(object response);
    
    public bool AutoDispose { get; set; } //if this window should be automatically disposed when closed (only when not modal)
    protected object Response { get; set; } //showmodal response etc.
    public abstract void SetMinSize(int width, int height);
    public abstract string Caption { get; set; }
    public abstract DecorationFlags DecorationFlags{get;set;}
    public abstract void Maximize();
    public abstract void Minimize();
    public abstract Point2i Position { get; set; }

  }
}
