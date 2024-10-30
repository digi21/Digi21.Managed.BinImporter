using System.Runtime.InteropServices;

namespace Digi21.Managed.BinImporter.InternalStructs;

[StructLayout(LayoutKind.Sequential)]
internal struct MaxMin
{
    public Point3D Min;
    public Point3D Max;
    public Point3D Extra;
}