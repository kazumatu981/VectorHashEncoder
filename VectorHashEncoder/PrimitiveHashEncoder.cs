using System.Collections.Generic;
using System.Linq;

using VectorHashEncoder.Lib;

namespace VectorHashEncoder;

public class PrimitiveHashEncoder
{
    #region Private Storage
    private readonly Segment[] InitialRectangle;
    private readonly char[]? CharactorTable;
    private readonly int BitsCount;
    #endregion


    #region Constructors
    /// <summary>
    /// コンストラクタ
    /// <remark>
    /// このコンストラクタでインスタンスを生成すると、EncodeToString()を呼び出すことができない。
    /// </remark>
    /// </summary>
    /// <param name="initialBoundary">初期メッシュ</param>
    /// <exception cref="ArgumentOutOfRangeException">初期メッシュの次元が0である。</exception>
    public PrimitiveHashEncoder((double, double)[] initialBoundary, int bitsCount)
    {
        if (initialBoundary.Length == 0) throw new ArgumentOutOfRangeException(nameof(initialBoundary));
        if (initialBoundary.Any(b => b.Item1 > b.Item2)) throw new ArgumentOutOfRangeException(nameof(initialBoundary));
        if (bitsCount <= 0 || bitsCount > 32) throw new ArgumentOutOfRangeException(nameof(bitsCount));

        InitialRectangle = initialBoundary
            .Select(boundary => new Segment(boundary))
            .ToArray();
        BitsCount = bitsCount;
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="initialBoundary">初期メッシュ</param>
    /// <param name="charactorTable">文字列エンコード時の文字テーブル</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// * 初期メッシュの次元が0である。
    /// * 文字テーブルが空。
    /// * 文字テーブルが2^32以上の長さがある。
    /// </exception>
    public PrimitiveHashEncoder((double, double)[] initialBoundary, char[] charactorTable)
        : this(initialBoundary, (int)Math.Log2(charactorTable.Length))
    {
        CharactorTable = charactorTable;
    }
    #endregion

    #region Public Property
    #region Read Only Property
    /// <summary>
    /// エンコード対象のベクトル空間の次元数
    /// </summary>
    public int Dimension => InitialRectangle.Length;
    #endregion
    #endregion

    #region Public Methods
    public IEnumerable<int> EncodeToInt(double[] vector, int levels)
    {
        if (vector.Length != Dimension) throw new ArgumentOutOfRangeException(nameof(vector));
        if (!IsInBoundaries(vector)) throw new ArgumentOutOfRangeException(nameof(vector));
        if (levels <= 0) throw new ArgumentOutOfRangeException(nameof(levels));

        var maxDepth = (levels * BitsCount) / Dimension;

        var flags = InitialRectangle
            .ShulinkInDeep(vector, maxDepth)
            .SelectMany(rectangle => rectangle)
            .Select(segment => segment.IsRightHandOfParent);

        return Bits
            .Encoder(BitsCount)
            .ToIntegers(flags);
    }

    public string EncodeToString(double[] vector, int levels)
        => CharactorTable switch
        {
            null => throw new NotSupportedException(),
            _ => string.Join("", EncodeToInt(vector, levels).Select(c => CharactorTable[c]))
        };

    public bool IsInBoundaries(double[] vector)
        => InitialRectangle.InInRectangle(vector);
    #endregion
}