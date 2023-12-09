namespace VectorSearch;

public static class RadixSort 
{
    private const double Tolerance = 1E-9;
    private static List<IReadOnlyList<double>> _arrays = null!;
    private static int _maxLength;
    
    public static void Sort(List<IReadOnlyList<double>> arrays, int maxLength)
    {
        if (maxLength <= 0 || arrays.Count <= 1) return;

        _arrays = arrays;
        _maxLength = maxLength;
        
        SortRange(0, arrays.Count - 1, 0);
        if (maxLength <= 1) return;
        SortExp(0, 0, arrays.Count);
    }

    private static void SortExp(int exp, int fromIndex, int toIndex)
    {
        if (exp == _maxLength) return;
        double groupValue = _arrays[fromIndex][exp];
        int currentIndex = fromIndex + 1;
        for (; currentIndex < toIndex; currentIndex++)
        {
            double nextValue = _arrays[currentIndex][exp];
            if (Math.Abs(groupValue - nextValue) <= Tolerance) continue;
            SortRange(fromIndex, currentIndex, exp);
            SortExp(exp + 1, fromIndex, currentIndex + 1);
            fromIndex = currentIndex;
            groupValue = nextValue;
        }
            
        if (--currentIndex == fromIndex) return;
        SortRange(fromIndex, currentIndex, exp);
    }

    private static void SortRange(int leftIndex, int rightIndex, int exp)
    {
        while (true)
        {
            int i = leftIndex;
            int j = rightIndex;
            IReadOnlyList<double> pivot = _arrays[leftIndex];
            while (i <= j)
            {
                while (_arrays[i][exp] < pivot[exp])
                {
                    i++;
                }

                while (_arrays[j][exp] > pivot[exp])
                {
                    j--;
                }

                if (i > j) continue;
                (_arrays[i], _arrays[j]) = (_arrays[j], _arrays[i]);
                i++;
                j--;
            }

            if (leftIndex < j) SortRange(leftIndex, j, exp);
            if (i < rightIndex)
            {
                leftIndex = i;
                continue;
            }

            break;
        }
    }
}