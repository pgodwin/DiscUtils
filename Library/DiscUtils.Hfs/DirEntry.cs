using System;
using System.IO;
using DiscUtils.Internal;
using DiscUtils.Streams;
using DiscUtils.Vfs;

namespace DiscUtils.Hfs
{
    internal sealed class DirEntry : VfsDirEntry
    {
        public override DateTime CreationTimeUtc => throw new NotImplementedException();

        public override FileAttributes FileAttributes => throw new NotImplementedException();

        public override string FileName => throw new NotImplementedException();

        public override bool HasVfsFileAttributes => throw new NotImplementedException();

        public override bool HasVfsTimeInfo => throw new NotImplementedException();

        public override bool IsDirectory => throw new NotImplementedException();

        public override bool IsSymlink => throw new NotImplementedException();

        public override DateTime LastAccessTimeUtc => throw new NotImplementedException();

        public override DateTime LastWriteTimeUtc => throw new NotImplementedException();

        public override long UniqueCacheId => throw new NotImplementedException();
    }
}
