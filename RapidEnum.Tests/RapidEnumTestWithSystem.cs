using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace RapidEnum.Tests;

[RapidEnumWithType(typeof(DateTimeKind))]
internal static partial class DateTimeKindEnumExtensions { }

[TestFixture]
public class RapidEnumTestWithSystem
{
    [Test]
    public void ToStringFastPassTest()
    {
        Assert.That(DateTimeKind.Utc.ToStringFast(), Is.EqualTo(nameof(DateTimeKind.Utc))); 
        Assert.That(((DateTimeKind)10).ToStringFast(), Is.EqualTo("10"));
    }

    [Test]
    public void IsDefinedUsingEnumPassTest()
    {
        Assert.That(DateTimeKindEnumExtensions.IsDefined(DateTimeKind.Local), Is.EqualTo(true));
        Assert.That(DateTimeKindEnumExtensions.IsDefined((DateTimeKind)int.MaxValue), Is.EqualTo(false));
    }
    
    [Test]
    public void IsDefinedUsingNamePassTest()
    {
        Assert.That(DateTimeKindEnumExtensions.IsDefined(nameof(DateTimeKind.Local)), Is.EqualTo(true));
        Assert.That(DateTimeKindEnumExtensions.IsDefined("10"), Is.EqualTo(false));
    }
    
    [Test]
    public void GetValuesPassTest()
    {
        var values = DateTimeKindEnumExtensions.GetValues();
    
        CollectionAssert.AreEqual(values, new[]
        {
            DateTimeKind.Unspecified,
            DateTimeKind.Utc,
            DateTimeKind.Local
        });
    }
    
    [Test]
    public void GetNamesPassTest()
    {
        var values = DateTimeKindEnumExtensions.GetNames();
    
        CollectionAssert.AreEqual(values, new[]
        {
            nameof(DateTimeKind.Unspecified),
            nameof(DateTimeKind.Utc),
            nameof(DateTimeKind.Local),
        });
    }
    
    [Test]
    public void GetMembersPassTest()
    {
        var members = DateTimeKindEnumExtensions.GetMembers();
        
        CollectionAssert.AreEqual(members.Select(x => x.Name), new[]
        {
            nameof(DateTimeKind.Unspecified),
            nameof(DateTimeKind.Utc),
            nameof(DateTimeKind.Local),
        });
        CollectionAssert.AreEqual(members.Select(x => x.Value), new[]
        {
            DateTimeKind.Unspecified,
            DateTimeKind.Utc,
            DateTimeKind.Local
        });
    }

    [Test]
    public void ToMemberPassTest()
    {
        var member = DateTimeKind.Unspecified.ToMember();

        Assert.That(member.Name, Is.EqualTo(nameof(DateTimeKind.Unspecified)));
        Assert.That(member.Value, Is.EqualTo(DateTimeKind.Unspecified));
    }
    
    [Test]
    public void TryParsePassTest()
    {
        {
            if (DateTimeKindEnumExtensions.TryParse(nameof(DateTimeKind.Local), out var value))
            {
                Assert.That(value, Is.EqualTo(DateTimeKind.Local));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (DateTimeKindEnumExtensions.TryParse("10", out var value))
            {
                Assert.That(value, Is.EqualTo((DateTimeKind)10));
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
            if (DateTimeKindEnumExtensions.TryParse(nameof(DateTimeKind.Local).ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo(DateTimeKind.Local));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (DateTimeKindEnumExtensions.TryParse("10".ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo((DateTimeKind)10));
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
        var underlyingType = DateTimeKindEnumExtensions.GetUnderlyingType();
        Assert.That(underlyingType.FullName, Is.EqualTo("System.Int32"));
    }
}
