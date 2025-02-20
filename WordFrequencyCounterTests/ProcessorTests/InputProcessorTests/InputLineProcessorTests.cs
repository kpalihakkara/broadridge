using Moq;
using WordFrequencyCounter.Managers;
using WordFrequencyCounter.Processors;
using WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

namespace WordFrequencyCounterTests.ValidatorsTests;

[TestFixture]
public sealed class InputLineProcessorTests
{
    [SetUp]
    public void Setup()
    {
        _wordFrequencyManagerMock = new Mock<IWordFrequencyManager>(MockBehavior.Strict);
    }

    [Test, TestCaseSource(typeof(InputLineProcessorTestData), nameof(InputLineProcessorTestData.GetTestCases))]
    public async Task TestProcessingInputLine(
        string inputData,
        bool cancellationRequested)
    {
        var lineProcessor = new InputLineProcessor(_wordFrequencyManagerMock.Object);

        _wordFrequencyManagerMock
            .Setup(x => x.AddOrUpdate(It.IsAny<string>()))
            .Returns(1); // note I consider this ok currently as the return value isn't actively used

        var cancellationToken = new CancellationToken(cancellationRequested);

        var addOrUpdatesExpected = inputData.Split().Length;

        var result = await lineProcessor.ProcessLine(
            inputData,
            cancellationToken);

        if (cancellationRequested)
        {
            // if cancellation requested check result and that no AddOrUpdate were performed
            Assert.That(
                result,
                Is.False);

            _wordFrequencyManagerMock.Verify(
                x => x.AddOrUpdate(It.IsAny<string>()),
                Times.Never);
        }
        else
        {
            // if cancellation was not requested check result and that matching number of AddOrUpdate were performed
            Assert.That(result, Is.True);
            _wordFrequencyManagerMock.Verify(
                x => x.AddOrUpdate(It.IsAny<string>()),
                Times.Exactly(addOrUpdatesExpected));
        }

        // in all cases we can just for completeness check GetWordFrequencies was not called
        _wordFrequencyManagerMock.Verify(
            x => x.GetWordFrequencies(),
            Times.Never);
    }
        
    private Mock<IWordFrequencyManager> _wordFrequencyManagerMock;
}
