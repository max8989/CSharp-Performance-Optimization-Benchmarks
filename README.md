# CSharp_Benchmark



## Enum 
|                Method |      Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------- |----------:|----------:|----------:|-------:|----------:|
|          EnumToString | 15.299 ns | 0.2271 ns | 0.2124 ns | 0.0057 |      24 B |
| EnumToStringOptimized |  3.890 ns | 0.1341 ns | 0.1744 ns |      - |         - |

## Guid (Span)
|                    Method |      Mean |    Error |    StdDev |    Median | Allocated |
|-------------------------- |----------:|---------:|----------:|----------:|----------:|
|          ToGuidFromString | 245.44 ns | 4.979 ns |  7.751 ns | 248.88 ns |     112 B |
| ToGuidFromStringOptimized |  98.41 ns | 9.571 ns | 28.220 ns |  88.10 ns |         - |
|           ToStingFromGuid | 142.35 ns | 2.718 ns |  4.832 ns | 140.67 ns |     184 B |
