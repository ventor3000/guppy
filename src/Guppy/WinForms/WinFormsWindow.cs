using System;
using System.Windows.Forms;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{

    public class CustomForm : Form
    {
        Window shellobject;

        public CustomForm(Window shellobj)
        {
            this.shellobject = shellobj;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            //we use this raw function catch tab key down etc
            KeyData kcode = WinFormsKeys.DecodeKey(keyData);
            if (kcode.KeyCode == KeyCode.Unknown)
                return base.ProcessCmdKey(ref msg, keyData);  //dont send unknown keys

            char oldchar=kcode.Char; //remember to check if user changed key stroke

            bool block = ((Window)shellobject).OnRawKeyDown(new GuppyKeyArgs(shellobject, kcode));
            if (block) return true;

           

            return base.ProcessCmdKey(ref msg, keyData);
        }

       

        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            if (m.Msg == 0x104) //WM_SYSKEYDOWN
                return base.ProcessKeyEventArgs(ref m);

            Keys keyData = Keys.None;

            if (m.Msg == 0x101)
            { //WM_KEYUP

                keyData = (Keys)m.WParam;
            }
            else if (m.Msg == 0x105)
            { //WM_SYSKEYUP ie. key up for alt held
                keyData = (Keys)m.WParam;
            }

            if (keyData != Keys.None) //we got a key!
            {

                //add modifier keys to keycode
                Keys mods = Control.ModifierKeys;
                if ((mods & Keys.Shift) != 0) keyData |= (Keys)0x10000;
                if ((mods & Keys.Control) != 0) keyData |= (Keys)0x20000;
                if ((mods & Keys.Alt) != 0) keyData |= (Keys)0x40000;
                

            

                KeyData kcode = WinFormsKeys.DecodeKey(keyData);
                if (kcode.KeyCode != KeyCode.Unknown)
                {
                    bool block = ((Window)shellobject).OnRawKeyUp(new GuppyKeyArgs(shellobject, kcode));
                    if (block) return true;
                }
            }

            return base.ProcessKeyEventArgs(ref m);
        }

        
      
    }

    public class WinFormsWindow : DriverWindow
    {
        CustomForm form;

        public WinFormsWindow(Widget shellobject, string caption)
            : base(shellobject)
        {
            form = new CustomForm(shellobject as Window);
            form.Text = caption;

            form.KeyPreview = true;
            form.StartPosition = FormStartPosition.Manual;


            //Set events (note keyboard handling in CustomForm
            form.Resize += new EventHandler(EventResize);
            form.FormClosed += delegate { ((Window)ShellObject).OnClosed(); };
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            //form.FormClosed += new FormClosedEventHandler(form_FormClosed);

            form.Load += delegate { ((Window)ShellObject).OnShowing(); };
            form.Shown += delegate { ((Window)ShellObject).OnShowed(); };

            if (WinFormsDriver.DefaultIcon != null)
                form.Icon = WinFormsDriver.DefaultIcon;
        }



        void form_FormClosing(object sender, FormClosingEventArgs e)
        {

            bool block = ((Window)ShellObject).OnClosing();
            if (block) { e.Cancel = true; return; }

            if (form.Modal)
                return; //use default behaviour for modal winforms

            //avoid normal close which will dispose forms which are not shown modally
            if (AutoDispose)
            {
                e.Cancel = false;
            }
            else
            { 
                //check if the mainform is still open

                if (e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason==CloseReason.WindowsShutDown)
                    return; //allow close on application exit
                
                e.Cancel = true;
                form.Hide();
            }


        }

        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Window)ShellObject).OnClosed();
        }

        void EventResize(object sender, EventArgs e)
        {
            ((Window)ShellObject).OnResized();
        }

        public override string Caption
        {
            get
            {
                return form.Text;
            }
            set
            {
                form.Text = value;
            }
        }

        public override Size2i GetNaturalSize()
        {
            throw new Exception("Natural size should not be called for composite objects");
        }

        public override void Show()
        {
            Response = null;
            SolveWindowPosition(Form.ActiveForm);
            form.Show();
        }

        public override void Append(DriverWidget dw)
        {
            Control ctrl = dw.NativeObject as Control;
            if (ctrl != null)
                form.Controls.Add(ctrl);
        }

        public override object NativeObject { get { return form; } }

        public override Size2i ClientSize
        {
            get
            {
                return WinFormsDriver.ConvertSize(form.ClientSize);
            }
            set
            {
                form.ClientSize = WinFormsDriver.ConvertSize(value);
            }
        }





        private void SolveWindowPosition(Form parent)
        {


            PositionMode wp = (ShellObject as Window).PositionMode;
            //if center parent and parent is null, center screen instead
            if (parent == null && wp == PositionMode.CenterParent)
                wp = PositionMode.CenterScreen;

            switch (wp)
            {
                case PositionMode.Manual:
                    break; //done already
                case PositionMode.CenterParent:
                    form.Left = parent.Left + parent.Width / 2 - form.Width / 2;
                    form.Top = parent.Top + parent.Height / 2 - form.Height / 2;
                    break;
                case PositionMode.CenterScreen:
                    {
                        Screen scr = Screen.FromControl(form);
                        if (scr == null)
                            scr = Screen.PrimaryScreen;
                        var bounds = scr.Bounds;
                        form.Left = bounds.Width / 2 - form.Width / 2;
                        form.Top = bounds.Height / 2 - form.Height / 2;
                        break;
                    }
                case PositionMode.MouseCursor:
                    form.Left = Cursor.Position.X;
                    form.Top = Cursor.Position.Y;
                    break;
            }
        }

        public override object ShowModal()
        {
            Response = null;

            SolveWindowPosition(Form.ActiveForm);
            
            form.ShowDialog();
            
            return Response;
        }

        public override Size2i Size
        {
            get { return WinFormsDriver.ConvertSize(form.Size); }
            set { form.Size = WinFormsDriver.ConvertSize(value); }
        }



        public override void Close(object response)
        {
            this.Response = response;
            form.Close();
        }


        public override void SetMinSize(int width, int height)
        {
            form.MinimumSize = new System.Drawing.Size(width, height);
        }

        public override Margin GetDecorationSize()
        {

            //the formula to use here was a trial-and-catch programming
            //but tried in multiple resolutions with diffrent DPI:s so
            //it's assumed to be correct

            int xsiz, ysiz;

            if (form.FormBorderStyle == FormBorderStyle.Sizable)
            {
                xsiz = SystemInformation.HorizontalResizeBorderThickness;
                ysiz = SystemInformation.VerticalResizeBorderThickness;
            }
            else
            {
                xsiz = SystemInformation.FixedFrameBorderSize.Width;
                ysiz = SystemInformation.FixedFrameBorderSize.Height;
            }

            return new Margin(
              xsiz,
              SystemInformation.CaptionHeight + ysiz,
              xsiz,
              ysiz
            );
        }

        public override DecorationFlags DecorationFlags
        {
            get
            {
                DecorationFlags res = 0;
                if (form.FormBorderStyle == FormBorderStyle.Sizable)
                    res |= DecorationFlags.Resizable;

                if (form.MaximizeBox)
                    res |= DecorationFlags.MaxButton;
                if (form.MinimizeBox)
                    res |= DecorationFlags.MinButton;

                if (form.ShowInTaskbar)
                    res |= DecorationFlags.TaskBarButton;
                if (form.ShowIcon)
                    res |= DecorationFlags.Icon;
                return res;
            }
            set
            {
                form.MaximizeBox = (value & DecorationFlags.MaxButton) != 0;
                form.MinimizeBox = (value & DecorationFlags.MinButton) != 0;
                form.FormBorderStyle = (value & DecorationFlags.Resizable) != 0 ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog;
                form.ShowIcon = (value & DecorationFlags.Icon) != 0 ? true : false;
                form.ShowInTaskbar = (value & DecorationFlags.TaskBarButton) != 0 ? true : false;

            }
        }


        Image Icon
        {
            set
            {
                var i = WinFormsDriver.ImageToWinFormsImage(value);
                if (i != null)
                    WinFormsUtils.MakeIcon(i, 16, true);

            }
        }



        public override void Maximize()
        {
            form.WindowState = FormWindowState.Maximized;
        }

        public override void Minimize()
        {
            form.WindowState = FormWindowState.Minimized;
        }

        public override Point2i Position
        {
            get
            {
                return new Point2i(form.Left, form.Top);
            }
            set
            {
                form.Location = new System.Drawing.Point(value.X, value.Y);
            }
        }

    }


}
