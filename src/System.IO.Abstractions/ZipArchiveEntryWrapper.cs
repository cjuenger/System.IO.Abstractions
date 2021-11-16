using System.IO.Compression;

namespace System.IO.Abstractions
{
    [Serializable]
    public class ZipArchiveEntryWrapper : IZipArchiveEntry
    {
        private readonly ZipArchiveEntry zipArchiveEntry;

        public IZipArchive Archive => new ZipArchiveWrapper(zipArchiveEntry.Archive);

        public long CompressedLength => zipArchiveEntry.CompressedLength;

#if NETSTANDARD2_1_OR_GREATER
        public uint Crc32 => zipArchiveEntry.Crc32;

        public int ExternalAttributes
        {
            get => zipArchiveEntry.ExternalAttributes;
            set => zipArchiveEntry.ExternalAttributes = value;
        }
#endif

        public string FullName => zipArchiveEntry.FullName;

        public DateTimeOffset LastWriteTime
        {
            get => zipArchiveEntry.LastWriteTime;
            set => zipArchiveEntry.LastWriteTime = value;
        }

        public long Length => zipArchiveEntry.Length;

        public string Name => zipArchiveEntry.Name;
        
        public ZipArchiveEntryWrapper(ZipArchiveEntry zipArchive) => zipArchiveEntry = zipArchive;

        public void Delete() => zipArchiveEntry.Delete();

        public Stream Open() => zipArchiveEntry.Open();
    }
}