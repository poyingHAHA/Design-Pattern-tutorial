public class Person
{
    public string Name, Position;
}

// sealed means you cannot inherit from it. And as a result, if you want to extend this somehow you don't have the ability to use inheritance.
public sealed class PersonBuilder
{
    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    // It's a bit of trick, when you take an action and you turn it into a func.
    // And the reason why you would do this is to preserve a fluent interface.
    // And the reason why you would preserve a fluent interface is because at some point we want to use the
    // aggregate link method in order to apply all the functions one after another.
    private PersonBuilder AddAction(Action<Person> action)
    {
        actions.Add(p => { 
            action(p); 
            return p; 
        });
        return this;
    }

    public PersonBuilder Do(Action<Person> action) => AddAction(action);
    // This is how we would specify a person's name
    public PersonBuilder Called(string name) => Do(p => p.Name = name);
    // So aggregate is something that is going to compact a list into a single application, we're starting with an empty person.
    // And for every step you have a pair, you have a pair where you have a person as well as a function to apply to this person.
    // So this is how you would actually apply every single action to a person.
    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
}

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