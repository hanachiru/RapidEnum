using System;
using System.Linq;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace RapidEnum.Tests;

[TestFixture]
public class RapidEnumAliasTest
{
    [RapidEnum]
    public enum Aliased
    {
        [EnumMember(Value = "first")] First = 1,
        AliasOfFirst = 1,
        Second = 2
    }

    [Test]
    public void ToStringFastUsesFirstDeclaredNameTest()
    {
        // First
        Assert.That(Aliased.First.ToStringFast(), Is.EqualTo(Aliased.First.ToString()));
        
        // First
        Assert.That(Aliased.AliasOfFirst.ToStringFast(), Is.EqualTo(Aliased.AliasOfFirst.ToString()));
        
        // First
        Assert.That(Aliased.AliasOfFirst.ToStringFast(), Is.EqualTo(nameof(Aliased.First)));
    }

    [Test]
    public void GetNamesKeepsAliasesTest()
    {
        // First, AliasOfFirst, Second
        Assert.That(AliasedEnumExtensions.GetNames(), Is.EqualTo(Enum.GetNames<Aliased>()));
    }

    [Test]
    public void GetValuesKeepsAliasesTest()
    {
        // First, First, Second
        Assert.That(AliasedEnumExtensions.GetValues(), Is.EqualTo(Enum.GetValues<Aliased>()));
    }

    [Test]
    public void GetMembersPairsEveryNameWithItsValueTest()
    {
        var members = AliasedEnumExtensions.GetMembers();
        
        // First, AliasOfFirst, Second
        Assert.That(members.Select(x => x.Name), Is.EqualTo(Enum.GetNames<Aliased>()));
        
        // First, First, Second
        Assert.That(members.Select(x => x.Value), Is.EqualTo(Enum.GetValues<Aliased>()));
    }

    [Test]
    public void ParseResolvesBothNamesTest()
    {
        // First
        Assert.That(AliasedEnumExtensions.Parse(nameof(Aliased.First)), Is.EqualTo(Aliased.First));
        
        // First
        Assert.That(AliasedEnumExtensions.Parse(nameof(Aliased.AliasOfFirst)), Is.EqualTo(Aliased.First));
        
        Assert.That(AliasedEnumExtensions.IsDefined(nameof(Aliased.AliasOfFirst)), Is.True);
        Assert.That(AliasedEnumExtensions.IsDefined(Aliased.AliasOfFirst), Is.True);
    }

    [Test]
    public void GetEnumMemberValueUsesFirstDeclaredMemberTest()
    {
        Assert.That(Aliased.First.GetEnumMemberValue(), Is.EqualTo("first"));
        Assert.That(Aliased.AliasOfFirst.GetEnumMemberValue(), Is.EqualTo("first"));
        Assert.That(Aliased.Second.GetEnumMemberValue(), Is.Null);
    }
}
