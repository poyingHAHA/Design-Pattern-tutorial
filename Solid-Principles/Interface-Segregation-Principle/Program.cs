public class Document
{

}

public interface IPrinter
{
    void Print(Document d);
}

public interface IScanner
{
    void Scan(Document d);
}

public interface IMultiFunctionDevice : IScanner, IPrinter // ...
{

}

public class MultiFunctionMechine : IMultiFunctionDevice
{
    private IPrinter printer;
    private IScanner scanner;
    public MultiFunctionMechine(IPrinter printer, IScanner scanner)
    {
        if(printer == null)
        {
            throw new ArgumentNullException(paramName: nameof(printer));
        }
        if(scanner == null)
        {
            throw new ArgumentNullException(paramName: nameof(scanner));
        }

        this.printer = printer;
        this.scanner = scanner;
    }

    public void Print(Document d)
    {
        printer.Print(d);
    }
    public void Scan(Document d)
    {
        scanner.Scan(d);
    }
}


public class OldFashionedPrinter : IPrinter
{
    public void Print(Document d)
    {
        //
    }
}

public class Demo
{
    static void Main(string[] args)
    {

    }
}
