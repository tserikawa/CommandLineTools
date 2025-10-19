using CommandLineTools.Parser;
using System.Reflection;

namespace CommandLineTools.Cat;

public class Program
{
    public static void Main(string[] args)
    {
        var options = CommandLineParser.Parse<CatOptions>(args);

        if (options.ShowHelp)
        {
            var programName = Assembly.GetExecutingAssembly()?.GetName().Name;
            Console.WriteLine(CommandLineParser.GenerateHelp<CatOptions>(programName));
            return;
        }

        var fileNames = options.Files;
        foreach(var fileName in fileNames)
        {
            try
            {
                using var sr = new StreamReader(fileName);
                while(!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"ファイル読み込みでエラーが発生しました。{ex.Message}");
            }
        }
    }
}
