using BenchmarkDotNet.Attributes;
using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace GuidBenchmark
{
    [MemoryDiagnoser]
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
        public Guid ToGuidFromString_Span()
        {
            return Guider.ToGuidFromStringOp(TestIdAsString);
        }

        [Benchmark]
        public string ToStingFromGuid()
        {
            return Guider.ToStringFromGuid(TestGuidId);
        }

        [Benchmark]
        public string ToStingFromGuid_Span()
        {
            return Guider.ToStringFromGuidOp(TestGuidId);
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

        public static string ToStringFromGuidOp(Guid id)
        {
            Span<byte> idBytes = stackalloc byte[16];
            Span<byte> base64Bytes = stackalloc byte[24];

            MemoryMarshal.TryWrite(idBytes, ref id);
            Base64.EncodeToUtf8(idBytes, base64Bytes, out _, out _);

            Span<char> finalChars = stackalloc char[22];
            for (int i = 0; i < 22; i++)
            {
                finalChars[i] = base64Bytes[i] switch
                {
                    (byte) '/' => '-',
                    (byte) '+' => '_',
                    _ => (char) base64Bytes[i]
                };
            }

            return new string(finalChars);
        }
    }
}

