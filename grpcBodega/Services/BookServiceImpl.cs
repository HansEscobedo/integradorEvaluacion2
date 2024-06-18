using Grpc.Core;
using grpcBodega;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace grpcBodega.Services
{
    public class BookServiceImpl : BookService.BookServiceBase
    {
        private readonly ContextoLibreria _context;
        private readonly ILogger<BookServiceImpl> _logger;

        public BookServiceImpl(ContextoLibreria context, ILogger<BookServiceImpl> logger)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<LibroList> ConsultaLibros(Empty request, ServerCallContext context)
        {
            var response = new LibroList();
            var libros = await _context.Libros.ToListAsync();

            response.Libros.AddRange(libros.Select(libro => new Libro
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Cantidad = libro.Cantidad,
                Precio = libro.Precio
            }));

            return response;
        }

        public override async Task<VentaResponse> VendeLibro(VentaRequest request, ServerCallContext context)
        {
            var libro = await _context.Libros.FindAsync(request.Id);
            if (libro == null)
            {
                return new VentaResponse { Success = false, Message = "Libro no encontrado" };
            }

            if (libro.Cantidad < request.Cantidad)
            {
                return new VentaResponse { Success = false, Message = "Cantidad insuficiente" };
            }

            libro.Cantidad -= request.Cantidad;
            await _context.SaveChangesAsync();

            return new VentaResponse { Success = true, Message = "Venta realizada con éxito" };
        }
    }
}
