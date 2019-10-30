using System;
using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{

    public class WinFormsChoice : DriverChoice
    {
        System.Windows.Forms.ComboBox combobox;


        public WinFormsChoice(Widget shellobject, params object[] items)
            : base(shellobject)
        {
            combobox = new System.Windows.Forms.ComboBox();
            combobox.Tag = shellobject; //map-back from native control to guppy object

            combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            combobox.Items.AddRange(items);
            
            combobox.AutoSize = false;

            combobox.IntegralHeight = false;    //needed for MaxDropDownCount to work in Vista or later (bug?)

            combobox.SelectedIndexChanged += delegate { ((Choice)ShellObject).OnChanged(); };
            combobox.Enter += delegate { ((Choice)ShellObject).OnEnter(); };
        }


        public void FitWidth()
        {
            //height never changes so we use it directly
            int h = combobox.Height;

            //find with of textfield, using text and all items
            int w = 0;
            foreach (string s in combobox.Items)
                w = Math.Max(w, System.Windows.Forms.TextRenderer.MeasureText(s, combobox.Font).Width);

            w += h; //HACK: add height of combobox hoping for the longest text to be visible assuming the button is higher than wider (o:


            ShellObject.Size = new Size2i(w, ShellObject.Size.Height);
        }

        public override object NativeObject
        {
            get { return combobox; }
        }

        public override Size2i GetNaturalSize()
        {
            return new Size2i(Guppy.DefaultEditWidth /*+ combobox.Height*/, combobox.Height);
        }


        public override int Append(object item)
        {
            return combobox.Items.Add(item);
        }

        public override int SelectedIndex
        {
            get
            {
                return combobox.SelectedIndex;
            }
            set
            {
                int cnt = combobox.Items.Count;
                if (value >= cnt) value = -1;
                    //value = cnt - 1;
                if (cnt == 0) return;
                if (value < 0 ) combobox.SelectedIndex = -1;
                else combobox.SelectedIndex = value;
            }
        }

        public override int Count
        {
            get { return combobox.Items.Count; }
        }

        public override bool Enabled
        {
            get
            {
                return combobox.Enabled;
            }
            set
            {
                combobox.Enabled = value;
            }
        }

      

        public override int DropDownCount
        {
            get
            {
                return combobox.MaxDropDownItems;
            }
            set
            {
                if (value < 1) value = 1;
                if (value > 100) value = 100; //Windows forms limits
                combobox.MaxDropDownItems = value;
            }
        }

        public override void Clear()
        {
            combobox.Items.Clear();
        }

        public override int RemoveIndex(int index)
        {

            int cnt = Count;

            if (index < 0 || index >= cnt)
                return -1;
            combobox.Items.RemoveAt(index);
            if (index == cnt-1)
                return cnt - 2;
            return index;
        }

        public override object this[int index]
        {
            get
            {
                if (index < 0 || index >= combobox.Items.Count)
                    return null;
                return combobox.Items[index];
            }
        }
        
    }

    /*public class WinFormsChoice:DriverChoice
    {
      System.Windows.Forms.ComboBox combobox;


      public void FitWidth()
      {
        //height never changes so we use it directly
        int h = combobox.Height;

        //find with of textfield, using text and all items
        int w = 0;
        foreach (string s in combobox.Items)
          w = Math.Max(w, System.Windows.Forms.TextRenderer.MeasureText(s, combobox.Font).Width);

        w += h; //HACK: add height of combobox hoping for the longest text to be visible assuming the button is higher than wider (o:


        ShellObject.Size = new Size2i(w, ShellObject.Size.Height);
      }

      public WinFormsChoice(Widget shellobject, bool editable, params string[] items)
        : base(shellobject)
      {
        combobox = new System.Windows.Forms.ComboBox();
        if (editable)
          combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        else
          combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        combobox.Items.AddRange(items);

        combobox.AutoSize = false;

        FitWidth();

        //combobox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      }

      public override void Show()
      {
        combobox.Show();
      }

    

      public override object NativeObject
      {
        get { return combobox; }
      }

      public override Size2i GetNaturalSize()
      {
        return new Size2i(Guppy.DefaultEditWidth + combobox.Height, combobox.Height);
      }

    }*/
}
