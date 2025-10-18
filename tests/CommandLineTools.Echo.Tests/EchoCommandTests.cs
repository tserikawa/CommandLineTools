using CommandLineTools.Echo;

namespace CommandLineTools.Echo.Tests;

public class EchoCommandTests
{
    [Theory]
    [InlineData("Hello World\n", new string[] { "Hello", "World" })]
    [InlineData("Hello World", new string[] { "-n", "Hello", "World" })]
    [InlineData("\n", new string[] { })]
    [InlineData("", new string[] { "-n" })]
    public void Echo_WithVariousArguments_ProducesExpectedOutput(string expected, string[] args)
    {
        // Arrange
        using var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);
        
        // Assert
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
        Assert.Contains("Usage: echo", result);
        Assert.Contains("Arguments:", result);
        Assert.Contains("Options:", result);
        Assert.Contains("-n", result);
        Assert.Contains("-h, --help", result);
    }
}