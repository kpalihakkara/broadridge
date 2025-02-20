using WordFrequencyCounter.Managers;

namespace WordFrequencyCounter.Processors;

public sealed class InputLineProcessor(IWordFrequencyManager wordFrequencyManager) : IInputLineProcessor
{
    public Task<bool> ProcessLine(
    string line,
    CancellationToken cancellationToken)
    {
        // we could test if cancellation was requested later (rather than immediately) or as well as here
        // to increase chance of being able to save unnecessary additional work but I will consider this ok 
        // for now.
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromResult(false);
        }

        var words = line.Split();

        foreach (var word in words)
        {
            wordFrequencyManager.AddOrUpdate(word);
        }

        return Task.FromResult(true);
    }
}