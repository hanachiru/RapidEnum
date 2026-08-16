namespace RapidEnum;

public sealed record EnumMemberInfo
{
    public EnumMemberInfo(string fullName, string? enumMemberValue)
    {
        FullName = fullName;
        EnumMemberValue = enumMemberValue;
    }

    /// <summary>Fully qualified member name, e.g. <c>My.Namespace.Weather.Sun</c>.</summary>
    public string FullName { get; }

    /// <summary>The <c>Value</c> named argument of <c>EnumMemberAttribute</c>.</summary>
    public string? EnumMemberValue { get; }
}
