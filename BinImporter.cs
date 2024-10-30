using System.Runtime.InteropServices;
using System.Text;
using Digi21.Managed.BinImporter.Geometry;
using Digi21.Managed.BinImporter.InternalStructs;
using MaxMin = Digi21.Managed.BinImporter.InternalStructs.MaxMin;
using Point3D = Digi21.Managed.BinImporter.Geometry.Point3D;

namespace Digi21.Managed.BinImporter;
public class Importador(Stream stream, Point3D origenGlobal, float precision) : IDisposable
{
    private readonly BinaryReader reader = new(stream);

    public Geometry.Geometry? LeeGeometria()
    {
        if (reader.BaseStream.Position == reader.BaseStream.Length)
            return null;

        var cabecera = ReadStruct<Header>();
        var max = ReadStruct<MaxMin>();

        switch (cabecera.Type[0])
        {
            case (byte)'C':
                if (1 == cabecera.NumberOfPoints)
                    return LeePunto(cabecera, max);
                return LeeLinea(cabecera, max);
            case (byte)'T':
                return LeeTexto(cabecera, max);
        }
        return null;
    }

    private Point3D Punto3DToPunto3D(InternalStructs.Point3D punto3D)
    {
        return new Point3D(punto3D.X / precision + origenGlobal.X, punto3D.Y / precision + origenGlobal.Y, punto3D.Z / precision + origenGlobal.Z);
    }

    private Geometry.MaxMin TipoMaxAMaxMin(MaxMin max)
    {
        return new Geometry.MaxMin(Punto3DToPunto3D(max.Max), Punto3DToPunto3D(max.Min));
    }

    private Geometry.Geometry LeeTexto(Header cabecera, MaxMin max)
    {
        var coordenadas = ReadStruct<InternalStructs.Point3D>();
        var texto = Encoding.ASCII.GetString(reader.ReadBytes(Marshal.SizeOf(typeof(InternalStructs.Point3D)) * (cabecera.NumberOfPoints - 1))).Trim();
        return new Text(cabecera.Code, TipoMaxAMaxMin(max), Punto3DToPunto3D(coordenadas), max.Extra.Z / 100.0f, texto, max.Extra.X / precision, (byte)max.Extra.Y);
    }

    private Geometry.Geometry LeePunto(Header cabecera, MaxMin max)
    {
        var coordenadas = ReadStruct<InternalStructs.Point3D>();
        return new Point(cabecera.Code, TipoMaxAMaxMin(max), Punto3DToPunto3D(coordenadas), max.Extra.Z/100.0f);
    }

    private Geometry.Geometry LeeLinea(Header cabecera, MaxMin max)
    {
        var coordenadasNativo = ReadArrayStruct<InternalStructs.Point3D>(cabecera.NumberOfPoints);

        var coordenadas = new Point3D[cabecera.NumberOfPoints];
        for(var i = 0; i < cabecera.NumberOfPoints; i++)
        {
            coordenadas[i] = Punto3DToPunto3D(coordenadasNativo[i]);
        }

        return new Line(cabecera.Code, TipoMaxAMaxMin(max), coordenadas);
    }

    private T[] ReadArrayStruct<T>(ushort cabeceraNumeroPuntos) where T : struct
    {
        var array = new T[cabeceraNumeroPuntos];
        for (var i = 0; i < cabeceraNumeroPuntos; i++)
        {
            array[i] = ReadStruct<T>();
        }
        return array;
    }

    private T ReadStruct<T>() where T : struct
    {
        T structData;
        var buffer = new byte[Marshal.SizeOf(typeof(T))];

        buffer = reader.ReadBytes(buffer.Length);

        var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        try
        {
            structData = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            handle.Free();
        }

        return structData;
    }

    public void Dispose()
    {
        reader.Dispose();
    }
}
