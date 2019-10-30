
namespace GuppyGUI.AbstractDriver
{
    public abstract class DriverToggle:DriverWidget
    {
        
        public DriverToggle(Widget shellobject)
            : base(shellobject)
        {
        }

        public abstract bool Checked { get; set; }
        public abstract string Caption { get; set; }
        public abstract bool CanFocus { get; set; }
        public abstract Image Image { set; }
    }
}
