using Mp3Renamer.Services;
using Mp3Renamer.Strategy;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Mp3Renamer
{
    public class Program
    {
        private static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var pathToFolder = configuration["pathToFolder"];
            var searchMask = "*.mp3";

            var renameFileService = new RenameFileService();
            var timerService = new TimerService<IEnumerable<string>>();

            var foreachStrategy = new ForeachStrategy(renameFileService);
            var parallelForeachStrategy = new ParallelForeachStrategy(renameFileService);
            var tasksStrategy = new TasksStrategy(renameFileService);

            var mp3Files = Directory.GetFiles(pathToFolder, searchMask, SearchOption.AllDirectories);

            timerService.MeasureExecutionTime(foreachStrategy.RenameFiles, mp3Files);

            timerService.MeasureExecutionTime(parallelForeachStrategy.RenameFiles, mp3Files);

            timerService.MeasureExecutionTime(tasksStrategy.RenameFiles, mp3Files);

            Console.ReadKey();
        }
    }
}
