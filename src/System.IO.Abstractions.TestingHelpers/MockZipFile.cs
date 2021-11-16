using System.IO.Compression;
using System.Linq;
using System.Text;

namespace System.IO.Abstractions.TestingHelpers
{
    public class MockZipFile : IZipFile
    {
        private readonly MockFileSystem mockFileSystem;

        public MockZipFile(MockFileSystem mockFileSystem)
        {
            this.mockFileSystem = mockFileSystem ?? throw new ArgumentNullException(nameof(mockFileSystem));
        }
        
        public void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName)
        {
            ThrowExceptionOnInvalidNameOfDirectoryOrFile(sourceDirectoryName, destinationArchiveFileName);
            
            // TODO: 20211116 CJ: Sum size of files in source directory and use this as a basis for the zip file.
            
            mockFileSystem.Directory.Delete(sourceDirectoryName, true);
            mockFileSystem.File.Create(destinationArchiveFileName);
        }

        public void CreateFromDirectory(
            string sourceDirectoryName, 
            string destinationArchiveFileName,
            CompressionLevel compressionLevel, 
            bool includeBaseDirectory)
        {
            ThrowExceptionOnInvalidNameOfDirectoryOrFile(sourceDirectoryName, destinationArchiveFileName);
            
            // TODO: 20211116 CJ: Sum size of files in source directory and use this as a basis for the zip file.
            // TODO: 20211116 CJ: Consider `compressionLevel` for the target zip file.
            // TODO: 20211116 CJ: Should `includeBaseDirectory` be mocked here?
            
            mockFileSystem.Directory.Delete(sourceDirectoryName, true);
            mockFileSystem.File.Create(destinationArchiveFileName);
        }

        public void CreateFromDirectory(
            string sourceDirectoryName, 
            string destinationArchiveFileName,
            CompressionLevel compressionLevel, 
            bool includeBaseDirectory, 
            Encoding entryNameEncoding)
        {
            throw new NotImplementedException();
        }

        public void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            throw new NotImplementedException();
        }

        public void ExtractToDirectory(
            string sourceArchiveFileName, 
            string destinationDirectoryName, 
            Encoding entryNameEncoding)
        {
            throw new NotImplementedException();
        }

        public IZipArchive Open(string archiveFileName, ZipArchiveMode mode)
        {
            throw new NotImplementedException();
        }

        public IZipArchive Open(string archiveFileName, ZipArchiveMode mode, Encoding entryNameEncoding)
        {
            throw new NotImplementedException();
        }

        public IZipArchive OpenRead(string archiveFileName)
        {
            throw new NotImplementedException();
        }

        private void ThrowExceptionOnInvalidNameOfDirectoryOrFile(string sourceDirectoryName, string destinationArchiveFileName)
        {
            if (sourceDirectoryName == null) throw new ArgumentNullException(nameof(sourceDirectoryName));
            if (destinationArchiveFileName == null) throw new ArgumentNullException(nameof(destinationArchiveFileName));

            if (string.IsNullOrWhiteSpace(sourceDirectoryName))
            {
                throw new ArgumentException(
                    $"{nameof(sourceDirectoryName)} is Empty, contains only white space, or contains at least one invalid character.");
            }
            
            if (string.IsNullOrWhiteSpace(destinationArchiveFileName))
            {
                throw new ArgumentException(
                    $"{nameof(destinationArchiveFileName)} is Empty, contains only white space, or contains at least one invalid character.");
            }

            if (mockFileSystem.AllDirectories.All(dir => dir != sourceDirectoryName))
            {
                throw new DirectoryNotFoundException(
                    $"{nameof(sourceDirectoryName)} is invalid or does not exist (for example, it is on an unmapped drive");
            }

            if (mockFileSystem.FileExists(destinationArchiveFileName))
                throw new IOException($"{nameof(destinationArchiveFileName)} already exists.");
        }
    }
}