using WordFrequencyCounter.Managers;
using WordFrequencyCounterTests.ProcessorTests.InputProcessorTests.TestData;

namespace WordFrequencyCounterTests.ValidatorsTests;

[TestFixture]
public sealed class WordFrequencyManagerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test, TestCaseSource(typeof(WordFrequencyManagerTestData), nameof(WordFrequencyManagerTestData.GetBaseTestCases))]
    public void TestAddOrUpdate(
        string word,
        int frequency)
    {
        var wordFrequencyManager = new WordFrequencyManager();

        for (int i = 0; i < frequency; i++)
        {
            Assert.DoesNotThrow(() => wordFrequencyManager.AddOrUpdate(word));
        }

        var wordFrequencies = wordFrequencyManager.GetWordFrequencies();

        // general overall test to see that total elements is correct
        Assert.That(
            wordFrequencies.Values.Sum(),
            Is.EqualTo(frequency));

        // check that the particular word exists
        Assert.That(
            wordFrequencies.ContainsKey(word),
            Is.True);

        // check that the particular word frequency is as expected
        Assert.That(
            wordFrequencies[word],
            Is.EqualTo(frequency));
    }

    [Test, TestCaseSource(typeof(WordFrequencyManagerTestData), nameof(WordFrequencyManagerTestData.GetExtendedTestCases))]
    public void TestAddOrUpdateMultiple(
        string[] words,
        int[] frequencies)
    {
        var wordFrequencyManager = new WordFrequencyManager();

        // I will not validate that words and frequencies are same length as this is test code

        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < frequencies[i]; j++)
            {
                Assert.DoesNotThrow(() => wordFrequencyManager.AddOrUpdate(words[i]));
            }
        }

        var wordFrequencies = wordFrequencyManager.GetWordFrequencies();

        for (int i = 0; i < words.Length; i++)
        {

            Assert.That(
                wordFrequencies.ContainsKey(words[i]),
                Is.True);

            Assert.That(
                wordFrequencies[words[i]],
                Is.EqualTo(frequencies[i]));
        }
    }

    [Test]
    public void TestGetWordFrequencies()
    {
    }
}