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

## Search Collection<int> (10,000 lookup values)
|                       Method |       Mean |     Error |    StdDev |     Median |   Gen0 | Allocated |
|----------------------------- |-----------:|----------:|----------:|-----------:|-------:|----------:|
|                     Contains | 5,218.0 us | 100.02 us | 111.17 us | 5,210.0 us |      - |    4103 B |
|                      HashSet |   286.1 us |   4.90 us |   8.57 us |   285.0 us | 4.3945 |   27784 B |
|                 BinarySearch | 1,555.8 us |  35.38 us | 103.20 us | 1,588.0 us |      - |    4098 B |
|            BinarySearch_Span | 1,263.0 us |  25.04 us |  50.59 us | 1,271.7 us |      - |    4098 B |
| BinarySearch_Span_StackAlloc | 1,463.1 us |  37.52 us | 107.65 us | 1,473.4 us |      - |       2 B |
  
## Search Collection<int> (5,000 lookup values)
|                       Method |      Mean |    Error |   StdDev |   Gen0 |   Gen1 | Allocated |
|----------------------------- |----------:|---------:|---------:|-------:|-------:|----------:|
|                     Contains | 130.79 us | 1.783 us | 1.751 us | 3.1738 |      - |   20096 B |
|                      HashSet | 118.71 us | 0.980 us | 0.916 us | 9.2773 | 0.4883 |   58736 B |
|                 BinarySearch |  24.84 us | 0.493 us | 0.888 us | 3.1738 |      - |   20096 B |
|            BinarySearch_Span |  22.78 us | 0.446 us | 0.513 us | 3.1738 |      - |   20096 B |
| BinarySearch_Span_StackAlloc |  17.80 us | 0.042 us | 0.033 us |      - |      - |         - |




