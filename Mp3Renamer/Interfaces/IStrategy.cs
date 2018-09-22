using System.Collections.Generic;

namespace Mp3Renamer.Strategy.Interfaces
{
    public interface IStrategy
    {
        void RenameFiles(IEnumerable<string> mp3Files);
    }
}
