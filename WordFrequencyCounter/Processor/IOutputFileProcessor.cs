namespace WordFrequencyCounter.Processors;

public interface IOutputFileProcessor
{
    Task<bool> ProcessFile(
        string fileName,
        IReadOnlyDictionary<string, int> wordFrequencies,
        CancellationToken cancellationToken);
}