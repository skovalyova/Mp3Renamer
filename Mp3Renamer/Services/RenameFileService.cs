using Mp3Renamer.Extensions;
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Mp3Renamer.Interfaces;
using File = TagLib.File;

namespace Mp3Renamer.Services
{
    public class RenameFileService : IRenameFileService
    {
        private readonly ILogger<RenameFileService> _logger;

        public RenameFileService(ILogger<RenameFileService> logger)
        {
            _logger = logger;
        }

        public void RenameFile(string pathToFile)
        {
            try
            {
                var tagFile = File.Create(pathToFile);
                var artist = tagFile.Tag.FirstAlbumArtist;
                var title = tagFile.Tag.Title;

                if (String.IsNullOrEmpty(artist) || String.IsNullOrEmpty(title))
                {
                    return;
                }

                var fileInfo = new FileInfo(pathToFile);
                var newNameWithoutExtension = $"{ artist } - { title }";

                fileInfo.Rename(newNameWithoutExtension);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
