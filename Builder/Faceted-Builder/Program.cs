public class Person
{
    // address
    public string StreetAddress, Postcode, City;

    // employment
    public string CompanyName, Position;
    public int AnnualIncome;

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}"; 
    }
}

// The PersonBuilder is a facade for other builders,do it doesn't actually build up person by itself, but it keeps a reference to the person that's being built up.
// And the second thing is it allows you access to those sub builders, if you will.
public class PersonBuilder // facade
{
    // It's a reference object. This is very important because if you were building up a struct, you would have a lot of problems with this approach.
    protected Person person = new Person();
    // we use a public property "Works" to return PersonJobBuilder
    public PersonJobBuilder Works => new PersonJobBuilder(person);
    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    // So what I'm doing is I introduce an implicit conversion operator to person and this allows me to write "Person person = pb..." instead of "var person = pb...."
    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.person;
    }
}

// a person job builder is designed for building up job information on a person object.
public class PersonJobBuilder : PersonBuilder
{
    // what's happening here is that you're passing into the builder, the object that you're building up
    //  and you're storing it in the field that you inherited from PersonBuilder.
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
        person.AnnualIncome = amount;
        return this;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }
    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }
    public PersonAddressBuilder WithPostcode(string postcode)
    {
        person.Postcode = postcode;
        return this;
    }

    public PersonAddressBuilder In(string City)
    {
        person.City = City;
        return this;
    }
} 

public class Demo
{
    static void Main(string[] args)
    {
        var pb = new PersonBuilder();
        Person person = pb
            .Lives.At("123 Taiwan Road")
                  .In("Taiwan")
                  .WithPostcode("505")
            .Works.At("Google")
                  .AsA("Engineer")
                  .Earning(123000);

        Console.WriteLine(person);
    }
}