namespace VectorHashEncoder;

public static class BaseEncodeCharactors
{
    public static readonly char[] Base64 =
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
        'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
        'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f',
        'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
        'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z', '0', '1', '2', '3',
        '4', '5', '6', '7', '8', '9', '+', '/'
    };
    public static readonly char[] Base32 =
    {
        '0', '1', '2', '3', '4', '5', '6', '7',  // 00000 - 00111
        '8', '9', 'b', 'c', 'd', 'e', 'f', 'g',  // 01000 - 01111
        'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r',  // 10000 - 10111
        's', 't', 'u', 'v', 'w', 'x', 'y', 'z'   // 11000 - 11111
    };
}