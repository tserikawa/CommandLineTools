using CommandLineTools.Parser.Attributes;

namespace CommandLineTools.Parser.Tests.TestModels;

public class Person
{
    [Range(Min = 1, Max = 100)]
    public int Age { get; set; }

    [Required(ErrorMessage = "名前は必須です")]
    public string? Name { get; set; }
}