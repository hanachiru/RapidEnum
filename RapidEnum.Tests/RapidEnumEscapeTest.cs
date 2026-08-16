using System.Runtime.Serialization;
using NUnit.Framework;

namespace RapidEnum.Tests;

[TestFixture]
public class RapidEnumEscapeTest
{
    [RapidEnum]
    internal enum NeedsEscaping
    {
        [EnumMember(Value = "quote\"inside")] Quote,
        [EnumMember(Value = @"back\slash")] Backslash,
        [EnumMember(Value = "line\nbreak")] NewLine,
        [EnumMember(Value = "tab\tstop")] Tab,
        [EnumMember(Value = "")] Empty
    }

    [Test]
    public void GetEnumMemberValueKeepsSpecialCharactersTest()
    {
        Assert.That(NeedsEscaping.Quote.GetEnumMemberValue(), Is.EqualTo("quote\"inside"));
        Assert.That(NeedsEscaping.Backslash.GetEnumMemberValue(), Is.EqualTo(@"back\slash"));
        Assert.That(NeedsEscaping.NewLine.GetEnumMemberValue(), Is.EqualTo("line\nbreak"));
        Assert.That(NeedsEscaping.Tab.GetEnumMemberValue(), Is.EqualTo("tab\tstop"));
        Assert.That(NeedsEscaping.Empty.GetEnumMemberValue(), Is.EqualTo(""));
    }
}
