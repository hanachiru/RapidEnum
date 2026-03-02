using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;

namespace RapidEnum.Sample;

[RapidEnumWithType(typeof(DateTimeKind))]
public static partial class DateTimeKindEnumExtensions
{
}

[RapidEnumWithType(typeof(JsonTokenType))]
public static partial class JsonTokenTypeEnumExtensions
{
}

public class SampleEnum
{
    [RapidEnum]
    public enum Weather
    {
        [EnumMember(Value = "sun")] Sun,
        [EnumMember] Cloud,
        [EnumMember(Value = "rain")] Rain,
        Snow
    }

    public void SampleUse()
    {
        // Sun,Cloud,Rain,Snow
        IReadOnlyList<Weather> values = WeatherEnumExtensions.GetValues();

        // Sun,Cloud,Rain,Snow
        IReadOnlyList<string> names = WeatherEnumExtensions.GetNames();

        // Rain
        string name = WeatherEnumExtensions.GetName(Weather.Rain);

        // Cloud
        string str = Weather.Cloud.ToStringFast();

        // True
        bool defined = WeatherEnumExtensions.IsDefined("Sun");

        // Sun
        Weather parse = WeatherEnumExtensions.Parse("Sun");

        // True
        // Sun
        bool tryParse = WeatherEnumExtensions.TryParse("Sun", out Weather value);

        // sun
        string enumMemberValue = Weather.Sun.GetEnumMemberValue();
    }

    public void SampleUse2()
    {
        // Unspecified,Utc,Local
        IReadOnlyList<DateTimeKind> values = DateTimeKindEnumExtensions.GetValues();

        // Unspecified,Utc,Local
        IReadOnlyList<string> names = DateTimeKindEnumExtensions.GetNames();

        // Local
        string name = DateTimeKindEnumExtensions.GetName(DateTimeKind.Local);

        // Local
        string str = DateTimeKind.Local.ToStringFast();

        // True
        bool defined = DateTimeKindEnumExtensions.IsDefined("Local");

        // Local
        DateTimeKind parse = DateTimeKindEnumExtensions.Parse("Local");

        // True
        // Local
        bool tryParse = DateTimeKindEnumExtensions.TryParse("Local", out DateTimeKind value);
    }
}