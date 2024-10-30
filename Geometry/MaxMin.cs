namespace Digi21.Managed.BinImporter.Geometry;
public readonly struct MaxMin(Point3D max, Point3D min)
{
    public Point3D Max { get; } = max;
    public Point3D Min { get; } = min;
}
