
public class Point
{
    private double x, y;
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // But if you do need to instantiate a new object, anytime somebody asks for something, then a property is a good alternative to a factory method.
    public static Point Origin => new Point(0, 0);
    public static Point Origin2 = new Point(0, 0); // better


    // The only way point factory can access the private constructor is if point factory is, in fact an inner class of point.
    // So now we have a private constructor for the point, but we can use this constructor because we are in our class of point.
    public static class Factory
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
}

public class Demo
{
    static void Main(string[] args)
    {
        //var p = new Point();
        var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);
    }
}