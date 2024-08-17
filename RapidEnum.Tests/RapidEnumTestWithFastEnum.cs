using System.Linq;
using FastEnumUtility;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace RapidEnum.Tests;

[TestFixture]
public class RapidEnumTestWithFastEnum
{
    [RapidEnum]
    internal enum Target
    {
        A,
        B,
        C
    }
    
    [Test]
    public void ToStringFastPassTest()
    {
        Assert.That(Target.A.ToStringFast(), Is.EqualTo(Target.A.FastToString()));
        Assert.That(Target.B.ToStringFast(), Is.EqualTo(Target.B.FastToString()));
        Assert.That(Target.C.ToStringFast(), Is.EqualTo(Target.C.FastToString()));
        Assert.That(((Target)10).ToStringFast(), Is.EqualTo(((Target)10).FastToString()));
    }

    [Test]
    public void IsDefinedUsingEnumPassTest()
    {
        Assert.That(TargetEnumExtensions.IsDefined(Target.A), Is.EqualTo(FastEnum.IsDefined(Target.A)));
        Assert.That(TargetEnumExtensions.IsDefined(Target.B), Is.EqualTo(FastEnum.IsDefined(Target.B)));
        Assert.That(TargetEnumExtensions.IsDefined(Target.C), Is.EqualTo(FastEnum.IsDefined(Target.C)));
        Assert.That(TargetEnumExtensions.IsDefined((Target)int.MaxValue), Is.EqualTo(FastEnum.IsDefined((Target)int.MaxValue)));
    }

    [Test]
    public void IsDefinedUsingNamePassTest()
    {
        Assert.That(TargetEnumExtensions.IsDefined(nameof(Target.A)), Is.EqualTo(FastEnum.IsDefined<Target>(nameof(Target.A))));
        Assert.That(TargetEnumExtensions.IsDefined(nameof(Target.B)), Is.EqualTo(FastEnum.IsDefined<Target>(nameof(Target.B))));
        Assert.That(TargetEnumExtensions.IsDefined(nameof(Target.C)), Is.EqualTo(FastEnum.IsDefined<Target>(nameof(Target.C))));
        Assert.That(TargetEnumExtensions.IsDefined("10"), Is.EqualTo(FastEnum.IsDefined<Target>("10")));
    }

    [Test]
    public void GetValuesPassTest()
    {
        var actual = TargetEnumExtensions.GetValues();
        var expected = FastEnum.GetValues<Target>();

        CollectionAssert.AreEqual(actual, expected);
    }

    [Test]
    public void GetNamesPassTest()
    {
        var actual = TargetEnumExtensions.GetNames();
        var expected = FastEnum.GetNames<Target>();
        
        CollectionAssert.AreEqual(actual, expected);
    }

    [Test]
    public void GetMembersPassTest()
    {
        var actual = TargetEnumExtensions.GetMembers();
        var expected = FastEnum.GetMembers<Target>();
        
        CollectionAssert.AreEqual(actual.Select(x => x.Name), expected.Select(x => x.Name));
        CollectionAssert.AreEqual(actual.Select(x => x.Value), expected.Select(x => x.Value));
    }
    
    [Test]
    public void TryParsePassTest()
    {
        {
            if (TargetEnumExtensions.TryParse(nameof(Target.A), out var actual) &&
                FastEnum.TryParse<Target>(nameof(Target.A), out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (TargetEnumExtensions.TryParse(nameof(Target.B), out var actual) &&
                FastEnum.TryParse<Target>(nameof(Target.B), out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (TargetEnumExtensions.TryParse(nameof(Target.C), out var actual) &&
                FastEnum.TryParse<Target>(nameof(Target.C), out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (TargetEnumExtensions.TryParse("10", out var actual) &&
                FastEnum.TryParse<Target>("10", out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
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
            if (TargetEnumExtensions.TryParse(nameof(Target.A).ToLower(), out var actual, true)
                && FastEnum.TryParse<Target>(nameof(Target.A).ToLower(), true, out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (TargetEnumExtensions.TryParse(nameof(Target.B).ToLower(), out var actual, true)
                && FastEnum.TryParse<Target>(nameof(Target.B).ToLower(), true, out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (TargetEnumExtensions.TryParse(nameof(Target.C).ToLower(), out var actual, true)
                && FastEnum.TryParse<Target>(nameof(Target.C).ToLower(), true, out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail();
            }
        }
        {
            if (TargetEnumExtensions.TryParse("10".ToLower(), out var actual, true)
                && FastEnum.TryParse<Target>("10".ToLower(), true, out var expected))
            {
                Assert.That(actual, Is.EqualTo(expected));
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
        var actual = TargetEnumExtensions.GetUnderlyingType();
        var expected = FastEnum.GetUnderlyingType<Target>();
        
        Assert.That(actual.FullName, Is.EqualTo(expected.FullName));
    }
}
