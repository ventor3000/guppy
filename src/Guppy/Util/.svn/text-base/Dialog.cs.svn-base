
namespace GuppyGUI.Util
{
  public class Dialog:Window
  {
    public readonly TableLayout Contents;
    public readonly TableLayout Buttons;

    public Dialog(string caption, params string[] button_response):base(caption)
    {
      DecorationFlags = DecorationFlags.DialogFrame;
      Margin = new Margin(Guppy.DefaultMargin);

      Contents = new TableLayout(this);
      new Separator(this, false);


      Buttons = new TableLayout(this) { Vertical = false,ChildrenUniformWidth=true };
      new Fill(Buttons) { ExpandX = true,UniformWidth=false };

      for (int l = 0; l < button_response.Length; l += 2)
      {
        Button b = new Button(Buttons, button_response[l]);
        b.Response = button_response[l + 1];
      }
      new Fill(Buttons) { ExpandX = true,UniformWidth=false };
    }

    internal override bool OnRawKeyDown(GuppyKeyArgs e)
    {
        if (e.Key.KeyCode == KeyCode.Escape)
        {
            Close("");
            return true;
        }
        else
            return base.OnRawKeyDown(e);
    }

      
   /* public override void Append(Widget w)
    {
      if(w!=null && Table!=null)
        Table.Append(w); //so that controls create with htis as parent is put in the table
    }*/

    
  }
}
