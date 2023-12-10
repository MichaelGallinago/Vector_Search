namespace VectorSearch;

public static class RadixSearch
{
    private const double Tolerance = 1E-9;
    private static IReadOnlyList<double> _target = null!;
    private static List<IReadOnlyList<double>> _vectors = null!;
    private static int _maxLength;
    
    public static int Search(IReadOnlyList<double> target, List<IReadOnlyList<double>> vectors, int maxLength)
    {
        _target = target;
        _vectors = vectors;
        _maxLength = maxLength;
        return Search(0, 0, vectors.Count - 1);
    }

    private static int Search(int exp, int left, int right)
    {
        int groupLeft = left;
        int groupRight = right;
        double targetValue = _target[exp];
        
        while (left <= right)
        {
            int mid = (left + right) / 2;
            switch (Math.Sign(_vectors[mid][exp] - targetValue))
            {
                case 0:
                    int i = mid;
                    for (; i >= groupLeft; i--)
                    { 
                        if (Math.Abs(_vectors[i][exp] - targetValue) >= Tolerance) break;
                    }
                    left = ++i;
                    
                    for (i = mid; i <= groupRight; i++)
                    {
                        if (Math.Abs(_vectors[i][exp] - targetValue) >= Tolerance) break;
                    }
                    right = --i;
                    
                    return exp < _maxLength - 1 ? Search(exp + 1, left, right) : mid;
                case -1:
                    left = mid + 1;
                    break;
                case 1:
                    right = mid - 1;
                    break;
            }
        }

        return -1;
    }
}