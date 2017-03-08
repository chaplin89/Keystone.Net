using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Keystone;
using System.Text;
using System.Collections.Generic;

namespace KeystoneBindingsTests
{
    [TestClass]
    public class KeystoneBindingsTests
    {
        [TestMethod]
        public unsafe void TestNOP()
        {
            using (var keystone = new Keystone.Keystone(KeystoneArchitecture.KS_ARCH_X86, KeystoneMode.KS_MODE_32))
            {
                var asm = new List<byte>();
                keystone.AppendAssemble("nop; mov eax, eax", asm);
                keystone.AppendAssemble("xor eax, eax", asm);
            }
        }
    }
}
