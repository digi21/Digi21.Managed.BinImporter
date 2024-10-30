namespace Digi21.Managed.BinImporter.Geometry;
public readonly struct Point3D(double x, double y, double z)
{
    public double X { get; } = x;
    public double Y { get; } = y;
    public double Z { get; } = z;
}
