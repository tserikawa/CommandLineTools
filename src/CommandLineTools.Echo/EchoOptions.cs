using CommandLineTools.Parser.Attributes;

namespace CommandLineTools.Echo;

public class EchoOptions
{
    /// <summary>
    /// 改行オプション
    /// </summary>
    [Flag("n", ShortName = "n")]
    public bool NoNewline { get; set; }
    
    /// <summary>
    /// 受け取った引数のリスト
    /// </summary>
    [RemainingArguments(Help = "Text to display")]
    public string[] Arguments { get; set; } = Array.Empty<string>();
}