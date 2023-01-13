using BenchmarkDotNet.Attributes;

namespace Benchmark
{

    [MemoryDiagnoser]
    public class StringBenchmarks
    {
        private const string INPUT = "MAXIME-Hello World";

        [Benchmark]
        public bool StarstWith()
        {
            string comparisonString = "MAX";
            return INPUT.StartsWith(comparisonString);
        }

        [Benchmark]
        public bool StartsWith_Span() 
        {
            ReadOnlySpan<char> inputSpan = INPUT.AsSpan();
            Span<char> comparisonSpan = new (new[] { 'M', 'A', 'X' });

            for (int i = 0; i < comparisonSpan.Length; i++)
            {
                if (comparisonSpan[i] != inputSpan[i])
                    return false;
            }

            return true;
            //return inputSpan.Slice(0, 3) == comparisonSpan;
        }

        [Benchmark]
        public bool StartsWith_SpanStackAlloc()
        {
            ReadOnlySpan<char> inputSpan = INPUT.AsSpan();
            Span<char> comparisonSpan = stackalloc char[] { 'M', 'A', 'X'};

            for (int i = 0; i < comparisonSpan.Length; i++)
            {
                if (comparisonSpan[i] != inputSpan[i])
                    return false;
            }

            return true;
        }
    }
}
