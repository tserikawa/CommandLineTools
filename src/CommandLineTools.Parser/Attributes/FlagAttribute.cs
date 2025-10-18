namespace CommandLineTools.Parser.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class FlagAttribute : Attribute
{
    /// <summary>
    /// 正式名称
    /// </summary>
    public string? LongName { get; set; }

    /// <summary>
    /// 省略名称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="longName">正式名称</param>
    public FlagAttribute(string? longName)
    {
        LongName = longName;
    }
}