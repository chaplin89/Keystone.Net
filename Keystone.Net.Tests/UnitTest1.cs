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
        public unsafe void TestNOP()
        {
            using (var keystone = new Keystone.Keystone(KeystoneArchitecture.KS_ARCH_HEXAGON, KeystoneMode.KS_MODE_32))
            {
                var encoded = keystone.Assemble("nop; mov eax, eax", 0);

            }
        }
    }
}
