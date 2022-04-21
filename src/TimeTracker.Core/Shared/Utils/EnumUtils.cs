using System.ComponentModel;
using System.Text;

namespace TimeTracker.Core.Shared.Utils;

public static class EnumUtils
{
    public static bool BelongToType<T>(string text) where T : Enum
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
           return false;

        var itemSet = new Dictionary<string, T>();
        var list = Enum.GetValues(typeof(T)) as T[];
        foreach (var item in list!)
        {
            itemSet[item.ToString()] = item;
        }

        return itemSet.ContainsKey(text);
    }
    
    
    public static T ToEnum<T>(this string text) where T : Enum
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(T), "invalid value for enum");
        ;

        var itemSet = new Dictionary<string, T>();
        var list = Enum.GetValues(typeof(T)) as T[];
        foreach (var item in list!)
        {
            itemSet[item.ToString()] = item;
        }

        if (itemSet.ContainsKey(text))
        {
            return itemSet[text];
        }

        throw new ArgumentOutOfRangeException(nameof(T), "invalid value for enum");
    }

    public static string Description<T>(this T e) where T : Enum
    {
        var info = e.GetType().GetField(e.ToString());
        var attributes = (DescriptionAttribute[]) info.GetCustomAttributes(typeof(DescriptionAttribute), false);

        // ?? is equivalent to ?: of kotlin 
        return attributes[0]?.Description ?? e.ToString();
    }

    public static T[] ToList<T>()
    {
        return (T[]) Enum.GetValues(typeof(T));
    }

    public static string ToSpacedSentence<T>(this T e, bool preserveAcronyms = false) where T : Enum
    {
        var text = e.ToString();
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        var newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
                if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                     i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(' ');
            newText.Append(text[i]);
        }

        return newText.ToString();
    }
}