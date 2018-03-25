using DiscUtils.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscUtils.Hfs
{
    /// <summary>
    /// Volume Header information. Reference info from https://opensource.apple.com/source/xnu/xnu-344/bsd/hfs/hfs_format.h
    /// </summary>
    internal sealed class VolumeHeader : IByteArraySerializable
    {
        public const ushort HfsSignature = 0x4244;

        public ForkData AllocationFile;
        public VolumeAttributes Attributes;
        public ForkData AttributesFile;
        public DateTime BackupDate;

        public uint BlockSize;
        public ForkData CatalogFile;
        public DateTime CheckedDate;

        public DateTime CreateDate;
        public uint DataClumpSize;
        public ulong EncodingsBitmap;
        public ForkData ExtentsFile;

        public uint FileCount;

        public uint[] FinderInfo;
        public uint FolderCount;
        public uint FreeBlocks;
        public uint JournalInfoBlock;
        public uint LastMountedVersion;
        public DateTime ModifyDate;

        public uint NextAllocation;
        //public CatalogNodeId NextCatalogId;
        public uint ResourceClumpSize;

        public ushort Signature;
        public ForkData StartupFile;
        public uint TotalBlocks;
        public ushort Version;

        public uint WriteCount;

        public bool IsValid
        {
            get { return Signature == HfsSignature; }
        }

        public int Size
        {
            get { return 512; }
        }

        public int ReadFrom(byte[] buffer, int offset)
        {
            Signature = EndianUtilities.ToUInt16BigEndian(buffer, offset + 0);
            Version = EndianUtilities.ToUInt16BigEndian(buffer, offset + 2);
            Attributes = (VolumeAttributes)EndianUtilities.ToUInt32BigEndian(buffer, offset + 4);
            LastMountedVersion = EndianUtilities.ToUInt32BigEndian(buffer, offset + 8);
            JournalInfoBlock = EndianUtilities.ToUInt32BigEndian(buffer, offset + 12);

            CreateDate = HfsUtilities.ReadHfsDate(DateTimeKind.Local, buffer, offset + 16);
            ModifyDate = HfsUtilities.ReadHfsDate(DateTimeKind.Utc, buffer, offset + 20);
            BackupDate = HfsUtilities.ReadHfsDate(DateTimeKind.Utc, buffer, offset + 24);
            CheckedDate = HfsUtilities.ReadHfsDate(DateTimeKind.Utc, buffer, offset + 28);

            FileCount = EndianUtilities.ToUInt32BigEndian(buffer, offset + 32);
            FolderCount = EndianUtilities.ToUInt32BigEndian(buffer, offset + 36);

            BlockSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 40);
            TotalBlocks = EndianUtilities.ToUInt32BigEndian(buffer, offset + 44);
            FreeBlocks = EndianUtilities.ToUInt32BigEndian(buffer, offset + 48);

            NextAllocation = EndianUtilities.ToUInt32BigEndian(buffer, offset + 52);
            ResourceClumpSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 56);
            DataClumpSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 60);
            //NextCatalogId = new CatalogNodeId(EndianUtilities.ToUInt32BigEndian(buffer, offset + 64));

            WriteCount = EndianUtilities.ToUInt32BigEndian(buffer, offset + 68);
            EncodingsBitmap = EndianUtilities.ToUInt64BigEndian(buffer, offset + 72);

            FinderInfo = new uint[8];
            for (int i = 0; i < 8; ++i)
            {
                FinderInfo[i] = EndianUtilities.ToUInt32BigEndian(buffer, offset + 80 + i * 4);
            }

            AllocationFile = EndianUtilities.ToStruct<ForkData>(buffer, offset + 112);
            ExtentsFile = EndianUtilities.ToStruct<ForkData>(buffer, offset + 192);
            CatalogFile = EndianUtilities.ToStruct<ForkData>(buffer, offset + 272);
            AttributesFile = EndianUtilities.ToStruct<ForkData>(buffer, offset + 352);
            StartupFile = EndianUtilities.ToStruct<ForkData>(buffer, offset + 432);

            return 512;
        }

        public void WriteTo(byte[] buffer, int offset)
        {
            throw new NotImplementedException();
        }
    }
}