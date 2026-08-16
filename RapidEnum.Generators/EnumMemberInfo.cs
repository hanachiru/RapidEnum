namespace RapidEnum;

public sealed record EnumMemberInfo
{
    public EnumMemberInfo(string fullName, string? enumMemberValue, string constantValue)
    {
        FullName = fullName;
        EnumMemberValue = enumMemberValue;
        ConstantValue = constantValue;
    }

    /// <summary>Fully qualified member name, e.g. <c>My.Namespace.Weather.Sun</c>.</summary>
    public string FullName { get; }

    /// <summary>The <c>Value</c> named argument of <c>EnumMemberAttribute</c>.</summary>
    public string? EnumMemberValue { get; }

    /// <summary>
    /// The member's underlying constant, formatted invariantly. Members sharing one constant are aliases of each other, which value-keyed switches have to collapse.
    /// </summary>
    public string ConstantValue { get; }
}
