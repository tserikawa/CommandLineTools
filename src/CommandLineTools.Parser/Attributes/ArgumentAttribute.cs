namespace CommandLineTools.Parser.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ArgumentAttribute : Attribute
{
    /// <summary>
    /// コマンドラインでの出現順序を表す数値
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// ヘルプテキスト
    /// </summary>
    public string? Help { get; set; }
    
    public ArgumentAttribute(int number)
    {
        Number = number;
    }
}