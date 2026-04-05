namespace Universal_Test;
using Universal;
using NUnit.Framework;
using System.Linq;
using System.Runtime.CompilerServices;
using static Global.EasyObject;

public class CoreTransformTest
{
    private static string GeminiSuperFullName(object obj, [CallerMemberName] string methodName = "")
    {
        // 引数を省略しても、コンパイラが自動的に呼び出し元のメソッド名を入れます
        return $"#⁅✨{methodName}()✨⁆(CLASS:🌐⁅{obj.GetType().FullName}⁆🌐)";
    }
    [SetUp]
    public void Setup()
    {
        Echo($"▶START!!", title: GeminiSuperFullName(this));
        // Do some setup here...
        Echo($"▶END!!", title: GeminiSuperFullName(this));
    }
    [Test]
    public void Test001()
    {
        // TestContextから現在のメソッド名を取得
        //var methodName = TestContext.CurrentContext.Test.MethodName;
        
        string original = "🌐Hello, World!?🌐⁅EMOJI⁆◉▶▸⸝↪️↩️➠✅🈂️❓❗𝑪𝒉𝒆𝒄𝒌";
        string transformed = UniversalTransformer.GeminiSuperSerifBoldItalicTransform(original);
        Echo(transformed, title: "Gemini Super Serif Bold Italic 風に変換した結果");
        AssertIdentical(actual: transformed, expected: @"🌐𝑯𝒆𝒍𝒍𝒐, 𝑾𝒐𝒓𝒍𝒅!?🌐⁅𝑬𝑴𝑶𝑱𝑰⁆◉▶▸⸝↪️↩️➠✅🈂️❓❗𝑪𝒉𝒆𝒄𝒌");
        transformed = UniversalTransformer.GeminiSuperSerifBoldItalicTransform(original,autoUpcase: true);
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
    }
}
