using DiscUtils.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscUtils.Hfs
{
    internal sealed class ExtentDescriptor : IByteArraySerializable
    {
        public uint BlockCount;
        public uint StartBlock;

        public int Size
        {
            get { return 8; }
        }

        public int ReadFrom(byte[] buffer, int offset)
        {
            StartBlock = EndianUtilities.ToUInt32BigEndian(buffer, offset + 0);
            BlockCount = EndianUtilities.ToUInt32BigEndian(buffer, offset + 4);

            return 8;
        }

        public void WriteTo(byte[] buffer, int offset)
        {
            throw new NotImplementedException();
        }
    }
}
