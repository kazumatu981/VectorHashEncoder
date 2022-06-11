namespace VectorHashEncoder.Lib;

public static class BoundaryAdjaster
{
    private static double Mid(this (double minimum, double maximum) thisBoundary) => (thisBoundary.minimum + thisBoundary.maximum) / 2;
    public static (double minimum, double maximum) Adjast(
        this (double minimum, double maximum) thisBoundary,
            double test, Action<double> OnAdjast)
    {
        var newBoundary = thisBoundary.Mid();
        if (test < newBoundary)
        {
            thisBoundary.maximum = newBoundary;
        }
        else
        {
            thisBoundary.minimum = newBoundary;
        }
        OnAdjast(newBoundary);
        return thisBoundary;
    }
}