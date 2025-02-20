using Moq;
using WordFrequencyCounter.Managers;
using WordFrequencyCounter.Processors;
using WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

namespace WordFrequencyCounterTests.ValidatorsTests;

[TestFixture]
public sealed class InputFileProcessorTests
{
    [SetUp]
    public void Setup()
    {
        _wordFrequencyManagerMock = new Mock<IWordFrequencyManager>(MockBehavior.Strict);
        _lineProcessorMock = new Mock<IInputLineProcessor>(MockBehavior.Strict);

        _lineProcessorMock.Setup(x => x.ProcessLine(
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }

    [Test, TestCaseSource(typeof(InputFileProcessorTestData), nameof(InputFileProcessorTestData.GetTestCases))]
    public async Task TestWordFrequenciesFromFile(
        string[] inputData,
        IReadOnlyDictionary<string, int> wordFrequencies)
    {
        var inputFileProcessor = new InputFileProcessor(
            _wordFrequencyManagerMock.Object,
            _lineProcessorMock.Object);

        _wordFrequencyManagerMock
            .Setup(x => x.GetWordFrequencies())
            .Returns(wordFrequencies);

        _wordFrequencyManagerMock
            .Setup(x => x.AddOrUpdate(It.IsAny<string>()))
            .Returns(1); // note I'm ok to do this as the return value isn't actively used

        using (var writer = new StreamWriter(CInputFileName))
        {

            foreach (var line in inputData)
            {
                await writer.WriteLineAsync(line);
            }
            await writer.FlushAsync();
        }

        var result = await inputFileProcessor.ProcessFile(
            CInputFileName,
            CancellationToken.None);

        Assert.That(
            result,
            Is.EquivalentTo(wordFrequencies));
    }
        
    private const string CInputFileName = "testfile.txt";
    private Mock<IWordFrequencyManager> _wordFrequencyManagerMock;
    private Mock<IInputLineProcessor> _lineProcessorMock;
}
