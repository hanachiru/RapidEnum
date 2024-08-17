using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace RapidEnum.Tests;

[TestFixture]
public class RapidEnumTest
{
    [RapidEnum]
    internal enum Sample
    {
        A,
        B,
        C
    }

    [RapidEnum]
    public enum SampleWithByte : byte
    {
        A,
        B,
        C
    }

    [Test]
    public void ToStringFastPassTest()
    {
        Assert.That(Sample.A.ToStringFast(), Is.EqualTo(nameof(Sample.A)));
        Assert.That(Sample.B.ToStringFast(), Is.EqualTo(nameof(Sample.B)));
        Assert.That(Sample.C.ToStringFast(), Is.EqualTo(nameof(Sample.C)));
        Assert.That(((Sample)10).ToStringFast(), Is.EqualTo("10"));
    }

    [Test]
    public void IsDefinedUsingEnumPassTest()
    {
        Assert.That(SampleEnumExtensions.IsDefined(Sample.A), Is.EqualTo(true));
        Assert.That(SampleEnumExtensions.IsDefined(Sample.B), Is.EqualTo(true));
        Assert.That(SampleEnumExtensions.IsDefined(Sample.C), Is.EqualTo(true));
        Assert.That(SampleEnumExtensions.IsDefined((Sample)int.MaxValue), Is.EqualTo(false));
    }

    [Test]
    public void IsDefinedUsingNamePassTest()
    {
        Assert.That(SampleEnumExtensions.IsDefined(nameof(Sample.A)), Is.EqualTo(true));
        Assert.That(SampleEnumExtensions.IsDefined(nameof(Sample.B)), Is.EqualTo(true));
        Assert.That(SampleEnumExtensions.IsDefined(nameof(Sample.C)), Is.EqualTo(true));
        Assert.That(SampleEnumExtensions.IsDefined("10"), Is.EqualTo(false));
    }

    [Test]
    public void GetValuesPassTest()
    {
        var values = SampleEnumExtensions.GetValues();

        CollectionAssert.AreEqual(values, new[]
        {
            Sample.A,
            Sample.B,
            Sample.C
        });
    }

    [Test]
    public void GetNamesPassTest()
    {
        var values = SampleEnumExtensions.GetNames();

        CollectionAssert.AreEqual(values, new[]
        {
            nameof(Sample.A),
            nameof(Sample.B),
            nameof(Sample.C),
        });
    }

    [Test]
    public void GetMembersPassTest()
    {
        var members = SampleEnumExtensions.GetMembers();
        
        CollectionAssert.AreEqual(members.Select(x => x.Name), new[]
        {
            nameof(Sample.A),
            nameof(Sample.B),
            nameof(Sample.C),
        });
        CollectionAssert.AreEqual(members.Select(x => x.Value), new[]
        {
            Sample.A,
            Sample.B,
            Sample.C,
        });
    }

    [Test]
    public void ToMemberPassTest()
    {
        var member = Sample.A.ToMember();

        Assert.That(member.Name, Is.EqualTo(nameof(Sample.A)));
        Assert.That(member.Value, Is.EqualTo(Sample.A));
    }

    [Test]
    public void TryParsePassTest()
    {
        {
            if (SampleEnumExtensions.TryParse(nameof(Sample.A), out var value))
            {
                Assert.That(value, Is.EqualTo(Sample.A));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (SampleEnumExtensions.TryParse(nameof(Sample.B), out var value))
            {
                Assert.That(value, Is.EqualTo(Sample.B));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (SampleEnumExtensions.TryParse(nameof(Sample.C), out var value))
            {
                Assert.That(value, Is.EqualTo(Sample.C));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (SampleEnumExtensions.TryParse("10", out var value))
            {
                Assert.That(value, Is.EqualTo((Sample)10));
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
            if (SampleEnumExtensions.TryParse(nameof(Sample.A).ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo(Sample.A));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (SampleEnumExtensions.TryParse(nameof(Sample.B).ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo(Sample.B));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (SampleEnumExtensions.TryParse(nameof(Sample.C).ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo(Sample.C));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (SampleEnumExtensions.TryParse("10".ToLower(), out var value, true))
            {
                Assert.That(value, Is.EqualTo((Sample)10));
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
        var underlyingType = SampleEnumExtensions.GetUnderlyingType();
        Assert.That(underlyingType.FullName, Is.EqualTo("System.Int32"));

        var underlyingType2 = SampleWithByteEnumExtensions.GetUnderlyingType();
        Assert.That(underlyingType2.FullName, Is.EqualTo("System.Byte"));
    }
}
