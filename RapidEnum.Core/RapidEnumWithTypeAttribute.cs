using System;

namespace RapidEnum;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RapidEnumWithTypeAttribute : Attribute
{
    public Type Type { get; }

    public RapidEnumWithTypeAttribute(Type type)
    {
        Type = type;
    }
}