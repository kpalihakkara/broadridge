using System.Runtime.CompilerServices;
using WordFrequencyCounter.Managers;

namespace WordFrequencyCounter.Processors;

public sealed class InputFileProcessor(
    IWordFrequencyManager wordFrequencyManager,
    IInputLineProcessor lineProcessor) : IInputFileProcessor
{
    public async Task<IReadOnlyDictionary<string, int>> ProcessFile(
        string fileName,
        CancellationToken cancellationToken)
    {
        // Looking to process the file in a manner that is cpu / memory efficient.
        // Using IAsyncEnumerable to reduce memory footprint by not trying to read the entire file in one go.
        // Use parallel for each to try to process file quicker by handling lines in parallel and using
        // the final output once all lines processed.

        var lines = GetLinesFromFile(fileName, cancellationToken);

        // not going to set max degrees of parallelism explicitly for now
        await Parallel.ForEachAsync(lines, async (line, cancellationToken) =>
        {
            await lineProcessor.ProcessLine(line, cancellationToken);
        });

        return wordFrequencyManager.GetWordFrequencies();
    }

    private static async IAsyncEnumerable<string> GetLinesFromFile(
        string fileName,
        [EnumeratorCancellation]CancellationToken cancellationToken)
    {
        string? line;
        using var reader = new StreamReader(fileName);
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            yield return line;
        }
    }
}
