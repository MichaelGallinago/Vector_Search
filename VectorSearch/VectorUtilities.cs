namespace VectorSearch;

public static class VectorUtilities
{
    public static bool CheckCondition(int minimalRequirement, 
        IReadOnlyList<double> firstVector, IReadOnlyList<double> coefficients)
    {
        var result = 0d;

        for (var i = 0; i < coefficients.Count; i++)
        {
            result += firstVector[i] * coefficients[i];
        }
        
        return result >= minimalRequirement;
    }
    
    public static int GetDifference(IReadOnlyList<double> firstVector, IReadOnlyList<double> secondVector)
    {
        var result = 0;
        int length = firstVector.Count;
        
        for (var i = 0; i < length; i++)
        {
            result += Math.Sign(firstVector[i] - secondVector[i]) * (1 << (length - 1 - i));
        }
        
        return result;
    }
    
    private static int BinarySearch(IReadOnlyList<double> target, List<IReadOnlyList<double>> vectors)
    {
        var left = 0;
        int right = vectors.Count - 1;
        
        while (left <= right)
        {
            int mid = (left + right) / 2;
            switch (Math.Sign(GetDifference(vectors[mid], target)))
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
}