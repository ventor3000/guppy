
namespace GuppyGUI
{
  public struct Margin
  {
    public readonly int Left;
    public readonly int Right;
    public readonly int Top;
    public readonly int Bottom;


    public static readonly Margin Empty = new Margin(0);

    public Margin(int left, int top, int right, int bottom)
    {
      Left = left;
      Right = right;
      Top = top;
      Bottom = bottom;
    }

    public Margin(int size)
    {
      Left = size;
      Top = size;
      Right = size;
      Bottom = size;
    }

    public int Horizontal
    {
      get { return Left + Right; }
    }

    public int Vertical
    {
      get
      {
        return Top + Bottom;
      }
    }
  }
}
