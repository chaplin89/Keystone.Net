using System;
using System.Runtime.InteropServices;

namespace Keystone
{
    /// <summary>
    /// Imports for keystone.dll
    /// </summary>
    internal class KeystoneImports
    {
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_version" )]
        internal extern static uint Version(ref uint major, ref uint minor);
        
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_open")]
        internal extern static KeystoneError Open(KeystoneArchitecture arch, int mode, ref IntPtr ks);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_close")]
        internal extern static KeystoneError Close(IntPtr ks);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_free")]
        internal extern static void Free(IntPtr buffer);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_strerror")]
        internal extern static IntPtr ErrorToString(KeystoneError code);
        
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_errno")]
        internal extern static KeystoneError GetLastKeystoneError(IntPtr ks);
        
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_arch_supported")]
        internal extern static bool IsArchitectureSupported(KeystoneArchitecture arch);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_option")]
        internal extern static KeystoneError SetOption(IntPtr ks, OptionType type, uint value);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_asm")]
        internal extern static int Assemble(IntPtr ks, 
            [MarshalAs(UnmanagedType.LPStr)] string toEncode, 
            ulong baseAddress, 
            out IntPtr encoding,
            out uint size, 
            out uint statements);
    }
}
