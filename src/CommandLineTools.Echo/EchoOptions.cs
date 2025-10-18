using CommandLineTools.Parser.Attributes;

namespace CommandLineTools.Echo;

public class EchoOptions
{
    [Flag("n")]
    public bool NoNewline { get; set; }
    
    [RemainingArguments(Help = "Text to display")]
    public string[] Arguments { get; set; } = Array.Empty<string>();
}