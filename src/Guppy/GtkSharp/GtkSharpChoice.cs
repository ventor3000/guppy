using System;
using GuppyGUI.AbstractDriver;
namespace GuppyGUI.GtkSharp
{
    public class GtkSharpChoice : DriverChoice
    {
        Gtk.ComboBox combobox;

        int StringWidth(Gtk.Widget w, string text)
        {
            int wi, he;
            Pango.Layout lo = w.CreatePangoLayout(text);
            lo.FontDescription = w.Style.FontDesc;
            lo.GetPixelSize(out wi, out he);
            lo.Dispose();

            return wi;
        }


        public int FitWidth()
        {
            int textwidth = 0;


            var re = combobox.SizeRequest();

            Gtk.TreeIter gi;
            if (combobox.Model.GetIterFirst(out gi))
            {
                do
                {
                    string str = combobox.Model.GetValue(gi, 0) as string;
                    if (str != null)
                        textwidth = Math.Max(textwidth, StringWidth(combobox, str));
                } while (combobox.Model.IterNext(ref gi));
            }

            textwidth += re.Height; //hopes this is enough to look nice with the dropdown button
            ShellObject.Size = new Size2i(textwidth, ShellObject.Size.Height);

            combobox.Child.SetSizeRequest(0, -1);
            return 0;
        }

        public GtkSharpChoice(Widget shellobject, params object[] entries)
            : base(shellobject)
        {
            //note: this function is somewhat hacky to get stupid gtk combos to behave like we want
            //the question is if we can guarantee default width of combos are same evrywhere?

            int xborder;


            combobox = new Gtk.ComboBox(); //TODO: support adding objects
            GtkSharpDriver.InitWidget(combobox, shellobject);
            
            var c = combobox.Child;

            Gtk.Requisition req_r = c.SizeRequest();
            Gtk.Requisition req_c = combobox.SizeRequest();
            int btnwidth = req_c.Width - req_r.Width;

            xborder = req_c.Height - req_r.Height;

            c.SetSizeRequest(Guppy.DefaultEditWidth - xborder, -1);

            FitWidth();
            combobox.Show();
        }

        public override Size2i GetNaturalSize()
        {
            return GtkSharpDriver.DefaultGetNaturalSize(combobox);
        }

        public override object NativeObject
        {
            get
            {
                return combobox;
            }
        }



        public override int Append(object item)
        {
            throw new NotImplementedException("Append item in combobox not implemented");   //TODO: implement this
        }

        public override int Count
        {
            get { throw new NotImplementedException(); }    //TODO: implement
        }


        public override int SelectedIndex
        {
            get
            {
                throw new NotImplementedException(); //TODO: implement
            }
            set
            {
                throw new NotImplementedException(); //TODO: implement
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

       

        public override int DropDownCount
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int RemoveIndex(int index)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override object this[int index]
        {
            get { throw new NotImplementedException(); }
        }

    }

   /* Gtk.ComboBox combobox;

    int StringWidth(Gtk.Widget w, string text)
    {
      int wi, he;
      Pango.Layout lo = w.CreatePangoLayout(text);
      lo.FontDescription = w.Style.FontDesc;
      lo.GetPixelSize(out wi, out he);
      lo.Dispose();

      return wi;
    }


    public int FitWidth()
    {
      int textwidth = 0;


      var re = combobox.SizeRequest();

      Gtk.TreeIter gi;
      if (combobox.Model.GetIterFirst(out gi))
      {
        do
        {
          string str = combobox.Model.GetValue(gi, 0) as string;
          if (str != null)
            textwidth = Math.Max(textwidth, StringWidth(combobox, str));
        } while (combobox.Model.IterNext(ref gi));
      }

      textwidth += re.Height; //hopes this is enough to look nice with the dropdown button
      ShellObject.Size = new Size2i(textwidth, ShellObject.Size.Height);

      combobox.Child.SetSizeRequest(0, -1);
      return 0;
    }

    public GtkSharpChoice(Widget shellobject, bool editable, params string[] entries)
      : base(shellobject)
    {
      //note: this function is somewhat hacky to get stupid gtk combos to behave like we want
      //the question is if we can guarantee default width of combos are same evrywhere?

      int xborder;

      if (!editable)
      {
        combobox = new Gtk.ComboBox(entries);
        var c = combobox.Child;

        Gtk.Requisition req_r = c.SizeRequest();
        Gtk.Requisition req_c = combobox.SizeRequest();
        int btnwidth = req_c.Width - req_r.Width;

        xborder = req_c.Height - req_r.Height;

        c.SetSizeRequest(Guppy.DefaultEditWidth - xborder, -1);
      }

      else
      {
        var ce = new Gtk.ComboBoxEntry(entries);
        combobox = ce;


        Gtk.Requisition req_r = ce.Entry.SizeRequest();
        Gtk.Requisition req_c = ce.SizeRequest();
        int btnwidth = req_c.Width - req_r.Width;

        xborder = (req_c.Height - req_r.Height) * 2;

        ce.Entry.SetSizeRequest(Guppy.DefaultEditWidth - xborder, -1);
      }

      FitWidth();


      combobox.Show();


    }







    public override Size2i GetNaturalSize()
    {
      return GtkSharpDriver.DefaultGetNaturalSize(combobox);
    }

    public override object NativeObject
    {
      get
      {
        return combobox;
      }
    }


    public override void Show()
    {
      combobox.Show();
    }
  }*/
}

