using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Universal;
public static class LegacyTransformer {
    public static string SafeMetaData(
        string metadata,
        bool prettyQuotesPairs = false,
        string replaceSurrogate = "✅"
    ) {
        metadata = metadata
                .Replace("\"", "“")
                .Replace("'", "‘")
                .Replace("\\", "＼")
            ;
        metadata = GeminiSuperTransformer.ReplaceSurrogatePair(metadata, replaceSurrogate);
        if (prettyQuotesPairs) {
            metadata = _PrettyQuotesPairs(metadata);
        }
        return metadata;
        string _PrettyQuotesPairs(string fileName) {
            var occurrences = GeminiSuperTransformer.FindCharacterOccurrences(fileName, '“');
            char[] array = fileName.ToCharArray();
            int pairCount = occurrences.Count / 2;
            for (int i = 0; i < pairCount; i++) {
                int pairA = occurrences[i * 2 + 0];
                int pairB = occurrences[i * 2 + 1];
                array[pairA] = '❝';
                array[pairB] = '❞';
            }
            fileName = new string(array);
            return fileName;
        }
    }
    public static bool UnicodeEscapeIsNeeded(char c, bool everything = false) {
        if (everything) return (c > 127);
        return (c > 127 && !SpecialCharcters.Contains(c));
    }
    public static List<char> SpecialCharcters = [
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
}