public class Libro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Cantidad { get; set; }
    public int Precio { get; set; }
}

public static class LibroData
{
    public static List<Libro> Libros = new List<Libro>
    {
        new Libro { Id = 1, Titulo = "Libro 1", Cantidad = 10, Precio = 100 },
        new Libro { Id = 2, Titulo = "Libro 2", Cantidad = 10, Precio = 200 },
    };
}
