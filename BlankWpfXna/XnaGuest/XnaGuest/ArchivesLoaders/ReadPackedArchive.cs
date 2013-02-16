using System;
using System.Drawing;
using System.IO;
using SharpCompress.Archive;

namespace XnaGuest
{
    public class ReadArchive : IDisposable
    {
        private IArchive arch;
        private Stream stream;

        public ReadArchive(string pathToFile)
        {
            stream = File.OpenRead(pathToFile);
            arch = ArchiveFactory.Open(stream, SharpCompress.Common.Options.KeepStreamsOpen);
            while (!arch.IsComplete) { }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.Equals(null))
            {
                if (disposing)
                {
                    arch.Dispose();
                }

                arch = null;
                stream.Close();
                stream = null;
            }
        }

        public System.Drawing.Image ReadPageFromRar(int page)
        {
            MemoryStream m = new MemoryStream();
            var entry = arch.GetCertainEntry(page);
            entry.WriteTo(m);
            return Bitmap.FromStream(m);
        }
    }
}