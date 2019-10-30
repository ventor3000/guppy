
using System.IO;
namespace GuppyGUI.AbstractDriver
{
    public abstract class Driver
    {

        public abstract void Open();
        public abstract void Run();
        public abstract void Quit();

        public abstract void Message(string title, string msg);
        public abstract Widget Focus { get; set; }


        public abstract DriverToggle CreateToggle(Widget shellobject, string caption, bool isbutton);
        public abstract DriverChoice CreateChoice(Widget shellobject, params object[] entries);
        public abstract DriverSeparator CreateSeparator(Widget shellobject, bool vertical);
        public abstract DriverWindow CreateWindow(Widget shellobject, string caption);
        public abstract DriverButton CreateButton(Widget shellobject, string caption);
        public abstract DriverLabel CreateLabel(Widget shellobject, string caption);
        public abstract DriverEdit CreateEdit(Widget shellobject);
        public abstract DriverFrame CreateFrame(Widget shellobject, string caption,bool border);
        public abstract DriverImage CreateImage(Stream src);
        public abstract DriverImageLabel CreateSlide(Widget shellobject, Image image);
        public abstract DriverListBox CreateListBox(Widget shellobject, params object[] items);
        public abstract DriverMemo CreateMemo(Widget shellobject);
        public abstract DriverSplitter CreateSplitter(Widget shellobject, bool vertical);
        public abstract DriverSplitterPanel CreateSplitterPanel(Widget shellobject,object driverobject);
        public abstract DriverRadioButton CreateRadioButton(Widget shellobject, string caption);
        public abstract DriverProgressBar CreateProgressBar(Widget shellobject);
        public abstract DriverValuator CreateValuator(Widget shellobject,bool vertical);
        public abstract DriverTabs CreateTabs(Widget shellobject);
        public abstract DriverTabPage CreateTabPage(Widget shellobject, string caption);
        public abstract DriverPopupMenu CreatePopupMenu(Widget shellobject);
        public abstract DriverMenuItem CreateMenuItem(Widget shellobject,string Caption,Image image,MenuFlags flags);
        public abstract DriverChoiceEdit CreateChoiceEdit(Widget shellobject, params object[] items);

        public abstract void Wait(bool block);
        

        

        #region DEFAULT_BEHAVIOUR
        public abstract void DefaultPlace(object target, int x, int y, int w, int h);
        public abstract void DefaultSetTooltip(object target,string tiptext);
        public abstract bool DefaultGetVisible(object target);
        public abstract void DefaultSetVisible(object target,bool isivisible);
        public abstract bool DefaultGetEnabled(object target);
        public abstract void DefaultSetEnabled(object target, bool isivisible);
        public abstract void DefaultDetach(object target, object child);
        public abstract void DefaultDispose(object target);
        #endregion
    }
}
