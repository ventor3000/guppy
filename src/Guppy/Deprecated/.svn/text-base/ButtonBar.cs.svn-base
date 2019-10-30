using System;

namespace GuppyEx.Util
{
    public class ButtonBar:HBox
    {
        public ButtonBar(CompositeWidget parent,params string[] buttons):base(parent)
        {
          new Fill(this) { ExpandX = true };
            foreach (string str in buttons)
            {
                new Button(this, str) { UniformWidth = true };
            }
            new Fill(this);
        }
        
    }
}
