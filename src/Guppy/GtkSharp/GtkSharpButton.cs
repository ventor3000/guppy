using GuppyGUI.AbstractDriver;
using System;

namespace GuppyGUI.GtkSharp
{



    public class GtkSharpButton : DriverButton
    {
        Gtk.Button button;
        Gtk.Label label;
        //Gtk.Image image;

        public GtkSharpButton(Widget shellobject, string caption)
            : base(shellobject)
        {

            button = new Gtk.Button();
            GtkSharpDriver.InitWidget(button, shellobject);
            button.Show();

            label = new Gtk.Label(caption);
            label.Show();

            CreateContent(null);

            button.Show();

            button.Clicked += delegate { ((Button)ShellObject).OnClicked(); };
        }

        private void CreateContent(Gtk.Image img)
        {

            if (button.Child != null)
                button.Remove(button.Child);


            Gtk.Box content = new Gtk.HBox();
            button.Add(content);
            content.Show();

            if (img != null)
                content.Add(img);


            if (label != null)
            {
                label = new Gtk.Label(label.Text);  //for some stupid reason we have to re-create it (why??)
                label.Show();
                content.Add(label);
            }
        }



        public override string Caption
        {
            get
            {
                if (label == null)
                    return "";
                return label.Text;
            }
            set
            {
                if (label != null)
                    label.Text = value;
            }
        }

        public override object NativeObject
        {
            get { return button; }
        }

        public override Size2i GetNaturalSize()
        {
            return GtkSharpDriver.DefaultGetNaturalSize(button);
        }


        public override bool Flat
        {
            get
            {
                return button.Relief == Gtk.ReliefStyle.None;
            }
            set
            {
                button.Relief = value ? Gtk.ReliefStyle.None : Gtk.ReliefStyle.Normal;
            }
        }

        public override bool CanFocus
        {
            get
            {
                return button.CanFocus;
            }
            set
            {
                button.CanFocus = value;
            }
        }

        public override Image Image
        {
            set
            {
                CreateContent(GtkSharpDriver.ImageToGtkImage(value));
            }
        }

        public override bool Enabled
        {
            get
            {
                throw new NotImplementedException(); //TODO:
            }
            set
            {
                throw new NotImplementedException(); //TODO:
            }
        }

        public override bool Default
        {
            get
            {
                throw new NotImplementedException();//TODO:
            }
            set
            {
                throw new NotImplementedException();//TODO:
            }
        }


    }
}
