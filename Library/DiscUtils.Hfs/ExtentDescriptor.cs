﻿//
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
    internal sealed class ExtentDescriptor : IByteArraySerializable
    {
        public uint BlockCount;
        public uint StartBlock;

        public int Size
        {
            get { return 4; }
        }

        public int ReadFrom(byte[] buffer, int offset)
        {
           /*
            * struct ExtDescriptor
            * size: 4 bytes
            * description:
            *
            * BP  Size  Type    Identifier   Description
            * ----------------------------------------------------------
            * 0   2     UInt16  xdrStABN     first allocation block
            * 2   2     UInt16  xdrNumABlks  number of allocation blocks
            */


            StartBlock = EndianUtilities.ToUInt16BigEndian(buffer, offset + 0);
            BlockCount = EndianUtilities.ToUInt16BigEndian(buffer, offset + 2);

            return 4;
        }

        public void WriteTo(byte[] buffer, int offset)
        {
            throw new NotImplementedException();
        }
    }
}