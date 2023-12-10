namespace VectorSearch;

public class BaseSearch
{
    public static int Search(IReadOnlyList<double> target, List<IReadOnlyList<double>> vectors)
    {
        for (var i = 0; i < vectors.Count; i++)
        {
            if (VectorUtilities.GetDifferenceSign(target, vectors[i]) == 0)
            {
                return i;
            }
        }

        return -1;
    }
}