namespace WordFrequencyCounter.Processors;

public sealed class OutputLineGenerator : IOutputLineGenerator
{
    public IEnumerable<string> GetLineOutput(IReadOnlyDictionary<string, int> wordFrequencies)
    {
        // order words by frequency descending
        var orderedWordFrequencies = wordFrequencies.OrderByDescending(x => x.Value);

        foreach (var wordWithFrequency in orderedWordFrequencies)
        {
            yield return $"{wordWithFrequency.Key},{wordWithFrequency.Value}";
        }
    }
}