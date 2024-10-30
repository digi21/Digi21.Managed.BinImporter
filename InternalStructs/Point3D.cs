using System.Runtime.InteropServices;

namespace Digi21.Managed.BinImporter.InternalStructs;

[StructLayout(LayoutKind.Sequential)]
internal struct Point3D
{
    public int X;
    public int Y;
    public int Z;
}
