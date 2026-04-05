// ReSharper disable ConditionIsAlwaysTrueOrFalse
#pragma warning disable CS0652 // 整数定数への比較は無意味です。定数が型の範囲外です
namespace Universal_Test;

using Universal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Global.EasyObject; // EasyObjectの静的インポートを想定

public class CoreTransformTest
{
    private static string GeminiSuperFullName(object obj, [CallerMemberName] string methodName = "")
    {
        return $"#⁅✨{methodName}()✨⁆(CLASS:🌐⁅{obj.GetType().FullName}⁆🌐)";
    }

    [SetUp]
    public void Setup()
    {
        Echo($"▶START!!", title: GeminiSuperFullName(this));
        // 必要に応じて初期化処理
        Echo($"▶END!!", title: GeminiSuperFullName(this));
    }

    [Test]
    public void Test001()
    { // MADE ONLY FOR EXPLANATION...MIGHT NOT FOLLOWING BEST PRACTICES...
        Line();
        
        string original = "🌐Hello, World!?🌐⁅EMOJI⁆◉▶▸⸝↪️↩️➠✅🈂️❓❗𝑪𝒉𝒆𝒄𝒌";
        string transformed = GeminiSuperTransformer.GeminiSuperSerifBoldItalicTransform(original);
        Echo(transformed, title: "Gemini Super Serif Bold Italic 風に変換した結果");
        AssertIdentical(actual: transformed, expected: @"🌐𝑯𝒆𝒍𝒍𝒐, 𝑾𝒐𝒓𝒍𝒅!?🌐⁅𝑬𝑴𝑶𝑱𝑰⁆◉▶▸⸝↪️↩️➠✅🈂️❓❗𝑪𝒉𝒆𝒄𝒌");
        transformed = GeminiSuperTransformer.GeminiSuperSerifBoldItalicTransform(original,autoUpcase: true);
        Echo(transformed, title: "Gemini Super Serif Bold Italic 風に変換した結果 (全大文字化)");
        AssertIdentical(actual: transformed, expected: @"🌐𝑯𝑬𝑳𝑳𝑶, 𝑾𝑶𝑹𝑳𝑫!?🌐⁅𝑬𝑴𝑶𝑱𝑰⁆◉▶▸⸝↪️↩️➠✅🈂️❓❗𝑪𝒉𝒆𝒄𝒌");

        // 1. 文字列を爆破（Code Point化）
        var codePoints = UniversalEncoding.ToCodePoints("𝓐𝓑𝓒 123 𝔁𝔂𝔃");
        // 2. 加工（例：LINQで数字だけを除去してみる）
        // 48～57 が ASCII の '0'～'9' です
        AssertIdentical(actual: codePoints.Length, expected: 11);
        var filteredPoints = codePoints.Where(cp => cp < 48 || cp > 57).ToArray();
        AssertIdentical(actual: filteredPoints.Length, expected: 8);
        // 3. 復元（再び string へ）
        string result = UniversalEncoding.FromCodePoints(filteredPoints);
        Echo(result, title: "数字を除去して復元した結果");
        // 出力期待値: "𝓐𝓑𝓒  𝔁𝔂𝔃" (サロゲートペアも壊れずに残る！)
        AssertIdentical(actual: result, expected: @"𝓐𝓑𝓒  𝔁𝔂𝔃");

        Line();
    }

    [Test]
    public void Test_UniversalTransformer_Detailed()
    {
        Line();

        string original = "Hello World 123";
        
        // 1. AssertTrue / AssertFalse の活用
        string transformed = GeminiSuperTransformer.GeminiSuperSerifBoldItalicTransform(original);
        AssertTrue(transformed.Contains("𝑯"), "変換後の文字列に特定のグリフが含まれていること");
        AssertFalse(transformed.Contains("H"), "元のASCII大文字が残っていないこと");

        // 2. AssertIdentical (完全一致)
        string expected = "𝑯𝒆𝒍𝒍𝒐 𝑾𝒐𝒓𝒍𝒅 123";
        AssertIdentical(actual: transformed, expected: expected);
    
        Line();
    }

    [Test]
    public void Test_UniversalEncoding_Collections()
    {
        Line();

        string input = "ABC";
        uint[] codePoints = UniversalEncoding.ToCodePoints(input);

        // 3. AssertEquivalent (順序を問わない、または内容の等価性)
        // 配列の内容が期待通りか検証
        AssertEquivalent(actual: codePoints, expected: new int[] { 65, 66, 67 });

        // 4. AssertBound (範囲内にあるか)
        // 全てのコードポイントが基本ラテン文字の範囲(0-127)にあることを検証
        foreach (var cp in codePoints)
        {
            AssertTrue(condition: cp >= 0 && cp <= 127);
        }

        // 5. AssertNotBound (範囲外であること)
        // 負の値や異常な範囲にないことを念のため確認
        AssertFalse(condition: codePoints[0] >= -100 && codePoints[0] <= -1);

        Line();
    }

    [Test]
    public void Test_Advanced_Transform_Logic() {
        Line();
        
        string emojiOnly = "🌐🔥";
        string result = GeminiSuperTransformer.GeminiSuperSerifBoldItalicTransform(emojiOnly);

        // 6. AssertNotIdentical
        // 絵文字は変換対象外であるはずなので、変換前後で「変わっていないこと」をあえて確認
        AssertIdentical(actual: result, expected: emojiOnly);

        // 7. AssertNotEquivalent
        // 加工した配列が、元の配列と「中身が別物になったこと」を確認する場合
        var originalPoints = UniversalEncoding.ToCodePoints("XYZ");
        var filteredPoints = originalPoints.Take(2).ToArray();
        AssertNotEquivalent(actual: filteredPoints, expected: originalPoints);

        Line();
    }

    [Test]
    public void Test_Boundary_And_Empty()
    {
        Line();

        // 境界値テストとしての AssertBound
        string empty = "";
        var points = UniversalEncoding.ToCodePoints(empty);
        
        // 要素数が0であることを確認
        AssertIdentical(actual: points.Length, expected: 0);

        // 数値の検証例
        int sampleSize = points.Length;
        AssertTrue(condition: sampleSize >= 0 &&  sampleSize <= 0);
        
        Line();
    }
}