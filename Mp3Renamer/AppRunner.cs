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
            var pathToFolder = GetPathToFolder();
            var searchMask = "*.mp3";

            var mp3Files = Directory.GetFiles(pathToFolder, searchMask, SearchOption.AllDirectories);

            var foreachStrategy = new ForeachStrategy(_renameFileService);
            _timerService.MeasureExecutionTime(foreachStrategy.RenameFiles, mp3Files);

            var parallelForeachStrategy = new ParallelForeachStrategy(_renameFileService);
            _timerService.MeasureExecutionTime(parallelForeachStrategy.RenameFiles, mp3Files);

            var tasksStrategy = new TasksStrategy(_renameFileService);
            _timerService.MeasureExecutionTime(tasksStrategy.RenameFiles, mp3Files);
        }

        private string GetPathToFolder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var pathToFolder = configuration["pathToFolder"];

            if (String.IsNullOrEmpty(pathToFolder))
            {
                throw new ApplicationException("Path to folder is not configured.");
            }

            return pathToFolder;
        }
    }
}
