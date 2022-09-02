﻿public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This tea is nice but I'd prefer it with milk.");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This coffee is sensational!");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

public class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Put in a tea bag, boil eater, pour {amount} ml, add lemon, enjoy");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    //public enum AvailableDrink
    //{
    //    Coffee, Tea
    //}
    //private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

    //public HotDrinkMachine()
    //{
    //    foreach(AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //    {
    //        // we need to use create instance because we're going to make the actual factory by name.
    //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
    //            Type.GetType(GetType().Namespace + Enum.GetName(typeof(AvailableDrink), drink) + "Factroy")
    //        );
    //        factories.Add(drink, factory);
    //    }
    //}
    //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    //{
    //    return factories[drink].Prepare(amount);
    //}

    private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();
    public HotDrinkMachine()
    {
        // So this is an approach where it's going to look in the assembly and find all the different types of factories and add them to the list.
        foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
        {
            if(typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
            {
                factories.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t)));
            }
        }
    }
    public IHotDrink MakeDrink()
    {
        Console.WriteLine("Avaliable drinks: ");
        for(var index=0; index < factories.Count; index++)
        {
            var tuple = factories[index];
            Console.WriteLine($"{index}: {tuple.Item1}");
        }

        while (true)
        {
            string s;
            if((s = Console.ReadLine()) != null 
                && int.TryParse(s, out int i)
                && i >= 0
                && i < factories.Count
              )
            {
                Console.Write("Specify amount: ");
                s = Console.ReadLine();
                if(s != null && int.TryParse(s, out int amount) && amount > 0)
                {
                    return factories[i].Item2.Prepare(amount);
                }
            }

            Console.WriteLine("Incorrect input, try again!");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink();
        drink.Consume();
    }
}