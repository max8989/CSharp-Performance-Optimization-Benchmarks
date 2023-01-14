using Benchmark;
using BenchmarkDotNet.Running;
using EnumBenchmark;
using GuidBenchmark;

//BenchmarkRunner.Run<EnumBenchmarks>();
BenchmarkRunner.Run<GuiderBenchmarks>();
//BenchmarkRunner.Run<StringBenchmarks>();

//var stringBenchmark = new StringBenchmarks();
//Console.WriteLine($"{nameof(StringBenchmarks.StarstWith)}: {stringBenchmark.StarstWith()}");
//Console.WriteLine($"{nameof(StringBenchmarks.StartsWith_Span)}: {stringBenchmark.StartsWith_Span()}");
//Console.WriteLine($"{nameof(StringBenchmarks.StartsWith_SpanStackAlloc)}: {stringBenchmark.StartsWith_SpanStackAlloc()}");
//Console.ReadLine();

// var guidBenchmark = new GuiderBenchmarks();
// Console.WriteLine($"{nameof(GuiderBenchmarks.ToGuidFromString)}: {guidBenchmark.ToGuidFromString()}");
// Console.WriteLine($"{nameof(GuiderBenchmarks.ToGuidFromString_Span)}: {guidBenchmark.ToGuidFromString_Span()}");
// Console.WriteLine($"{nameof(GuiderBenchmarks.ToStingFromGuid)}: {guidBenchmark.ToStingFromGuid()}");
// Console.WriteLine($"{nameof(GuiderBenchmarks.ToStingFromGuid_Span)}: {guidBenchmark.ToStingFromGuid_Span()}");
// Console.ReadLine();