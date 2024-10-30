namespace Digi21.Managed.BinImporter.Geometry;
public class Point(string code, MaxMin maxmin, Point3D position, float rotation) : Geometry(code, maxmin)
{
    public Point3D Position { get; } = position;
    public float Rotation { get; } = rotation;
}
