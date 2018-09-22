using Mp3Renamer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mp3Renamer.Strategy
{
    public class ParallelForeachStrategy : IStrategy
    {
        private readonly IRenameFileService _renameFileService;

        public ParallelForeachStrategy(IRenameFileService renameFileService)
        {
            _renameFileService = renameFileService;
        }

        public void RenameFiles(IEnumerable<string> mp3Files)
        {
            Parallel.ForEach(mp3Files, _renameFileService.RenameFile);
        }
    }
}
