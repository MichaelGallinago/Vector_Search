using System.Diagnostics;
using System.Text;

namespace VectorSearch;

internal static class Program
{
    private const byte MaxLength = 4;
    
    private static readonly Stopwatch Stopwatch = new();
    
    private static List<IReadOnlyList<double>> _vectors = [];
    
    private static void Main()
    {

        _vectors = //GenerateDataSet(4, MaxLength);
        [
            [1, 1, 7, 8],
            [3, 7, 5, 3],
            [3, 6, 1, 1],
            [0, 5, 7, 5]
        ];
        Print(_vectors);
        CheckElapsedTime(() => RadixSort.Sort(_vectors, MaxLength));
        Print(_vectors);

        //List<IReadOnlyList<double>> list1 = _vectors.ToList();
        //CheckElapsedTime(() => RadixSort.Sort(list1, MaxLength));
        
        //List<IReadOnlyList<double>> list2 = _vectors.ToList();
        //CheckElapsedTime(() => QuickSort.Sort(list2));
    }

    private static void CheckElapsedTime(Action action)
    {
        Stopwatch.Reset();
        Stopwatch.Start();
        action();
        Stopwatch.Stop();
        Console.WriteLine(Stopwatch.ElapsedTicks);
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
    
    private static void Print(List<IReadOnlyList<double>> lists)
    {
        var builder = new StringBuilder();
        foreach (IReadOnlyList<double> list in lists)
        {
            builder.Append('[');

            if (list.Count > 0)
            {
                builder.Append(list[0]);
            
                for (var i = 1; i < list.Count; i++)
                {
                    builder.Append(", ");
                    builder.Append(list[i]);
                }
            }
            
            builder.Append("]\n");
        }
        
        Console.WriteLine(builder.ToString());
    }
}