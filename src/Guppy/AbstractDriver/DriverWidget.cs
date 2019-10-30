
using System;
namespace GuppyGUI.AbstractDriver
{
    public abstract class DriverWidget:IDisposable
    {
        protected Widget ShellObject;

        public DriverWidget(Widget shellobject)
        {
            this.ShellObject = shellobject;
        }

        public abstract object NativeObject { get; }

        public abstract Size2i GetNaturalSize();

        

        public virtual string Tip {
            set {Guppy.Driver.DefaultSetTooltip(NativeObject,value);}
        }
        public virtual bool Visible
        {
            get { return Guppy.Driver.DefaultGetVisible(NativeObject); }
            set { Guppy.Driver.DefaultSetVisible(NativeObject, value); }
        }

        public virtual bool Enabled
        {
            get { return Guppy.Driver.DefaultGetEnabled(NativeObject); }
            set { Guppy.Driver.DefaultSetEnabled(NativeObject, value); }
        }

        public virtual void Place(int x, int y, int w, int h) {
            Guppy.Driver.DefaultPlace(NativeObject, x, y, w, h); 
        }



        public void Dispose()
        {
            Guppy.Driver.DefaultDispose(NativeObject);
        }
    }
}
