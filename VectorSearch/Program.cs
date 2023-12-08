﻿using System.Diagnostics;
using System.Text;

namespace VectorSearch;

internal static class Program
{
    private static readonly Stopwatch Stopwatch = new();
    
    private static List<IReadOnlyList<double>> _vectors = [];
    
    private static void Main()
    {
        _vectors = GenerateDataSet(4, 4);
            
        Print(_vectors);
        CheckElapsedTime(() => QuickSort.Sort(_vectors));
        Print(_vectors);
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