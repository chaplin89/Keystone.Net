# Keystone.Net
.Net bindings for Keystone

##Usage

 ```csharp
 using(var keystone = new Keystone(KeystoneArchitecture.KS_ARCH_X86, KeystoneMode.KS_MODE_32, false))
 {
    ulong address = 0;
    KeystoneEncoded enc = keystone.Assemble("xor eax, eax", address);
    ...
 }
