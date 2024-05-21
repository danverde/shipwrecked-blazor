namespace Shared;

/// <summary>
/// List of null & whitespace strings to be used as class data for unit tests
/// </summary>
public class NullAndWhitespaceStrings : IEnumerable<object[]>
{
    private readonly List<string[]> _data = new List<string[]>
    {
        new string[] { null! },
        new string[] { "" },
        new string[] { " " },
        new string[] { "    " },
        new string[] { "\n" },
        new string[] { "\t" },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}