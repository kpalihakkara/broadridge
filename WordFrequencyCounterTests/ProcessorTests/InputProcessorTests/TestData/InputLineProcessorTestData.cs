using System.Collections;

namespace WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

public sealed class InputLineProcessorTestData
{
    public static IEnumerable GetTestCases()
    {
        // A test line with cancellation requested
        yield return new TestCaseData(
            CTestLine,
            true);

        // A test line without cancellation requested
        yield return new TestCaseData(
            CTestLine,
            false);
    }

    public const string CTestLine = "A test line";
}