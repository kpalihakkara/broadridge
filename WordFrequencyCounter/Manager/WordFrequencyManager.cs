using System.Collections.Concurrent;

namespace WordFrequencyCounter.Managers;

public sealed class WordFrequencyManager : IWordFrequencyManager
{
    private readonly ConcurrentDictionary<string, int> wordFrequencies = new ConcurrentDictionary<string, int>();

    public int AddOrUpdate(string word)
    {
        return wordFrequencies.AddOrUpdate(
            word.ToLower(),
            1,
            (key, existingValue) => existingValue + 1);
    }

    public IReadOnlyDictionary<string, int> GetWordFrequencies() => wordFrequencies;
}
    