using WordFrequencyCounter.Processors;
using WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

namespace WordFrequencyCounterTests.ValidatorsTests;

[TestFixture]
public sealed class OutputLineGeneratorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test, TestCaseSource(typeof(OutputLineGeneratorTestData), nameof(OutputLineGeneratorTestData.GetTestCases))]
    public void TestGeneratingOutputLine(
        IReadOnlyDictionary<string, int> wordFrequencies,
        string[] expected)
    {
        var outputLineGenerator = new OutputLineGenerator();

        var actual = outputLineGenerator.GetLineOutput(wordFrequencies);

        Assert.That(
            actual,
            Is.EquivalentTo(expected));
    }
}
