using System;
using GuppyEx.AbstractDriver;

namespace GuppyEx
{
  public class HBox : CompositeWidget
  {

    public HBox(CompositeWidget parent)
      : base(parent, null, null)
    {
      ChildrenAlign = GuppyEx.LayoutAlign.Center;
      //ChildrenExpandX = false;
    }

    /// <summary>
    /// Default behaviour is that of a vertical box layout
    /// </summary>
    public override void CalcLayoutInfoRecursive(ref bool anyexpandx, ref bool anyexpandy)
    {

      int vsize = 0, hsize = 0;
      int uniwidth = -1, uniheight = -1;    //-1 means not set
      bool childexpandx = false, childexpandy = false;

      foreach (Widget c in Children)
      {
        childexpandx = false; childexpandy = false;
        c.CalcLayoutInfoRecursive(ref childexpandx, ref childexpandy);
        if (childexpandx) anyexpandx = true;
        if (childexpandy) anyexpandy = true;

        if (c.LayoutInfo.UniformWidth)
          uniwidth = Math.Max(uniwidth, c.LayoutInfo.Size.Width);
        if (c.LayoutInfo.UniformHeight)
          uniheight = Math.Max(uniheight, c.LayoutInfo.Size.Height);
      }

      if (uniwidth >= 0 || uniheight >= 0)
      {
        foreach (Widget c in Children)
        {
          if (c.LayoutInfo.UniformWidth) c.LayoutInfo.Size.Width = uniwidth;
          if (c.LayoutInfo.UniformHeight) c.LayoutInfo.Size.Height = uniheight;
        }
      }

      foreach (Widget c in Children)
      {
        vsize = Math.Max(vsize, c.LayoutInfo.Size.Height);
        hsize += c.LayoutInfo.Size.Width;
      }


      //append margins and gaps...
      vsize += Margin.Vertical;
      hsize += Math.Max(Children.Count - 1, 0) * Gap + Margin.Horizontal;

      LayoutInfo.Size = new Size(hsize, vsize);

      if (ExpandX != null && (bool)ExpandX) anyexpandx = true;
      if (ExpandY != null && (bool)ExpandY) anyexpandy = true;

      LayoutInfo.ExpandX = ExpandX ?? childexpandx;
      LayoutInfo.ExpandY = ExpandY ?? childexpandy;
      LayoutInfo.UniformWidth = UniformWidth ?? (Parent == null ? false : Parent.ChildrenUniformWidth);
      LayoutInfo.UniformHeight = UniformHeight ?? (Parent == null ? false : Parent.ChildrenUniformHeight);
      LayoutInfo.Align = Align ?? (Parent == null ? GuppyEx.LayoutAlign.Left : Parent.ChildrenAlign);
    }


    /*public override void CalcPositionsRecursive(int left, int top, int width, int height)
    {
        int xpos = left + Margin.Left;
        int ypos = top + Margin.Top;
        width -= Margin.Horizontal;
        height -= Margin.Vertical;


        //compute how much space we have for expansion
        int expandsize = width;
        int numexpanding = 0;
        expandsize -= Math.Max(0, Children.Count - 1) * Gap;
        foreach (Widget child in Children)
        {
            expandsize -= child.LayoutInfo.Size.Width;
            if (child.LayoutInfo.ExpandX)
                numexpanding++;
        }
        if (expandsize < 0) expandsize = 0;


        foreach (Widget child in Children)
        {
            int childwidth = child.LayoutInfo.Size.Width;
            if (child.LayoutInfo.ExpandX)
                childwidth += expandsize / numexpanding;
            int childheight = Math.Max(
              child.LayoutInfo.ExpandY ? height : child.LayoutInfo.Size.Height,
              child.LayoutInfo.Size.Height
            );


            child.CalcPositionsRecursive(xpos, ypos, childwidth, childheight);
            xpos += childwidth + Gap;
        }
            
    }*/


    private Margin DecorationSize
    {
      get
      {
        DriverCompositeWidget dw = DriverObject as DriverCompositeWidget;
        if (dw != null)
          return dw.GetDecorationSize();
        return Margin.Empty;
      }
    }

    public override void CalcPositionsRecursive(int left, int top, int width, int height)
    {
      int xpos, ypos;

      //compute client size, that is the size children can reside in
      int clientwidth = width - Margin.Horizontal;
      int clientheight = height - Margin.Vertical;

      //if we have a physical driver object, we start over at x,y=0,0 otherwise we continue
      //to build on current x,y
      DriverCompositeWidget dw = DriverObject as DriverCompositeWidget;
      Margin decorations = Margin.Empty;
      if (dw == null)
      { //no physical driver object
        xpos = left + Margin.Left;
        ypos = top + Margin.Top;
      }
      else
      {
        Margin m = DecorationSize;
        decorations = dw.GetDecorationSize();
        clientwidth -= decorations.Horizontal;
        clientheight -= decorations.Vertical;
        Point orig = dw.GetClientOrigin();
        xpos = orig.X + Margin.Left;
        ypos = orig.Y + Margin.Top;
      }

      //compute how much space we have for expansion to expandsize
      int expandsize = clientwidth;
      int numexpanding = 0;
      expandsize -= Math.Max(0, Children.Count - 1) * Gap;
      foreach (Widget child in Children)
      {
        expandsize -= child.LayoutInfo.Size.Width;
        if (child.LayoutInfo.ExpandX)
          numexpanding++;
      }
      if (expandsize < 0) expandsize = 0;


      //layout each child, taking expansion and align into account
      foreach (Widget child in Children)
      {
        int childheight = Math.Max(
          child.LayoutInfo.Size.Height,
          child.LayoutInfo.ExpandY ? clientheight : child.LayoutInfo.Size.Height
          );
        
        int childwidth = child.LayoutInfo.Size.Width;
        if (child.LayoutInfo.ExpandX)
          childwidth += expandsize / numexpanding;

        int aligned_ypos = ypos;
        if (child.LayoutInfo.Align == GuppyEx.LayoutAlign.Center)
          aligned_ypos = ypos + clientheight / 2 - childheight / 2;
        else if (child.LayoutInfo.Align == GuppyEx.LayoutAlign.Bottom)
          aligned_ypos = ypos + clientheight - childheight;
        
        child.CalcPositionsRecursive(xpos, aligned_ypos, childwidth, childheight);
        xpos += childwidth + Gap;
      }


      //NEVER CALL BASE CLASS POSITIONER FROM HBOX! 
      /*if (!(this is Window))	//dont place window objects here
        base.CalcPositionsRecursive(left, top, width, height);*/

    }


  }
}
