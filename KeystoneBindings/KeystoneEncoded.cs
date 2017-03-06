using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeystoneBindings
{
    public unsafe class KeystoneEncoded : IDisposable
    {
        private IntPtr buffer;

        public KeystoneEncoded(IntPtr buffer, uint size, uint statementCount)
        {
            Buffer = (byte*)buffer;
            Size = size;
            StatementCount = statementCount;
        }

        public uint Size { get; private set; }
        public byte* Buffer { get; private set; }
        public uint StatementCount { get; private set; }

        public byte[] ToByteArray()
        {
            if (Size != 0)
            {
                byte[] returnValue = new byte[Size];
                Marshal.Copy((IntPtr)Buffer, returnValue, 0, (int)Size);
                return returnValue;
            }
            return null;
        }

        public void Dispose()
        {
            if (buffer != null)
                KeystoneImports.Free(buffer);

            buffer = IntPtr.Zero;
            Size = 0;
            StatementCount = 0;
        }
    }
}
