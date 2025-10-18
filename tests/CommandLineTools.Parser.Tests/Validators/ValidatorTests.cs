using Xunit;
using CommandLineTools.Parser.Validators;
using CommandLineTools.Parser.Tests.TestModels;

namespace CommandLineTools.Parser.Tests.Validators;

public class ValidatorTests
{
    [Fact]
    public void Validate_WhenAgeIsOutOfRange_ReturnsError()
    {
        var person = new Person()
        {
            Age = 200,
            Name = "Alice"
        };

        var errors = CommandLineTools.Parser.Validators.Validators.Validate(person);
        
        Assert.NotEmpty(errors);
        Assert.Contains("Age=200が最大値を超えています", errors[0]);
    }

    [Fact]
    public void Validate_WhenNameIsNull_ReturnsError()
    {
        var person = new Person()
        {
            Age = 20,
            Name = null
        };

        var errors = CommandLineTools.Parser.Validators.Validators.Validate(person);
        
        Assert.NotEmpty(errors);
        Assert.Contains("名前は必須です", errors[0]);
    }
}