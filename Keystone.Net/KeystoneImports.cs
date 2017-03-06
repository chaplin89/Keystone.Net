using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Keystone
{
    public class KeystoneImports
    {
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_version" )]
        public extern static uint Version(ref uint major, ref uint minor);
        
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_open")]
        public extern unsafe static KeystoneError Open(KeystoneArchitecture arch, int mode, ref IntPtr ks);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_close")]
        public extern unsafe static KeystoneError Close(IntPtr ks);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_free")]
        public extern unsafe static void Free(IntPtr buffer);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_strerror")]
        public extern static unsafe char* ErrorToString(KeystoneError code);
        
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_errno")]
        public extern static KeystoneError GetLastKeystoneError(IntPtr ks);
        
        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_arch_supported")]
        public extern static bool IsArchitectureSupported(KeystoneArchitecture arch);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_option")]
        public extern static KeystoneError SetOption(IntPtr ks, OptionType type, uint value);

        [DllImport("keystone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ks_asm")]
        public extern unsafe static int Assemble(IntPtr ks, 
            [MarshalAs(UnmanagedType.LPStr)] string toEncode, 
            ulong baseAddress, 
            out IntPtr encoding,
            out uint size, 
            out uint statements);
    }
}
