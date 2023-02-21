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

## Search Collection<int> 500,000 items with 10,000 lookup values
|                            Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------------------- |-----------:|----------:|----------:|-------:|----------:|
|                          Contains | 573.185 ms | 3.1400 ms | 2.9372 ms |      - |   41032 B |
|                           HashSet |   6.619 ms | 0.0473 ms | 0.0420 ms | 7.8125 |   58743 B |
|                 Sort_BinarySearch |  16.249 ms | 0.0655 ms | 0.0580 ms |      - |   40125 B |
|            Sort_BinarySearch_Span |  14.579 ms | 0.0300 ms | 0.0251 ms |      - |   40111 B |
| Sort_BinarySearch_Span_StackAlloc |  14.540 ms | 0.0180 ms | 0.0151 ms |      - |      15 B |



  - Mean      : Arithmetic mean of all measurements
  - Error     : Half of 99.9% confidence interval
  - StdDev    : Standard deviation of all measurements
  - Median    : Value separating the higher half of all measurements (50th percentile)
  - Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
   - 1 us      : 1 Microsecond (0.000001 sec)
  - 1 ns      : 1 Nanosecond



