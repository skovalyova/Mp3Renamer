using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mp3Renamer.Interfaces;
using Mp3Renamer.Services;
using NLog.Extensions.Logging;

namespace Mp3Renamer
{
    public class Program
    {
        private static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServiceCollection(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            ConfigureNLog(serviceProvider);

            serviceProvider.GetRequiredService<AppRunner>().Run();

            Console.ReadKey();

            // Ensure to flush and stop internal timers/threads before application-exit.
            // (Avoid segmentation fault on Linux).
            NLog.LogManager.Shutdown();
        }

        private static void ConfigureServiceCollection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<AppRunner>();

            serviceCollection.AddTransient<ITimerService<IEnumerable<string>>, TimerService<IEnumerable<string>>>();
            serviceCollection.AddTransient<IRenameFileService, RenameFileService>();

            serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
            serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            serviceCollection.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));
        }

        private static void ConfigureNLog(ServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            NLog.LogManager.LoadConfiguration("nlog.config");
        }
    }
}
