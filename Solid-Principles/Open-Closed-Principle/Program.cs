
public enum Color
{
    Red, Green, Blue
}

public enum Size
{
    Small, Medium, Large, Huge
}

public class Product
{
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {
        if(name == null)
        {
            throw new ArgumentNullException(paramName: nameof(name));
        }
        Name = name;
        Color = color;
        Size = size;
    }
}

public class ProductFilter
{
    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        foreach (Product product in products)
            if(product.Size == size)
                yield return product;
    }

    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        foreach (Product product in products)
            if (product.Color == color)
                yield return product;
    }

    public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
    {
        foreach (Product product in products)
            if (product.Color == color && product.Size == size)
                yield return product;
    }
}

public class Demo
{
    static void Main(string[] args)
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Huge);

        Product[] products = { apple, tree, house };

        var pf = new ProductFilter();
        Console.WriteLine("Green products (old): ");
        foreach(var p in pf.FilterByColor(products, Color.Green))
        {
            Console.WriteLine($" - {p.Name} is green");
        }
    }
}