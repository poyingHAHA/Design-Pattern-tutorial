using System.Text;

class Demo
{
    static void Main(string[] args)
    {
        var hello = "hello";
        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(hello);
        sb.Append("</p>");
        Console.WriteLine(sb);

        var words = new[] { "hello", "world" };
        sb.Clear();
        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.AppendFormat("<li>{0}</li>", word);
        }
        sb.Append("</ul>");
        Console.WriteLine(sb);
    }
}