using System.Collections;
using MiniJSON;
using NUnit.Framework;

[TestFixture]
public class MiniJSONTest
{

    [Test]
    public void TestDeserialize()
    {
        const string jsonString = @"
{
    ""int"": 65536,
    ""float"": 3.1415926,
    ""string"": ""The quick brown fox \""jumps\"" over the lazy dog "",
    ""unicode"": ""\u3041 Men\u00fa sesi\u00f3n 汉字"",
    ""bool"": true,
    ""array"": [
        1.44,
        2,
        3
    ],
    ""object"": {
        ""key1"": ""value1"",
        ""key2"": 256
    },
    ""null"": null
}
";

        var dict = Json.Deserialize(jsonString) as IDictionary;

        Assert.IsTrue(dict["int"] is int);
        Assert.AreEqual(65536, dict["int"]);
        Assert.IsTrue(dict["float"] is float);
        Assert.AreEqual(3.1415926F, (float) dict["float"], 1e-6F);
        Assert.AreEqual("The quick brown fox \"jumps\" over the lazy dog ", dict["string"]);
        Assert.AreEqual("\u3041 Men\u00fa sesi\u00f3n 汉字", dict["unicode"]);
        Assert.IsTrue(dict["bool"] is bool);
        Assert.AreEqual(true, dict["bool"]);

        Assert.IsTrue(dict["array"] is IList);
        var array = (IList)dict["array"];
        Assert.AreEqual(3, array.Count);
        Assert.AreEqual(new [] { 1.44F, 2, 3 }, array);

        Assert.IsTrue(dict["object"] is IDictionary);
        var obj = dict["object"] as IDictionary;
        Assert.AreEqual("value1", obj["key1"]);
        Assert.AreEqual(256, obj["key2"]);

        Assert.IsNull(dict["null"]);

    }

    [Test]
    public void TestDeserialize_Array2()
    {
        const string jsonString = @"
[
    {
        ""string"": ""Hello World"",
        ""int"": 123,
        ""bool"": true
    },
    [
        123,
        true,
        {
            ""key1"": ""value1"",
            ""key2"": 234
        },
        [
            123,
            34,
            12.3,
            ""abc"",
            true
        ]
    ]
]
";

        var list = Json.Deserialize(jsonString) as IList;
        Assert.AreEqual(2, list.Count);


    }
}
