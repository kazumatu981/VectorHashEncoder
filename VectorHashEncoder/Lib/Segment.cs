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
    public Segment Shulink()
        => new((Minimum, Mid), false);
    public Segment ShiftToOrigin()
        => new((0.0, Maximum - Minimum), true);
    public bool IsInRange(double test)
        => Minimum < test && test < Maximum;

}

public static class SegmentUtils
{
    public static bool IsInRectangle(this Segment[] rectangle, double[] test)
        => rectangle.Select((segment, index) => (segment, index))
            .All(x => x.segment.IsInRange(test[x.index]));
    public static Segment[] Shulink(this Segment[] current, double[] test)
        => current.Select((segment, i) => segment.Shulink(test[i]))
            .ToArray();
    public static Segment[] Shulink(this Segment[] current)
        => current.Select((segment) => segment.Shulink())
            .ToArray();
    public static Segment[] ShiftToOrigin(this Segment[] current)
        => current.Select(segment => segment.ShiftToOrigin())
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
    public static IEnumerable<Segment[]> GetMeshes(this Segment[] baseRectangle, int MaxDepth)
    {
        var rectangle = baseRectangle.ShiftToOrigin();
        for (var depth = 0; depth <= MaxDepth; depth++)
        {
            rectangle = rectangle.Shulink();
            yield return rectangle;
        }
    }
}