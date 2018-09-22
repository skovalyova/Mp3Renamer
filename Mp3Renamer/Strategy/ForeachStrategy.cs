using Mp3Renamer.Interfaces;
using System.Collections.Generic;

namespace Mp3Renamer.Strategy
{
    public class ForeachStrategy : IStrategy
    {
        private readonly IRenameFileService _renameFileService;

        public ForeachStrategy(IRenameFileService renameFileService)
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
