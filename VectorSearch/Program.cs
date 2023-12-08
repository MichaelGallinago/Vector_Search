using System.Text;

namespace VectorSearch;

internal static class Program
{
    private static List<IReadOnlyList<double>> _vectors = [];
    
    private static void Main()
    {
        _vectors = GenerateDataSet(4, 4);
            
        Print(_vectors);
        RadixSort.Sort(_vectors);
        Print(_vectors);
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
        
            for (var i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(',');
                    builder.Append(' ');
                }
                
                builder.Append(list[i]);
            }
        
            builder.Append(']');
            builder.Append('\n');
        }
        
        Console.WriteLine(builder.ToString());
    }
    
    private static int BinarySearch(IReadOnlyList<double> target, List<IReadOnlyList<double>> vectors)
    {
        var left = 0;
        int right = vectors.Count - 1;
        
        while (left <= right)
        {
            int mid = (left + right) / 2;
            switch (Math.Sign(GetVectorDifference(vectors[mid], target)))
            {
                case 0: 
                    return mid;
                case 1: 
                    left = mid + 1;
                    break;
                case -1: 
                    right = mid - 1;
                    break;
            }
        }

        return -1;
    }
    
    private static int GetVectorDifference(IReadOnlyList<double> firstVector, IReadOnlyList<double> secondVector)
    {
        var result = 0;
        int length = firstVector.Count;
        
        for (var i = 0; i < length; i++)
        {
            result += Math.Sign(firstVector[i] - secondVector[i]) * (1 << i);
        }
        
        return result;
    }
    
    private static bool VectorCheckCondition(int minimalRequirement, 
        IReadOnlyList<double> firstVector, IReadOnlyList<double> configurationVector)
    {
        var result = 0d;
        int length = configurationVector.Count;
        
        for (var i = 0; i < length; i++)
        {
            result += firstVector[i] * configurationVector[i];
        }
        
        return result >= minimalRequirement;
    }
}