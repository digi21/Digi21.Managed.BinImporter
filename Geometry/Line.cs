namespace Digi21.Managed.BinImporter.Geometry;
public class Line(string code, MaxMin maxmin, Point3D[] points) : Geometry(code, maxmin)
{
    public Point3D[] Points { get; } = points;
}
