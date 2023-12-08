namespace VectorSearch;

public static class RadixSort 
{
    private const double Tolerance = 1E-9;
    public static void Sort(List<IReadOnlyList<double>> arrays)
    {
        if (arrays.Count <= 1) return;
        
        int maxLength = GetMaxLength(arrays);
        var buckets = new CustomPriorityQueue<(IReadOnlyList<double>, double)>();
        
        SortedSet<int> groups = [arrays.Count];
        Queue<int> savedGroups = [];
        
        for (var exp = 0; exp < maxLength; exp++)
        {
            var fromIndex = 0;
            foreach (int toIndex in groups)
            {
                if (toIndex - fromIndex <= 1)
                {
                    fromIndex = toIndex;
                    continue;
                }
                
                for (int j = fromIndex; j < toIndex; j++)
                {
                    IReadOnlyList<double> array = arrays[j];
                    double priority = exp >= array.Count ? 0 : array[exp];  
                    buckets.Enqueue((array, priority), priority);
                }
            
                (arrays[fromIndex], double lastPriority) = buckets.Dequeue();
                
                for (fromIndex++; fromIndex < toIndex; fromIndex++)
                {
                    (arrays[fromIndex], double priority) = buckets.Dequeue();
                    
                    if (Math.Abs(priority - lastPriority) > Tolerance)
                    {
                        savedGroups.Enqueue(fromIndex);
                    }

                    lastPriority = priority;
                }
                fromIndex++;
            }

            while (savedGroups.Count > 0)
            {
                groups.Add(savedGroups.Dequeue());
            }
        }
    }

    private static int GetMaxLength(List<IReadOnlyList<double>> arrays)
    {
        return arrays.Select(array => array.Count).Prepend(0).Max();
    }
}