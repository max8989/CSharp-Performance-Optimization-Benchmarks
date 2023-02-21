using System.Runtime.InteropServices.JavaScript;
using BenchmarkDotNet.Attributes;

namespace Benchmark;

[MemoryDiagnoser]
public class SearchArray
{
    private List<TestClass> _testData;
    private List<TestClass> _lookupData;

    public SearchArray()
    {
        _testData = CreateData(1_000_000);
        _lookupData = CreateData(100_000);
    }

    [Benchmark]
    public void Contains()
    {
        var lookupDataInt = _lookupData
            .Select(x => x.Id)
            .ToArray();

        for (int i = 0; i < _testData.Count - 1; i++)
        {
            if (lookupDataInt.Contains(_testData[i].Id))
                _testData.RemoveAt(i);
        }
    }

    [Benchmark]
    public void HashSet()
    {
        var lookupDataInt = _lookupData
            .Select(x => x.Id)
            .ToHashSet();

        for (int i = 0; i < _testData.Count - 1; i++)
        {
            if (lookupDataInt.Contains(_testData[i].Id))
                _testData.RemoveAt(i);
        }
    }

    [Benchmark]
    public void Sort_BinarySearch()
    {
        var lookupDataInt = _lookupData
            .Select(x => x.Id)
            .ToArray();

        Array.Sort(lookupDataInt);

        for (int i = 0; i < _testData.Count - 1; i++)
        {
            if (Array.BinarySearch(lookupDataInt, _testData[i].Id) > 0)
                _testData.RemoveAt(i);
        }
    }

    [Benchmark]
    public void Sort_BinarySearch_Span()
    {
        Span<int> lookupDataInt = _lookupData
            .Select(x => x.Id)
            .ToArray()
            .AsSpan();

        lookupDataInt.Sort();

        for (int i = 0; i < _testData.Count - 1; i++)
        {
            if (lookupDataInt.BinarySearch(_testData[i].Id) > 0)
                _testData.RemoveAt(i);
        }
    }

    [Benchmark]
    public void Sort_BinarySearch_Span_StackAlloc()
    {
        Span<int> lookupDataInt = stackalloc int[_lookupData.Count];

        for (int i = 0; i < _lookupData.Count - 1; i++)
            lookupDataInt[i] = _lookupData[i].Id;

        lookupDataInt.Sort();

        for (int i = 0; i < _testData.Count - 1; i++)
        {
            if (lookupDataInt.BinarySearch(_testData[i].Id) > 0)
                _testData.RemoveAt(i);
        }
    }


    private List<TestClass> CreateData(int nbItems)
    {
        Random rand = new Random(42);

        return Enumerable.Range(1, nbItems)
            .Select(x => new TestClass { Id = rand.Next(0, nbItems / 10) })
            .ToList();
        
    }
}

public class TestClass
{
    public string Name { get; init; } = "Name";
    public int Id { get; init; }
}