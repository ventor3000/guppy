
namespace GuppyGUI
{
    public class Separator : Widget
    {
        public Separator(CompositeWidget parent, bool vertical)
        {
            if (vertical)
                ExpandY = true;
            else
                ExpandX = true;
 
            AttachDriverObject(parent, Guppy.Driver.CreateSeparator(this, vertical));


        }
    }
}
