
namespace GuppyGUI.AbstractDriver
{
    public abstract class DriverSplitter : DriverCompositeWidget
    {

        protected SplitterPanel child1;
        protected SplitterPanel child2;

        public DriverSplitter(Widget shellobject)
            : base(shellobject)
        {

        }

        public SplitterPanel Child1 { get { return child1; } }
        public SplitterPanel Child2 { get { return child2; } }

        public abstract int SplitterWidth { get; set; }
    }

    public abstract class DriverSplitterPanel : DriverCompositeWidget
    {
        public DriverSplitterPanel(Widget shellobject)
            : base(shellobject)
        {

        }

        //public abstract Size2i ClientSize { get; }
        public abstract Size2i Size { get; }

    }
}
