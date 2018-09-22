using Mp3Renamer.Extensions;
using System;
using System.IO;

namespace Mp3Renamer.Services
{
    public class RenameFileService
    {
        public void RenameFile(string file)
        {
            try
            {
                var tagFile = TagLib.File.Create(file);
                var artist = tagFile.Tag.FirstAlbumArtist;
                var title = tagFile.Tag.Title;

                if (string.IsNullOrEmpty(artist) || string.IsNullOrEmpty(title))
                {
                    return;
                }

                var fileInfo = new FileInfo(file);
                fileInfo.Rename($"{ artist } - { title }");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
