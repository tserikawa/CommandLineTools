using CommandLineTools.Parser.Attributes;

namespace CommandLineTools.Echo;

public class EchoOptions
{
    /// <summary>
    /// 改行オプション
    /// </summary>
    [Flag("n", ShortName = "n", Help = "Do not output the trailing newline")]
    public bool NoNewline { get; set; }

    /// <summary>
    /// 受け取った引数のリスト
    /// </summary>
    [RemainingArguments(Help = "Text to display")]
    public string[] Arguments { get; set; } = Array.Empty<string>();

    /// <summary>
    /// ヘルプを表示するフラグ
    /// </summary>
    [Flag("help", ShortName = "h", Help = "Show this help message")]
    public bool ShowHelp { get; set; }
}