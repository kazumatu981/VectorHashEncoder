using System;
using VectorHashEncoder;
using Xunit;

namespace VectorHashEncoder.Tests;

public class GeoHashEncoderTests
{
    [Theory]
    [InlineData(36.562500, 140.625000, "xn")]
    [InlineData(38.262634, 140.883179, "xnu1tv")]
    [InlineData(38.262634, 140.850220, "xnu1tj")]
    [InlineData(35.681448, 139.765835, "xn76urw7")]
    [InlineData(38.258944, 140.839062, "xnu1sugf")]
    public void StandardTest(double lat, double lon, string expected)
    {
        var encoder = new GeoHashEncoder();
        var actual = encoder.EncodeToString(
            new GeoLocation() { Longitude = lon, Latitude = lat },
            expected.Length);
        Assert.Equal(expected, actual);
    }
}