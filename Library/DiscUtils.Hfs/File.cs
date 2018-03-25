using DiscUtils.Streams;
using DiscUtils.Vfs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscUtils.Hfs
{
    internal class File : IVfsFileWithStreams
    {
        public DateTime CreationTimeUtc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FileAttributes FileAttributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IBuffer FileContent => throw new NotImplementedException();

        public long FileLength => throw new NotImplementedException();

        public DateTime LastAccessTimeUtc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime LastWriteTimeUtc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SparseStream CreateStream(string name)
        {
            throw new NotImplementedException();
        }

        public SparseStream OpenExistingStream(string name)
        {
            throw new NotImplementedException();
        }
    }
}
