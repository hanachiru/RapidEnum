using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace RapidEnum.Tests;

[TestFixture]
public class RapidEnumTestWithSystem
{
    [RapidEnumMarker(typeof(DateTimeKind))]
    public static partial class DateTimeKindEnumExtensions { }

    [Test]
    public void ToStringFastPassTest()
    {
        Assert.That(DateTimeKind.Utc.ToStringFast(), Is.EqualTo(nameof(DateTimeKind.Utc))); 
        Assert.That(((DateTimeKind)10).ToStringFast(), Is.EqualTo("10"));
    }

    [Test]
    public void IsDefinedUsingEnumPassTest()
    {
        Assert.That(System.DateTimeKindEnumExtensions.IsDefined(DateTimeKind.Local), Is.EqualTo(true));
        Assert.That(System.DateTimeKindEnumExtensions.IsDefined((DateTimeKind)int.MaxValue), Is.EqualTo(false));
    }
    
    [Test]
    public void IsDefinedUsingNamePassTest()
    {
        Assert.That(System.DateTimeKindEnumExtensions.IsDefined(nameof(DateTimeKind.Local)), Is.EqualTo(true));
        Assert.That(System.DateTimeKindEnumExtensions.IsDefined("10"), Is.EqualTo(false));
    }
    
    [Test]
    public void GetValuesPassTest()
    {
        var values = System.DateTimeKindEnumExtensions.GetValues();
    
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
        var values = System.DateTimeKindEnumExtensions.GetNames();
    
        CollectionAssert.AreEqual(values, new[]
        {
            nameof(DateTimeKind.Unspecified),
            nameof(DateTimeKind.Utc),
            nameof(DateTimeKind.Local),
        });
    }
    
    [Test]
    public void TryParsePassTest()
    {
        {
            if (System.DateTimeKindEnumExtensions.TryParse(nameof(DateTimeKind.Local), out var value))
            {
                Assert.That(value, Is.EqualTo(DateTimeKind.Local));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (System.DateTimeKindEnumExtensions.TryParse("10", out var value))
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
            if (System.DateTimeKindEnumExtensions.TryParse(nameof(DateTimeKind.Local).ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo(DateTimeKind.Local));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (System.DateTimeKindEnumExtensions.TryParse("10".ToLower(), out var value, true))
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
        var underlyingType = System.DateTimeKindEnumExtensions.GetUnderlyingType();
        Assert.That(underlyingType.FullName, Is.EqualTo("System.Int32"));
    }
}
