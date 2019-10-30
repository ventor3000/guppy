
using System.Collections;
using System.Collections.Generic;
using System;


namespace GuppyGUI
{

  public class GuppyEventArgs : EventArgs
  {
    public readonly object Sender;
    public bool Block = false;

    public GuppyEventArgs(object sender)
    {
      this.Sender = sender;
    }
  }
  public delegate void GuppyEventHandler(GuppyEventArgs e);


  public class GuppyKeyArgs : GuppyEventArgs
  {
    public readonly KeyData Key;

    public GuppyKeyArgs(object sender, KeyData keycode)
      : base(sender)
    {
      Key = keycode;
    }
  }
  public delegate void GuppyKeyHandler(GuppyKeyArgs e);
}
