namespace WordFrequencyCounter.Processors;

public interface IInputLineProcessor
{
    Task<bool> ProcessLine(
        string line,
        CancellationToken cancellationToken);
}