using System.Collections;

namespace WordFrequencyCounterTests.ProcessorTests.OutputProcessorTests.TestData;

public sealed class OutputFileProcessorTestData
{
    public static IEnumerable GetTestCases()
    {
        // empty file example
        yield return new TestCaseData(
            new Dictionary<string, int>(),
            new string[] { });

        // populated file example
        yield return new TestCaseData(
            new Dictionary<string, int>() { { "hello", 3 }, { "my", 3 }, { "name", 2 }, { "is", 1 }, { "test", 1 } },
            new string[] { "hello,3", "my,3", "name,2", "is,1", "test,1" });
    }
}
