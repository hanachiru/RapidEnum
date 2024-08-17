using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RapidEnum.Sample;

public class SampleEnum
{
    [RapidEnum]
    public enum Weather
    {
        Sun,
        Cloud,
        Rain,
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
    }


    [RapidEnumWithType(typeof(DateTimeKind))]
    public static partial class DateTimeKindEnumExtensions
    {
    }

    [RapidEnumWithType(typeof(JsonTokenType))]
    public static partial class JsonTokenTypeEnumExtensions
    {
    }
}