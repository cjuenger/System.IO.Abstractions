namespace System.IO.Abstractions
{
    public interface IZipArchive : IDisposable
    {
        Collections.ObjectModel.ReadOnlyCollection<IZipArchiveEntry> Entries { get; }
        Compression.ZipArchiveMode Mode { get; }
        IZipArchiveEntry CreateEntry (string entryName);
        IZipArchiveEntry CreateEntry (string entryName, Compression.CompressionLevel compressionLevel);
        IZipArchiveEntry GetEntry (string entryName);
    }
}