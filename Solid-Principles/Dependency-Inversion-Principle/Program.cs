public enum Relationship
{
    Parent,
    Child,
    Sibling
}

public class Person
{
    public string Name;
    // public DateOfBirth;
}

// So this concept will actually give us particular information, like suppose I want to be able to find the children of a particular person.
// So what I can do is I can put here a member which returns an on IEnumerable person and is going to be called
// FindAllChildrenOf and you specify the name of the person whose children you actually want to find.
public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllChildrenOf(string name);
}

//low-level
public class Relationships : IRelationshipBrowser
{
    private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)> ();

    public void AddParentAndChild(Person parent, Person child)
    {
        relations.Add ((parent, Relationship.Parent, child));
        relations.Add ((child, Relationship.Child, parent));
    }

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        return relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(x => x.Item3);
    }

    //public List<(Person, Relationship, Person)> Relations => relations;
}

public class Research {
    //public Research(Relationships relationships)
    //{
    //    var relations = relationships.Relations;
    //    foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
    //    {
    //        Console.WriteLine($"John has a child called {r.Item3.Name}");
    //    }
    //}

    // But this time around we don't depend on relationships. Instead, we depend on this IRelationshipB rowser interface called browser.
    public Research(IRelationshipBrowser browser)
    {
        foreach(var p in browser.FindAllChildrenOf("John"))
            Console.WriteLine($"John has a child called {p.Name}");
    }

    static void Main(string[] args)
    {
        var parent = new Person { Name = "John" };
        var child1 = new Person { Name = "Chris" };
        var child2 = new Person { Name = "Mary" };

        var relationships = new Relationships();
        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        // And of course if I execute this I get exactly the same result, but I get it without the dependency on low level implementation details.
        // So now what happens is relationships can actually go ahead and change the way that it stores the relationships.
        // It can change the underlying data structure because it's never exposed to the high level modules which
        new Research(relationships);
    }
}