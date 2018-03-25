using System;
using System.Collections.Generic;
using System.Text;

namespace DiscUtils.Hfs
{
    [Flags]
    internal enum VolumeAttributes : uint
    {
        /* Bits 0-6 are reserved */
        None = 0, 
        VolumeHardwareLock = 0x00000080,        // 1 << 7
        VolumeUnmounted = 0x00000100,           // 1 << 8
        VolumeSparedBlocks = 0x00000200,        // 1 << 9
        VolumeNoCacheRequired = 0x00000400,     // 1 << 10
        BootVolumeInconsistent = 0x00000800,    // 1 << 11
        CatalogNodeIdsReused = 0x00001000,      // 1 << 12
        // VolumeJournaled = 0x00002000,        // No journalling in HFS!
        VolumeSoftwareLock = 0x00008000         // 1 << 15
    }
}
