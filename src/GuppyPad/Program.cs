using GuppyGUI;

namespace GuppyPad
{
    static class Program
    {
        static TableLayout toolbar;
        static Label statustext;
        static Memo editor;

        static Button AddToolButton(string resname,string tip)
        {
            Image img = Image.FromResource(resname);
            return new Button(toolbar, "") { Image = img, Flat = true, CanFocus = false,Tip=tip };
        }

        public static void Main()
        {
            Guppy.Open(DriverMode.WinForms);

            Window mainwin = new Window("Guppy-Pad");
            mainwin.Maximize();
            toolbar = new TableLayout(mainwin) { Vertical = false, Gap = 0, ExpandY = false };
            var btn = AddToolButton("document-new.png","New");

            btn.EvClicked += delegate { editor.Clear(); };

            AddToolButton("document-open.png","Open");
            AddToolButton("document-save.png","Save");
            AddToolButton("document-save-as.png","Save as");
            new Separator(toolbar, true);
            AddToolButton("edit-undo.png","Undo");
            AddToolButton("edit-redo.png","Redo");
            new Button(toolbar, "Test").EvClicked += new GuppyEventHandler(Program_EvClicked);

            editor = new Memo(mainwin) { ExpandX = true, ExpandY = true };
            editor.EvChanged += new GuppyEventHandler(EditorChanged);
            
            TableLayout statusbar = new TableLayout(mainwin) { Vertical = false, Margin = new Margin(4) };

            statustext = new Label(statusbar, "Statusbar...") { ExpandX = true };

            Guppy.Run(mainwin);
        }

        static void Program_EvClicked(GuppyEventArgs e)
        {
            

            editor.Append("Hello\r\n\r\n");
            editor.Append(", world");

        }

        static int debugindex = 0;

        static void EditorChanged(GuppyEventArgs e)
        {
            var pos = editor.CaretPosition;
            statustext.Caption = (pos.Y + 1).ToString() + " : " + (pos.X + 1).ToString() + " DEBUG:" + (debugindex++).ToString();
        }
    }
}
