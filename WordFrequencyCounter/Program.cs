using Autofac;
using WordFrequencyCounter.DI;

namespace WordFrequencyCounter.Validators;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var container = AutoFacModule.BuildContainer();

        await container
            .Resolve<WordFrequencyApplication>()
            .Run(args);
    }
}