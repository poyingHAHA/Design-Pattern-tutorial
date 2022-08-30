public class Rect
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Rect()
    {
    }

    public Rect(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public override string? ToString()
    {
        return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }
}

public class Square : Rect
{
    public new int Width { 
        set { base.Width = base.Height = value; }
    }
    public new int Height
    {
        set { base.Width = base.Height = value; }
    }
}

public class Demo
{
    static public int Area(Rect r) => r.Width * r.Height;    
    static void Main(string[] args)
    {
        Rect rc = new Rect(2, 3);
        Console.WriteLine($"{rc} has area {Area(rc)}");
    }
}