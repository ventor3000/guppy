
namespace GuppyGUI.AbstractDriver
{
    public abstract class DriverButton : DriverWidget
    {
        public DriverButton(Widget shellobject)
            : base(shellobject)
        {
        }

        public abstract bool Flat { get; set; }

        public abstract bool CanFocus { get; set; }
        public abstract string Caption { get; set; }
        public abstract Image Image { set; }

        public abstract bool Default { get; set; }


    }
}
