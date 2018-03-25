using System;
using System.IO;
using DiscUtils.Streams;
using DiscUtils.Vfs;

namespace DiscUtils.Hfs
{
    
    internal sealed class HfsFileSystemImpl : VfsFileSystem<DirEntry, File, Directory, Context>, IUnixFileSystem
    {

        public HfsFileSystemImpl(Stream s)
            : base(new DiscFileSystemOptions())
        {

        }

        public override string FriendlyName => "Apple HFS";


        public override string VolumeLabel => throw new NotImplementedException();
        
        public override bool CanWrite => false;

        public override long Size => throw new NotImplementedException();

        public override long UsedSpace => throw new NotImplementedException();

        public override long AvailableSpace => throw new NotImplementedException();

        public UnixFileSystemInfo GetUnixFileInfo(string path)
        {
            throw new NotImplementedException();
        }

        protected override File ConvertDirEntryToFile(DirEntry dirEntry)
        {
            throw new NotImplementedException();
        }
    }
}
