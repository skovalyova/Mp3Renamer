using System;
using System.IO;

namespace Mp3Renamer.Extensions
{
    public static class FileInfoExtensions
    {
        public static void Rename(this FileInfo fileInfo, string newNameWithoutExtension)
        {
            if (fileInfo == null)
            {
                throw new ApplicationException("FileInfo cannot be null.");
            }

            var fileNameWithExtension = $"{ newNameWithoutExtension }{ fileInfo.Extension }";
            fileInfo.MoveTo(Path.Combine(fileInfo.Directory?.FullName, fileNameWithExtension));
        }
    }
}
