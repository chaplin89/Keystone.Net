using System;
using System.Runtime.InteropServices;

namespace Keystone
{
    public class Keystone : IDisposable
    {
        IntPtr engine = IntPtr.Zero;

        public Keystone(KeystoneArchitecture architecture, KeystoneMode mode)
        {
            var result = KeystoneImports.Open(architecture, (int)mode, ref engine);
            if (result != KeystoneError.KS_ERR_OK)
                throw new InvalidOperationException($"Error while initializing keystone: {ErrorToString(result)}");
        }

        public void SetOption(IntPtr ks, OptionType type, uint value)
        {
            var result = KeystoneImports.SetOption(engine, type, value);
            if (result != KeystoneError.KS_ERR_OK)
                throw new InvalidOperationException($"Error while setting option in keystone: {ErrorToString(result)}");
        }

        private string ErrorToString(KeystoneError result)
        {
            unsafe
            {
                IntPtr error = (IntPtr)KeystoneImports.ErrorToString(result);
                if (error != IntPtr.Zero)
                    return Marshal.PtrToStringAnsi(error);
                return string.Empty;
            }
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

            if (result != 0)
                throw new InvalidOperationException($"Error while assembling {toEncode}: {ErrorToString(GetLastKeystoneError())}");

            return new KeystoneEncoded(encoding, size, statementCount);
        }

        public KeystoneError GetLastKeystoneError()
        {
            return KeystoneImports.GetLastKeystoneError(engine);
        }

        public bool IsArchitectureSupported(KeystoneArchitecture architecture)
        {
            return KeystoneImports.IsArchitectureSupported(architecture);
        }

        public uint GetKeystoneVersion(ref uint major, ref uint minor)
        {
            return KeystoneImports.Version(ref major, ref minor);
        }

        public void Dispose()
        {
            if (engine != IntPtr.Zero)
                KeystoneImports.Close(engine);
            engine = IntPtr.Zero;
        }
    }
}
