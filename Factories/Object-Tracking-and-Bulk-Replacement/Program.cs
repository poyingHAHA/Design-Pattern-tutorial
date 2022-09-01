using System.Text;

public interface ITheme
{
    string TextColor { get; }
    string BgrColor { get; }
}

class LightTheme : ITheme
{
    public string TextColor => "black";
    public string BgrColor => "White";
}

class DarkTheme : ITheme
{
    public string TextColor => "White";
    public string BgrColor => "dark gray";
}

public class TrackingThemeFactory
{
    private readonly List<WeakReference<ITheme>> themes = new();
    public ITheme CreateTheme(bool dark)
    {
        // So now when we create a theme, not only do we return, we also wrap it with a weak reference and add it to this particular list.
        // So this is something that allows us to output diagnostic information about all the objects that have been constructed by this particular factory.
        ITheme theme = dark ? new DarkTheme() : new LightTheme();
        themes.Add(new WeakReference<ITheme>(theme));
        return theme;
    }
    public string Info
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var reference in themes)
            {
                // So we're going to try getting the theme that the weak reference points to.
                if (reference.TryGetTarget(out var theme))
                {
                    // And if we do have it, we can check whether or not it's dark.
                    bool dark = theme is DarkTheme;
                    sb.Append(dark ? "Dark" : "Light")
                        .AppendLine(" theme");
                }
            }
            return sb.ToString();
        }
    }
}

// Now this class is completely useless. It's just a wrapper and doesn't do anything. And if you want to work with it, you end up having to go into value to get the actual type.
// But what it does give us is an ability to create a factory which not only tracks the object it has constructed, but is able to perform bulk replacement.
public class Ref<T> where T : class
{
    public T Value;
    public Ref(T value)
    {
        Value = value;
    }
}

public class ReplaceableThemeFactory
{
    // So here we're going to have another private list of weak references, but those are going to be different weak references.
    // So the idea here is once again that we're going to construct the different themes using a factory method, but we're going to return them as a ref as opposed to a new theme.
    private readonly List<WeakReference<Ref<ITheme>>> themes = new();
    private ITheme CreateThemeImpl(bool dark)
    {
        return dark ? new DarkTheme() : new LightTheme();
    }

    public Ref<ITheme> CreateTheme(bool dark)
    {
        // Now, first of all, we would create the underlying object, but we need to wrap it with a reference.
        var r = new Ref<ITheme>(CreateThemeImpl(dark));
        // I'm using the shorthand new here because the full specify type looks like this "new WeakReference<Ref<ITheme>>(r)", but I don't want to keep typing all of that.
        themes.Add(new(r));
        return r;
        // So now we're getting a ref whenever we actually use this, and this is what it becomes a bit annoying
        // because now you have to unwrap the ref, you have to go into its value.
        // But there is a major benefit to this, and the benefit is that we can perform a bulk replacement,
        // replacing every single theme that has been created and given out to the user with a different theme.
    }

    public void ReplaceTheme(bool dark)
    {
        // So what we're going is we're going into each of the refs that have been given out from the factory and
        // we're changing its value, therefore affecting every single object in the system that has been given out through this factory.
        foreach (var wr in themes)
        {
            if(wr.TryGetTarget(out var reference))
            {
                reference.Value = CreateThemeImpl(dark);
            }
        }
    }

}

public class Program
{
    public static void Main()
    {
        var factory = new TrackingThemeFactory();
        var theme1 = factory.CreateTheme(false);
        var theme2 = factory.CreateTheme(true);
        var theme3 = factory.CreateTheme(false);
        Console.WriteLine(factory.Info);

        var factory2 = new ReplaceableThemeFactory();
        var magicTheme = factory2.CreateTheme(true);
        Console.WriteLine(magicTheme.Value.BgrColor);
        factory2.ReplaceTheme(false);
        Console.WriteLine(magicTheme.Value.BgrColor);
    }
}