namespace VectorSearch;

public static class QuickSort
{
    private static void Sort(List<IReadOnlyList<double>> arrays, int leftIndex, int rightIndex)
    {
        int i = leftIndex;
        int j = rightIndex;
        IReadOnlyList<double> pivot = arrays[leftIndex];
        while (i <= j)
        {
            while (VectorUtilities.GetDifference(arrays[i], pivot) < 0)
            {
                i++;
            }
        
            while (VectorUtilities.GetDifference(arrays[j], pivot) > 0)
            {
                j--;
            }

            if (i > j) continue;
            (arrays[i], arrays[j]) = (arrays[j], arrays[i]);
            i++;
            j--;
        }
    
        if (leftIndex < j)
            Sort(arrays, leftIndex, j);
        if (i < rightIndex)
            Sort(arrays, i, rightIndex);
    }

    public static void Sort(List<IReadOnlyList<double>> arrays)
    {
        Sort(arrays, 0, arrays.Count - 1);
    }
}