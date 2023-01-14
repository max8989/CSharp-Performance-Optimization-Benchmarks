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
        public string EnumToString_Swtich()
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
                EnumTest.MySQL => nameof(EnumTest.MySQL),
                EnumTest.Dotnet => nameof(EnumTest.Dotnet),
                EnumTest.HelloWorld => nameof(EnumTest.HelloWorld),
                EnumTest.Linux => nameof(EnumTest.Linux),
                _ => throw new NotSupportedException()
            };
        }
    }
}
