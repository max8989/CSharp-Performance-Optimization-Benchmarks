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

    public static class Guider
    {
        public static string ToStringFromGuid(Guid id)
        {
            return Convert.ToBase64String(id.ToByteArray())
                .Replace("/", "-")
                .Replace("+", "_")
                .Replace("=", string.Empty);
        }

        public static Guid ToGuidFromString(string id)
        {
            byte[] efficientBase64 = Convert.FromBase64String(id
                .Replace("-", "/")
                .Replace("_", "+") + "==");

            return new Guid(efficientBase64);
        }

        public static Guid ToGuidFromStringOp(string id)
        {
            Span<char> base64Chars = stackalloc char[24];

            for (int i = 0; i < 22; i++)
            {
                base64Chars[i] = id[i] switch
                {
                    '/' => '-',
                    '_' => '+',
                    _ => id[i]
                };
            }

            base64Chars[22] = '=';
            base64Chars[23] = '=';

            Span<byte> idBytes = stackalloc byte[16];
            Convert.TryFromBase64Chars(base64Chars, idBytes, out _);
            return new Guid(idBytes);
        }
    }
}

