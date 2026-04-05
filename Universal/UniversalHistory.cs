using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Universal;
    public static class UniversalHistoory {
        public static string ____GeminiSuperSerifBoldItalicTransform_ARCHIVED_VERSION____(string text,
            bool autoUpcase = false) {
            uint _CPOC_(char c) {
                return UniversalEncoding.CodePointOfCharacter(c);
            }
            // This is a placeholder for a function that would convert text to a super serif bold italic style.
            // The actual implementation would depend on the specific Unicode characters used for this style.
            // For demonstration purposes, we'll just return the original text.
            Dictionary<uint, string> conversionMap = new Dictionary<uint, string> {
                // Serif Bold Italic: 𝑨 𝑩 𝑪 𝑫 𝑬 𝑭 𝑮 𝑯 𝑰 𝑱 𝑲 𝑳 𝑴 𝑵 𝑶 𝑷 𝑸 𝑹 𝑺 𝑻 𝑼 𝑽 𝑾 𝑿 𝒀 𝒁
                { _CPOC_('A'), "𝑨" }, { _CPOC_('B'), "𝑩" }, { _CPOC_('C'), "𝑪" }, { _CPOC_('D'), "𝑫" },
                { _CPOC_('E'), "𝑬" },
                { _CPOC_('F'), "𝑭" }, { _CPOC_('G'), "𝑮" }, { _CPOC_('H'), "𝑯" }, { _CPOC_('I'), "𝑰" },
                { _CPOC_('J'), "𝑱" },
                { _CPOC_('K'), "𝑲" }, { _CPOC_('L'), "𝑳" }, { _CPOC_('M'), "𝑴" }, { _CPOC_('N'), "𝑵" },
                { _CPOC_('O'), "𝑶" },
                { _CPOC_('P'), "𝑷" }, { _CPOC_('Q'), "𝑸" }, { _CPOC_('R'), "𝑹" }, { _CPOC_('S'), "𝑺" },
                { _CPOC_('T'), "𝑻" },
                { _CPOC_('U'), "𝑼" }, { _CPOC_('V'), "𝑽" }, { _CPOC_('W'), "𝑾" }, { _CPOC_('X'), "𝑿" },
                { _CPOC_('Y'), "𝒀" },
                { _CPOC_('Z'), "𝒁" },
                // Serif Bold Italic: 𝒂 𝒃 𝒄 𝒅 𝒆 𝒇 𝒈 𝒉 𝒊 𝒋 𝒌 𝒍 𝒎 𝒏 𝒐 𝒑 𝒒 𝒓 𝒔 𝒕 𝒖 𝒗 𝒘 𝒙 𝒚 𝒛
                { _CPOC_('a'), "𝒂" }, { _CPOC_('b'), "𝒃" }, { _CPOC_('c'), "𝒄" }, { _CPOC_('d'), "𝒅" },
                { _CPOC_('e'), "𝒆" },
                { _CPOC_('f'), "𝒇" }, { _CPOC_('g'), "𝒈" }, { _CPOC_('h'), "𝒉" }, { _CPOC_('i'), "𝒊" },
                { _CPOC_('j'), "𝒋" },
                { _CPOC_('k'), "𝒌" }, { _CPOC_('l'), "𝒍" }, { _CPOC_('m'), "𝒎" }, { _CPOC_('n'), "𝒏" },
                { _CPOC_('o'), "𝒐" },
                { _CPOC_('p'), "𝒑" }, { _CPOC_('q'), "𝒒" }, { _CPOC_('r'), "𝒓" }, { _CPOC_('s'), "𝒔" },
                { _CPOC_('t'), "𝒕" },
                { _CPOC_('u'), "𝒖" }, { _CPOC_('v'), "𝒗" }, { _CPOC_('w'), "𝒘" }, { _CPOC_('x'), "𝒙" },
                { _CPOC_('y'), "𝒚" },
                { _CPOC_('z'), "𝒛" }
            };
            if (autoUpcase) text = text.ToUpper();
            uint[] codePoints = UniversalEncoding.ToCodePoints(text);
            List<string> convertedCharacters = new List<string>();
            for (int i = 0; i < codePoints.Length; i++) {
                uint codePoint = codePoints[i];
                if (conversionMap.ContainsKey(codePoint)) {
                    convertedCharacters.Add(conversionMap[codePoint]);
                }
                else {
                    convertedCharacters.Add(UniversalEncoding.FromCodePoints([codePoint]));
                }
            }
            text = string.Join("", convertedCharacters);
            return text;
        }



    // Enumを使わず、直接「開始地点のコードポイント」を返すストレートな設計案
    private static int GetOffsetFromSample(string sample)
    {
        if (string.IsNullOrEmpty(sample)) return 0;
        return char.ConvertToUtf32(sample, 0); // "𝒜" を渡せば 0x1D49C が返る
    }

    /// <summary>
    /// 🌈 サンプル文字（𝒩 や ℕ など）を一つ渡すだけで、文字列全体をそのスタイルに染め上げます。
    /// </summary>
    /// <param name="sampleChar">スタイルの基準となる文字（例: "𝒜", "𝔸", "𝙰"）</param>
    /// <param name="target">変換したい英数字テキスト</param>
    /// <returns>変貌を遂げたテキスト</returns>
    public static string GeminiSuperAllPurposeEmojifier(string sampleChar, string target)
    {
        if (string.IsNullOrEmpty(target)) return target;

        // サンプルから「'A' がそのスタイルで何番のコードポイントか」を取得
        int offsetA = GetBaseCodePoint(sampleChar, true);
        int offsetSmallA = GetBaseCodePoint(sampleChar, false);

        if (offsetA == 0) return target; // 判定不能ならそのまま返す

        var sb = new StringBuilder(target.Length * 2);
        foreach (var c in target)
        {
            if (c >= 'A' && c <= 'Z')
            {
                int cp = offsetA + (c - 'A');
                // Script体などの「Unicodeの穴」を修正（必要に応じて拡張）
                cp = FixUnicodeGap(cp);
                sb.Append(char.ConvertFromUtf32(cp));
            }
            else if (c >= 'a' && c <= 'z')
            {
                int cp = offsetSmallA + (c - 'a');
                cp = FixUnicodeGap(cp);
                sb.Append(char.ConvertFromUtf32(cp));
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    private static int GetBaseCodePoint(string sample, bool isUpper)
    {
        if (string.IsNullOrEmpty(sample)) return 0;
        int cp = char.ConvertToUtf32(sample, 0);

        // 与えられた文字がどのレンジに属するか判定し、そのスタイルの 'A' または 'a' の起点を返す
        if (cp >= 0x1D400 && cp <= 0x1D7FF) // Mathematical Alphanumeric Symbols 領域
        {
            // スタイルごとのブロックサイズは 26(英字) または 52(大文字+小文字)
            // 簡易的に「今の文字から A(0x41) または a(0x61) の距離を引く」
            // ただし、サンプルが小文字の場合もあるので調整が必要
            if (cp >= 0x1D400 && cp <= 0x1D6A5) // 英字セクション
            {
                // 大文字ならそのスタイルの開始点、小文字なら対応する大文字の開始点を推測
                // 多くのスタイルは 大文字(26個) -> 小文字(26個) の順で並んでいます
                // 1D468 (A) -> 1D482 (a) の差は 0x1A (26)
                // ここでは単純化のため、サンプルの文字種に関わらず大文字起点を計算
                bool sampleIsUpper = (cp - 0x1D400) % 52 < 26; // ざっくりとした判定

                // 汎用的なオフセット計算（非常に洗練された方法）
                // ※実際には TextStyle Enum と dictionary で管理するのが安全ですが、
                // 「サンプル文字から盗む」というコンセプトなら以下の計算がクールです。
                return isUpper ?
                    (cp - (sampleIsUpper ? 0 : 26)) : // Aの地点
                    (cp + (sampleIsUpper ? 26 : 0));  // aの地点
            }
        }
        return 0;
    }

    private static int FixUnicodeGap(int cp)
    {
        // 𝑩, ℰ, ℱ などの特殊な「穴」を埋めるためのマッピング
        // 数学記号領域では一部が既存の BMP 領域（U+2100台）に飛ばされています
        return cp switch
        {
            0x1D472 => 0x210E, // h (Plancks constant)
            0x1D49C => 0x212C, // B (Script)
            0x1D49E => 0x2130, // E (Script)
            0x1D49F => 0x2131, // F (Script)
            0x1D4A2 => 0x210B, // H (Script)
            0x1D4A5 => 0x2110, // I (Script)
            0x1D4A6 => 0x2112, // L (Script)
            0x1D4A9 => 0x2133, // M (Script)
            0x1D4AC => 0x211B, // R (Script)
            0x1D4AE => 0x2124, // Z (Double-struck)
            _ => cp
        };
    }
    /// <summary>
    /// 文字装飾のスタイルを指定するための列挙型
    /// </summary>
    public enum TextStyle
    {
        Default,
        Monospace,    // 𝚘 𝚘
        DoubleStruck, // 𝕠 𝕠
        BoldSans,     // 𝗼 𝗼
        Script,       // ℴ ℴ
        BoldItalic,   // 𝑨 𝑩 𝑪 (今回の本命！)
        Typewriter,   // 𝚠 𝚠
        Circled,      // Ⓐ Ⓑ Ⓒ
        CrossedOut    // o͓̽ o͓̽
    }
}
