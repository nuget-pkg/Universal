// ReSharper disable once CheckNamespace
namespace Universal.Test;

using System.Collections.Generic;
using NUnit.Framework;
using static Global.EasyObject;
public class Add2Test
{
    [SetUp]
    public void Setup()
    {
        Echo($"{FullName(this)}#Setup() called");
    }
    [Test]
    public void Test01()
    {
        //AssertIdentical(expected: 333, actual: Core.Universal.Add2(111, 222));
        Log("▶passed-01");
        AssertEquivalent(expected: new object[] { 11, 22, 33 }, actual: NewArray(11, 22, 33));
        Log("▶passed-02");
        var data1 = new Dictionary<string, object?> {
            { "Apple", 1 },
            { "Banana", 2 },
            { "List", NewArray(11, 22, 33) },
        };
        var data2 = new { Apple = 1, Banana = 2, List = NewArray(11, 22, 33) };
        Echo(data1, "▶data1");
        Echo(data2, "▶data2");
        AssertEquivalent(expected: data1, actual: data2);
        Log("▶passed-03");
    }
}
