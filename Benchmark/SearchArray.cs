using BenchmarkDotNet.Attributes;

namespace Benchmark;


[MemoryDiagnoser]
public class SearchArray
{
    private List<TestClass> testData;
    private List<TestClass> lookupData;
    public SearchArray()
    {
        testData = CreateData(100_000);
        lookupData = CreateData(1000);
    }
    
    [Benchmark]
    public void Contains()
    {
        var lookupDataInt = lookupData.Select(x => x.Id).ToArray();
        
        testData.Where(x => lookupDataInt.Contains(x.Id)).ToList();
    }
    
    [Benchmark]
    public void HashSet()
    {
        var lookupDataInt = lookupData.Select(x => x.Id).ToHashSet();
        
        testData.Where(x => lookupDataInt.Contains(x.Id)).ToList();
    }
    
    [Benchmark]
    public void BinarySearch()
    {
        var lookupDataInt = lookupData.Select(x => x.Id).ToArray();
            
        testData.Where(x => lookupDataInt.Contains(x.Id)).ToList();
    }
    
    [Benchmark]
    public void BinarySearch_Span()
    {
        Span<int> lookupDataInt =  lookupData.Select(x => x.Id).ToArray().AsSpan();
        
        for (int i = 0; i < testData.Count; i++)
            if(lookupDataInt.BinarySearch(testData[i].Id) > 0)
                testData.RemoveAt(i);
    }
    
    [Benchmark]
    public void BinarySearch_Span_StackAlloc()
    {
        Span<int> lookupDataInt = stackalloc int[lookupData.Count];
        for (int i = 0; i < lookupData.Count - 1; i++)
            lookupDataInt[i] = lookupData[i].Id;
        
        for (int i = 0; i < testData.Count; i++)
            if(lookupDataInt.BinarySearch(testData[i].Id) > 0)
                testData.RemoveAt(i);
    }
    
    
    
    public List<TestClass> CreateData(int nbItems)
    {
        Random rand = new Random(42);
        var data = new List<TestClass>();

        for (int i = 0; i < nbItems - 1; i++)
        {
            data.Add(new TestClass{Name = "Name", Id = rand.Next(0, 1000)});
        }

        return data.OrderBy(x => x.Id).ToList();
    }
    
}

public class TestClass
{
    public string Name { get; init; } = default!;
    public int Id { get; init; }
}