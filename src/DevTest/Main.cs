using GuppyGUI;
using GuppyGUI.Util;
using System.IO;
using System;

namespace DevTest
{
    static class MainClass
    {


       


        public class ComboItem
        {

            string text;

            public ComboItem(string txt)
            {
                this.text = txt;
            }

            public override string ToString()
            {
                return text;
            }
        }

        static Button A, B, C,D,E,F;
        static Window win;
        static PopupMenu pmu;
        static MenuItem MA, MB, MC;
        static TableLayout tbl;

        static Toggle t;
        static Frame f1, f2;

        public static void Main()
        {
            Guppy.Open(DriverMode.WinForms);

            win = new Window("test") { Vertical = false,Margin=new Margin(10,10,10,10) };

            f1 = new Frame(win,"",false);
            new RadioButton(f1,"Röd");
            new RadioButton(f1,"Grön");
            new RadioButton(f1,"Gul");

            f2 = new Frame(win, "", false);
            new RadioButton(f2, "Bananer");
            new RadioButton(f2, "Äpplen");
            new RadioButton(f2, "Appelsiner");

            t=new Toggle(win, "Disabled", true);
            t.EvChanged += new GuppyEventHandler(t_EvChanged);

            Guppy.Run(win);
        }

        static void t_EvChanged(GuppyEventArgs e)
        {
            f1.Enabled = !t.Checked;

                
        }

        static void A_EvClicked(GuppyEventArgs e)
        {

          
        }

        static void toremove_EvClicked(GuppyEventArgs e)
        {
            using (Dialog d = new Dialog("Dialog", "Hello", "HELLO", "World", "W","A","A"))
            {
                d.ShowModal();
            }
        }

        
    }
}
