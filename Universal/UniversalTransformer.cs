using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Universal;

public static class UniversalTransformer
{
    public static List<char> SpecialCharcters =
    [
        //"🌐", /*I WANTED TO USE THIS EMOJI...BUT LENGTH IS 2. */
        //"↕️", /*I WANTED TO USE THIS EMOJI...BUT LENGTH IS 2. */
        '✿',
        '﴾',
        '﴿',
        'ー',
        'ヾ',
        '＃',
        '＄',
        '％',
        '＆',
        '＊',
        '＊',
        '／',
        '：',
        '：',
        '；',
        '＼',
        '＾',
        '～',
        '￤',
        '‘',
        '“',
        '【',
        '】',
        '〘',
        '〙',
        '＋',
        '＝',
        '≪',
        '≪',
        '▶',
        '◉',
        '❓',
        '❗',
        '❝',
        '❞',
    ];
    public static string GeminiSuperSerifBoldItalicTransform(string text, bool autoUpcase = false)
    {
        uint _CPOC_(char c)
        {
            return UniversalEncoding.CodePointOfCharacter(c);
        }
        // This is a placeholder for a function that would convert text to a super serif bold italic style.
        // The actual implementation would depend on the specific Unicode characters used for this style.
        // For demonstration purposes, we'll just return the original text.
        Dictionary<uint, string> conversionMap = new Dictionary<uint, string>
        {
            // Serif Bold Italic: 𝑨 𝑩 𝑪 𝑫 𝑬 𝑭 𝑮 𝑯 𝑰 𝑱 𝑲 𝑳 𝑴 𝑵 𝑶 𝑷 𝑸 𝑹 𝑺 𝑻 𝑼 𝑽 𝑾 𝑿 𝒀 𝒁
            {_CPOC_('A'), "𝑨"}, {_CPOC_('B'), "𝑩" }, {_CPOC_('C'), "𝑪"}, {_CPOC_('D'), "𝑫"}, {_CPOC_('E'), "𝑬"},
            {_CPOC_('F'), "𝑭"}, {_CPOC_('G'), "𝑮"}, {_CPOC_('H'), "𝑯"}, {_CPOC_('I'), "𝑰"}, {_CPOC_('J'), "𝑱"},
            {_CPOC_('K'), "𝑲"}, {_CPOC_('L'), "𝑳" }, {_CPOC_('M'), "𝑴"}, {_CPOC_('N'), "𝑵"}, {_CPOC_('O'), "𝑶"},
            {_CPOC_('P'), "𝑷"}, {_CPOC_('Q'), "𝑸"}, {_CPOC_('R'), "𝑹"}, {_CPOC_('S'), "𝑺"}, {_CPOC_('T'), "𝑻"},
            {_CPOC_('U'), "𝑼"}, {_CPOC_('V'), "𝑽"}, {_CPOC_('W'), "𝑾"}, {_CPOC_('X'), "𝑿" }, {_CPOC_('Y'), "𝒀" },
            {_CPOC_('Z'), "𝒁"},
            // Serif Bold Italic: 𝒂 𝒃 𝒄 𝒅 𝒆 𝒇 𝒈 𝒉 𝒊 𝒋 𝒌 𝒍 𝒎 𝒏 𝒐 𝒑 𝒒 𝒓 𝒔 𝒕 𝒖 𝒗 𝒘 𝒙 𝒚 𝒛
            {_CPOC_('a'), "𝒂"}, {_CPOC_('b'), "𝒃" }, {_CPOC_('c'), "𝒄"}, {_CPOC_('d'), "𝒅"}, {_CPOC_('e'), "𝒆"},
            {_CPOC_('f'), "𝒇"}, {_CPOC_('g'), "𝒈"}, {_CPOC_('h'), "𝒉"}, {_CPOC_('i'), "𝒊"}, {_CPOC_('j'), "𝒋"},
            {_CPOC_('k'), "𝒌"}, {_CPOC_('l'), "𝒍" }, {_CPOC_('m'), "𝒎"}, {_CPOC_('n'), "𝒏"}, {_CPOC_('o'), "𝒐"},
            {_CPOC_('p'), "𝒑"}, {_CPOC_('q'), "𝒒"}, {_CPOC_('r'), "𝒓"}, {_CPOC_('s'), "𝒔"}, {_CPOC_('t'), "𝒕"},
            {_CPOC_('u'), "𝒖"}, {_CPOC_('v'), "𝒗"}, {_CPOC_('w'), "𝒘"}, {_CPOC_('x'), "𝒙" }, {_CPOC_('y'), "𝒚" },
            {_CPOC_('z'), "𝒛"}
        };
        if (autoUpcase) text = text.ToUpper();
        uint[] codePoints = UniversalEncoding.ToCodePoints(text);
        List<string> convertedCharacters = new List<string>();
        for (int i = 0; i < codePoints.Length; i++)
        {
            uint codePoint = codePoints[i];
            if (conversionMap.ContainsKey(codePoint))
            {
                convertedCharacters.Add(conversionMap[codePoint]);
            }
            else
            {
                convertedCharacters.Add(UniversalEncoding.FromCodePoints([codePoint]));
            }
        }
        text = string.Join("", convertedCharacters);
        return text;
    }
    public static bool UnicodeEscapeIsNeeded(char c, bool everything = false)
    {
        if (everything) return (c > 127);
        return (c > 127 && !SpecialCharcters.Contains(c));
    }

    public static string UnicodeEscape(string text, bool everything = false)
    {
        var sb = new StringBuilder();
        sb.Length = 0;
        if (sb.Capacity < text.Length + (text.Length / 10))
        {
            sb.Capacity = text.Length + (text.Length / 10);
        }

        foreach (char c in text)
        {
            if (UnicodeEscapeIsNeeded(c, everything))
            {
                ushort val = c;
                sb.Append("\\u").Append(val.ToString("X4"));
            }
            else
            {
                sb.Append(c);
            }
        }

        string result = sb.ToString();
        sb.Length = 0;
        return result;
    }

    public static string UnicodeUnescape(string text)
    {
        return Regex.Unescape(text);
    }

    public static List<int> FindCharacterOccurrences(string input, char targetChar)
    {
        List<int> occurrences = input
            .Select((character, index) => new { character, index })
            .Where(item => item.character == targetChar)
            .Select(item => item.index)
            .ToList();
        return occurrences;
    }

    public static string ReplaceSurrogatePair(string str, string replaceSurrogate = "✅")
    {
        if (replaceSurrogate == "")
        {
            return str;
        }

        str = Regex.Replace(str, @"[\uD800-\uDFFF]", "{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}");
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}",
            replaceSurrogate);
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}", replaceSurrogate);
        return str;
    }

    public static string JustOneLineFeed(string code)
    {
        code = code.Replace("\r\n", "\n");
        code = code.Replace("\r", "\n");
        return code;
    }

    public static string JustOneSpace(string str)
    {
        str = Regex.Replace(str, @"\s+", " ");
        str = Regex.Replace(str, @"[ ]*↩[ ]*", "↩");
        return str;
    }

    public static string SafeBaseName(
        string baseName,
        bool followRecommendation = false
    )
    {
        // [⭕ファイル名に使えない文字 - Google](https://bit.ly/invalid-filename-chars)
        // \ / : * ? " < > |
        baseName = baseName
                .Replace("\\", "＼")
                .Replace("/", "／")
                .Replace(":", "：")
                .Replace("*", "＊")
                .Replace("?", "❓")
                .Replace("\"", "“")
                .Replace("<", "≪")
                .Replace(">", "≫")
                .Replace("|", "￤")
            ;
        if (followRecommendation)
        {
            // Not necessary; but NO ONE SHOULD USE these characters for a filename!
            baseName = baseName
                    .Replace("\\", "＼")
                    .Replace("/", "／")
                    .Replace("?", "❓")
                    .Replace("!", "❗")
                    .Replace(":", "：")
                    .Replace(";", "；")
                    .Replace("'", "‘")
                    .Replace("`", "ヾ")
                    .Replace("#", "＃")
                    .Replace("%", "％")
                    .Replace("$", "＄")
                    .Replace("&", "＆")
                    .Replace("^", "＾")
                    .Replace("~", "～")
                    .Replace("|", "￤")
                    .Replace("(", "﴾")
                    .Replace(")", "﴿")
                    .Replace("[", "〘")
                    .Replace("]", "〙")
                    .Replace("{", "【")
                    .Replace("}", "】")
                    .Replace("<", "≪")
                    .Replace(">", "≫")
                    .Replace("+", "＋")
                    .Replace("-", "ー")
                    .Replace("*", "＊")
                    .Replace("=", "＝")
                    .Replace(",", "▶")
                ;
        }

        return baseName;
    }

    public static string SafeSourceCode(
        string text,
        bool unicodeEacape = true,
        bool keepPeriodAsIs = false,
        bool asSingleLine = false
    )
    {
        text = JustOneLineFeed(text);
        if (unicodeEacape)
        {
            text = UnicodeEscape(text, everything: true);
        }

        text = SafeBaseName(text, followRecommendation: true);
        if (!keepPeriodAsIs)
        {
            text = text.Replace(".", "◉");
        }

        if (asSingleLine)
        {
            //text = text.Replace("\n", "↩");
            text = text.Replace("\n", "✿");
        }

        return text;
    }

    public static string RestoreSourceCode(string safeCode)
    {
        string restored = safeCode;
        restored =
            restored
                //.Replace("↩", "\n")
                .Replace("✿", "\n")
                .Replace("❗", "!")
                .Replace("❓", "?")
                .Replace("“", "\"")
                .Replace("‘", "'")
                .Replace("ヾ", "`")
                .Replace("＃", "#")
                .Replace("％", "%")
                .Replace("＆", "&")
                .Replace("＾", "^")
                .Replace("～", "~")
                .Replace("￥", "\\") /**/
                .Replace("＼", "\\")
                .Replace("￤", "|")
                .Replace("；", ";")
                .Replace("：", ":")
                .Replace("﴾", "(")
                .Replace("﴿", ")")
                .Replace("〘", "[")
                .Replace("〙", "]")
                .Replace("【", "{")
                .Replace("】", "}")
                .Replace("≪", "<")
                .Replace("≫", ">")
                .Replace("＋", "+")
                .Replace("ー", "-")
                .Replace("＊", "*")
                .Replace("／", "/")
                .Replace("＝", "=")
                .Replace("❝", "\"")
                .Replace("❞", "\"")
                .Replace("▶", ",")
                .Replace("◉", ".")
            ;
        restored = UnicodeUnescape(restored);
        return restored;
    }

    private static string _PrettyQuotesPairs(string fileName)
    {
        var occurrences = FindCharacterOccurrences(fileName, '“');
        char[] array = fileName.ToCharArray();
        int pairCount = occurrences.Count / 2;
        for (int i = 0; i < pairCount; i++)
        {
            int pairA = occurrences[i * 2 + 0];
            int pairB = occurrences[i * 2 + 1];
            array[pairA] = '❝';
            array[pairB] = '❞';
        }

        fileName = new string(array);
        return fileName;
    }

    public static string SafeFileName(
        string fileName,
        bool keepPeriodAsIs = true,
        bool prettyQuotesPairs = false,
        string replaceSurrogate = "✅"
    )
    {
        fileName = SafeSourceCode(
            fileName,
            unicodeEacape: false,
            keepPeriodAsIs: keepPeriodAsIs
        );
        //fileName = fileName.Replace("\n", "↩");
        fileName = fileName.Replace("\n", "✿");
        fileName = fileName
                //.Replace("　", " ")
                .Replace("\u3000", " ") //全角スペースを半角スペースに統一する
            ;
        fileName = JustOneSpace(fileName);
        fileName = ReplaceSurrogatePair(fileName, replaceSurrogate);
        if (prettyQuotesPairs)
        {
            fileName = _PrettyQuotesPairs(fileName);
        }

        return fileName;
    }

    public static string SafeMetaData(
        string metadata,
        bool prettyQuotesPairs = false,
        string replaceSurrogate = "✅"
    )
    {
        metadata = metadata
                .Replace("\"", "“")
                .Replace("'", "‘")
                .Replace("\\", "＼")
            ;
        metadata = ReplaceSurrogatePair(metadata, replaceSurrogate);
        if (prettyQuotesPairs)
        {
            metadata = _PrettyQuotesPairs(metadata);
        }

        return metadata;
    }
}