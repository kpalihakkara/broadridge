using Autofac;
using WordFrequencyCounter.Handler;
using WordFrequencyCounter.Managers;
using WordFrequencyCounter.Processors;
using WordFrequencyCounter.Validators;

namespace WordFrequencyCounter.DI;

public static class AutoFacModule
{
    public static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<InputValidator>().As<IInputValidator>().SingleInstance();
        builder.RegisterType<InputFileProcessor>().As<IInputFileProcessor>().SingleInstance();
        builder.RegisterType<InputLineProcessor>().As<IInputLineProcessor>().SingleInstance();
        builder.RegisterType<FileHandler>().As<IFileHandler>().SingleInstance();
        builder.RegisterType<OutputFileProcessor>().As<IOutputFileProcessor>().SingleInstance();
        builder.RegisterType<OutputLineGenerator>().As<IOutputLineGenerator>().SingleInstance();
        builder.RegisterType<WordFrequencyManager>().As<IWordFrequencyManager>().SingleInstance();
        builder.RegisterType<WordFrequencyApplication>().SingleInstance();
        
        return builder.Build();
    }
}
