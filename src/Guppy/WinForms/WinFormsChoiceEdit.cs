using GuppyGUI.AbstractDriver;

namespace GuppyGUI.WinForms
{
    class WinFormsChoiceEdit:DriverChoiceEdit
    {
        System.Windows.Forms.ComboBox combobox;

        public WinFormsChoiceEdit(Widget shellobject, params object[] items):base(shellobject)
        {
            combobox = new System.Windows.Forms.ComboBox();
            combobox.Tag = shellobject; //map-back from native control to guppy object

            foreach (object i in items)
                combobox.Items.Add(i);
        }

        

       

        public override object NativeObject
        {
            get { return combobox; }
        }

        public override Size2i GetNaturalSize()
        {
            return new Size2i(Guppy.DefaultEditWidth , combobox.Height);
        }

        public override int RemoveIndex(int index)
        {
            int cnt = Count;

            if (index < 0 || index >= cnt)
                return -1;
            combobox.Items.RemoveAt(index);
            if (index == cnt - 1)
                return cnt - 2;
            return index;
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

        public override int Count
        {
            get { return combobox.Items.Count; }
        }

        public override void Clear()
        {
            combobox.Items.Clear();
        }

        public override int Append(object obj)
        {
            return combobox.Items.Add(obj);
        }

        public override string Text
        {
            get
            {
                return combobox.Text;
            }
            set
            {
                combobox.Text = value;
            }
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
                if (value < 0) combobox.SelectedIndex = -1;
                else combobox.SelectedIndex = value;
            }
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
}
