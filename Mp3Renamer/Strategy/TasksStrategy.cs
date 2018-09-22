using Mp3Renamer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mp3Renamer.Strategy
{
    public class TasksStrategy : IStrategy
    {
        private readonly IRenameFileService _renameFileService;

        public TasksStrategy(IRenameFileService renameFileService)
        {
            _renameFileService = renameFileService;
        }

        public void RenameFiles(IEnumerable<string> mp3Files)
        {
            var tasks = mp3Files.Select(file => Task.Run(() => _renameFileService.RenameFile(file)));
            Task whenAllTask = Task.WhenAll(tasks.ToArray());
            whenAllTask.Wait();
        }
    }
}
