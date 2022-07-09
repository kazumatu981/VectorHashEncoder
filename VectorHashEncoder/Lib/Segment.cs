namespace VectorHashEncoder.Lib;

public class Segment
{
    public readonly bool IsRightHandOfParent;
    public readonly double Minimum;
    public readonly double Maximum;

    public double Mid => (Minimum + Maximum) / 2;

    public Segment((double, double) segment, bool isRightHandOfParent)
    {
        Minimum = segment.Item1;
        Maximum = segment.Item2;
        IsRightHandOfParent = isRightHandOfParent;
    }

    public Segment((double, double) segment) : this(segment, true) { }

    public Segment Shulink(double test)
        => Mid < test ?
            new Segment((Mid, Maximum), true) :
            new Segment((Minimum, Mid), false);

    public bool IsInRange(double test)
        => Minimum < test && test < Maximum;

}

public static class SegmentUtils
{
    public static bool InInRectangle(this Segment[] rectangle, double[] test)
        => rectangle.Select((segment, index) => (segment, index))
            .All(x => x.segment.IsInRange(test[x.index]));
    public static Segment[] Shulink(this Segment[] current, double[] test)
        => current.Select((segment, i) => segment.Shulink(test[i]))
            .ToArray();
    public static IEnumerable<Segment[]> ShulinkInDeep(this Segment[] current, double[] test, int MaxDepth)
    {
        var rectangle = current;
        for (var depth = 0; depth <= MaxDepth; depth++)
        {
            rectangle = rectangle.Shulink(test);
            yield return rectangle;
        }
    }
}