// It's really up to you whether to make it static or not.
public static class PointFactory
{
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }
    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }
}

public class Point
{
    private double x, y;
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // factory method: The key point about this is that the name of the factory method is not directly tied to the name of the containing class.
    //public static Point NewCartesianPoint(double x, double y)
    //{
    //    return new Point(x, y);
    //}

    //public static Point NewPolarPoint(double rho, double theta)
    //{
    //    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    //}
}

public class Demo
{
    static void Main(string[] args)
    {
        var p = new Point();
        var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);
    }
}