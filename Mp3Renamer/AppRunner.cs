using Microsoft.Extensions.Configuration;
using Mp3Renamer.Interfaces;
using Mp3Renamer.Strategy;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mp3Renamer
{
    public class AppRunner
    {
        private readonly ITimerService<IEnumerable<string>> _timerService;
        private readonly IRenameFileService _renameFileService;

        public AppRunner(
            ITimerService<IEnumerable<string>> timerService,
            IRenameFileService renameFileService
        )
        {
            _timerService = timerService;
            _renameFileService = renameFileService;
        }

        public void Run()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var pathToFolder = configuration["pathToFolder"];
            var searchMask = "*.mp3";

            var foreachStrategy = new ForeachStrategy(_renameFileService);
            var parallelForeachStrategy = new ParallelForeachStrategy(_renameFileService);
            var tasksStrategy = new TasksStrategy(_renameFileService);

            var mp3Files = Directory.GetFiles(pathToFolder, searchMask, SearchOption.AllDirectories);

            _timerService.MeasureExecutionTime(foreachStrategy.RenameFiles, mp3Files);
            _timerService.MeasureExecutionTime(parallelForeachStrategy.RenameFiles, mp3Files);
            _timerService.MeasureExecutionTime(tasksStrategy.RenameFiles, mp3Files);

            Console.ReadKey();
        }
    }
}
