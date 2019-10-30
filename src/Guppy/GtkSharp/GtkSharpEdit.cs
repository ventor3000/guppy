using GuppyGUI.AbstractDriver;
using System;

namespace GuppyGUI.GtkSharp
{
	/// <summary>
	/// Description of GtkSharpEdit.
	/// </summary>
  public class GtkSharpEdit : DriverEdit
  {

    Gtk.Entry entry;

    public GtkSharpEdit(Widget shellobject)
      : base(shellobject)
    {

      entry = new Gtk.Entry();
      GtkSharpDriver.InitWidget(entry, shellobject);
      entry.Show();

      shellobject.Size = new Size2i(Guppy.DefaultEditWidth, shellobject.Size.Height);
    }

    public override object NativeObject
    {
      get
      {
        return entry;
      }
    }

    /* private Gtk.Widget TheWidget
     {
       get { return NativeObject as Gtk.Widget; }
     }*/


    public override Size2i GetNaturalSize()
    {

      return GtkSharpDriver.DefaultGetNaturalSize(entry);
    }




    public override string Text
    {
      get
      {

        return entry.Text;
      }
      set
      {

        entry.Text = value;
      }
    }

    public override void Append(string txt)
    {
      entry.Text += txt;
    }

    public override void Clear()
    {

      entry.Text = "";
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

    public override void SelectAll()
    {
        throw new NotImplementedException();
    }

    public override bool ReadOnly
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

  }

		/*Gtk.Entry entry;
    Gtk.TextView textview;

    bool multiline;
		
		public GtkSharpEdit(Widget shellobject,bool multiline):base(shellobject)
		{

      this.multiline = multiline;
      if (multiline)
      {
        textview = new Gtk.TextView();
        textview.Show();
      }
      else
      {
        entry = new Gtk.Entry();
        entry.Show();
      }
			
      shellobject.Size = new Size2i(Guppy.DefaultEditWidth, shellobject.Size.Height);
      
		}
		
		public override object NativeObject {
			get {
        if (multiline)
          return textview;
        else
				  return entry;
			}
		}

    private Gtk.Widget TheWidget
    {
      get { return NativeObject as Gtk.Widget; }
    }
		
				
		public override Size2i GetNaturalSize()
		{
      if (multiline)
        return new Size2i(50,50);
      else
        return GtkSharpDriver.DefaultGetNaturalSize(TheWidget);
		}


		
		public override void Show()
		{
			TheWidget.Show();
		}
		
		public override string Text {
			get {

        return multiline ? textview.Buffer.Text : entry.Text;
			}
			set {
        if (multiline)
          textview.Buffer.Text = value;
        else
          entry.Text = value;
			}
		}

    public override void Append(string txt)
    {
      if (multiline)
      {
        textview.Buffer.Insert(textview.Buffer.EndIter, txt);
      }
      else
      {
        entry.Text += txt;
      }
    }

    public override void Clear()
    {
      if (multiline)
        textview.Buffer.Clear();
      else
        entry.Text = "";
    }
	}*/
}
