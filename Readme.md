# Digi21.Managed.BinImporter

Este proyecto es una implementación OpenSource de un lector de archivos binarios clásicos de Digi21.


A continuación un ejemplo de uso:

```csharp
using Digi21.Managed.BinImporter;
using Digi21.Managed.BinImporter.Geometry;

using var archivo = File.OpenRead(@"E:\Desktop\prueba.bin");
using var importador = new Importador(archivo, new Point3D(0,0,0), 100);

while (true)
{
    var geometria = importador.LeeGeometria();
    if (geometria == null)
    {
        break;
    }

    Console.WriteLine(geometria.Code);
}
```