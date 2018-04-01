using DiscUtils.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscUtils.Hfs
{
    /// <summary>
    /// Volume Header information. Reference info from https://opensource.apple.com/source/xnu/xnu-344/bsd/hfs/hfs_format.h
    /// </summary>
    internal sealed class MasterDirectoryBlock : IByteArraySerializable
    {
        public const ushort HfsSignature = 0x4244;

        //public ForkData AllocationFile;
        public VolumeAttributes Attributes;
        //public ForkData AttributesFile;
        public DateTime BackupDate;

        public uint BlockSize;
        //public ForkData CatalogFile;
        //public DateTime CheckedDate;

        public DateTime CreateDate;
        public uint DataClumpSize;
        //public ulong EncodingsBitmap;
        //public ForkData ExtentsFile;

        public uint FileCount;

        public uint[] FinderInfo;
        public uint FolderCount;
        public uint FreeBlocks;
        //public uint JournalInfoBlock;
        //public uint LastMountedVersion;
        public DateTime ModifyDate;

        public uint NextAllocation;
        public CatalogNodeId NextCatalogId;
        public uint ResourceClumpSize;

        public ushort Signature;
        //public ForkData StartupFile;
        public uint TotalBlocks;
        //public ushort Version;

        public uint WriteCount;
        public ushort FirstBlock;
        public ushort RootFileCount;
        /// <summary>
        /// extents B-tree's first 3 extents
        /// </summary>
        public ExtentDataRecord ExtentsOverflow;

        /// <summary>
        /// catalog B-tree's first 3 extents
        /// </summary>
        public ExtentDataRecord CatalogExtent;
        /// <summary>
        /// Bytes in the extents B-tree
        /// </summary>
        public uint ExtentsSize;

        /// <summary>
        /// bytes in the catalog B-tree 
        /// </summary>
        private uint CatalogSize;

        public bool IsValid
        {
            get { return Signature == HfsSignature; }
        }

        public int Size
        {
            get { return 162; }
        }

        public string VolumeName { get; set; }

        public int ReadFrom(byte[] buffer, int offset)
        {
            /*
    * struct MasterDirectoryBlock
    * size: 162 bytes
    * description:
    *
    * BP   Size  Type        Identifier  Description
    * --------------------------------------------------------------------------
    * 0    2     UInt16      drSigWord   volume signature
    * 2    4     UInt32      drCrDate    date and time of volume creation
    * 6    4     UInt32      drLsMod     date and time of last modification
    * 10   2     UInt16      drAtrb      volume attributes
    * 12   2     UInt16      drNmFls     number of files in root directory
    * 14   2     UInt16      drVBMSt     first block of volume bitmap
    * 16   2     UInt16      drAllocPtr  start of next allocation search
    * 18   2     UInt16      drNmAlBlks  number of allocation blocks in volume
    * 20   4     UInt32      drAlBlkSiz  size (in bytes) of allocation blocks
    * 24   4     UInt32      drClpSiz    default clump size
    * 28   2     UInt16      drAlBlSt    first allocation block in volume
    * 30   4     UInt32      drNxtCNID   next unused catalog node ID
    * 34   2     UInt16      drFreeBks   number of unused allocation blocks
    * 36   1     UInt8       drVNLength  length of volume name
    * 37   1*27  Char[28]    drVN        volume name
    * 64   4     UInt32      drVolBkUp   date and time of last backup
    * 68   2     UInt16      drVSeqNum   volume backup sequence number
    * 70   4     UInt32      drWrCnt     volume write count
    * 74   4     UInt32      drXTClpSiz  clump size for extents overflow file
    * 78   4     UInt32      drCTClpSiz  clump size for catalog file
    * 82   2     UInt16      drNmRtDirs  number of directories in root directory
    * 84   4     UInt32      drFilCnt    number of files in volume
    * 88   4     UInt32      drDirCnt    number of directories in volume
    * 92   32    HFSVolumeFinderInfo drFndrInfo information used by the Finder
    * 124  2     UInt16      drVCSize    size (in blocks) of volume cache
    * 126  2     UInt16      drVBMCSize  size (in blocks) of volume bitmap cache
    * 128  2     UInt16      drCtlCSize  size (in blocks) of common volume cache
    * 130  4     UInt32      drXTFlSize  size of extents overflow file
    * 134  12    ExtDataRec  drXTExtRec  extent record for extents overflow file
    * 146  4     UInt32      drCTFlSize  size of catalog file
    * 150  12    ExtDataRec  drCTExtRec  extent record for catalog file
    */

            Signature = EndianUtilities.ToUInt16BigEndian(buffer, offset + 0);
            
            CreateDate = HfsUtilities.ReadHfsDate(DateTimeKind.Local, buffer, offset + 2);
            ModifyDate = HfsUtilities.ReadHfsDate(DateTimeKind.Utc, buffer, offset + 6);

            Attributes = (VolumeAttributes)EndianUtilities.ToUInt16BigEndian(buffer, offset + 10);

            RootFileCount = EndianUtilities.ToUInt16BigEndian(buffer, offset + 12);
            FirstBlock = EndianUtilities.ToUInt16BigEndian(buffer, offset + 14);
            

            TotalBlocks = EndianUtilities.ToUInt16BigEndian(buffer, offset + 18);
            BlockSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 20);
            ResourceClumpSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 24);
            DataClumpSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 24);
            NextAllocation = EndianUtilities.ToUInt16BigEndian(buffer, offset + 28);
            NextCatalogId = new CatalogNodeId(EndianUtilities.ToUInt32BigEndian(buffer, offset + 30));
            FreeBlocks = EndianUtilities.ToUInt16BigEndian(buffer, offset + 34);

            // VolumeName
            VolumeName = EndianUtilities.BytesToString(buffer, offset + 37, buffer[36]);
            
            BackupDate = HfsUtilities.ReadHfsDate(DateTimeKind.Local, buffer, offset + 64);

            FileCount = EndianUtilities.ToUInt32BigEndian(buffer, offset + 84);
            FolderCount = EndianUtilities.ToUInt32BigEndian(buffer, offset + 88);

            

            WriteCount = EndianUtilities.ToUInt32BigEndian(buffer, offset + 70);
            //EncodingsBitmap = EndianUtilities.ToUInt64BigEndian(buffer, offset + 72);

            FinderInfo = new uint[8];
            for (int i = 0; i < 8; ++i)
            {
                FinderInfo[i] = EndianUtilities.ToUInt32BigEndian(buffer, offset + 92 + i * 4);
            }

            ExtentsSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 130);
            ExtentsOverflow = EndianUtilities.ToStruct<ExtentDataRecord>(buffer, offset + 134);

            CatalogSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 146);
            CatalogExtent = EndianUtilities.ToStruct<ExtentDataRecord>(buffer, offset + 150);
           
            return 162;
        }

       

        public void WriteTo(byte[] buffer, int offset)
        {
            throw new NotImplementedException();
        }
    }
}