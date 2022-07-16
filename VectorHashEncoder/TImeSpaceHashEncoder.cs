namespace VectorHashEncoder;

public class TimeSpaceHashEncoder : PrimitiveHashEncoder
{
    private static readonly (double, double)[] DefaultBoundary =
        new[]
        {
            (-180.0, 180.0),
            (-90.0, 90.0) ,
            (0, 24 * 60 * 60 * 1000)
        };
    public TimeSpaceHashEncoder() : base(DefaultBoundary, BaseEncodeCharactors.Base32)
    {
        // no-constructor action except base constructor action.
    }

    public string EncodeToString(GeoLocation geoLocation, DateTime time, int levels)
    {
        return time.ToString("yyyyMMdd") + "-" + EncodeToString(new[]
        {
            geoLocation.Longitude,
            geoLocation.Latitude,
            time.MillisecondsFromMidNight()
        }, levels);
    }
}

public static class DateTimeUtils
{
    public static int MillisecondsFromMidNight(this DateTime thisDateTime)
        => (int)thisDateTime.Subtract(new DateTime(thisDateTime.Year, thisDateTime.Month, thisDateTime.Day)).TotalMilliseconds;
}