using System;
using System.Text.Json;

namespace RapidEnum.Sample;

public class SampleEnum
{
    [RapidEnum]
    public enum Test
    {
        A,
        B,
        C
    }
    
    [RapidEnumMarker(typeof(DateTimeKind))]
    public static partial class DateTimeKindEnumExtensions { }

    [RapidEnumMarker(typeof(JsonTokenType))]
    public static partial class JsonTokenTypeEnumExtensions { }
}