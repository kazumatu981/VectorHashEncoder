using System.Collections.Generic;

namespace VectorHashEncoder.Lib;

public interface IEnumerableEncoder<TSource, TTarget>
{
    IEnumerable<TTarget> Encode(IEnumerable<TSource> source);
}

public static class EncoderUtil
{
    public static IEnumerable<TTarget> Encode<TSource, TTarget>(
        this IEnumerable<TSource> source, IEnumerableEncoder<TSource, TTarget> encoder)
        => encoder.Encode(source);
}