namespace Digi21.Managed.BinImporter.Geometry;
public class Geometry(string code, MaxMin maxmin)
{
    public string Code { get; } = code;
    public MaxMin MaxMin { get; } = maxmin;
}
