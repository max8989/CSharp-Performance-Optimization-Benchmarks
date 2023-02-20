using BenchmarkDotNet.Attributes;

namespace Benchmark;


[MemoryDiagnoser]
public class SearchArray
{
    [Benchmark]
    public void SearchWithHashSet()
    {
        var testData = CreateData(100_000);
        var lookupData = CreateData(10_000);
        
        var lookupDataInt = testData.Select(x => x.Id).ToHashSet();
        
        testData.Where(x => lookupDataInt.Contains(x.Id)).ToList();
    }
    
    [Benchmark]
    public void SearchWithBinarySearch()
    {
        var testData = CreateData(100_000);
        var lookupData = CreateData(10_000);

        var lookupDataInt = lookupData.Select(x => x.Id).ToArray();
            
        testData.Where(x => lookupDataInt.Contains(x.Id)).ToList();
    }
    
    [Benchmark]
    public void SearchWithBinarySearchSpan()
    {
        var testData = CreateData(100_000);
        var lookupData = CreateData(10_000);

        Span<int> lookupDataInt =  lookupData.Select(x => x.Id).ToArray().AsSpan();
        
        for (int i = 0; i < testData.Count; i++)
            if(lookupDataInt.BinarySearch(testData[i].Id) > 0)
                testData.RemoveAt(i);
    }
    
    [Benchmark]
    public void SearchWithBinarySearchSpanStackAlloc()
    {
        var testData = CreateData(100_000);
        var lookupData = CreateData(10_000);

        Span<int> lookupDataInt = stackalloc int[lookupData.Count];
        for (int i = 0; i < lookupData.Count - 1; i++)
            lookupDataInt[i] = lookupData[i].Id;
        
        for (int i = 0; i < testData.Count; i++)
            if(lookupDataInt.BinarySearch(testData[i].Id) > 0)
                testData.RemoveAt(i);
    }
    
    
    
    public List<TestClass> CreateData(int nbItems)
    {
        Random rand = new Random();
        var data = new List<TestClass>();

        for (int i = 0; i < nbItems - 1; i++)
        {
            data.Add(new TestClass{Name = "Name", Id = rand.Next(0, 10_000)});
        }

        return data;
    }
    
}

public class TestClass
{
    public string Name { get; init; } = default!;
    public int Id { get; init; }
}