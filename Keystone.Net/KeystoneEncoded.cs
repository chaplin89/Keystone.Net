using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Keystone
{
    public unsafe class KeystoneEncoded
    {
        public KeystoneEncoded(byte[] buffer, uint statementCount)
        {
            Buffer = buffer;
            StatementCount = statementCount;
        }
        
        public byte[] Buffer { get; private set; }
        public uint StatementCount { get; private set; }
    }
}
