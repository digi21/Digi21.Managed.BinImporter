using System.Runtime.InteropServices;
using System.Text;

namespace Digi21.Managed.BinImporter.InternalStructs;

[StructLayout(LayoutKind.Sequential)]
internal struct Header
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte[] Type;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    private byte[] code;

    public string Code => Encoding.ASCII.GetString(code).Trim();

    public byte NumberOfAttributes;
    public ushort NumberOfPoints;
}
