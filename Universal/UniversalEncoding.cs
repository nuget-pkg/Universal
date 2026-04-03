using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Core.CoreObject;

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
        Log(byteUtf32.Length, title: "byteUtf32.Length");
        return byteUtf32;
    }
    // 4バイト(byte)を1つの数値(uint)として読み替える
    public static uint[] ToCodePoints(string s)
    {
        byte[] bytes = ParseStringIntoUtf32Characters(s);
        uint[] codePoints = new uint[bytes.Length / 4];
        Buffer.BlockCopy(bytes, 0, codePoints, 0, bytes.Length);
        return codePoints;
    }
    public static string LimitStringLength(string s, int limit, string ellipsis = "...")
    {
        UTF32Encoding enc = new UTF32Encoding();
        byte[] byteUtf32 = enc.GetBytes(s);
        if (byteUtf32.Length <= limit * 4)
        {
            return s;
        }
        ArraySegment<byte> segment = new ArraySegment<byte>(byteUtf32, 0, limit * 4);
        byteUtf32 = segment.ToArray();
        string decodedString = enc.GetString(byteUtf32);
        return decodedString + ellipsis;
    }

}
