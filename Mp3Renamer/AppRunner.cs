using Microsoft.Extensions.Configuration;
using Mp3Renamer.Interfaces;
using Mp3Renamer.Strategy;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Mp3Renamer
{
    public class AppRunner
    {
        private readonly ITimerService<IEnumerable<string>> _timerService;
        private readonly IRenameFileService _renameFileService;
        private readonly ILogger<AppRunner> _logger;

        public AppRunner(
            ITimerService<IEnumerable<string>> timerService,
            IRenameFileService renameFileService,
            ILogger<AppRunner> logger
        )
        {
            _timerService = timerService;
            _renameFileService = renameFileService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogDebug("Application is started.");

            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            finally
            {
                _logger.LogDebug("Application is stopped.");
            }
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
