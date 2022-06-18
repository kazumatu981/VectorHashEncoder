namespace VectorHashEncoder;

public class GeoHashEncoder : PrimitiveHashEncoder
{
    private static readonly (double, double)[] RadiasBoundary = new[] { (-180.0, 180.0), (-90.0, 90.0) };
    public GeoHashEncoder() : base(RadiasBoundary, BaseEncodeCharactors.Base32)
    {
        // no-constructor action except base constructor action.
    }

    public string EncodeToString(GeoLocation geoLocation, int levels)
        => EncodeToString(new[] { geoLocation.Longitude, geoLocation.Latitude }, levels);

}