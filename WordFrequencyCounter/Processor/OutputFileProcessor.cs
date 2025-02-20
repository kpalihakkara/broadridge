namespace WordFrequencyCounter.Processors;

public sealed class OutputFileProcessor(IOutputLineGenerator outputLineGenerator) : IOutputFileProcessor
{
    public async Task<bool> ProcessFile(
        string fileName,
        IReadOnlyDictionary<string, int> wordFrequencies,
        CancellationToken cancellationToken)
    {
        try
        {
            using var writer = new StreamWriter(fileName);

            foreach (var line in outputLineGenerator.GetLineOutput(wordFrequencies))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                await writer.WriteLineAsync(line);
            }

            await writer.FlushAsync(cancellationToken).ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process file: {ex.Message}");
            return false;
        }
    }
}
