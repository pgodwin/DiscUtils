using System;
using System.Collections.Generic;
using DiscUtils.Vfs;

namespace DiscUtils.Hfs
{
    internal sealed class Directory : File, IVfsDirectory<DirEntry, File>
    {
        public ICollection<DirEntry> AllEntries => throw new NotImplementedException();

        public DirEntry Self => throw new NotImplementedException();

        public DirEntry CreateNewFile(string name)
        {
            throw new NotImplementedException();
        }

        public DirEntry GetEntryByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
