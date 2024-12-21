using System.Collections;

namespace Lab4.Tests;

public class CommandTestDataIncorrect : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new string[] { "connect" },
        };

        yield return new object[]
        {
            new string[] { "disconnect", "-m" },
        };

        yield return new object[]
        {
            new string[] { "file", "copy", "/source/test/path" },
        };

        yield return new object[]
        {
            new string[] { "file" },
        };

        yield return new object[]
        {
            new string[] { "file", "delete" },
        };

        yield return new object[]
        {
            new string[] { "file", "move", "/source/test/path" },
        };

        yield return new object[]
        {
            new string[] { "file", "rename", "/test/path" },
        };

        yield return new object[]
        {
            new string[] { "file", "show" },
        };

        yield return new object[]
        {
            new string[] { "tree", "goto" },
        };

        yield return new object[]
        {
            new string[] { "tree" },
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}