using System.Collections.Generic;
using UnityEngine;
using RapidEnum;

public class Sample : MonoBehaviour
{
    [RapidEnum]
    public enum Weather
    {
        Sunny,
        Rainy,
        Cloudy,
        Snowy
    }

    public void Start()
    {
        // Sun,Cloud,Rain,Snow
        IReadOnlyList<Weather> values = WeatherEnumExtensions.GetValues();
        
        Debug.Log(string.Join(",", values));

        // Sun,Cloud,Rain,Snow
        IReadOnlyList<string> names = WeatherEnumExtensions.GetNames();
        
        Debug.Log(string.Join(",", names));

        // Rain
        string name = WeatherEnumExtensions.GetName(Weather.Rainy);
        
        Debug.Log(name);

        // Cloud
        string str = Weather.Cloudy.ToStringFast();
        
        Debug.Log(str);

        // True
        bool defined = WeatherEnumExtensions.IsDefined("Sun");
        
        Debug.Log(defined);

        // Sun
        Weather parse = WeatherEnumExtensions.Parse("Sun");
        
        Debug.Log(parse);

        // True
        // Sun
        bool tryParse = WeatherEnumExtensions.TryParse("Sun", out Weather value);
        
        Debug.Log(tryParse);
        Debug.Log(value);
    }
}
