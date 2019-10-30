using System;

namespace GuppyEx.Util
{
    public class LabeledEdit:HBox
    {
        public LabeledEdit(CompositeWidget parent, string caption):base(parent)
        {
            Gap = 0;
            new Fill(this) { ExpandX = true };
            new Label(this, caption) { ExpandY = true };
            new Edit(this);
        }
    }
}
