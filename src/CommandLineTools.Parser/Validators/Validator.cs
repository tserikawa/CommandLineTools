using CommandLineTools.Parser.Attributes;
using System.Reflection;

namespace CommandLineTools.Parser.Validators;

public static class Validators
{
    public static List<string> Validate(object obj)
    {
        var messages = new List<string>();

        Type t = obj.GetType();
        PropertyInfo[] properties = t.GetProperties();
        foreach (var property in properties)
        {
            var rangeAttributes = property.GetCustomAttribute<RangeAttribute>();
            if (rangeAttributes != null)
            {
                var value = property.GetValue(obj) as int?;

                if (value != null)
                {
                    if (value < rangeAttributes.Min)
                    {
                        messages.Add($"プロパティ{property.Name}={value}が最小値未満です。");
                    }
                    else if (value > rangeAttributes.Max)
                    {
                        messages.Add($"プロパティ{property.Name}={value}が最大値を超えています。");
                    }
                }
                else
                {
                    messages.Add($"属性RangeAttributesはint型にのみ使用できます。");
                }
            }

            var requiredAttributes = property.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttributes != null)
            {
                if (property.GetValue(obj) == null)  // ← この条件が必要！
                {
                    messages.Add(requiredAttributes.ErrorMessage ?? $"プロパティ{property.Name}はnullにできません。");
                }
            }
        }

        return messages;
    }
}