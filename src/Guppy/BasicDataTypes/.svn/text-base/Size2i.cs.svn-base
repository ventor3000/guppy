
namespace GuppyGUI
{
  public struct Size2i
  {
    public readonly int Width;
    public readonly int Height;

    public static readonly Size2i Empty = new Size2i(0, 0);
    public static readonly Size2i Natural = new Size2i(-1, -1);



    public Size2i(int width, int height)
    {
      this.Width = width;
      this.Height = height;
    }

    public Size2i GrowNew(int grow_width, int grow_height)
    {
      return new Size2i(Width + grow_width,Height+grow_height);
    }

    public static bool operator == (Size2i s1,Size2i s2) {
      if ((object)s1 == null)
      {
        if ((object)s2 == null)
          return true;
        return false;
      }
      if ((object)s2 == null)
        return false;

      return s1.Width == s2.Width && s1.Height == s2.Height;
    }

    public override bool Equals(object obj)
    {
      if (obj is Size2i)
        return (((Size2i)obj) == this);

      return base.Equals(obj);
    }

    public static bool operator !=(Size2i s1, Size2i s2)
    {
      return !(s1==s2);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public Size2i Grow(int dx, int dy)
    {
      return new Size2i(Width + dx, Height + dy);
    }


    public override string ToString()
    {
      return Width.ToString() + "," + Height.ToString();
    }
  }
}
