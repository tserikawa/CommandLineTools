namespace CommandLineTools.Parser.Attributes;

public class RangeAttribute : Attribute
{
    public int Min { get; set; }

    public int Max { get; set; }
}