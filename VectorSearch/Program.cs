using System.Diagnostics;
using System.Text;

namespace VectorSearch;

internal static class Program
{
    private const byte MaxLength = 4;
    private const int Count = 256;
    
    private static readonly Stopwatch Stopwatch = new();
    
    private static List<IReadOnlyList<double>> _vectors = [];

    private static readonly Random Random = new();
    
    private static void Main()
    {
        //Console.WriteLine(VectorUtilities.GetDifference([4, 5, 1, 6], [6, 2, 2, 1]));
        //TestSearch();
        //TestSort();
        //TestSortTime();
        //TestSearchTime();
    }
    
    private static void TestSearch()
    {
        _vectors = GenerateDataSet(Count, MaxLength);
        
        RadixSort.Sort(_vectors, MaxLength);
        
        PrintVectors(_vectors);
        int index = Random.Next() % Count;
        Console.WriteLine($"Index: {index}");
        PrintVector(_vectors[index]);
        Console.WriteLine(RadixSearch.Search(_vectors[index], _vectors, MaxLength));
    }
    
    private static void TestSearchTime()
    {
        CheckElapsedTimeSearch((vector, vectors) => 
            RadixSearch.Search(vector, vectors, MaxLength));
        CheckElapsedTimeSearch(BinarySearch.Search);
    }
    
    private static void CheckElapsedTimeSearch(Func<IReadOnlyList<double>, List<IReadOnlyList<double>>, int> action)
    {
        Stopwatch.Reset();
        Stopwatch.Start();
        for (var i = 0; i < 10000; i++)
        {
            List<IReadOnlyList<double>> vectors = GenerateDataSet(Count, MaxLength);
            RadixSort.Sort(vectors, MaxLength);
            int index = Random.Next() % Count;
            Stopwatch.Start();
            action(vectors[index], vectors);
            Stopwatch.Stop();
        }
        Console.WriteLine(Stopwatch.Elapsed.TotalNanoseconds / 10000);
    }

    private static void TestSort()
    {
        _vectors = GenerateDataSet(Count, MaxLength);
        
        PrintVectors(_vectors);
        RadixSort.Sort(_vectors, MaxLength);
        PrintVectors(_vectors);
    }

    private static void TestSortTime()
    {
        CheckElapsedTimeSort(vector => RadixSort.Sort(vector, MaxLength));
        CheckElapsedTimeSort(QuickSort.Sort);
    }

    private static void CheckElapsedTimeSort(Action<List<IReadOnlyList<double>>> action)
    {
        Stopwatch.Reset();
        Stopwatch.Start();
        for (var i = 0; i < 1000; i++)
        {
            List<IReadOnlyList<double>> vector = GenerateDataSet(Count, MaxLength);
            Stopwatch.Start();
            action(vector);
            Stopwatch.Stop();
        }
        Console.WriteLine(Stopwatch.Elapsed.TotalNanoseconds / 1000);
    }
    
    private static List<IReadOnlyList<double>> GenerateDataSet(int count, int length)
    {
        var random = new Random();
        List<IReadOnlyList<double>> dataSet = [];
        for (var i = 0; i < count; i++)
        {
            var vector = new double[length];
            for (var j = 0; j < length; j++)
            {
                vector[j] = random.Next() % 10;
            }
            
            dataSet.Add(vector);
        }
        
        return dataSet;
    }
    
    private static void PrintVectors(List<IReadOnlyList<double>> lists)
    {
        foreach (IReadOnlyList<double> vector in lists)
        {
            PrintVector(vector);
        }
    }
    
    private static void PrintVector(IReadOnlyList<double> vector)
    {
        var builder = new StringBuilder();

            builder.Append('[');

            if (vector.Count > 0)
            {
                builder.Append(vector[0]);
            
                for (var i = 1; i < vector.Count; i++)
                {
                    builder.Append(", ");
                    builder.Append(vector[i]);
                }
            }
            
            builder.Append(']');
        
        Console.WriteLine(builder.ToString());
    }
}