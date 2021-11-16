using System.Diagnostics;
using System.IO.Compression;

namespace System.IO.Abstractions.TestingHelpers
{
    public class MockZipArchiveEntry : IZipArchiveEntry
    {
        private MockZipArchive archive;
        private bool currentlyOpenForWrite;
        private readonly CompressionLevel compressionLevel;
        private MemoryStream memoryStream;

        public IZipArchive Archive => archive;

        public long CompressedLength
        {
            get
            {
                var length = memoryStream?.Length ?? 0;
                
                switch (compressionLevel)
                {
                    case CompressionLevel.Fastest:
                        length /= 2;
                        break;
                    case CompressionLevel.Optimal:
                        length /= 4;
                        break;
                    case CompressionLevel.NoCompression:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return length;
            }
        }
        
        public string FullName { get; }
        
        public DateTimeOffset LastWriteTime { get; set; }

        public long Length => memoryStream.Length;
        public string Name { get; }

        public MockZipArchiveEntry(MockZipArchive mockArchive, string entryName)
        {
            archive = mockArchive;
            FullName = entryName;
            Name = entryName;
            compressionLevel = CompressionLevel.NoCompression;
        }
        
        public MockZipArchiveEntry(MockZipArchive mockArchive, string entryName, CompressionLevel compressionLevel)
        {
            archive = mockArchive;
            FullName = entryName;
            Name = entryName;
            this.compressionLevel = compressionLevel;
        }
        
        public void Delete()
        {
            if(archive == null) return;
            
            if (currentlyOpenForWrite)
                throw new IOException("DeleteOpenEntry");

            if (archive.Mode != ZipArchiveMode.Update)
                throw new NotSupportedException("DeleteOnlyInUpdate");
            
            archive.RemoveEntry(this);
            archive = null;
        }

        public Stream Open()
        {
            ThrowIfInvalidArchive();

            switch (archive.Mode)
            {
                case ZipArchiveMode.Read:
                    memoryStream = new MemoryStream();
                    return memoryStream;
                case ZipArchiveMode.Create:
                    currentlyOpenForWrite = true;
                    memoryStream = new MemoryStream();
                    return memoryStream;
                // ReSharper disable once RedundantCaseLabel
                case ZipArchiveMode.Update:
                default:
                {
                    if (currentlyOpenForWrite)
                        throw new IOException("UpdateModeOneStream");
                    Debug.Assert(archive.Mode == ZipArchiveMode.Update);
                    memoryStream = new MemoryStream();
                    return memoryStream;
                }
            }
        }
        
        private void ThrowIfInvalidArchive()
        {
            if (archive == null)
                throw new InvalidOperationException("DeletedEntry");
            archive.ThrowIfDisposed();
        }
    }
}