using Mp3Renamer.Services;
using Mp3Renamer.Strategy.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mp3Renamer.Strategy
{
    public class ParallelForeachStrategy : IStrategy
    {
        private readonly RenameFileService _renameFileService;

        public ParallelForeachStrategy(RenameFileService renameFileService)
        {
            _renameFileService = renameFileService;
        }

        public void RenameFiles(IEnumerable<string> mp3Files)
        {
            Parallel.ForEach(mp3Files, _renameFileService.RenameFile);
        }
    }
}
