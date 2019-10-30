using System.Windows.Forms;
using GuppyGUI.AbstractDriver;
using System.Collections.Generic;
using System.Drawing;
using System;


namespace GuppyGUI.WinForms
{


    public class WinFormsDriver : Driver
    {
        //IMPORTANT: do not create any static windows forms specific objects
        //to avoid collisions with other drivers than winforms
        internal static Icon DefaultIcon = null;
        internal static ToolTip ToolTip = null;
        

        public override void Open()
        {

          

            
            
            if (!Application.MessageLoop)
            {

                //no message loop=> assume we are starting a new application
                //otherwise we assume we are extending another application and do not bother fiddling
                //with the application
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            }

            //Grab the default guppy icon
            if (DefaultIcon == null)
            {
                var i = ImageToWinFormsImage(Image.FromResource("guppyicon.png"));
                if (i != null)
                    DefaultIcon = WinFormsUtils.MakeIcon(i, 48, true);
            }

        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Guppy.Message("UNHANDLED EXCEPTION", "An unhandled fatal exception occured in the program:" + e.Exception.ToString());

        }

        public override void Run()
        {
            Application.Run();
        }

        public override void Quit()
        { //called when the mainform is closed
            Application.Exit();
        }

        public override void Message(string title, string msg)
        {
            MessageBox.Show(msg, title);
        }

        public override DriverToggle CreateToggle(Widget shellobject, string caption, bool isbutton)
        {
            return new WinFormsToggle(shellobject, caption, isbutton);
        }


        public override DriverChoice CreateChoice(Widget shellobject, params object[] entries)
        {
            return new WinFormsChoice(shellobject, entries);

        }

        public override DriverSeparator CreateSeparator(Widget shellobject, bool vertical)
        {
            return new WinFormsSeparator(shellobject, vertical);

        }


        public override DriverWindow CreateWindow(Widget shellobject, string caption)
        {
            return new WinFormsWindow(shellobject, caption);
        }

        public override DriverButton CreateButton(Widget shellobject, string caption)
        {
            return new WinFormsButton(shellobject, caption);
        }

        public override DriverLabel CreateLabel(Widget shellobject, string caption)
        {
            return new WinFormsLabel(shellobject, caption);
        }

        public override DriverEdit CreateEdit(Widget shellobject)
        {
            return new WinFormsEdit(shellobject);
        }

        public override DriverFrame CreateFrame(Widget shellobject, string caption,bool border)
        {
            return new WinFormsFrame(shellobject, caption,border);
        }

        internal static GuppyGUI.Size2i ConvertSize(System.Drawing.Size siz)
        {
            return new GuppyGUI.Size2i(siz.Width, siz.Height);
        }

        internal static System.Drawing.Size ConvertSize(GuppyGUI.Size2i siz)
        {
            return new System.Drawing.Size(siz.Width, siz.Height);
        }

        public override DriverImage CreateImage(System.IO.Stream src)
        {
            return new WinFormsImage(src);
        }


        internal static System.Drawing.Image ImageToWinFormsImage(Image value)
        {
            if (value == null)
                return null;
            else
            {
                WinFormsImage wi = value.DriverObject as WinFormsImage;
                if (wi != null)
                {
                    System.Drawing.Image img = wi.NativeObject as System.Drawing.Image;
                    if (img != null)
                    {
                        return img;
                    }
                }
            }

            return null;
        }

        public override DriverImageLabel CreateSlide(Widget shellobject, Image image)
        {
            return new WinFormsImageLabel(shellobject, image);
        }

        public override DriverListBox CreateListBox(Widget shellobject, params object[] items)
        {
            return new WinFormsListBox(shellobject, items);
        }

        public override DriverMemo CreateMemo(Widget shellobject)
        {
            return new WinFormsMemo(shellobject);
        }

        public override DriverSplitter CreateSplitter(Widget shellobject, bool vertical)
        {
            return new WinFormsSplitter(shellobject, vertical);
        }

        public override DriverSplitterPanel CreateSplitterPanel(Widget shellobject,object driverobject)
        {
          return new WinFormsSplitterPanel(shellobject,driverobject);
        }

        public override DriverRadioButton CreateRadioButton(Widget shellobject, string caption)
        {
            return new WinFormsRadioButton(shellobject, caption);
        }

        public override DriverProgressBar CreateProgressBar(Widget shellobject)
        {
            return new WinFormsProgressBar(shellobject);
        }

        public override DriverValuator CreateValuator(Widget shellobject,bool vertical)
        {
            return new WinFormsValuator(shellobject, vertical);
        }


        public override DriverTabs CreateTabs(Widget shellobject)
        {
            return new WinFormsTabs(shellobject);
        }

        public override DriverTabPage CreateTabPage(Widget shellobject, string caption)
        {
            return new WinFormsTabPage(shellobject, caption);
        }

        public override DriverPopupMenu CreatePopupMenu(Widget shellobject)
        {
            return new WinFormsPopupMenu(shellobject);
        }

        public override DriverMenuItem CreateMenuItem(Widget shellobject,string caption,Image image,MenuFlags flags)
        {
            return new WinFormsMenuItem(shellobject, caption, image, flags);
        }

        public override DriverChoiceEdit CreateChoiceEdit(Widget shellobject, params object[] items)
        {
            return new WinFormsChoiceEdit(shellobject, items);
        }
        
        public override Widget Focus
        {
            get
            {
                Form f = Form.ActiveForm; //note: stepping over this line in debug mode returns null, M$ bug???
                if (f != null)
                {
                    Control c = f.ActiveControl;
                    if (c != null)
                    {
                        
                        Widget w = c.Tag as Widget;
                        if (w != null)
                            return w;
                    }
                }

                return null;
            }
            set
            {
                if (value == null)
                    return;
                var dw = value.DriverObject as DriverWidget;
                if (dw != null)
                {
                    Control ctrl = dw.NativeObject as Control;
                    if (ctrl != null)
                    {
                        var win = value.Window;
                        if (win != null)
                        {
                            Form form=win.DriverWindow.NativeObject as Form;
                            form.ActiveControl = ctrl;
                            ctrl.Focus();
                        }
                    }
                }
            }
        }


        public override void Wait(bool block)
        {
            GuppyGUI.WindowsSpecific.WinAPI.HandleMessage(block);
        }

        
                
                

    

        #region DEFAULT_BEHAVIOUR

        public override void DefaultSetTooltip(object target, string tiptext)
        {
            //this is the function that sets tooltip for all winforms controls unless
            //overridden in base class
            if (ToolTip == null)
                ToolTip = new ToolTip();

            Control ctrl = target as Control;
            if (ctrl != null)
                ToolTip.SetToolTip(ctrl, tiptext);
        }

        public override bool DefaultGetVisible(object target)
        {
            Control ctrl = target as Control;
            if (ctrl != null)
                return ctrl.Visible;

            throw new Exception("DefaultGetVisible not supported for the sent object in WinForms driver");
        }

        public override void DefaultSetVisible(object target, bool isivisible)
        {
            Control ctrl = target as Control;
            if (ctrl != null)
            {
                ctrl.Visible = isivisible;
                return;
            }

            throw new Exception("DefaultSetVisible not supported for the sent object in WinForms driver");
        }

        public override bool DefaultGetEnabled(object target)
        {
            Control ctrl = target as Control;
            if (ctrl != null)
                return ctrl.Enabled;

            throw new Exception("DefaultGetEnabled not supported for the sent object in WinForms driver");
        }

        public override void DefaultSetEnabled(object target, bool isivisible)
        {
            Control ctrl = target as Control;
            if (ctrl != null)
            {
                ctrl.Enabled = isivisible;
                return;
            }

            throw new Exception("DefaultSetEnabled not supported for the sent object in WinForms driver");
        }

        public override void DefaultPlace(object target, int x, int y, int w, int h)
        {
             Control ctrl = target as Control;
             if (ctrl != null)
             {
                 ctrl.SetBounds(x, y, w, h);
             }
        }

        public override void DefaultDetach(object target, object child)
        {
            Control ctrl = target as Control;
            if (ctrl != null)
            {
                Control wc = child as Control;
                if (wc != null)
                {
                    ctrl.Controls.Remove(wc);
                }
                else
                {
                    throw new Exception("Internal error: Default detach->item is not of default type for driver");

                }
            }
        }

        public override void DefaultDispose(object target) {
            IDisposable disp=target as IDisposable;
            if(disp!=null)
                disp.Dispose();
        }
        

        #endregion
    }

    
    


}
