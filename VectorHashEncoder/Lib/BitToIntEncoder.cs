
using System.Collections.Generic;

namespace VectorHashEncoder.Lib;

public class BitsToIntEncoder
{
    public readonly List<int> Encoded;
    private readonly int BitsInInt;

    public BitsToIntEncoder(int bitsInInt, int capacity)
    {
        Encoded = new List<int>(capacity);
        BitsInInt = bitsInInt;
    }
    private int CurrentDigit = 0;
    private int BitsCount = 0;

    public bool IsFull => Encoded.Count == Encoded.Capacity;

    public void AddFlag(bool flag)
    {
        CurrentDigit = (CurrentDigit << 1) + (flag ? 1 : 0);

        if (++BitsCount == BitsInInt)
        {
            Encoded.Add(CurrentDigit);
            Reset();
        }
    }
    private void Reset()
    {
        CurrentDigit = 0;
        BitsCount = 0;
    }
}