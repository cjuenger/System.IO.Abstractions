using System.IO.Compression;
using System.Text;

namespace System.IO.Abstractions
{
    [Serializable]
    public class ZipFileWrapper : IZipFile
    {
        public void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }

        public void CreateFromDirectory(
            string sourceDirectoryName, 
            string destinationArchiveFileName,
            CompressionLevel compressionLevel, 
            bool includeBaseDirectory)
        {
            ZipFile.CreateFromDirectory(
                sourceDirectoryName, 
                destinationArchiveFileName, 
                compressionLevel, 
                includeBaseDirectory);
        }

        public void CreateFromDirectory(
            string sourceDirectoryName, 
            string destinationArchiveFileName,
            CompressionLevel compressionLevel, 
            bool includeBaseDirectory,
            Encoding entryNameEncoding)
        {
            ZipFile.CreateFromDirectory(
                sourceDirectoryName, 
                destinationArchiveFileName, 
                compressionLevel, 
                includeBaseDirectory, 
                entryNameEncoding);
        }

        public void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }

        public void ExtractToDirectory(
            string sourceArchiveFileName, 
            string destinationDirectoryName,
            Encoding entryNameEncoding)
        {
            ZipFile.ExtractToDirectory(
                sourceArchiveFileName, 
                destinationDirectoryName, 
                entryNameEncoding);
        }

#if NETSTANDARD2_1_OR_GREATER
        public void ExtractToDirectory(
            string sourceArchiveFileName, 
            string destinationDirectoryName, 
            bool overwriteFiles)
        {
            
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, overwriteFiles);
            
        }
        
        public void ExtractToDirectory(
            string sourceArchiveFileName, 
            string destinationDirectoryName,
            Encoding entryNameEncoding, 
            bool overwriteFiles)
        {
            ZipFile.ExtractToDirectory(
                sourceArchiveFileName, 
                destinationDirectoryName, 
                entryNameEncoding, 
                overwriteFiles);
        }
#endif

        public IZipArchive Open(string archiveFileName, ZipArchiveMode mode)
        {
            var zipArchive = ZipFile.Open(archiveFileName, mode);
            return new ZipArchiveWrapper(zipArchive);
        }

        public IZipArchive Open(string archiveFileName, ZipArchiveMode mode, Encoding entryNameEncoding)
        {
            var zipArchive = ZipFile.Open(archiveFileName, mode, entryNameEncoding);
            return new ZipArchiveWrapper(zipArchive);
        }

        public IZipArchive OpenRead(string archiveFileName)
        {
            var zipArchive = ZipFile.OpenRead(archiveFileName);
            return new ZipArchiveWrapper(zipArchive);
        }
    }
}