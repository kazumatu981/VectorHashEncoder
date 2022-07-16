using System;

namespace VectorHashEncoder.Lib;

public class BitsEncoder : IEnumerableEncoder<bool, int>
{
    private readonly int BitsInDigit;
    private int _digit = 0;
    private int _bitsCount = 0;

    public BitsEncoder(int bitsInDigit) => BitsInDigit = bitsInDigit;

    public IEnumerable<int> Encode(IEnumerable<bool> bits)
    {
        Reset();
        foreach (var bit in bits)
        {
            if (AddBitLast(bit) is int digit)
            {
                yield return digit;
            }
        }
    }
    protected int? AddBitLast(bool bit)
    {
        if (!(_bitsCount < BitsInDigit)) Reset();
        _digit = (_digit << 1) + (bit ? 1 : 0);
        return (++_bitsCount == BitsInDigit) ? _digit : null;
    }

    protected void Reset()
    {
        _digit = 0;
        _bitsCount = 0;
    }

}