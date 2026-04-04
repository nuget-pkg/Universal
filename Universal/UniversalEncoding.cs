using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

////using static Core.CoreObject;

namespace Universal;

public class UniversalEncoding
{
    public static byte[] ConvertSegmentToArray(ArraySegment<byte> segment)
    {
        // ArraySegmentから新しいbyte[]を生成（正確なサイズで）
        byte[] result = new byte[segment.Count];
        // segment.ArrayのOffset位置から、resultの0位置へ、segment.Count分コピー
        Buffer.BlockCopy(segment.Array!, segment.Offset, result, 0, segment.Count);
        return result;
    }
    public static byte[] ParseStringIntoUtf32Characters(string s)
    {
        UTF32Encoding enc = new UTF32Encoding();
        byte[] byteUtf32 = enc.GetBytes(s);
        ////Log(byteUtf32.Length, title: "byteUtf32.Length");
        return byteUtf32;
    }
    // 4バイト(byte)を1つの数値(uint)として読み替える
    /// Author: Gemini (Google Large Language Model)
    /// See: https://gemini.google.com/share/383721c0d6ef
    public static uint[] ToCodePoints(string s)
    {
        byte[] bytes = ParseStringIntoUtf32Characters(s);
        uint[] codePoints = new uint[bytes.Length / 4];
        Buffer.BlockCopy(bytes, 0, codePoints, 0, bytes.Length);
        return codePoints;
    }
    /// Author: Gemini (Google Large Language Model)
    /// See: https://gemini.google.com/share/383721c0d6ef
    public static uint CodePointOfCharacter(char c)
    {
        var array = ToCodePoints($"{c}");
        return array[0];
    }
    /// <summary>
    /// コードポイントの配列(uint[])を、C#標準の文字列(string)に変換して戻します。
    /// </summary>
    /// Author: Gemini (Google Large Language Model)
    /// See: https://gemini.google.com/share/383721c0d6ef
    public static string FromCodePoints(uint[] codePoints)
    {
        if (codePoints == null || codePoints.Length == 0) return string.Empty;
        // uint[] (1要素4バイト) を byte[] に変換するためのバッファ確保
        byte[] bytes = new byte[codePoints.Length * 4];
        // メモリブロックを高速コピー
        Buffer.BlockCopy(codePoints, 0, bytes, 0, bytes.Length);
        // UTF32Encoding を使って一気に string (UTF-16) へ復元
        return new System.Text.UTF32Encoding().GetString(bytes);
    }
    /// Author: Gemini (Google Large Language Model)
    /// See: https://gemini.google.com/share/383721c0d6ef
    public static string LimitStringLength(string s, int limit, string ellipsis = "...")
    {
        if (string.IsNullOrEmpty(s)) return s;
        // 1. 文字列をコードポイント(uint)の配列に変換
        // これにより、絵文字や特殊記号も「1つの要素」として扱える
        uint[] codePoints = ToCodePoints(s);
        // 2. 指定された制限文字数以内ならそのまま返す
        if (codePoints.Length <= limit)
        {
            return s;
        }
        // 3. 制限文字数分だけを切り出す（Slice）
        // 1文字4バイト固定なので、ここでは単なる配列のインデックス操作
        uint[] truncatedPoints = new uint[limit];
        Array.Copy(codePoints, 0, truncatedPoints, 0, limit);

        // 4. 再び文字列(UTF-16)に戻して、省略記号を付与
        byte[] truncatedBytes = new byte[limit * 4];
        Buffer.BlockCopy(truncatedPoints, 0, truncatedBytes, 0, truncatedBytes.Length);
        UTF32Encoding enc = new UTF32Encoding();
        return enc.GetString(truncatedBytes) + ellipsis;
    }
}
