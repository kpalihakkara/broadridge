using System.Collections;

namespace WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

public sealed class ValidatorTestData
{
    public static IEnumerable GetTestCases()
    {
        // InputFileExists, OutputFileExists, Valid
        yield return new TestCaseData(true, false, true);
        yield return new TestCaseData(true, true, false);
        yield return new TestCaseData(false, false, false);
        yield return new TestCaseData(false, true, false);
    }
}