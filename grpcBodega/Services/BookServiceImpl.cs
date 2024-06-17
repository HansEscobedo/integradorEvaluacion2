using Grpc.Core;
using grpcBodega;

namespace grpcBodega.Services;

public class BookServiceImpl : BookService.BookServiceBase
{
    private readonly ILogger<BookServiceImpl> _logger;

    public BookServiceImpl(ILogger<BookServiceImpl> logger)
    {
        _logger = logger;
    }

    public override Task<LibroList> ConsultaLibros(Empty request, ServerCallContext context)
    {
        var response = new LibroList();
        response.Libros.AddRange(LibroData.Libros.Select(libro => new Libro
        {
            Id = libro.Id,
            Titulo = libro.Titulo,
            Cantidad = libro.Cantidad,
            Precio = libro.Precio
        }));
        return Task.FromResult(response);
    }

    public override Task<VentaResponse> VendeLibro(VentaRequest request, ServerCallContext context)
    {
        var libro = LibroData.Libros.FirstOrDefault(l => l.Id == request.Id);
        if (libro == null)
        {
            return Task.FromResult(new VentaResponse { Success = false, Message = "Libro no encontrado" });
        }

        if (libro.Cantidad < request.Cantidad)
        {
            return Task.FromResult(new VentaResponse { Success = false, Message = "Cantidad insuficiente" });
        }

        libro.Cantidad -= request.Cantidad;
        return Task.FromResult(new VentaResponse { Success = true, Message = "Venta realizada con éxito" });
    }
}
