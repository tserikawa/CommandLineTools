using CommandLineTools.Cat;

namespace CommandLineTools.Cat.Tests;

public class CatCommandTests
{

    [Fact]
    public void Cat_Empty()
    {
        var args = new string[] { "../../../TestFiles/empty.txt" };
        var expected = string.Empty;

        using var output = new StringWriter();
        Console.SetOut(output);
        Program.Main(args);
        Assert.Equal(expected, output.ToString());
    }

    [Fact]
    public void Cat_SingleLine_NoOptions()
    {
        var args = new string[] { "../../../TestFiles/fox.txt" };
        var expected = "The quick brown fox jumps over the lazy dog.\n";

        using var output = new StringWriter();
        Console.SetOut(output);
        Program.Main(args);
        Assert.Equal(expected, output.ToString());
    }

    [Fact]
    public void Cat_MultipleLines_NoOptions()
    {
        var args = new string[] { "../../../TestFiles/spiders.txt" };
        var expected = "Don't worry, spiders,\nI keep house\ncasually.\n";

        using var output = new StringWriter();
        Console.SetOut(output);
        Program.Main(args);
        Assert.Equal(expected, output.ToString());
    }

    [Theory]
    [InlineData("-h")]
    [InlineData("--help")]
    public void Echo_WithHelpOption_DisplaysHelpMessage(string helpOption)
    {
        // Arrange
        using var output = new StringWriter();
        Console.SetOut(output);
        
        // Act
        Program.Main(new[] { helpOption });
        
        // Assert
        var result = output.ToString();
        Assert.Contains("Usage: cat", result);
        Assert.Contains("Arguments:", result);
        Assert.Contains("Options:", result);
        Assert.Contains("-h, --help", result);
    }
}
