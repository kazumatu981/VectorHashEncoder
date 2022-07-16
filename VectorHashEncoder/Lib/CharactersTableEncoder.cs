namespace VectorHashEncoder.Lib;

public class CharactersTableEncoder : IEnumerableEncoder<int, char>
{
    protected readonly char[] CharactersTable;

    public CharactersTableEncoder(char[] charactersTable) => CharactersTable = charactersTable;

    public IEnumerable<char> Encode(IEnumerable<int> source)
        => source.Select(i => CharactersTable[i]);
}

public static class CharaArrayUtils
{
    public static string JoinIntoString(this IEnumerable<char> chars, string seperator = "")
        => string.Join(seperator, chars);
}