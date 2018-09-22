using Mp3Renamer.Services;
using Mp3Renamer.Strategy.Interfaces;
using System.Collections.Generic;

namespace Mp3Renamer.Strategy
{
    public class ForeachStrategy : IStrategy
    {
        private readonly RenameFileService _renameFileService;

        public ForeachStrategy(RenameFileService renameFileService)
        {
            _renameFileService = renameFileService;
        }

        public void RenameFiles(IEnumerable<string> mp3Files)
        {
            foreach (var file in mp3Files)
            {
                _renameFileService.RenameFile(file);
            }
        }
    }
}
