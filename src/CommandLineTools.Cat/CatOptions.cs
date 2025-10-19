using CommandLineTools.Parser.Attributes;

namespace CommandLineTools.Cat;

public class CatOptions
{
    // TODO: コマンド固有のオプションを定義してください
    // 例:
    // [Flag("verbose", ShortName = "v")]
    // public bool Verbose { get; set; }

    /// <summary>
    /// 読み込むファイル名のリスト
    /// </summary>
    [RemainingArguments(Help = "Input files")]
    public string[] Files { get; set; } = Array.Empty<string>();
    
    /// <summary>
    /// ヘルプを表示するフラグ
    /// </summary>
    [Flag("help", ShortName = "h", Help = "Show this help message")]
    public bool ShowHelp { get; set; }
}
