using Mp3Renamer.Services;
using Mp3Renamer.Strategy;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mp3Renamer
{
    public class Program
    {
        private static void Main()
        {
            var renameFileService = new RenameFileService();
            var timerService = new TimerService<IEnumerable<string>>();

            var foreachStrategy = new ForeachStrategy(renameFileService);
            var parallelForeachStrategy = new ParallelForeachStrategy(renameFileService);
            var tasksStrategy = new TasksStrategy(renameFileService);

            var mp3Files = Directory
                .GetFiles(@"D:\Documents\Documents\Music", "*.mp3", SearchOption.AllDirectories);

            timerService.MeasureExecutionTime(foreachStrategy.RenameFiles, mp3Files);

            timerService.MeasureExecutionTime(parallelForeachStrategy.RenameFiles, mp3Files);

            timerService.MeasureExecutionTime(tasksStrategy.RenameFiles, mp3Files);

            Console.ReadKey();
        }
    }
}
