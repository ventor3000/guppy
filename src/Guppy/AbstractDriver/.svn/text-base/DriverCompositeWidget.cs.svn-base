using System;

namespace GuppyGUI.AbstractDriver
{
	public abstract class DriverCompositeWidget : DriverWidget
	{
		public DriverCompositeWidget (Widget shellobject) : base(shellobject) { }

		public abstract void Append (DriverWidget dw);
        
		public virtual Margin GetDecorationSize () {return Margin.Empty;}

		public virtual Point2i GetClientOrigin () {return Point2i.Empty;} //default origin is 0,0

		public override Size2i GetNaturalSize ()
		{
			throw new Exception ("GetNaturalSize should not be used for composite objects");
		}

        public virtual void Detach(DriverWidget child) {
            Guppy.Driver.DefaultDetach(NativeObject, child.NativeObject);
        }
	}
}

