
public class Person
{
    public string Name;
    public string Position;
    public class Builder : PersonJobBuilder<Builder> { }
    // Have a property called new, and every time you get a new, you try to construct a new person. It gives you a new builder.
    public static Builder New => new Builder();  
    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersonBuilder
{
    protected Person person = new Person();

    public Person Build()
    {
        return person;
    }
}

public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
{
    public SELF called(string name)
    {
        person.Name = name;
        return (SELF) this;
    }
}

// So you can see that as you go ahead and you inherit builders from other builders.
// It's going to be a big list of types or what's called a type list in languages such as C++.
public class PersonJobBuilder<SELF>: PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
{
    public SELF WorkAsA(string position)
    {
        person.Position = position;
        return (SELF)this;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var me = Person.New.called("Edwin").WorkAsA("Engineer").Build();
        Console.WriteLine(me);
    }
}