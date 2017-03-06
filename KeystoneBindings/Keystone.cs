using System;

namespace Keystone
{
    public class Keystone : IDisposable
    {
        IntPtr engine = IntPtr.Zero;

        public Keystone(KeystoneArchitecture architecture, KeystoneMode mode)
        {
            var result = KeystoneImports.Open(architecture, (int)mode, ref engine);
            if (result != KeystoneError.KS_ERR_OK)
                throw new InvalidOperationException($"Error while initializing keystone: {KeystoneImports.ErrorToString(result)}");
        }

        public KeystoneEncoded Assemble(string toEncode, ulong address)
        {
            IntPtr encoding;
            uint size;
            uint statementCount;

            int result = KeystoneImports.Assemble(engine,
                toEncode,
                address,
                out encoding,
                out size,
                out statementCount);

            return new KeystoneEncoded(encoding, size, statementCount);
        }

        public KeystoneError GetLastKeystoneError()
        {
            return KeystoneImports.GetLastKeystoneError(engine);
        }

        public void Dispose()
        {
            if (engine != IntPtr.Zero)
                KeystoneImports.Close(engine);
            engine = IntPtr.Zero;
        }
    }
}
