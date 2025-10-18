using CommandLineTools.Echo;

namespace CommandLineTools.Echo.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData( "Hello World\n", new string[] { "Hello", "World" })]
    [InlineData( "Hello World", new string[] { "-n", "Hello", "World" })]
    public void Test1(string expected, string[] args)
    {
        // コンソール出力を受け取る
        using var output = new StringWriter();
        Console.SetOut(output);

        // コンソールアプリを実行する
        Program.Main(args);
        
        // 結果を確認する
        Assert.Equal(expected, output.ToString());
    }
}
