using BenchmarkDotNet.Attributes;

namespace EnumBenchmark
{
    public enum EnumTest
    {
        Yeeyee,
        MySQL,
        Dotnet,
        HelloWorld,
        Linux
    }

    [MemoryDiagnoser]
    public class EnumBenchmarks
    {
        [Benchmark]
        public string EnumToString()
        {
            var test = EnumTest.Dotnet;
            return test.ToString();
        }

        [Benchmark]
        public string EnumToStringOptimized()
        {
            var test = EnumTest.Dotnet;
            return test.EnumToString();
        }
    }

    public static class EnumExtentions
    {
        public static string EnumToString(this EnumTest enumTest)
        {
            return enumTest switch
            {
                EnumTest.Yeeyee => nameof(EnumTest.Yeeyee),
                EnumTest.MySQL => nameof(EnumTest.Yeeyee),
                EnumTest.Dotnet => nameof(EnumTest.Yeeyee),
                EnumTest.HelloWorld => nameof(EnumTest.Yeeyee),
                EnumTest.Linux => nameof(EnumTest.Yeeyee),
                _ => throw new NotSupportedException()
            };
        }
    }
}
