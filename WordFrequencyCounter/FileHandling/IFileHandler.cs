namespace WordFrequencyCounter.Handler;

public interface IFileHandler
{
    bool Exists(string fileName);
    FileInfo GetFileInfo(string fileName);
}