namespace Digi21.Managed.BinImporter.Geometry;
public class Text(string code, MaxMin maxmin, Point3D position,float rotation, string txt, float height, byte justification) : Point(code, maxmin, position, rotation)
{
    public string Txt { get; } = txt;
    public float Height { get; } = height;
    public byte Justification { get; } = justification;
}
