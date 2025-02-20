using Moq;
using WordFrequencyCounter.Processors;
using WordFrequencyCounterTests.ProcessorTests.OutputProcessorTests.TestData;

namespace WordFrequencyCounterTests.ValidatorsTests;

[TestFixture]
public sealed class OutputFileProcessorTests
{
    [SetUp]
    public void Setup()
    {
        _outputLineGeneratorMock = new Mock<IOutputLineGenerator>(MockBehavior.Strict);
    }

    [Test, TestCaseSource(typeof(OutputFileProcessorTestData), nameof(OutputFileProcessorTestData.GetTestCases))]
    public async Task TestProcessFile(
        IReadOnlyDictionary<string, int> wordFrequencies,
        string[] wordFrequenciesOutput)
    {
        var outputFileProcessor = new OutputFileProcessor(_outputLineGeneratorMock.Object);

        _outputLineGeneratorMock
            .Setup(x => x.GetLineOutput(wordFrequencies))
            .Returns(wordFrequenciesOutput);

        var result = await outputFileProcessor.ProcessFile(
            COutputFileName,
            wordFrequencies,
            CancellationToken.None);

        Assert.That(
            result,
            Is.True);

        // let's read the data back in to check output
        using (var reader = new StreamReader(COutputFileName))
        {

            var allLines = await reader.ReadToEndAsync(); // not going to consider performance for test case as data is small

            var lines = allLines.Split(
                Environment.NewLine,
                StringSplitOptions.RemoveEmptyEntries);

            Assert.That(lines, Is.EquivalentTo(wordFrequenciesOutput));
        }
    }
        
    private const string COutputFileName = "testfile.txt";
    private Mock<IOutputLineGenerator> _outputLineGeneratorMock;
}
