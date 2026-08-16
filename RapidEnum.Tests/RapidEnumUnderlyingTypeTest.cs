using System;
using NUnit.Framework;

namespace RapidEnum.Tests;

[TestFixture]
public class RapidEnumUnderlyingTypeTest
{
    [RapidEnum]
    public enum LongBacked : long
    {
        Small = 1,
        Huge = 5_000_000_000
    }

    [RapidEnum]
    public enum ByteBacked : byte
    {
        A = 1,
        B = 2
    }

    [RapidEnum]
    public enum UlongBacked : ulong
    {
        A = 1,
        Max = ulong.MaxValue
    }

    [Test]
    public void ParsesNumbersBeyondIntRangeTest()
    {
        Assert.That(LongBackedEnumExtensions.TryParse("5000000000", out var value), Is.True);
        Assert.That(value, Is.EqualTo(LongBacked.Huge));
        Assert.That(LongBackedEnumExtensions.TryParse("5000000000", out value),
            Is.EqualTo(Enum.TryParse<LongBacked>("5000000000", out _)));
    }

    [Test]
    public void ParsesUlongMaxTest()
    {
        Assert.That(UlongBackedEnumExtensions.TryParse("18446744073709551615", out var value), Is.True);
        Assert.That(value, Is.EqualTo(UlongBacked.Max));
    }

    [Test]
    public void RejectsNumbersOutOfUnderlyingRangeTest()
    {
        Assert.That(ByteBackedEnumExtensions.TryParse("300", out var value), Is.False);
        Assert.That(value, Is.EqualTo(default(ByteBacked)));
        Assert.That(Enum.TryParse<ByteBacked>("300", out _), Is.False);
    }

    [Test]
    public void RejectsNegativeNumbersForUnsignedUnderlyingTypeTest()
    {
        Assert.That(ByteBackedEnumExtensions.TryParse("-1", out var value), Is.False);
        Assert.That(value, Is.EqualTo(default(ByteBacked)));
        Assert.That(Enum.TryParse<ByteBacked>("-1", out _), Is.False);
    }

    [Test]
    public void StillParsesInRangeNumbersTest()
    {
        Assert.That(ByteBackedEnumExtensions.TryParse("2", out var value), Is.True);
        Assert.That(value, Is.EqualTo(ByteBacked.B));

        Assert.That(ByteBackedEnumExtensions.TryParse("99", out var undefined), Is.True);
        Assert.That((byte)undefined, Is.EqualTo(99));
    }

    [Test]
    public void IgnoreCaseParseUsesUnderlyingTypeTooTest()
    {
        Assert.That(LongBackedEnumExtensions.TryParse("5000000000", out var value, ignoreCase: true), Is.True);
        Assert.That(value, Is.EqualTo(LongBacked.Huge));

        Assert.That(ByteBackedEnumExtensions.TryParse("300", out _, ignoreCase: true), Is.False);
    }
}
