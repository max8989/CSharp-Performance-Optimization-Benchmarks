# CSharp_Benchmark



## Enum 
|                Method |      Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------- |----------:|----------:|----------:|-------:|----------:|
|          EnumToString | 15.299 ns | 0.2271 ns | 0.2124 ns | 0.0057 |      24 B |
|   EnumToString_Swtich |  3.890 ns | 0.1341 ns | 0.1744 ns |      - |         - |

## Guid (Span)
|                Method |      Mean |    Error |   StdDev |   Gen0 | Allocated |
|---------------------- |----------:|---------:|---------:|-------:|----------:|
|      ToGuidFromString |  94.61 ns | 1.890 ns | 2.022 ns | 0.0267 |     112 B |
| ToGuidFromString_Span |  60.12 ns | 1.215 ns | 1.664 ns |      - |         - |
|       ToStingFromGuid | 110.19 ns | 2.224 ns | 3.045 ns | 0.0439 |     184 B |
|  ToStingFromGuid_Span |  60.48 ns | 1.233 ns | 1.467 ns | 0.0172 |      72 B |

## String StartsWith
|                    Method |      Mean |     Error |    StdDev |    Median |   Gen0 | Allocated |
|-------------------------- |----------:|----------:|----------:|----------:|-------:|----------:|
|                StarstWith | 37.673 ns | 0.7501 ns | 0.7367 ns | 37.693 ns |      - |         - |
|           StartsWith_Span |  7.541 ns | 0.2191 ns | 0.6355 ns |  7.301 ns | 0.0076 |      32 B |
| StartsWith_SpanStackAlloc |  2.799 ns | 0.0584 ns | 0.0518 ns |  2.813 ns |      - |         - |

## Search Collection<int>
|                       Method |      Mean |     Error |    StdDev |     Gen0 |     Gen1 |     Gen2 | Allocated |
|----------------------------- |----------:|----------:|----------:|---------:|---------:|---------:|----------:|
|                     Contains | 15.723 ms | 0.3101 ms | 0.4348 ms | 468.7500 | 468.7500 | 468.7500 | 2102116 B |
|                      HashSet |  3.369 ms | 0.0599 ms | 0.0531 ms | 343.7500 | 320.3125 | 320.3125 | 2127411 B |
|                 BinarySearch | 14.408 ms | 0.1217 ms | 0.1079 ms | 500.0000 | 484.3750 | 484.3750 | 2102114 B |
|            BinarySearch_Span |  1.224 ms | 0.0244 ms | 0.0514 ms |        - |        - |        - |    4098 B |
| BinarySearch_Span_StackAlloc |  1.292 ms | 0.0258 ms | 0.0431 ms |        - |        - |        - |       2 B |


