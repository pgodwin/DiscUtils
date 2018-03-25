using DiscUtils.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscUtils.Hfs
{
    internal sealed class ForkData : IByteArraySerializable
    {
        public const int StructSize = 80;
        public uint ClumpSize;
        public ExtentDescriptor[] Extents;

        public ulong LogicalSize;
        public uint TotalBlocks;

        public int Size
        {
            get { return StructSize; }
        }

        public int ReadFrom(byte[] buffer, int offset)
        {
            LogicalSize = EndianUtilities.ToUInt64BigEndian(buffer, offset + 0);
            ClumpSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 8);
            TotalBlocks = EndianUtilities.ToUInt32BigEndian(buffer, offset + 12);

            Extents = new ExtentDescriptor[8];
            for (int i = 0; i < 8; ++i)
            {
                Extents[i] = EndianUtilities.ToStruct<ExtentDescriptor>(buffer, offset + 16 + i * 8);
            }

            return StructSize;
        }

        public void WriteTo(byte[] buffer, int offset)
        {
            throw new NotImplementedException();
        }
    }
}
