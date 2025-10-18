using CommandLineTools.Parser;

namespace CommandLineTools.Echo;

public class Program
{
    public static void Main(string[] args)
    {
        var options = CommandLineParser.Parse<EchoOptions>(args);
        var text = string.Join(" ", options.Arguments);
        if(options.NoNewline)
        {
            Console.Write(text);
        }
        else
        {
            Console.WriteLine(text);
        }
    }
}