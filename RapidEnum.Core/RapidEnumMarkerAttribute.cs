using System;

namespace RapidEnum;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RapidEnumMarkerAttribute : Attribute
{
    public Type Type { get; }

    public RapidEnumMarkerAttribute(Type type)
    {
        Type = type;
    }
}