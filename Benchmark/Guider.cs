using System;
namespace SpanBenchmark
{
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

