using System;
using System.Collections.Generic;

using System.Text;
using GuppyEx.AbstractDriver;

namespace GuppyEx
{
  public class GridBox : CompositeWidget
  {
    private int linecount;
    bool vertical;
    
    public GridBox(CompositeWidget parent,int linecnt, bool vertical):base(parent,null,null)
    {
      this.linecount = linecnt;
      this.vertical = vertical;
    }


    private Widget ControlAt(int col,int row) {
      int idx;
      if(vertical)
        idx=col+row*linecount;
      else
        idx=row+col*linecount;
      if(idx<Children.Count)
        return Children[idx];
      return null;
    }

    private int NumColumns
    {
      get
      {
        return vertical ? linecount : (Children.Count / linecount) + Math.Min(Children.Count % linecount, 1);
      }
    }

    private int NumRows
    {
      get
      {
        return vertical ? (Children.Count / linecount) + Math.Min(Children.Count % linecount, 1) : linecount;
      }
    }


    public override void CalcLayoutInfoRecursive(ref bool anyexpandx, ref bool anyexpandy)
    {
      int hsize = 0, vsize = 0;
      int numctrl = Children.Count;
      bool childexpandx = false, childexpandy = false;

      int numrows= NumRows;
      int numcols= NumColumns;

      int[] rowsizes=new int[numrows];
      int[] colsizes=new int[numcols];

      for(int row=0;row<numrows;row++) {
        for(int col=0;col<numcols;col++) {
          
          Widget child=ControlAt(col,row);
          if(child==null)
            continue;

          childexpandx = false; 
          childexpandy = false;
          child.CalcLayoutInfoRecursive(ref childexpandx,ref childexpandy);
          if (childexpandx) anyexpandx = true;
          if (childexpandy) anyexpandy = true;

          rowsizes[row]=Math.Max(rowsizes[row],child.LayoutInfo.Size.Height);
          colsizes[col]=Math.Max(colsizes[col],child.LayoutInfo.Size.Width);
        }
      }

      foreach (int i in rowsizes) vsize += i;
      foreach (int i in colsizes) hsize += i;
      hsize += Math.Max(numcols - 1, 0) * Gap + Margin.Horizontal;
      hsize += Margin.Vertical;
      vsize += Math.Max(numrows - 1, 0) * Gap + Margin.Vertical;
      vsize += Margin.Horizontal;

      LayoutInfo.Size = new Size(hsize, vsize);

      //base.CalcMinimumSizeRecursive(ref anyexpandx, ref anyexpandy);

      //compute expansion flags for layout info
      LayoutInfo.ExpandX = ExpandX ?? anyexpandx;
      LayoutInfo.ExpandY = ExpandY ?? anyexpandy;
      LayoutInfo.UniformWidth = UniformWidth ?? (Parent == null ? false : Parent.ChildrenUniformWidth);
      LayoutInfo.UniformHeight = UniformHeight ?? (Parent == null ? false : Parent.ChildrenUniformHeight);
      LayoutInfo.Align = Align ?? (Parent == null ? GuppyEx.LayoutAlign.Left : Parent.ChildrenAlign);
    }
    

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
      int numrows = NumRows;
      int numcols = NumColumns;

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

      //compute how much space we have for expansion to expandsizex/y
      //by subtracting the (natural size of) highest column from available height
      //an widest row from available width
      int[] natural_rowwidth = new int[numrows];
      int[] natural_colheight = new int[numcols];
      int[] natural_rowheight = new int[numrows];
      int[] natural_colwidth = new int[numcols];

      int[] xexpanding = new int[numrows];
      int[] yexpanding = new int[numcols];
      int expandsizex = clientwidth,expandsizey=clientheight;
      int numexpandingx = 0,numexpandingy=0;
      expandsizex -= Math.Max(0, numcols - 1) * Gap;
      expandsizey -= Math.Max(0, numrows - 1) * Gap;

      for(int row=0;row<numrows;row++) {
        for(int col=0;col<numcols;col++) {
          Widget child=ControlAt(col,row);
          if(child==null)
            continue;
          natural_rowwidth[row] += child.LayoutInfo.Size.Width;
          natural_colheight[col] += child.LayoutInfo.Size.Height;
          natural_rowheight[row] = Math.Max(child.LayoutInfo.Size.Height,natural_rowheight[row]);
          natural_colwidth[col] = Math.Max(child.LayoutInfo.Size.Width, natural_colwidth[col]);
          if (child.LayoutInfo.ExpandX) xexpanding[row]++;
          if (child.LayoutInfo.ExpandY) yexpanding[col]++;
        }
      }
      int maxrowwidth = 0, maxcolheight = 0;
      maxrowwidth=MaxInt(natural_rowwidth);
      maxcolheight=MaxInt(natural_colheight);
      numexpandingx=MaxInt(xexpanding);
      numexpandingy=MaxInt(yexpanding);
      
      expandsizex -= maxrowwidth;
      expandsizey -= maxcolheight;
      

      if (expandsizex < 0) expandsizex = 0;
      if (expandsizey < 0) expandsizey = 0;
      
      

      //compute the final width and height off all rows and columns
      int[] colwidth = new int[numcols];
      int[] rowheight = new int[numrows];
      for (int row = 0; row < numrows; row++)
      {
        int oldnumexpandingy = numexpandingy; //so we can restyore it after loop
        int oldexpandsizey = expandsizey;
        for (int col = 0; col < numcols; col++)
        {
          Widget child = ControlAt(col, row);
          if (child == null)
            continue;

          int childwidth = natural_colwidth[col]; // child.LayoutInfo.Size.Width;
          if (child.LayoutInfo.ExpandX)
            childwidth += GetExpand(expandsizex,numexpandingx,col);
            //childwidth += expandsizex / numexpandingx;

          int childheight = natural_rowheight[row]; // child.LayoutInfo.Size.Height;
          if (child.LayoutInfo.ExpandY)
            childheight += GetExpand(expandsizey,numexpandingy,row);

          colwidth[col] = Math.Max(childwidth, colwidth[col]);
          rowheight[row] = Math.Max(childheight, rowheight[row]);
        }
        numexpandingy = oldnumexpandingy;
        expandsizey = oldexpandsizey;
      }

            
      int cx=xpos,cy=ypos;
      for (int row = 0; row < numrows; row++)
      {

        for (int col = 0; col < numcols; col++)
        {
          Widget child = ControlAt(col, row);
          if (child == null)
            continue;

          int childwidth = child.LayoutInfo.ExpandX ? colwidth[col] : child.LayoutInfo.Size.Width;
          int childheight = child.LayoutInfo.ExpandY ? rowheight[row] : child.LayoutInfo.Size.Height;

          int ctrlx, ctrly;
          ComputeXYUsingAlign(child.LayoutInfo.Align,childwidth, childheight, cx, cy, colwidth[col], rowheight[row],out ctrlx,out ctrly);

          child.CalcPositionsRecursive(ctrlx, ctrly, childwidth, childheight);

          cx += colwidth[col] + Gap; // colsizes[col] + Gap;
          
        }

        cx = xpos;
        cy += rowheight[row] + Gap;
      }


     

      //layout each child, taking expansion and align into account
     /* foreach (Widget child in Children)
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
      }*/
      //base.CalcPositionsRecursive(left, top, width, height);
    }

    private static void ComputeXYUsingAlign(LayoutAlign align,int ctrlw, int ctrlh, int allocx, int allocy, int allocw, int alloch, out int ctrlx, out int ctrly)
    {
      //horizontal:
      if ((align & LayoutAlign.Left) != 0)
        ctrlx = allocx;
      else if ((align & LayoutAlign.Right) != 0)
        ctrlx = allocx + allocw - ctrlw;
      else //center
        ctrlx = allocx + allocw/2 - ctrlw/2;

      //vertical:
      if ((align & LayoutAlign.Top) != 0)
        ctrly = allocy;
      else if ((align & LayoutAlign.Bottom) != 0)
        ctrly = allocy + alloch - ctrlh;
      else //center
        ctrly = allocy + alloch / 2 - ctrlh / 2;
    }

  

    /// <summary>
    /// Computes the number of pixels a control which expans should get extra
    /// to divide evenly over the total space 'sizeleft'.
    /// </summary>
    /// <param name="sizeleft"></param>
    /// <param name="numexpanding"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private int GetExpand(int sizeleft,int numexpanding,int index)
    {
      int res = sizeleft / numexpanding;
      int pixelsleft = sizeleft - res * numexpanding;

      if (index < pixelsleft)
        res++;

      return res;
    }


    /// <summary>
    /// Returns the largest int in the arary sent, but never less than zero.
    /// </summary>
    private int MaxInt(int[] elem)
    {
      int m = 0;
      foreach (int i in elem)
        if (i > m) m = i;
      return m;
    }
  }

  

}

