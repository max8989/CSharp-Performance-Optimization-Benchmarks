# CSharp_Benchmark



## Enum 
|                Method |      Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------- |----------:|----------:|----------:|-------:|----------:|
|          EnumToString | 15.299 ns | 0.2271 ns | 0.2124 ns | 0.0057 |      24 B |
|   EnumToString_Swtich |  3.890 ns | 0.1341 ns | 0.1744 ns |      - |         - |

## Guid (Span)
|                    Method |      Mean |    Error |    StdDev |    Median | Allocated |
|-------------------------- |----------:|---------:|----------:|----------:|----------:|
|          ToGuidFromString | 245.44 ns | 4.979 ns |  7.751 ns | 248.88 ns |     112 B |
| ToGuidFromStringOptimized |  98.41 ns | 9.571 ns | 28.220 ns |  88.10 ns |         - |
|           ToStingFromGuid | 142.35 ns | 2.718 ns |  4.832 ns | 140.67 ns |     184 B |

## String StartsWith
|                    Method |      Mean |     Error |    StdDev |    Median |   Gen0 | Allocated |
|-------------------------- |----------:|----------:|----------:|----------:|-------:|----------:|
|                StarstWith | 37.673 ns | 0.7501 ns | 0.7367 ns | 37.693 ns |      - |         - |
|           StartsWith_Span |  7.541 ns | 0.2191 ns | 0.6355 ns |  7.301 ns | 0.0076 |      32 B |
| StartsWith_SpanStackAlloc |  2.799 ns | 0.0584 ns | 0.0518 ns |  2.813 ns |      - |         - |
