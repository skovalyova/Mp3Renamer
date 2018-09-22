using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Mp3Renamer.Interfaces;
using Mp3Renamer.Services;

namespace Mp3Renamer
{
    public class Program
    {
        private static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetRequiredService<AppRunner>().Run();


            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITimerService<IEnumerable<string>>, TimerService<IEnumerable<string>>>();
            serviceCollection.AddTransient<IRenameFileService, RenameFileService>();

            serviceCollection.AddTransient<AppRunner>();
        }
    }
}
