using WordFrequencyCounter.Processors;
using WordFrequencyCounter.Validators;

namespace WordFrequencyCounter;

public sealed class WordFrequencyApplication(
    IInputValidator inputValidator,
    IInputFileProcessor inputFileProcessor,
    IOutputFileProcessor outputFileProcessor)
{
    public async Task Run(string[] args)
    {
        try
        {
            if (inputValidator.ValidateArgs(args))
            {
                var cancellationToken = new CancellationToken();

                // I've added the plumbing for cancellation just in case we subsequently
                // add mechanism to facilitate cancellation

                var inputFileName = args[0];
                var outputFileName = args[1];

                var wordFrequencies = inputFileProcessor.ProcessFile(
                    inputFileName,
                    cancellationToken);

                var success = await outputFileProcessor.ProcessFile(
                    outputFileName,
                    await wordFrequencies,
                    cancellationToken);

                if (success)
                {
                    Console.WriteLine($"Successfully processed file {inputFileName}. Results stored to {outputFileName}");
                }
                else
                {
                    Console.WriteLine($"Failed to process file {inputFileName}.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process file: {ex}");
        }
    }
}
