namespace VectorSearch;

public static class BinarySearch
{
    public static int Search(IReadOnlyList<double> target, List<IReadOnlyList<double>> vectors)
    {
        var left = 0;
        int right = vectors.Count - 1;
        
        while (left <= right)
        {
            int mid = (left + right) / 2;
            switch (VectorUtilities.GetDifferenceSign(vectors[mid], target))
            {
                case 0: 
                    return mid;
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