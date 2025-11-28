
# C# Performance Optimization Benchmarks

## Overview

We had a web application built with microservices where the search functionality was causing serious performance issues - taking **1 minute to execute** and consuming **1GB of memory per request**. 

While Kubernetes scaling provided a temporary solution, we needed to address the underlying problem. By applying modern C# performance techniques in .NET 6 like `Span<T>`, stack allocation, and efficient data structures, we achieved significant improvements:

- **80% faster query execution**
- **75% reduction in memory usage**
- **Dramatically improved scalability**

While Kubernetes helped us scale temporarily, these code-level optimizations provided the fundamental performance improvements needed for long-term sustainability.

## Code Example: The Power of Modern C#

Here's a real example from our benchmarks showing how we transformed a simple string operation:

```csharp
// Original approach: 37.6 ns, heap allocation
[Benchmark]
public bool StartsWith_Original()
{
    string comparisonString = "MAX";
    return INPUT.StartsWith(comparisonString);
}

// Span<T> approach: 7.5 ns, reduced allocation  
[Benchmark]
public bool StartsWith_Span() 
{
    ReadOnlySpan<char> inputSpan = INPUT.AsSpan();
    Span<char> comparisonSpan = new(new[] { 'M', 'A', 'X' });

    for (int i = 0; i < comparisonSpan.Length; i++)
    {
        if (comparisonSpan[i] != inputSpan[i])
            return false;
    }
    return true;
}

// Stack allocation: 2.8 ns, zero heap allocation!
[Benchmark]
public bool StartsWith_SpanStackAlloc()
{
    ReadOnlySpan<char> inputSpan = INPUT.AsSpan();
    Span<char> comparisonSpan = stackalloc char[] { 'M', 'A', 'X'};

    for (int i = 0; i < comparisonSpan.Length; i++)
    {
        if (comparisonSpan[i] != inputSpan[i])
            return false;
    }
    return true;
}
```

**Result**: 13x performance improvement with zero memory allocation!

## Benchmark Categories

### [Enum Performance](Benchmark/EnumBenchmarks.cs)
Comparing enum-to-string conversion methods to avoid boxing and allocation overhead.

|                Method |      Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------- |----------:|----------:|----------:|-------:|----------:|
|          EnumToString | 15.299 ns | 0.2271 ns | 0.2124 ns | 0.0057 |      24 B |
|   EnumToString_Switch |  3.890 ns | 0.1341 ns | 0.1744 ns |      - |         - |

** Key Insight**: Switch expressions eliminate memory allocation and provide 4x better performance than `ToString()`.

---

### [GUID Operations with Span](Benchmark/GuiderBenchmarks.cs)
Optimizing GUID-to-string conversions using `Span<T>` for zero-allocation operations.

|                Method |      Mean |    Error |   StdDev |   Gen0 | Allocated |
|---------------------- |----------:|---------:|---------:|-------:|----------:|
|      ToGuidFromString |  94.61 ns | 1.890 ns | 2.022 ns | 0.0267 |     112 B |
| ToGuidFromString_Span |  60.12 ns | 1.215 ns | 1.664 ns |      - |         - |
|       ToStringFromGuid | 110.19 ns | 2.224 ns | 3.045 ns | 0.0439 |     184 B |
|  ToStringFromGuid_Span |  60.48 ns | 1.233 ns | 1.467 ns | 0.0172 |      72 B |

**Key Insight**: `Span<T>` operations provide 40-50% performance improvement with significantly reduced allocations.

---

### [String Operations](Benchmark/StringBenchmarks.cs)
Comparing string StartsWith operations using different memory allocation strategies.

|                    Method |      Mean |     Error |    StdDev |    Median |   Gen0 | Allocated |
|-------------------------- |----------:|----------:|----------:|----------:|-------:|----------:|
|                StartsWith | 37.673 ns | 0.7501 ns | 0.7367 ns | 37.693 ns |      - |         - |
|           StartsWith_Span |  7.541 ns | 0.2191 ns | 0.6355 ns |  7.301 ns | 0.0076 |      32 B |
| StartsWith_SpanStackAlloc |  2.799 ns | 0.0584 ns | 0.0518 ns |  2.813 ns |      - |         - |

**Key Insight**: Stack allocation with `Span<T>` provides 13x better performance with zero heap allocation.

---

### [Collection Search Optimization](Benchmark/SearchArray.cs) - **The Game Changer**
This benchmark directly addresses our production search performance issues across different data sizes.

#### Performance Results by Collection Size

<details>
<summary><strong>🔴 1 Million Items (10,000 lookups)</strong></summary>

|                            Method |        Mean |     Error |    StdDev | Allocated |
|---------------------------------- |------------:|----------:|----------:|----------:|
|                          Contains | 1,151.70 ms | 22.794 ms | 58.839 ms |   41032 B |
|                           HashSet |    12.83 ms |  0.196 ms |  0.241 ms |   58751 B |
|                 Sort_BinarySearch |    32.64 ms |  0.247 ms |  0.219 ms |   40158 B |
|            Sort_BinarySearch_Span |    28.79 ms |  0.170 ms |  0.142 ms |   40125 B |
| Sort_BinarySearch_Span_StackAlloc |    28.99 ms |  0.118 ms |  0.158 ms |      29 B |

</details>

<details>
<summary><strong>🟡 500,000 Items (10,000 lookups)</strong></summary>

|                            Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------------------- |-----------:|----------:|----------:|-------:|----------:|
|                          Contains | 573.185 ms | 3.1400 ms | 2.9372 ms |      - |   41032 B |
|                           HashSet |   6.619 ms | 0.0473 ms | 0.0420 ms | 7.8125 |   58743 B |
|                 Sort_BinarySearch |  16.249 ms | 0.0655 ms | 0.0580 ms |      - |   40125 B |
|            Sort_BinarySearch_Span |  14.579 ms | 0.0300 ms | 0.0251 ms |      - |   40111 B |
| Sort_BinarySearch_Span_StackAlloc |  14.540 ms | 0.0180 ms | 0.0151 ms |      - |      15 B |

</details>

<details>
<summary><strong>🟢 100,000 Items (10,000 lookups)</strong></summary>

|                            Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------------------------- |-----------:|----------:|----------:|-------:|----------:|
|                          Contains | 144.576 ms | 0.3995 ms | 0.3336 ms |      - |   40330 B |
|                           HashSet |   1.980 ms | 0.0140 ms | 0.0131 ms | 7.8125 |   58740 B |
|                 Sort_BinarySearch |   5.043 ms | 0.0082 ms | 0.0073 ms |      - |   40103 B |
|            Sort_BinarySearch_Span |   4.607 ms | 0.0369 ms | 0.0345 ms |      - |   40103 B |
| Sort_BinarySearch_Span_StackAlloc |   4.653 ms | 0.0726 ms | 0.0679 ms |      - |       7 B |

</details>

<details>
<summary><strong>🔵 Small Collections: 20 Items (10 lookups)</strong></summary>

|                            Method |     Mean |   Error |  StdDev |   Gen0 | Allocated |
|---------------------------------- |---------:|--------:|--------:|-------:|----------:|
|                          Contains | 145.7 ns | 2.91 ns | 4.70 ns | 0.0215 |     136 B |
|                           HashSet | 280.6 ns | 1.05 ns | 0.88 ns | 0.0381 |     240 B |
|                 Sort_BinarySearch | 213.4 ns | 3.02 ns | 2.83 ns | 0.0215 |     136 B |
|            Sort_BinarySearch_Span | 177.5 ns | 0.88 ns | 0.78 ns | 0.0215 |     136 B |
| Sort_BinarySearch_Span_StackAlloc | 125.6 ns | 0.67 ns | 0.62 ns |      - |         - |

</details>

<details>
<summary><strong>🟠 High Lookup Ratio: 1,000 Items (100,000 lookups)</strong></summary>

|                            Method |       Mean |     Error |    StdDev |     Gen0 |     Gen1 |     Gen2 | Allocated |
|---------------------------------- |-----------:|----------:|----------:|---------:|---------:|---------:|----------:|
|                          Contains |   744.5 us |  28.27 us |  78.81 us | 124.0234 | 124.0234 | 124.0234 |  400180 B |
|                           HashSet | 1,940.4 us |  38.29 us |  98.14 us | 119.1406 |  89.8438 |  83.9844 |  538750 B |
|                 Sort_BinarySearch | 5,855.5 us |  61.08 us |  54.14 us | 117.1875 | 117.1875 | 117.1875 |  400182 B |
|            Sort_BinarySearch_Span | 5,949.9 us | 116.14 us | 162.82 us | 117.1875 | 117.1875 | 117.1875 |  400182 B |
| Sort_BinarySearch_Span_StackAlloc | 5,514.1 us | 103.96 us |  97.24 us |        - |        - |        - |       7 B |

</details>

** Key Insights**: 
- **HashSet**: Provides the best performance for large collections with excellent O(1) lookup time
- **Span + Stack Allocation**: Minimizes memory pressure and GC overhead
- **Algorithm choice matters**: The difference between O(n) and O(log n) operations becomes critical at scale

---

## Benchmark Legend

| Metric | Description |
|--------|-------------|
| **Mean** | Arithmetic mean of all measurements |
| **Error** | Half of 99.9% confidence interval |
| **StdDev** | Standard deviation of all measurements |
| **Median** | Value separating the higher half of all measurements (50th percentile) |
| **Allocated** | Allocated memory per single operation (managed only, inclusive, 1KB = 1024B) |
| **Gen0/1/2** | Garbage collection counts for different generations |

**Time Units:**
- 1 ms = 1 Millisecond (0.001 sec)
- 1 μs = 1 Microsecond (0.000001 sec)  
- 1 ns = 1 Nanosecond (0.000000001 sec)

---

##  Running the Benchmarks

```bash
cd Benchmark
dotnet run -c Release
```

## Real-World Impact

These optimizations transformed our production environment:

- **Before**: 1-minute search queries consuming 1GB memory
- **After**: Sub-second responses with 250MB memory usage
- **Result**: Eliminated the need for aggressive horizontal scaling and improved user experience dramatically

The key takeaway: **Modern C# features like `Span<T>`, stack allocation, and thoughtful algorithm selection can provide orders of magnitude performance improvements.**




