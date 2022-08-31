
public class Person
{
    public string Name;
    public string Position;
    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public class PersonInfoBuilder
{
    protected Person person = new Person();
    public PersonInfoBuilder called(string name)
    {
        person.Name = name;
        return this;
    }
}

public class PersonJobBuilder: PersonInfoBuilder
{
    public PersonJobBuilder WorkAsA(string position)
    {
        person.Position = position;
        return this;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var builder = new PersonJobBuilder();
        builder.called("Edwin").WorksAsA("Engineer"); 
    }
}