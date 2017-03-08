using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Keystone
{
    /// <summary>
    /// Manage a Keystone engine.
    /// </summary>
    public class Keystone : IDisposable
    {
        IntPtr engine = IntPtr.Zero;

        /// <summary>
        /// Construct the object with a given architecture and a given mode.
        /// </summary>
        /// <param name="architecture">Architecture</param>
        /// <param name="mode">Mode, i.e. endianess, word size etc.</param>
        /// <remarks>
        /// Some architectures are not supported.
        /// Check with <see cref="IsArchitectureSupported(KeystoneArchitecture)"/> if the engine
        /// support the architecture.
        /// </remarks>
        public Keystone(KeystoneArchitecture architecture, KeystoneMode mode, bool throwIfError = true)
        {
            var result = KeystoneImports.Open(architecture, (int)mode, ref engine);
            if (result != KeystoneError.KS_ERR_OK)


                throw new InvalidOperationException($"Error while initializing keystone: {ErrorToString(result)}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void SetOption(OptionType type, uint value)
        {
            var result = KeystoneImports.SetOption(engine, type, value);
            if (result != KeystoneError.KS_ERR_OK)
                throw new InvalidOperationException($"Error while setting option in keystone: {ErrorToString(result)}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ErrorToString(KeystoneError result)
        {
            IntPtr error = KeystoneImports.ErrorToString(result);
            if (error != IntPtr.Zero)
                return Marshal.PtrToStringAnsi(error);
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEncode"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public KeystoneEncoded Assemble(string toEncode, ulong address)
        {
            IntPtr encoding;
            uint size;
            uint statementCount;
            byte[] buffer;

            int result = KeystoneImports.Assemble(engine, 
                                                  toEncode, 
                                                  address, 
                                                  out encoding, 
                                                  out size, 
                                                  out statementCount);

            if (result != 0)
                throw new InvalidOperationException($"Error while assembling {toEncode}: {ErrorToString(GetLastKeystoneError())}");

            buffer = new byte[size];

            unsafe
            {
                byte* ptrBuffer = (byte*)encoding;
                Marshal.Copy(encoding, buffer, 0, (int)size);
            }

            KeystoneImports.Free(encoding);
            return new KeystoneEncoded(buffer, statementCount);
        }

        /// <summary>
        /// Get the last error for this instance.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        public KeystoneError GetLastKeystoneError()
        {
            return KeystoneImports.GetLastKeystoneError(engine);
        }

        /// <summary>
        /// Check if an architecture is supported.
        /// </summary>
        /// <param name="architecture">Architecture</param>
        /// <returns>True if it is supported</returns>
        public static bool IsArchitectureSupported(KeystoneArchitecture architecture)
        {
            return KeystoneImports.IsArchitectureSupported(architecture);
        }

        /// <summary>
        /// Get the version of the engine.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <returns></returns>
        public static uint GetKeystoneVersion(ref uint major, ref uint minor)
        {
            return KeystoneImports.Version(ref major, ref minor);
        }
        
        /// <summary>
        /// Release the engine.
        /// </summary>
        public void Dispose()
        {
            var currentEngine = Interlocked.Exchange(ref engine, IntPtr.Zero);
            if (currentEngine != IntPtr.Zero)
                KeystoneImports.Close(currentEngine);

            GC.SuppressFinalize(this);
        }
    }
}
