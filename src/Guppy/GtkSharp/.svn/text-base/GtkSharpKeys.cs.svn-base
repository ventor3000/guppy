
namespace GuppyGUI.GtkSharp
{
    public static class GtkSharpKeys
    {
        public static KeyData DecodeKey(Gdk.EventKey evt)
        {
            KeyCode kc = KeyCode.Unknown;

            Gdk.Keymap kmap = Gdk.Keymap.GetForDisplay(Gdk.Display.Default);
            Gdk.KeymapKey kmk;
            kmk.Level = 0;
            kmk.Group = 0;
            kmk.Keycode = evt.HardwareKeycode;

            uint key = kmap.LookupKey(kmk);

            char c = (char)Gdk.Keyval.ToUnicode(key);
            if (c != '\0') kc = (KeyCode)c;

           

            if (kc == KeyCode.Unknown)
            { //still not found, try non-typable keys
                switch ((Gdk.Key)key)
                {
                    case Gdk.Key.BackSpace: kc = KeyCode.Backspace; break;
                    case Gdk.Key.Tab: kc = KeyCode.Tab; break;
                    case Gdk.Key.Linefeed: kc = KeyCode.LineFeed; break;
                    case Gdk.Key.Return: kc = KeyCode.Return; break;
                    case Gdk.Key.Escape: kc = KeyCode.Escape; break;
                    

                    case Gdk.Key.Home: kc = KeyCode.Home; break;
                    case Gdk.Key.Up: kc = KeyCode.Up; break;
                    case Gdk.Key.Page_Up: kc = KeyCode.PageUp; break;
                    case Gdk.Key.Page_Down: kc = KeyCode.PageDown; break;
                    case Gdk.Key.Left: kc = KeyCode.Left; break;
                    case Gdk.Key.Right: kc = KeyCode.Right; break;
                    case Gdk.Key.End: kc = KeyCode.End; break;
                    case Gdk.Key.Down: kc = KeyCode.Down; break;
                    case Gdk.Key.Insert: kc = KeyCode.Insert; break;
                    case Gdk.Key.Delete: kc = KeyCode.Delete; break;
                    case Gdk.Key.Pause: kc = KeyCode.Pause; break;
                    case Gdk.Key.Caps_Lock: kc = KeyCode.CapsLock; break;
                    case Gdk.Key.F1: kc = KeyCode.F1; break;
                    case Gdk.Key.F2: kc = KeyCode.F2; break;
                    case Gdk.Key.F3: kc = KeyCode.F3; break;
                    case Gdk.Key.F4: kc = KeyCode.F4; break;
                    case Gdk.Key.F5: kc = KeyCode.F5; break;
                    case Gdk.Key.F6: kc = KeyCode.F6; break;
                    case Gdk.Key.F7: kc = KeyCode.F7; break;
                    case Gdk.Key.F8: kc = KeyCode.F8; break;
                    case Gdk.Key.F9: kc = KeyCode.F9; break;
                    case Gdk.Key.F10: kc = KeyCode.F10; break;
                    case Gdk.Key.F11: kc = KeyCode.F11; break;
                    case Gdk.Key.F12: kc = KeyCode.F12; break;
                    case Gdk.Key.Print: kc = KeyCode.Print; break;
                    case Gdk.Key.Menu: kc = KeyCode.Menu; break;
                    case Gdk.Key.Num_Lock: kc = KeyCode.NumLock; break;
                    case Gdk.Key.Scroll_Lock: kc = KeyCode.ScrollLock; break;
                }
            }

            bool ctrl = (evt.State & Gdk.ModifierType.ControlMask) != 0;
            bool alt = (evt.State & Gdk.ModifierType.Mod1Mask) != 0;
            bool shift = (evt.State & Gdk.ModifierType.ShiftMask) != 0;

            if (kc == KeyCode.Unknown)
                return new KeyData(kc, (uint)evt.HardwareKeycode,'\0',ctrl,shift,alt);

          
            /*
            if (alt) kc |= KeyCode.Alt;
            if (shift) kc |= KeyCode.Shift;
            if (ctrl) kc |= KeyCode.Control;*/

            return new KeyData(kc, evt.HardwareKeycode,'\0',ctrl,shift,alt);    //TODO: map character correctly
        }

        
    }
}
