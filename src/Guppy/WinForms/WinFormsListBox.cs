using System;
using GuppyGUI.AbstractDriver;


namespace GuppyGUI.WinForms
{
    public class WinFormsListBox : DriverListBox
    {
        System.Windows.Forms.ListBox listbox;
        private int numrows = 8; //TODO: user access to this? This is the number of rows if height not specifically set

        public WinFormsListBox(Widget shellobject, params object[] items)
            : base(shellobject)
        {
            listbox = new System.Windows.Forms.ListBox();
            listbox.Tag = shellobject; //map-back from native control to guppy object

            foreach (object str in items)
            {
                if (str != null)
                    Append(str);
            }

            listbox.SelectedIndexChanged += delegate { ((ListBox)ShellObject).OnChanged(); };
            listbox.Enter += delegate { ((ListBox)ShellObject).OnEnter(); };
            listbox.DoubleClick += delegate { ((ListBox)ShellObject).OnDoubleClick(); };

        }

        public override int Append(object obj)
        {
            return listbox.Items.Add(obj);
        }

        public override void Clear()
        {
            listbox.Items.Clear();
        }


        public override object NativeObject
        {
            get { return listbox; }
        }

        public override Size2i GetNaturalSize()
        {

            var siz = System.Windows.Forms.TextRenderer.MeasureText("W", listbox.Font);
            var bohe = listbox.Height - listbox.ClientSize.Height;


            return new Size2i(100, siz.Height * numrows + bohe);

            //TODO: maybe we want a smarter size algorithm so that all items is visible. Limits?
        }


        public override int Count
        {
            get { return listbox.Items.Count; }
        }

        public override int SelectedIndex
        {
            get
            {
                return listbox.SelectedIndex;
            }
            set
            {
                int cnt = listbox.Items.Count;
                if (cnt == 0) return;
                if (value < 0 || value >= cnt)
                    listbox.SelectedIndex = -1;
                else
                    listbox.SelectedIndex = value;
            }
        }

        public override bool Enabled
        {
            get
            {
                return listbox.Enabled;
            }
            set
            {
                listbox.Enabled = value;
            }
        }

        public override int RemoveIndex(int index)
        {

            int cnt = Count;

            if (index < 0 || index >= cnt)
                return -1;
            listbox.Items.RemoveAt(index);
            if (index == cnt-1)
                return cnt - 2;
            return index;
        }

        public override object this[int index]
        {
            get
            {
                if (index < 0 || index >= listbox.Items.Count)
                    return null;
                return listbox.Items[index];
            }
        }

        

    }
}
