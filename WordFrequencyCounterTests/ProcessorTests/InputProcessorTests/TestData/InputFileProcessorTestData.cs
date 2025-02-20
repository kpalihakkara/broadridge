using System.Collections;

namespace WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

public sealed class InputFileProcessorTestData
{
    public static IEnumerable GetTestCases()
    {
        // empty file example
        yield return new TestCaseData(
            new string[] { },
            new Dictionary<string, int>());

        // populated file example
        yield return new TestCaseData(
            new string[] { "hello my name is test", "hello my", "hello my name" },
            new Dictionary<string, int>() { { "hello", 3 }, { "my", 3 }, { "name", 2 }, { "is", 1 }, { "test", 1 } });
    }
}
