
namespace GuppyGUI.Util
{
  public class ButtonDialog:Dialog
  {
    public ButtonDialog(string title, string message, params string[] button_response):base(title,button_response)
    {
      Contents.Margin = new Margin(16);
      new Label(Contents, message);
    }

    
    
  }
}
