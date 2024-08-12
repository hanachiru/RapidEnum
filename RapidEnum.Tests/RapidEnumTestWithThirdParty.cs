using System.Text.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace RapidEnum.Tests;

[TestFixture]
public class RapidEnumTestWithThirdParty
{
    [RapidEnumMarker(typeof(JsonTokenType))]
    public static partial class JsonTokenTypeEnumExtensions { }

    [Test]
    public void ToStringFastPassTest()
    {
        Assert.That(JsonTokenType.Comment.ToStringFast(), Is.EqualTo(nameof(JsonTokenType.Comment))); 
        Assert.That(((JsonTokenType)100).ToStringFast(), Is.EqualTo("100"));
    }

    [Test]
    public void IsDefinedUsingEnumPassTest()
    {
        Assert.That(System.Text.Json.JsonTokenTypeEnumExtensions.IsDefined(JsonTokenType.Comment), Is.EqualTo(true));
        Assert.That(System.Text.Json.JsonTokenTypeEnumExtensions.IsDefined((JsonTokenType)byte.MaxValue), Is.EqualTo(false));
    }
    
    [Test]
    public void IsDefinedUsingNamePassTest()
    {
        Assert.That(System.Text.Json.JsonTokenTypeEnumExtensions.IsDefined(nameof(JsonTokenType.Comment)), Is.EqualTo(true));
        Assert.That(System.Text.Json.JsonTokenTypeEnumExtensions.IsDefined("10"), Is.EqualTo(false));
    }
    
    [Test]
    public void GetValuesPassTest()
    {
        var values = System.Text.Json.JsonTokenTypeEnumExtensions.GetValues();
    
        CollectionAssert.AreEqual(values, new[]
        {
            JsonTokenType.None,
            JsonTokenType.StartObject,
            JsonTokenType.EndObject,
            JsonTokenType.StartArray,
            JsonTokenType.EndArray,
            JsonTokenType.PropertyName,
            JsonTokenType.Comment,
            JsonTokenType.String,
            JsonTokenType.Number,
            JsonTokenType.True,
            JsonTokenType.False,
            JsonTokenType.Null,
        });
    }

    [Test]
    public void GetNamesPassTest()
    {
        var values = System.Text.Json.JsonTokenTypeEnumExtensions.GetNames();

        CollectionAssert.AreEqual(values, new[]
        {
            nameof(JsonTokenType.None),
            nameof(JsonTokenType.StartObject),
            nameof(JsonTokenType.EndObject),
            nameof(JsonTokenType.StartArray),
            nameof(JsonTokenType.EndArray),
            nameof(JsonTokenType.PropertyName),
            nameof(JsonTokenType.Comment),
            nameof(JsonTokenType.String),
            nameof(JsonTokenType.Number),
            nameof(JsonTokenType.True),
            nameof(JsonTokenType.False),
            nameof(JsonTokenType.Null),
        });
    }

    [Test]
    public void TryParsePassTest()
    {
        {
            if (System.Text.Json.JsonTokenTypeEnumExtensions.TryParse(nameof(JsonTokenType.Comment), out var value))
            {
                Assert.That(value, Is.EqualTo(JsonTokenType.Comment));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (System.Text.Json.JsonTokenTypeEnumExtensions.TryParse("10", out var value))
            {
                Assert.That(value, Is.EqualTo((JsonTokenType)10));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
    
    [Test]
    public void TryParseIgnoreCasePassTest()
    {
        {
            if (System.Text.Json.JsonTokenTypeEnumExtensions.TryParse(nameof(JsonTokenType.Comment).ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo(JsonTokenType.Comment));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (System.Text.Json.JsonTokenTypeEnumExtensions.TryParse("10".ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo((JsonTokenType)10));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
    
    [Test]
    public void GetUnderlyingTypeTest()
    {
        var underlyingType2 = System.Text.Json.JsonTokenTypeEnumExtensions.GetUnderlyingType();
        Assert.That(underlyingType2.FullName, Is.EqualTo("System.Byte"));
    }
}
