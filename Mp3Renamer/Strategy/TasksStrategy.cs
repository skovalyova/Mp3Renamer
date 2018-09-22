using Mp3Renamer.Strategy.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mp3Renamer.Services;

namespace Mp3Renamer.Strategy
{
    public class TasksStrategy : IStrategy
    {
        private readonly RenameFileService _renameFileService;

        public TasksStrategy(RenameFileService renameFileService)
        {
            _renameFileService = renameFileService;
        }

        public void RenameFiles(IEnumerable<string> mp3Files)
        {
            var tasks = mp3Files.Select(file => Task.Run(() => _renameFileService.RenameFile(file)));
            Task all = Task.WhenAll(tasks.ToArray());
            all.Wait();
        }
    }
}
