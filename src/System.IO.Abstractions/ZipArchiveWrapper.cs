using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;

namespace System.IO.Abstractions
{
    [Serializable]
    public class ZipArchiveWrapper : IZipArchive
    {
        private readonly ZipArchive zipArchive;

        public ReadOnlyCollection<IZipArchiveEntry> Entries
        {
            get
            {
                var entries = zipArchive
                    .Entries
                    .Select(e => new ZipArchiveEntryWrapper(e))
                    .ToList<IZipArchiveEntry>();
                
                var readOnlyCollection = new ReadOnlyCollection<IZipArchiveEntry>(entries);
                return readOnlyCollection;
            }
        }

        public ZipArchiveMode Mode => zipArchive.Mode;

        public ZipArchiveWrapper(ZipArchive instance)
        {
            zipArchive = instance;
        }
        
        public void Dispose() => zipArchive.Dispose();

        public IZipArchiveEntry CreateEntry(string entryName)
        {
            var entry = zipArchive.CreateEntry(entryName);
            var entryWrapper = new ZipArchiveEntryWrapper(entry);
            return entryWrapper;
        }

        public IZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
        {
            var entry = zipArchive.CreateEntry(entryName, compressionLevel);
            var entryWrapper = new ZipArchiveEntryWrapper(entry);
            return entryWrapper;
        }

        public IZipArchiveEntry GetEntry(string entryName)
        {
            var entry = zipArchive.GetEntry(entryName);
            var entryWrapper = new ZipArchiveEntryWrapper(entry);
            return entryWrapper;
        }
    }
}