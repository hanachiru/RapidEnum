using System.Linq;
using System.Text.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace RapidEnum.Tests;

[RapidEnumMarker(typeof(JsonTokenType))]
internal static partial class JsonTokenTypeEnumExtensions { }

[TestFixture]
public class RapidEnumTestWithThirdParty
{
    [Test]
    public void ToStringFastPassTest()
    {
        Assert.That(JsonTokenType.Comment.ToStringFast(), Is.EqualTo(nameof(JsonTokenType.Comment))); 
        Assert.That(((JsonTokenType)100).ToStringFast(), Is.EqualTo("100"));
    }

    [Test]
    public void IsDefinedUsingEnumPassTest()
    {
        Assert.That(JsonTokenTypeEnumExtensions.IsDefined(JsonTokenType.Comment), Is.EqualTo(true));
        Assert.That(JsonTokenTypeEnumExtensions.IsDefined((JsonTokenType)byte.MaxValue), Is.EqualTo(false));
    }
    
    [Test]
    public void IsDefinedUsingNamePassTest()
    {
        Assert.That(JsonTokenTypeEnumExtensions.IsDefined(nameof(JsonTokenType.Comment)), Is.EqualTo(true));
        Assert.That(JsonTokenTypeEnumExtensions.IsDefined("10"), Is.EqualTo(false));
    }
    
    [Test]
    public void GetValuesPassTest()
    {
        var values = JsonTokenTypeEnumExtensions.GetValues();
    
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
        var values = JsonTokenTypeEnumExtensions.GetNames();

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
    public void GetMembersPassTest()
    {
        var members = JsonTokenTypeEnumExtensions.GetMembers();

        CollectionAssert.AreEqual(members.Select(x => x.Name), new[]
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
        CollectionAssert.AreEqual(members.Select(x => x.Value), new[]
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
    public void ToMemberPassTest()
    {
        var member = JsonTokenType.Comment.ToMember();

        Assert.That(member.Name, Is.EqualTo(nameof(JsonTokenType.Comment)));
        Assert.That(member.Value, Is.EqualTo(JsonTokenType.Comment));
    }

    [Test]
    public void TryParsePassTest()
    {
        {
            if (JsonTokenTypeEnumExtensions.TryParse(nameof(JsonTokenType.Comment), out var value))
            {
                Assert.That(value, Is.EqualTo(JsonTokenType.Comment));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (JsonTokenTypeEnumExtensions.TryParse("10", out var value))
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
            if (JsonTokenTypeEnumExtensions.TryParse(nameof(JsonTokenType.Comment).ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo(JsonTokenType.Comment));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (JsonTokenTypeEnumExtensions.TryParse("10".ToLower(), out var value, true))
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
        var underlyingType2 = JsonTokenTypeEnumExtensions.GetUnderlyingType();
        Assert.That(underlyingType2.FullName, Is.EqualTo("System.Byte"));
    }
}
