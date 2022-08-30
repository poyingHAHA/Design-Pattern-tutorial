
using System.Diagnostics;

public class Journal
{
    private readonly List<string> entries = new List<string>();
    private static int count = 0;
    
    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count;
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }

    // too many responsibilities
    //public void Save(string filename){}
    //public void Load(string filename){}
}

public class Persistence
{
    public void SaveToFile(Journal j, string filename, bool overwrite = false)
    {
        if(overwrite || File.Exists(filename)) File.WriteAllText(filename, j.ToString());
    }
}

public class Demo
{
    static void Main(string[] args)
    {
        var j = new Journal();
        j.AddEntry("I cried today");
        j.AddEntry("I ate a bug");
        Console.WriteLine(j);

        var p = new Persistence();
        var filename = @"E:/temp/journal.txt";
        p.SaveToFile(j, filename, true);
        Process.Start(filename);
    }
}