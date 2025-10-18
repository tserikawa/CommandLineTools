using CommandLineTools.Parser.Attributes;
using System.Reflection;

namespace CommandLineTools.Parser;

public static class CommandLineParser
{
    public static T Parse<T>(string[] args) where T : new()
    {
        // 1. T型の新しいインスタンスを作成
        var instance = new T();
        
        // 2. プロパティからAttribute情報を取得
        var properties = typeof(T).GetProperties();
        
        // 3. 各プロパティのAttributeを処理
        foreach (var property in properties)
        {
            // FlagAttribute の処理
            var flagAttribute = property.GetCustomAttribute<FlagAttribute>();
            if (flagAttribute != null)
            {
                bool hasFlag = args.Contains("--" + flagAttribute.LongName) || 
                              (flagAttribute.ShortName != null && args.Contains("-" + flagAttribute.ShortName));
                
                if (hasFlag)
                {
                    property.SetValue(instance, true);
                }
            }
            
            // OptionAttribute の処理
            var optionAttribute = property.GetCustomAttribute<OptionAttribute>();
            if (optionAttribute != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    // 長い名前または短い名前と一致するかチェック
                    bool isLongMatch = args[i] == "--" + optionAttribute.LongName;
                    bool isShortMatch = optionAttribute.ShortName != null && 
                                       args[i] == "-" + optionAttribute.ShortName;
                    
                    if (isLongMatch || isShortMatch)
                    {
                        // 次の要素が値
                        if (i + 1 < args.Length)
                        {
                            string value = args[i + 1];
                            
                            // プロパティの型に応じて値を変換
                            var convertedValue = Convert.ChangeType(value, property.PropertyType);
                            property.SetValue(instance, convertedValue);
                        }
                        break;
                    }
                }
            }
            
            // ArgumentAttribute の処理
            var argumentAttribute = property.GetCustomAttribute<ArgumentAttribute>();
            if (argumentAttribute != null)
            {
                // 位置引数だけを抽出（-で始まらないもの）
                var positionalArgs = new List<string>();

                for (int i = 0; i < args.Length; i++)
                {
                    if (!args[i].StartsWith("-"))
                    {
                        positionalArgs.Add(args[i]);
                    }
                    else if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
                    {
                        // オプションの値はスキップ
                        i++;
                    }
                }

                // Number番目の位置引数を設定
                if (argumentAttribute.Number < positionalArgs.Count)
                {
                    var value = positionalArgs[argumentAttribute.Number];
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(instance, convertedValue);
                }
            }

            // RemainingArgumentsAttributeの処理
            var remainingArgumentsAttribute = property.GetCustomAttribute<RemainingArgumentsAttribute>();
            if(remainingArgumentsAttribute != null)
            {
                var arguments = new List<string>();
                for (int i = 0; i < args.Length; i++)
                {
                    if (!args[i].StartsWith("-"))
                    {
                        arguments.Add(args[i]);
                    }
                    else if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
                    {
                        i++;
                    }
                }
                property.SetValue(instance, arguments.ToArray());
            }
        }
        
        return instance;
    }
}