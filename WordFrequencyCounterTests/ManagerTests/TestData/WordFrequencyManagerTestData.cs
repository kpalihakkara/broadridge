using System.Collections;

namespace WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

public sealed class WordFrequencyManagerTestData
{
    public static IEnumerable GetBaseTestCases()
    {
        // various examples
        yield return new TestCaseData("hello", 1);
        yield return new TestCaseData("my", 3);
    }

    public static IEnumerable GetExtendedTestCases()
    {
        // empty example
        yield return new TestCaseData(
            new string[] { },
            new int[] { });

        // various word population frequencies
        yield return new TestCaseData(
            new string[] { "hello", "my", "name" },
            new int[] { 3, 1, 2 });
    }
}