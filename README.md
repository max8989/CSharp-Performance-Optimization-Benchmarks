
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
|                StartstWith | 37.673 ns | 0.7501 ns | 0.7367 ns | 37.693 ns |      - |         - |
|           StartsWith_Span |  7.541 ns | 0.2191 ns | 0.6355 ns |  7.301 ns | 0.0076 |      32 B |
| StartsWith_SpanStackAlloc |  2.799 ns | 0.0584 ns | 0.0518 ns |  2.813 ns |      - |         - |

## Search Collection<int> 
### 1 million items with 10,000 lookup values

|                            Method |        Mean |     Error |    StdDev | Allocated |
|---------------------------------- |------------:|----------:|----------:|----------:|
|                          Contains | 1,151.70 ms | 22.794 ms | 58.839 ms |   41032 B |
|                           HashSet |    12.83 ms |  0.196 ms |  0.241 ms |   58751 B |
|                 Sort_BinarySearch |    32.64 ms |  0.247 ms |  0.219 ms |   40158 B |
|            Sort_BinarySearch_Span |    28.79 ms |  0.170 ms |  0.142 ms |   40125 B |
| Sort_BinarySearch_Span_StackAlloc |    28.99 ms |  0.118 ms |  0.158 ms |      29 B |

### 500,000 items with 10,000 lookup values

|                            Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------------------- |-----------:|----------:|----------:|-------:|----------:|
|                          Contains | 573.185 ms | 3.1400 ms | 2.9372 ms |      - |   41032 B |
|                           HashSet |   6.619 ms | 0.0473 ms | 0.0420 ms | 7.8125 |   58743 B |
|                 Sort_BinarySearch |  16.249 ms | 0.0655 ms | 0.0580 ms |      - |   40125 B |
|            Sort_BinarySearch_Span |  14.579 ms | 0.0300 ms | 0.0251 ms |      - |   40111 B |
| Sort_BinarySearch_Span_StackAlloc |  14.540 ms | 0.0180 ms | 0.0151 ms |      - |      15 B |

### 100,000 items with 10,000 lookup values

|                            Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------------------- |-----------:|----------:|----------:|-------:|----------:|
|                          Contains | 144.576 ms | 0.3995 ms | 0.3336 ms |      - |   40330 B |
|                           HashSet |   1.980 ms | 0.0140 ms | 0.0131 ms | 7.8125 |   58740 B |
|                 Sort_BinarySearch |   5.043 ms | 0.0082 ms | 0.0073 ms |      - |   40103 B |
|            Sort_BinarySearch_Span |   4.607 ms | 0.0369 ms | 0.0345 ms |      - |   40103 B |
| Sort_BinarySearch_Span_StackAlloc |   4.653 ms | 0.0726 ms | 0.0679 ms |      - |       7 B |

### 20 items with 10 lookup values

|                            Method |     Mean |   Error |  StdDev |   Gen0 | Allocated |
|---------------------------------- |---------:|--------:|--------:|-------:|----------:|
|                          Contains | 145.7 ns | 2.91 ns | 4.70 ns | 0.0215 |     136 B |
|                           HashSet | 280.6 ns | 1.05 ns | 0.88 ns | 0.0381 |     240 B |
|                 Sort_BinarySearch | 213.4 ns | 3.02 ns | 2.83 ns | 0.0215 |     136 B |
|            Sort_BinarySearch_Span | 177.5 ns | 0.88 ns | 0.78 ns | 0.0215 |     136 B |
| Sort_BinarySearch_Span_StackAlloc | 125.6 ns | 0.67 ns | 0.62 ns |      - |         - |

### 1,000 items with 100,000 lookup values

|                            Method |       Mean |     Error |    StdDev |     Gen0 |     Gen1 |     Gen2 | Allocated |
|---------------------------------- |-----------:|----------:|----------:|---------:|---------:|---------:|----------:|
|                          Contains |   744.5 us |  28.27 us |  78.81 us | 124.0234 | 124.0234 | 124.0234 |  400180 B |
|                           HashSet | 1,940.4 us |  38.29 us |  98.14 us | 119.1406 |  89.8438 |  83.9844 |  538750 B |
|                 Sort_BinarySearch | 5,855.5 us |  61.08 us |  54.14 us | 117.1875 | 117.1875 | 117.1875 |  400182 B |
|            Sort_BinarySearch_Span | 5,949.9 us | 116.14 us | 162.82 us | 117.1875 | 117.1875 | 117.1875 |  400182 B |
| Sort_BinarySearch_Span_StackAlloc | 5,514.1 us | 103.96 us |  97.24 us |        - |        - |        - |       7 B |

  - Mean      : Arithmetic mean of all measurements
  - Error     : Half of 99.9% confidence interval
  - StdDev    : Standard deviation of all measurements
  - Median    : Value separating the higher half of all measurements (50th percentile)
  - Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  - 1 ms      : 1 Millisecond (0.001 sec)
  - 1 us      : 1 Microsecond (0.000001 sec)
  - 1 ns      : 1 Nanosecond




