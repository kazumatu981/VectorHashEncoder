using System.Collections.Generic;
using System.Linq;

using VectorHashEncoder.Lib;

namespace VectorHashEncoder;

public class PrimitiveHashEncoder
{
    #region Private Storage
    private readonly (double, double)[] InitialBoundaries;
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

        InitialBoundaries = initialBoundary;
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
    {
        if (initialBoundary.Length == 0) throw new ArgumentOutOfRangeException(nameof(initialBoundary));
        if (initialBoundary.Any(b => b.Item1 > b.Item2)) throw new ArgumentOutOfRangeException(nameof(initialBoundary));
        if (charactorTable.Length == 0) throw new ArgumentOutOfRangeException(nameof(charactorTable));
        if ((int)Math.Log2(charactorTable.Length) > 32) throw new ArgumentOutOfRangeException(nameof(charactorTable));

        InitialBoundaries = initialBoundary;
        CharactorTable = charactorTable;
        BitsCount = (int)Math.Log2(charactorTable.Length);
    }
    #endregion

    #region Public Property
    #region Read Only Property
    /// <summary>
    /// エンコード対象のベクトル空間の次元数
    /// </summary>
    public int Dimension => InitialBoundaries.Length;
    #endregion
    #endregion

    #region Public Methods
    public IEnumerable<int> EncodeToInt(double[] vector, int levels)
    {
        if (vector.Length != Dimension) throw new ArgumentOutOfRangeException(nameof(vector));
        if (!IsInBoundaries(vector)) throw new ArgumentOutOfRangeException(nameof(vector));
        if (levels <= 0) throw new ArgumentOutOfRangeException(nameof(levels));

        var boundaries = InitialBoundaries;

        var bitsEncoder = new BitsToIntEncoder(BitsCount, levels);

        while (!bitsEncoder.IsFull)
        {
            boundaries = boundaries.Select(
                (boundary, i) => boundary.Adjast(
                    vector[i],
                    mid => bitsEncoder.AddFlag(mid < vector[i]))
            ).ToArray();
        }
        return bitsEncoder.Encoded;
    }

    public string EncodeToString(double[] vector, int levels)
        => CharactorTable switch
        {
            null => throw new NotSupportedException(),
            _ => string.Join("", EncodeToInt(vector, levels).Select(c => CharactorTable[c]))
        };

    public bool IsInBoundaries(double[] vector)
        => vector.Select((value, index) => (index, value))
            .All(element =>
                InitialBoundaries[element.index].Item1 < element.value
                    && element.value < InitialBoundaries[element.index].Item2);
    #endregion
}