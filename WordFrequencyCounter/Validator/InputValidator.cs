using WordFrequencyCounter.Handler;

namespace WordFrequencyCounter.Validators;

public sealed class InputValidator(IFileHandler fileHandler) : IInputValidator
{
    public bool ValidateArgs(string[] args)
    {
        bool valid = true;

        if (args.Length != 2)
        {
            Console.WriteLine("Two arguments must be supplied.");
            valid = false;
        }
        else
        {
            var inputFileName = args[0];
            var outputFileName = args[1];

            if (!fileHandler.Exists(inputFileName))
            { 
                Console.WriteLine($"Input file {inputFileName} does not exist.");
                valid = false;
            }
            
            if (fileHandler.Exists(outputFileName))
            {
                Console.WriteLine($"Output file {outputFileName} already exists, aborting.");
                valid = false;
            }

            try
            {
                // This may not be exhaustive - could use regex etc for more complete check
                var fileInfo = fileHandler.GetFileInfo(outputFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Output file {outputFileName} is not valid. {ex.Message}");
                valid = false;
            }
        }

        return valid;
    }
}
