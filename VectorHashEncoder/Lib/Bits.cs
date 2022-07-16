using System;

namespace VectorHashEncoder.Lib;

public class Bits
{
    private readonly int Capacity;
    public int Value { get; private set; }
    private int Digits = 0;

    public Bits(int capacity) => Capacity = capacity;

    public int? AddFlag(bool flag)
    {
        if (!(Digits < Capacity)) Reset();
        Value = (Value << 1) + (flag ? 1 : 0);
        return (++Digits == Capacity) ? Value : null;
    }

    public void Reset()
    {
        Value = 0;
        Digits = 0;
    }
}