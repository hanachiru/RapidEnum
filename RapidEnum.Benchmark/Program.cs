using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FastEnumUtility;
using RapidEnum;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<BenchmarkTarget>();
    }
}

[RapidEnum]
public enum Target
{
    A,
    B,
    C,
    D
}

[MemoryDiagnoser]
//[SimpleJob(RunStrategy.ColdStart)]
public class BenchmarkTarget
{
    [Benchmark]
    public void RapidEnum_GetValues()
    {
        _ = TargetEnumExtensions.GetValues();
    }

    [Benchmark]
    public void FastEnum_GetValues()
    {
        _ = FastEnum.GetValues<Target>();
    }
    
    [Benchmark]
    public void NET_GetValues()
    {
        _ = Enum.GetValues(typeof(Target)) as Target[];
    }

    [Benchmark]
    public void RapidEnum_GetNames()
    {
        _ = TargetEnumExtensions.GetNames();
    }

    [Benchmark]
    public void FastEnum_GetNames()
    {
        _ = FastEnum.GetNames<Target>();
    }

    [Benchmark]
    public void NET_GetNames()
    {
        _ = Enum.GetNames(typeof(Target));
    }

    [Benchmark]
    public void RapidEnum_GetName()
    {
        _ = TargetEnumExtensions.GetName(Target.B);
    }

    [Benchmark]
    public void FastEnum_GetName()
    {
        _ = FastEnum.GetName(Target.B);
    }

    [Benchmark]
    public void NET_GetName()
    {
        _ = Enum.GetName(typeof(Target), Target.B);
    }

    [Benchmark]
    public void RapidEnum_ToString()
    {
        _ = Target.A.ToStringFast();
    }

    [Benchmark]
    public void FastEnum_ToString()
    {
        _ = Target.A.FastToString();
    }

    [Benchmark]
    public void NET_ToString()
    {
        _ = Target.A.ToString();
    }

    [Benchmark]
    public void RapidEnum_IsDefines()
    {
        _ = TargetEnumExtensions.IsDefined("B");
    }

    [Benchmark]
    public void FastEnum_IsDefines()
    {
        _ = FastEnum.IsDefined<Target>("B");
    }

    [Benchmark]
    public void NET_IsDefines()
    {
        _ = Enum.IsDefined(typeof(Target), "B");
    }

    [Benchmark]
    public void RapidEnum_Parse()
    {
        _ = TargetEnumExtensions.Parse("A");
    }

    [Benchmark]
    public void FastEnum_Parse()
    {
        _ = FastEnum.Parse<Target>("A");
    }
    
    [Benchmark]
    public void NET_Parse()
    {
        _ = Enum.Parse<Target>("A");
    }

    [Benchmark]
    public void RapidEnum_TryParse()
    {
        _ = TargetEnumExtensions.TryParse("A", out var _);
    }

    [Benchmark]
    public void FastEnum_TryParse()
    {
        _ = FastEnum.TryParse<Target>("A", out var _);
    }

    [Benchmark]
    public void NET_TryParse()
    {
        _ = Enum.TryParse<Target>("A", out var _);
    }
}