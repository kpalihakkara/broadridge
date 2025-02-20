namespace WordFrequencyCounter.Managers;

public interface IWordFrequencyManager
{
    int AddOrUpdate(string word);
    IReadOnlyDictionary<string, int> GetWordFrequencies();
}
    