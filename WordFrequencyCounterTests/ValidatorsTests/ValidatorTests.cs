using Moq;
using WordFrequencyCounter.Handler;
using WordFrequencyCounter.Validators;
using WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

namespace WordFrequencyCounterTests.ValidatorsTests;

[TestFixture]
public sealed class ValidatorTests
{
    [SetUp]
    public void Setup()
    {
        _fileHandlerMock = new Mock<IFileHandler>(MockBehavior.Strict);
    }

    [Test, TestCaseSource(typeof(ValidatorTestData), nameof(ValidatorTestData.GetTestCases))]

    public void TestValidateArgs(
        bool inputFileExists,
        bool outputFileExists,
        bool valid)
    {
        // note: the above listed test cases could alternatively be returned via yield return
        // but I've done it this way for conciseness here

        var inputValidator = new InputValidator(_fileHandlerMock.Object);

        _fileHandlerMock
            .Setup(x => x.Exists(CInputFileName))
            .Returns(inputFileExists);

        _fileHandlerMock
            .Setup(x => x.Exists(COutputFileName))
            .Returns(outputFileExists);

        _fileHandlerMock
            .Setup(x => x.GetFileInfo(COutputFileName))
            .Returns(new FileInfo(COutputFileName));
        
        var args = new string[] { CInputFileName, COutputFileName };
        var result = inputValidator.ValidateArgs(args);

        Assert.That(
            result,
            valid ? Is.True : Is.False);
    }

    private const string CInputFileName = "input.txt";
    private const string COutputFileName = "output.txt";
    private Mock<IFileHandler> _fileHandlerMock;
}