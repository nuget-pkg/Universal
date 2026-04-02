using System.Runtime;
namespace Universal.Demo;

using static Core.CoreObject;
using static Global.HyperOperatingSystem;
public static class Program1
{
    public static void Main(string[] args)
    {
        try
        {
            DebugOutput = true;
            string fname =
                """[1080p]✅👀🫧💻🌐`within backticks`<xml>aaa</xml>;{Title}!?x=(11+22-33)*11/2;,(🔥引火帝国🔥):"name1"'name2'?.txt""";
            Log(UniversalTransformer.SafeFileName(fname, prettyQuotesPairs: true),
                "adjusted file name");
            Log(UniversalTransformer.SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: ""),
                "adjusted file name (keeping surrogate pairs)");
            Log(UniversalTransformer.SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: "@"),
                "adjusted file name (specifying surrogate pairs' replacement)");
            string code =
                """
                namespace HelloWorldApp
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine("Hello, World!?");
                            Console.WriteLine("ハロー©,World!?⁅EMOJI⁆◉▶▸⸝↪️↩️➠✅🈂️❓❗𝑪𝒉𝒆𝒄𝒌");
                            Console.WriteLine("Hello, World❗❓");
                            Console.WriteLine("\u2757\u2753");
                            Console.WriteLine("“"); // this line causes a problem when encoded with SafeSourceCode(code, unicodeEscape: false)
                            Console.WriteLine(Add2(11, 22));
                            EvaluateJavaScript(" console.log(`answer=${11+22}`); ");
                        }
                        private static int Add2(int x, int y)
                        {
                            return x + y;
                        }
                    }
                }
                """;
            string safeCode1 = UniversalTransformer.SafeSourceCode(code); // [DEFAULT] unicodeEacape: true
            // string
            //     safeCode2 = UniversalTransformer.SafeSourceCode(code,
            //         unicodeEacape: false); // DON'T USE `unicodeEacape: false` if you have PLAN OF RESTORING!
            // //Log(safeCode2, "safeCode2");
            // string safeCode3 = UniversalTransformer.SafeSourceCode(code, asSingleLine: true);
            // //Log(safeCode3, "safeCode3");
            string restoredCode1 = UniversalTransformer.RestoreSourceCode(safeCode1);
            //string restoredCode2 = UniversalTransformer.RestoreSourceCode(safeCode2);
            //string restoredCode3 = UniversalTransformer.RestoreSourceCode(safeCode3);
            Log(restoredCode1, "restoredCode1");
            //Log(restoredCode2, "restoredCode2");
            //Log(restoredCode3, "restoredCode3");
            if (false)
            {
                string sample = System.IO.File.ReadAllText(GitProjectFile(GetCwd(), "UniversalEncoding.Demo", "assets", "sample.txt")!);
                string escaped = UniversalTransformer.UnicodeEscape(sample);
                Log(escaped, "escaped");
            }
        }
        catch (System.Exception ex)
        {
            System.Console.Error.WriteLine(ex.ToString());
            System.Environment.Exit(1);
        }
    }
}
