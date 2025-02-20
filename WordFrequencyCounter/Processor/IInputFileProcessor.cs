namespace WordFrequencyCounter.Processors;

public interface IInputFileProcessor
{
    Task<IReadOnlyDictionary<string, int>> ProcessFile(
        string fileName,
        CancellationToken cancellationToken);
}
