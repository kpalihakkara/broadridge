using System.Collections;

namespace WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

public sealed class OutputLineGeneratorTestData
{
    public static IEnumerable GetTestCases()
    {
        // empty example
        yield return new TestCaseData(
            new Dictionary<string, int>(),
            new string[] { });

        // populated lines example
        yield return new TestCaseData(
            new Dictionary<string, int>() { { "hello", 3 }, { "my", 3 }, { "name", 2 }, { "is", 1 }, { "test", 1 } },
            new string[] { "hello,3", "my,3", "name,2", "is,1", "test,1" });

        // populated lines example - test with initial entries not in desired order
        yield return new TestCaseData(
            new Dictionary<string, int>() { { "name", 2 }, { "is", 1 }, { "hello", 3 } },
            new string[] { "hello,3", "name,2", "is,1" });
    }
}