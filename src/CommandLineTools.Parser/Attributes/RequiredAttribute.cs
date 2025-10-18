namespace CommandLineTools.Parser.Attributes;

public class RequiredAttribute : Attribute
{
    public string? ErrorMessage { get; set; }
}