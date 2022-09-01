public class Person
{
    public string Name, Position;
}

public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    private readonly List<Func<TSubject, TSubject>> actions = new List<Func<TSubject, TSubject>>();

    private TSelf AddAction(Action<TSubject> action)
    {
        actions.Add(p => { 
            action(p); 
            return p; 
        });
        return (TSelf)this;
    }

    public TSelf Do(Action<TSubject> action) => AddAction(action);
    public TSubject Build() => actions.Aggregate(new TSubject(), (p, f) => f(p));
}

public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name) => Do(p => p.Name = name);
}


// sealed means you cannot inherit from it. And as a result, if you want to extend this somehow you don't have the ability to use inheritance.
//public sealed class PersonBuilder
//{
//    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

//    private PersonBuilder AddAction(Action<Person> action)
//    {
//        actions.Add(p => { 
//            action(p); 
//            return p; 
//        });
//        return this;
//    }

//    public PersonBuilder Do(Action<Person> action) => AddAction(action);
//    public PersonBuilder Called(string name) => Do(p => p.Name = name);
//    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
//}

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);
}

public class Program
{
    public static void Main(string[] args)
    {
        var person = new PersonBuilder()
            .Called("Edwin")
            .WorksAs("Developer")
            .Build();
    }
}