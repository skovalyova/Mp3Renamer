using System;
using System.IO;

namespace Mp3Renamer.Extensions
{
    public static class FileInfoExtensions
    {
        public static void Rename(this FileInfo fileInfo, string newNameWithoutExtension)
        {
            var fileNameWithExtension = $"{ newNameWithoutExtension }{ fileInfo.Extension }";

            if (fileInfo.Name == fileNameWithExtension)
            {
                return;
            }

            var fullPath = Path.Combine(fileInfo.Directory?.FullName, fileNameWithExtension);

            if (File.Exists(fullPath))
            {
                throw new ApplicationException($"File by path { fullPath } already exists.");
            }

            fileInfo.MoveTo(fullPath);
        }
    }
}
