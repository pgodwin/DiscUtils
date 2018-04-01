//
// Copyright (c) 2008-2011, Kenneth Bell
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//

using System;
using DiscUtils.Streams;

namespace DiscUtils.Hfs
{
    internal class BTreeHeaderRecord : BTreeNodeRecord
    {
        //public int Attributes;
        //public int ClumpSize;
        public int FirstLeafNode;
        public int FreeNodes;
        //public byte KeyCompareType;
        public int LastLeafNode;
        public short MaxKeyLength;
        public short NodeSize;
        public int NumLeafRecords;
        //public ushort Res1;
        public int RootNode;
        public int TotalNodes;
        public short TreeDepth;
        //public byte TreeType;

        public override int Size
        {
            get { return 106; }
        }

        public override int ReadFrom(byte[] buffer, int offset)
        {
            /*
            * struct BTHdrRec
            * size: 106 bytes
            * description:
            *
            * BP  Size  Type       Identifier   Description
            * --------------------------------------------------------------------------
            * 0   2     SInt16     bthDepth     current depth of tree (Integer)
            * 2   4     SInt32     bthRoot      number of root node (LongInt)
            * 6   4     SInt32     bthNRecs     number of leaf records in tree (LongInt)
            * 10  4     SInt32     bthFNode     number of first leaf node (LongInt)
            * 14  4     SInt32     bthLNode     number of last leaf node (LongInt)
            * 18  2     SInt16     bthNodeSize  size of a node (Integer)
            * 20  2     SInt16     bthKeyLen    maximum length of a key (Integer)
            * 22  4     SInt32     bthNNodes    total number of nodes in tree (LongInt)
            * 26  4     SInt32     bthFree      number of free nodes (LongInt)
            * 30  1*76  SInt8[76]  bthResv      reserved (ARRAY[1..76] OF SignedByte)
            */
            TreeDepth = EndianUtilities.ToInt16BigEndian(buffer, offset + 0);
            RootNode = EndianUtilities.ToInt32BigEndian(buffer, offset + 2);
            NumLeafRecords = EndianUtilities.ToInt32BigEndian(buffer, offset + 6);
            FirstLeafNode = EndianUtilities.ToInt32BigEndian(buffer, offset + 10);
            LastLeafNode = EndianUtilities.ToInt32BigEndian(buffer, offset + 14);
            NodeSize = EndianUtilities.ToInt16BigEndian(buffer, offset + 18);
            MaxKeyLength = EndianUtilities.ToInt16BigEndian(buffer, offset + 20);
            TotalNodes = EndianUtilities.ToInt32BigEndian(buffer, offset + 22);
            FreeNodes = EndianUtilities.ToInt32BigEndian(buffer, offset + 26);

            // Ignore the reserved?

            //Res1 = EndianUtilities.ToUInt16BigEndian(buffer, offset + 28);
            //ClumpSize = EndianUtilities.ToUInt32BigEndian(buffer, offset + 30);
            //TreeType = buffer[offset + 34];
            //KeyCompareType = buffer[offset + 35];
            //Attributes = EndianUtilities.ToUInt32BigEndian(buffer, offset + 36);

            return 106;
        }

        public override void WriteTo(byte[] buffer, int offset)
        {
            throw new NotImplementedException();
        }
    }
}