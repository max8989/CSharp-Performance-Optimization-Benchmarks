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
|                               Method |     Mean |    Error |   StdDev |      Gen0 |      Gen1 |      Gen2 | Allocated |
|------------------------------------- |---------:|---------:|---------:|----------:|----------:|----------:|----------:|
|                    SearchWithHashSet | 17.66 ms | 0.346 ms | 0.650 ms | 1375.0000 | 1375.0000 | 1156.2500 |   8.12 MB |
|               SearchWithBinarySearch | 87.64 ms | 1.742 ms | 2.661 ms | 1000.0000 | 1000.0000 |  833.3333 |   6.65 MB |
|           SearchWithBinarySearchSpan | 20.03 ms | 0.300 ms | 0.266 ms |  968.7500 |  843.7500 |  500.0000 |   5.65 MB |
| SearchWithBinarySearchSpanStackAlloc | 20.21 ms | 0.200 ms | 0.187 ms |  968.7500 |  843.7500 |  500.0000 |   5.61 MB |

