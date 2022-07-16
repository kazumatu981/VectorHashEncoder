using System;
using VectorHashEncoder;
using Xunit;

namespace VectorHashEncoder.Tests;

public class TimeSpaceHashEncoderTests
{
    [Fact]
    public void EncodeToString_NearPlaceMustSameCode_Latitude()
    {
        double epsiron = 0.001;  // 0.001度
        var place1 = new GeoLocation()
        {
            Latitude = 38.258772,
            Longitude = 140.838918
        };
        var place2 = new GeoLocation()
        {
            Longitude = place1.Longitude,
            Latitude = place1.Latitude + epsiron
        };
        var place3 = new GeoLocation()
        {
            Longitude = place1.Longitude,
            Latitude = place1.Latitude + epsiron * 2
        };
        var place4 = new GeoLocation()
        {
            Longitude = place1.Longitude,
            Latitude = place1.Latitude + epsiron * 3
        };
        var time1 = new DateTime(2022, 6, 16, 21, 21, 00);

        var encoder = new TimeSpaceHashEncoder();

        var encoded1 = encoder.EncodeToString(place1, time1, 9);
        var encoded2 = encoder.EncodeToString(place2, time1, 9);
        var encoded3 = encoder.EncodeToString(place3, time1, 9);
        var encoded4 = encoder.EncodeToString(place4, time1, 9);


        // place1 and place2 must be near.
        Assert.Equal(encoded1, encoded2);

        // place1 and place3 must not be near.
        Assert.NotEqual(encoded1, encoded3);

        // but place3 and place4 must be near.
        Assert.Equal(encoded3, encoded4);
    }
    [Fact]
    public void EncodeToString_NearPlaceMustSameCode_Longitude()
    {
        double epsiron = 0.003;  // 0.003度
        var place1 = new GeoLocation()
        {
            Latitude = 38.258872,
            Longitude = 140.838918
        };
        var place2 = new GeoLocation()
        {
            Longitude = place1.Longitude + epsiron,
            Latitude = place1.Latitude
        };
        var place3 = new GeoLocation()
        {
            Longitude = place1.Longitude + epsiron * 2,
            Latitude = place1.Latitude
        };
        var place4 = new GeoLocation()
        {
            Longitude = place1.Longitude + epsiron * 3,
            Latitude = place1.Latitude
        };
        var time1 = new DateTime(2022, 6, 16, 21, 21, 00);

        var encoder = new TimeSpaceHashEncoder();

        var encoded1 = encoder.EncodeToString(place1, time1, 9);
        var encoded2 = encoder.EncodeToString(place2, time1, 9);
        var encoded3 = encoder.EncodeToString(place3, time1, 9);
        var encoded4 = encoder.EncodeToString(place4, time1, 9);


        // place1 and place2 must be near.
        Assert.Equal(encoded1, encoded2);

        // place1 and place3 must not be near.
        Assert.NotEqual(encoded1, encoded3);

        // but place3 and place4 must be near.
        Assert.Equal(encoded3, encoded4);
    }
    [Fact]
    public void EncodeToString_NearTimeMustSameCode()
    {
        double delta = 1000;     // 1s
        var place1 = new GeoLocation()
        {
            Latitude = 38.258872,
            Longitude = 140.838918
        };
        var time1 = new DateTime(2022, 6, 16, 21, 21, 01, 20);
        var time2 = time1.AddMilliseconds(delta);
        var time3 = time1.AddMilliseconds(delta * 2);
        var time4 = time1.AddMilliseconds(delta * 3);

        var encoder = new TimeSpaceHashEncoder();

        var encoded1 = encoder.EncodeToString(place1, time1, 9);
        var encoded2 = encoder.EncodeToString(place1, time2, 9);
        var encoded3 = encoder.EncodeToString(place1, time3, 9);
        var encoded4 = encoder.EncodeToString(place1, time4, 9);


        // time1 and time2 must be near.
        Assert.Equal(encoded1, encoded2);

        // time1 and time3 must not be near.
        Assert.NotEqual(encoded1, encoded3);

        // but time3 and time4 must be near.
        Assert.Equal(encoded3, encoded4);
    }
}