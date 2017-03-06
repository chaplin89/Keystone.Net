using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Keystone;
using System.Text;

namespace KeystoneBindingsTests
{
    [TestClass]
    public class KeystoneBindingsTests
    {
        [TestMethod]
        public unsafe void TestVersion()
        {
            uint major = 0;
            uint minor = 0;
            KeystoneImports.Version(ref major, ref minor);
            IntPtr engine = IntPtr.Zero;
            IntPtr encoding = IntPtr.Zero;
            bool isSupported = KeystoneImports.IsArchitectureSupported(KeystoneArchitecture.KS_ARCH_X86);

            KeystoneImports.Open(KeystoneArchitecture.KS_ARCH_X86, (int)(KeystoneMode.KS_MODE_32 | KeystoneMode.KS_MODE_LITTLE_ENDIAN), &engine);

            KeystoneImports.Assemble(engine, "inc eax;", 0, &encoding, ref major, ref minor);
        }
    }
}
