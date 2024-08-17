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
        // Sunny,Rainy,Cloudy,Snowy
        IReadOnlyList<Weather> values = WeatherEnumExtensions.GetValues();
        
        Debug.Log(string.Join(",", values));

        // Sunny,Rainy,Cloudy,Snowy
        IReadOnlyList<string> names = WeatherEnumExtensions.GetNames();
        
        Debug.Log(string.Join(",", names));

        // Rainy
        string name = WeatherEnumExtensions.GetName(Weather.Rainy);
        
        Debug.Log(name);

        // Cloudy
        string str = Weather.Cloudy.ToStringFast();
        
        Debug.Log(str);

        // True
        bool defined = WeatherEnumExtensions.IsDefined("Sunny");
        
        Debug.Log(defined);

        // Sunny
        Weather parse = WeatherEnumExtensions.Parse("Sunny");
        
        Debug.Log(parse);

        // True
        // Sunny
        bool tryParse = WeatherEnumExtensions.TryParse("Sunny", out Weather value);
        
        Debug.Log(tryParse);
        Debug.Log(value);
    }
}
