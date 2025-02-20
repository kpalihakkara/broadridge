namespace WordFrequencyCounter.Handler;

public sealed class FileHandler : IFileHandler
{
    public bool Exists(string fileName)
    {
        return File.Exists(fileName);
    }

    public FileInfo GetFileInfo(string fileName)
    {
        return new FileInfo(fileName);
    }
}