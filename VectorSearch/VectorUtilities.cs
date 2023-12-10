namespace VectorSearch;

public static class VectorUtilities
{
    private const double Tolerance = 1E-9;
    
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
    
    public static int GetDifferenceSign(IReadOnlyList<double> firstVector, IReadOnlyList<double> secondVector)
    {
        int length = firstVector.Count;
        
        for (var i = 0; i < length; i++)
        {
            int sign = Math.Sign(firstVector[i] - secondVector[i]);
            if (sign != 0)
            {
                return sign;
            }
        }
        
        return 0;
    }
}