using System;
using System.Collections.Generic;
using System.Text;
////using System.Text;
// ReSharper disable RedundantCast
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

// #if NET8_0_OR_GREATER
// using System.Text;
// #endif

namespace Universal
{
    public static class UniversalEncoding
    {
        public static byte[] ConvertSegmentToArray(ArraySegment<byte> segment)
        {
            if (segment.Count == 0) return [];
            byte[] result = new byte[segment.Count];
#if NET8_0_OR_GREATER
            segment.AsSpan().CopyTo(result);
#else
            Buffer.BlockCopy(segment.Array, segment.Offset, result, 0, segment.Count);
#endif
            return result;
        }

        public static byte[] ParseStringIntoUtf32Characters(string s)
        {
            if (string.IsNullOrEmpty(s)) return [];
            return new UTF32Encoding(false, false).GetBytes(s);
        }

        public static uint[] ToCodePoints(string s)
        {
            if (string.IsNullOrEmpty(s)) return [];

#if NET8_0_OR_GREATER
            // .NET 8+: System.Text.Rune を使用した超高速列挙
            var result = new List<uint>(s.Length);
            foreach (var rune in s.EnumerateRunes())
            {
                result.Add((uint)rune.Value);
            }
            return result.ToArray();
#else
            // Legacy: char.ConvertToUtf32 による堅牢な列挙
            List<uint> codePoints = new List<uint>(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                codePoints.Add((uint)char.ConvertToUtf32(s, i));
                if (char.IsHighSurrogate(s, i)) i++;
            }
            return codePoints.ToArray();
#endif
        }

        public static uint CodePointOfCharacter(char c) => (uint)c;

        public static string FromCodePoints(uint[] codePoints)
        {
            if (codePoints == null || codePoints.Length == 0) return string.Empty;

#if NET8_0_OR_GREATER
            // .NET 8+: Spanを活用して中間文字列を作らずに構築（よりスマートに）
            return string.Create(GetUtf16Length(codePoints), codePoints, (span, points) =>
            {
                int pos = 0;
                foreach (var cp in points)
                {
                    Rune.TryCreate(cp, out var rune);
                    rune.EncodeToUtf16(span.Slice(pos));
                    pos += rune.Utf16SequenceLength;
                }
            });
#else
            StringBuilder sb = new StringBuilder(codePoints.Length);
            foreach (uint cp in codePoints)
            {
                sb.Append(char.ConvertFromUtf32((int)cp));
            }
            return sb.ToString();
#endif
        }

        public static string FromSingleCodePoint(uint codePoint)
        {
            return FromCodePoints([codePoint]);
        }
#if NET8_0_OR_GREATER
        private static int GetUtf16Length(uint[] codePoints)
        {
            int len = 0;
            foreach (var cp in codePoints)
            {
                len += (cp > 0xFFFF) ? 2 : 1;
            }
            return len;
        }
#endif

        public static string LimitStringLength(string s, int limit, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(s) || limit <= 0) return string.Empty;

            uint[] codePoints = ToCodePoints(s);
            if (codePoints.Length <= limit) return s;

            uint[] truncatedPoints = new uint[limit];
            Array.Copy(codePoints, truncatedPoints, limit);
            
            return FromCodePoints(truncatedPoints) + ellipsis;
        }
    }
}