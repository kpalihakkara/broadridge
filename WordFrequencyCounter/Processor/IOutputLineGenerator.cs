namespace WordFrequencyCounter.Processors;

public interface IOutputLineGenerator
{
    IEnumerable<string> GetLineOutput(IReadOnlyDictionary<string, int> wordFrequencies);
}
