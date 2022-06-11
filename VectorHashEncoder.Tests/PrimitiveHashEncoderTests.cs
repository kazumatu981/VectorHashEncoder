using System;
using System.Linq;
using VectorHashEncoder;
using Xunit;

namespace VectorHashEncoder.Tests;

public class PrimitiveHashEncoderTests
{
    [Fact]
    public void ConstructWithNoTable_WithEmptyBoundary()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _ = new PrimitiveHashEncoder(Array.Empty<(double, double)>(), 5);
        });
    }
    [Fact]
    public void ConstructWithNoTable_WithNegativeBits()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _ = new PrimitiveHashEncoder(Array.Empty<(double, double)>(), -1);
        });
    }
    [Fact]
    public void ConstructWithNoTable_VeryLargeBits()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _ = new PrimitiveHashEncoder(Array.Empty<(double, double)>(), 33);
        });
    }
    [Fact]
    public void NotSupportEncodeToString_WhenConstructWithNoTable()
    {
        var encoder = new PrimitiveHashEncoder(
                new[] { (-180.0, 180.0), (-90.0, 90.0) }, 5);
        Assert.Throws<NotSupportedException>(() =>
        {
            _ = encoder.EncodeToString(new[] { 140.625000, 36.562500 }, 1);
        });
    }
    [Fact]
    public void NotSupportEncodeToInt_WhenConstructWithNoTable()
    {
        var encoder = new PrimitiveHashEncoder(
                new[] { (-180.0, 180.0), (-90.0, 90.0) }, 5);
        var encoded = encoder.EncodeToInt(new[] { 140.625000, 36.562500 }, 1);

        Assert.Single(encoded);
        Assert.Equal(29, encoded.First());
    }

    [Theory]
    [InlineData(36.562500, 140.625000, "xn")]
    [InlineData(38.262634, 140.883179, "xnu1tv")]
    [InlineData(38.262634, 140.850220, "xnu1tj")]
    [InlineData(35.681448, 139.765835, "xn76urw7")]
    [InlineData(38.258944, 140.839062, "xnu1sugf")]
    public void StandardTest(double lat, double lon, string expected)
    {
        var encoder = new PrimitiveHashEncoder(
                new[] { (-180.0, 180.0), (-90.0, 90.0) }, EncoderUtils.Base32Charactors);
        var actual = encoder.EncodeToString(new[] { lon, lat }, expected.Length);
        Assert.Equal(expected, actual);
    }
}