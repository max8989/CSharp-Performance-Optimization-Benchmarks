using BenchmarkDotNet.Attributes;

namespace GuidBenchmark
{
    [MemoryDiagnoser(false)]
	public class GuiderBenchmarks
	{
        private static readonly Guid TestGuidId = Guid.Parse("25fa07b1-cebd-4c78-b23a-a17b3f4845ce");
        private const string TestIdAsString = "sQf6Jb3OeEyyOqF7P0hFzg";

        [Benchmark]
        public Guid ToGuidFromString()
        {
            return Guider.ToGuidFromString(TestIdAsString);
        }

        [Benchmark]
        public Guid ToGuidFromStringOptimized()
        {
            return Guider.ToGuidFromStringOp(TestIdAsString);
        }

        [Benchmark]
        public string ToStingFromGuid()
        {
            return Guider.ToStringFromGuid(TestGuidId);
        }
    }
}

