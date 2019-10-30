using System;
using System.Collections.Generic;

using GuppyGUI.AbstractDriver;

namespace GuppyGUI
{
    public enum LayoutAlign
    {
        Center = 0,
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,

        TopLeft = Top | Left,
        TopRight = Top | Right,
        BottomLeft = Bottom | Left,
        BottomRight = Bottom | Right
    }

    public abstract class CompositeWidget : Widget
    {
        //internal List<Widget> ChildrenOld = new List<Widget>();
        public ChildCollection Children;
        public int Gap = Guppy.DefaultGap;
        public Margin Margin = Margin.Empty;

        public bool ChildrenExpandX = false;
        public bool ChildrenExpandY = false;
        public bool ChildrenUniformWidth = false;
        public bool ChildrenUniformHeight = false;
        public LayoutAlign ChildrenAlign = GuppyGUI.LayoutAlign.Left;


        private GridSpans spans;
        private bool vertical = true;
        private int linecount;

        public CompositeWidget()
        {
            Children = new ChildCollection(this);

            Align = GuppyGUI.LayoutAlign.TopLeft;  //same as left if this is a hbox

            vertical = true;
            linecount = 1;
        }

        virtual public int WrapCount
        {
            get
            {
                return linecount;
            }
            set
            {
                linecount = Math.Max(1, value);
            }
        }

        virtual public bool Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }


        private Widget WidgetAt(int row, int col)
        {
            int idx;
            if (vertical)
                idx = col + row * linecount;
            else
                idx = row + col * linecount;
            if (idx < Children.Count)
                return Children[idx];
            return null;
        }



        public override void CalcLayoutInfoRecursive(ref bool anyexpandx, ref bool anyexpandy)
        {
            int numcol = vertical ? linecount : (Children.Count / linecount) + Math.Min(Children.Count % linecount, 1);
            int numrows = vertical ? (Children.Count / linecount) + Math.Min(Children.Count % linecount, 1) : linecount;
            bool childexpandx = false, childexpandy = false;
            spans = new GridSpans(numrows, numcol);

            foreach (Widget w in Children)
            {
                childexpandx = false;
                childexpandy = false;
                w.CalcLayoutInfoRecursive(ref childexpandx, ref childexpandy);
                if (childexpandx) anyexpandx = true;
                if (childexpandy) anyexpandy = true;

            }

            FixUniformSizes();


            for (int row = 0; row < numrows; row++)
            {
                for (int col = 0; col < numcol; col++)
                {
                    Widget w = WidgetAt(row, col);
                    if (w == null)
                        continue;


                    spans.Grow(row, col, w.LayoutInfo.Size, w.LayoutInfo.ExpandX, w.LayoutInfo.ExpandY);
                }
            }


            Margin m = DecorationSize;
            LayoutInfo.Size = spans.Size();

            LayoutInfo.Size = new Size2i(
              LayoutInfo.Size.Width + Math.Max(numcol - 1, 0) * Gap + Margin.Horizontal + m.Horizontal,
              LayoutInfo.Size.Height + Math.Max(numrows - 1, 0) * Gap + Margin.Vertical + m.Vertical
            );

            /*LayoutInfo.Size.Width += Math.Max(numcol - 1, 0) * Gap + Margin.Horizontal+m.Horizontal;
            LayoutInfo.Size.Height += Math.Max(numrows - 1, 0) * Gap + Margin.Vertical+m.Vertical;*/



            LayoutInfo.ExpandX = ExpandX ?? anyexpandx;
            LayoutInfo.ExpandY = ExpandY ?? anyexpandy;
            LayoutInfo.UniformWidth = UniformWidth ?? (Parent == null ? false : Parent.ChildrenUniformWidth);
            LayoutInfo.UniformHeight = UniformHeight ?? (Parent == null ? false : Parent.ChildrenUniformHeight);
            LayoutInfo.Align = Align ?? (Parent == null ? GuppyGUI.LayoutAlign.Left : Parent.ChildrenAlign);
        }

        private void FixUniformSizes()
        {
            //make all controls that have uniform width have the same with as the widest uniform
            //and dito with height
            int uniwidth = 0, uniheight = 0;
            foreach (Widget w in Children)
            {
                if (w.LayoutInfo.UniformWidth)
                    uniwidth = Math.Max(uniwidth, w.LayoutInfo.Size.Width);
                if (w.LayoutInfo.UniformHeight)
                    uniheight = Math.Max(uniheight, w.LayoutInfo.Size.Height);
            }

            foreach (Widget w in Children)
            {
                if (w.LayoutInfo.UniformHeight || w.LayoutInfo.UniformWidth)
                {
                    int nw = w.LayoutInfo.Size.Width, nh = w.LayoutInfo.Size.Height;

                    if (w.LayoutInfo.UniformWidth) nw = uniwidth;
                    if (w.LayoutInfo.UniformHeight) nh = uniheight;
                    w.LayoutInfo.Size = new Size2i(nw, nh);
                }
            }
        }


        virtual protected Margin DecorationSize
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
                Point2i orig = dw.GetClientOrigin();
                xpos = orig.X + Margin.Left;
                ypos = orig.Y + Margin.Top;
            }

            //compute how much space we have left for exapnsion in x and y
            int expandx = Math.Max(0, width - LayoutInfo.Size.Width /* clientwidth - (LayoutInfo.Size.Width+Math.Max(spans.NumColumns - 1, 0) * Gap + Margin.Horizontal)*/);
            int expandy = Math.Max(0, height - LayoutInfo.Size.Height/*clientheight - (LayoutInfo.Size.Height+Math.Max(spans.NumRows - 1, 0) * Gap + Margin.Vertical)*/);
            int numexpandx = spans.NumExpandingColumns;
            int numexpandy = spans.NumExpandingRows;


            //recompute the cells of table taking expansion into account
            for (int row = 0; row < spans.NumRows; row++)
            {
                int curx = xpos;
                for (int col = 0; col < spans.NumColumns; col++)
                {
                    Widget w = WidgetAt(row, col);
                    if (w == null)
                        continue;

                    int cellwidth = spans.ColumnWidth(col) + (w.LayoutInfo.ExpandX ? GetExpand(expandx, numexpandx, col) : 0);
                    int cellheight = spans.RowHeight(row) + (w.LayoutInfo.ExpandY ? GetExpand(expandy, numexpandy, row) : 0);

                    spans.GrowFinalSize(row, col, cellwidth, cellheight);
                }
            }

            //finally, actually place the controls
            int cury = ypos;
            for (int row = 0; row < spans.NumRows; row++)
            {
                int curx = xpos;
                int rowheight = spans.FinalRowHeight(row);
                for (int col = 0; col < spans.NumColumns; col++)
                {
                    Widget w = WidgetAt(row, col);
                    if (w == null)
                        continue;

                    int colwidth = spans.FinalColumnWidth(col);


                    int ctrlwidth = w.LayoutInfo.ExpandX ? colwidth : w.LayoutInfo.Size.Width;
                    int ctrlheight = w.LayoutInfo.ExpandY ? rowheight : w.LayoutInfo.Size.Height;

                    int ctrlx, ctrly;
                    ComputeXYUsingAlign(w.LayoutInfo.Align, ctrlwidth, ctrlheight, curx, cury, colwidth, rowheight, out ctrlx, out ctrly);

                    w.CalcPositionsRecursive(ctrlx, ctrly, ctrlwidth, ctrlheight);
                    curx += colwidth + Gap;

                }

                cury += rowheight + Gap;
            }

            if (!(this is Window))	//dont alter window object sizes here
                base.CalcPositionsRecursive(left, top, width, height);
        }


        /// <summary>
        /// Computes the number of pixels a control which expans should get extra
        /// to divide evenly over the total space 'sizeleft'.
        /// </summary>
        private int GetExpand(int expandsize, int numexpanding, int index)
        {
            int res = expandsize / numexpanding;
            int pixelsleft = expandsize - res * numexpanding;

            if (index < pixelsleft)
                res++;

            return res;
        }

        private static void ComputeXYUsingAlign(LayoutAlign align, int ctrlw, int ctrlh, int allocx, int allocy, int allocw, int alloch, out int ctrlx, out int ctrly)
        {
            //horizontal:
            if ((align & LayoutAlign.Left) != 0)
                ctrlx = allocx;
            else if ((align & LayoutAlign.Right) != 0)
                ctrlx = allocx + allocw - ctrlw;
            else //center
                ctrlx = allocx + allocw / 2 - ctrlw / 2;

            //vertical:
            if ((align & LayoutAlign.Top) != 0)
                ctrly = allocy;
            else if ((align & LayoutAlign.Bottom) != 0)
                ctrly = allocy + alloch - ctrlh;
            else //center
                ctrly = allocy + alloch / 2 - ctrlh / 2;
        }


        internal DriverCompositeWidget PhysicalParentDriverObject
        {
            get
            {
                Widget cur = this;

                while (cur.DriverObject == null)
                {
                    if (cur.Parent == null)
                        return null;
                    cur = cur.Parent;
                }

                return cur.DriverObject as DriverCompositeWidget;
            }
        }

        /// <summary>
        /// Refreshes the layout for this composite widget and its children.
        /// </summary>
        public virtual void Refresh()
        {
            bool anyexpandx = false, anyexpandy = false;
            CalcLayoutInfoRecursive(ref anyexpandx, ref anyexpandy);
            CalcPositionsRecursive(0, 0, LayoutInfo.Size.Width,LayoutInfo.Size.Height);
        }
    }


    internal class GridSpan
    {
        public int size = 0; //width for columns and height for rows
        public int finalsize = 0;
        public bool expand = false;  //true if any expand in the perp. direction of the span (x for columns and y for rows)
    }

    internal class GridSpans
    {
        GridSpan[] rows;
        GridSpan[] columns;

        public GridSpans(int numrows, int numcolumns)
        {
            rows = new GridSpan[numrows];
            for (int l = 0; l < numrows; l++)
                rows[l] = new GridSpan();
            columns = new GridSpan[numcolumns];
            for (int l = 0; l < numcolumns; l++)
                columns[l] = new GridSpan();

        }

        public void Grow(int row, int col, Size2i siz, bool expandx, bool expandy)
        {
            if (expandx)
                columns[col].expand = true;
            if (expandy)
                rows[row].expand = true;

            rows[row].size = Math.Max(rows[row].size, siz.Height);
            columns[col].size = Math.Max(columns[col].size, siz.Width);
        }

        public void GrowFinalSize(int row, int col, int width, int height)
        {
            rows[row].finalsize = Math.Max(rows[row].finalsize, height);
            columns[col].finalsize = Math.Max(columns[col].finalsize, width);
        }


        public Size2i Size()
        {
            int w = 0, h = 0;
            foreach (GridSpan row in rows)
                h += row.size;

            foreach (GridSpan col in columns)
                w += col.size;

            return new Size2i(w, h);
        }

        public int NumExpandingColumns
        {
            get
            {
                int res = 0;
                foreach (GridSpan q in columns)
                    if (q.expand) res++;
                return res;
            }
        }

        public int NumExpandingRows
        {
            get
            {
                int res = 0;
                foreach (GridSpan q in rows)
                    if (q.expand)
                        res++;
                return res;
            }
        }

        public int NumColumns { get { return columns.Length; } }
        public int NumRows { get { return rows.Length; } }
        public int ColumnWidth(int col) { return columns[col].size; }
        public int RowHeight(int row) { return rows[row].size; }
        public int FinalColumnWidth(int col) { return columns[col].finalsize; }
        public int FinalRowHeight(int row) { return rows[row].finalsize; }

        public Size2i GetSize(int row, int col)
        {
            return new Size2i(columns[col].size, rows[row].size);
        }

       
    }

}
