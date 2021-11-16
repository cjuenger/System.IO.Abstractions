using System.IO.Compression;
using System.Text;

namespace System.IO.Abstractions
{
    public interface IZipFile
    {
        void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName);

        void CreateFromDirectory(
            string sourceDirectoryName, 
            string destinationArchiveFileName,
            CompressionLevel compressionLevel, 
            bool includeBaseDirectory);

        void CreateFromDirectory(
            string sourceDirectoryName, 
            string destinationArchiveFileName,
            CompressionLevel compressionLevel, 
            bool includeBaseDirectory,
            Encoding entryNameEncoding);

        void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName);

        void ExtractToDirectory(
            string sourceArchiveFileName, 
            string destinationDirectoryName,
            Encoding entryNameEncoding);

#if NETSTANDARD2_1_OR_GREATER
        void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, bool overwriteFiles);
        
        void ExtractToDirectory(
            string sourceArchiveFileName, 
            string destinationDirectoryName,
            Encoding entryNameEncoding, 
            bool overwriteFiles);
#endif
        
        IZipArchive Open(string archiveFileName, ZipArchiveMode mode);

        IZipArchive Open(string archiveFileName, ZipArchiveMode mode, Encoding entryNameEncoding);
    
        IZipArchive OpenRead(string archiveFileName);
    }
}