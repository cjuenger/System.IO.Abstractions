using System.IO.Compression;

namespace System.IO.Abstractions
{
    public interface IZipArchiveEntry
    {
        IZipArchive Archive { get; }
        long CompressedLength { get; }
        
#if NETSTANDARD2_1_OR_GREATER
        uint Crc32 { get; }
        int ExternalAttributes { get; set; }
#endif
        string FullName { get; }
        DateTimeOffset LastWriteTime { get; set; }
        long Length { get; }
        string Name { get; }
        void Delete();
        Stream Open();
    }
}