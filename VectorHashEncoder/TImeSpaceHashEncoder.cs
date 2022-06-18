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
        var timeValue = (((time.Hour * 60) + time.Minute) * 60 + time.Second) * 1000 + time.Millisecond;

        return EncodeToString(new[]
        {
            geoLocation.Longitude,
            geoLocation.Latitude,
            timeValue
        }, levels);
    }

}