namespace CommandLineTools.Parser.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class RemainingArgumentsAttribute : Attribute
{
    /// <summary>
    /// ヘルプテキスト
    /// </summary>
    public string? Help{ get; set; }
}