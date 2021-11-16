using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;

namespace System.IO.Abstractions.TestingHelpers
{
    public class MockZipArchive : IZipArchive
    {
        private bool isDisposed;
        
        private readonly List<IZipArchiveEntry> entries = new();

        public ReadOnlyCollection<IZipArchiveEntry> Entries => new(entries);

        public ZipArchiveMode Mode { get; }

        public MockZipArchive(ZipArchiveMode mode)
        {
            Mode = mode;
        }
        
        public void Dispose() => isDisposed = true;

        public IZipArchiveEntry CreateEntry(string entryName)
        {
            var mockEntry = new MockZipArchiveEntry(this, entryName);
            entries.Add(mockEntry);
            return mockEntry;
        }

        public IZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
        {
            var mockEntry = new MockZipArchiveEntry(this, entryName, compressionLevel);
            entries.Add(mockEntry);
            return mockEntry;
        }

        public IZipArchiveEntry GetEntry(string entryName)
        {
            var mockEntry = entries.FirstOrDefault(e => e.Name == entryName);
            return mockEntry;
        }

        internal void RemoveEntry(IZipArchiveEntry entry) => entries.Remove(entry);
        
        internal void ThrowIfDisposed()
        {
            if (isDisposed) throw new ObjectDisposedException(GetType().ToString());
        }
    }
}