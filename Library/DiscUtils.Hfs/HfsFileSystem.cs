using DiscUtils.Streams;
using DiscUtils.Vfs;
using System;
using System.IO;

namespace DiscUtils.Hfs
{
    /// <summary>
    /// Class that interprets Apple's HFS file system, found in Macintosh images prior to MacOS 8.1
    /// </summary>
    public class HfsFileSystem : VfsFileSystemFacade, IUnixFileSystem
    {
        /// <summary>
        /// Initializes a new instance of the HfsFileSystem class.
        /// </summary>
        /// <param name="stream">A stream containing the file system.</param>
        public HfsFileSystem(Stream stream)
            : base(new HfsFileSystemImpl(stream)) { }

        /// <summary>
        /// Gets the Unix (BSD) file information about a file or directory.
        /// </summary>
        /// <param name="path">The path of the file or directory.</param>
        /// <returns>Unix file information.</returns>
        public UnixFileSystemInfo GetUnixFileInfo(string path)
        {
            return GetRealFileSystem<HfsFileSystemImpl>().GetUnixFileInfo(path);
        }

        internal static bool Detect(Stream stream)
        {
            if (stream.Length < 1536)
            {
                return false;
            }

            stream.Position = 1024;

            byte[] headerBuf = StreamUtilities.ReadExact(stream, 512);
            VolumeHeader hdr = new VolumeHeader();
            hdr.ReadFrom(headerBuf, 0);

            return hdr.IsValid;
        }
    }
}
