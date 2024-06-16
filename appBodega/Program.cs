using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var contexto = new ContextoLibreria())
        {
            // Asegurarse de que la base de datos exista y esté actualizada
            contexto.Database.EnsureCreated();

            // Menú de opciones
            int opcion;
            do
            {
                Console.WriteLine("=== Menú de Libros ===");
                Console.WriteLine("1. Listar Libros");
                Console.WriteLine("2. Agregar Libro");
                Console.WriteLine("3. Actualizar Libro");
                Console.WriteLine("4. Eliminar Libro");
                Console.WriteLine("0. Salir");
                Console.Write("Ingrese la opción deseada: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            ListarLibros(contexto);
                            break;
                        case 2:
                            AgregarLibro(contexto);
                            break;
                        case 3:
                            ActualizarLibro(contexto);
                            break;
                        case 4:
                            EliminarLibro(contexto);
                            break;
                        case 0:
                            Console.WriteLine("Saliendo del programa.");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Intente nuevamente.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                }

                Console.WriteLine();
            } while (opcion != 0);
        }
    }

    static void ListarLibros(ContextoLibreria contexto)
    {
        var libros = contexto.Libros.ToList();
        
        Console.WriteLine("=== Lista de Libros ===");
        foreach (var libro in libros)
        {
            Console.WriteLine($"ID: {libro.Id}, Título: {libro.Titulo}, Cantidad: {libro.Cantidad}, Precio: {libro.Precio}");
        }
    }

    static void AgregarLibro(ContextoLibreria contexto)
    {
        Console.Write("Ingrese el título del libro: ");
        var titulo = Console.ReadLine();
        Console.Write("Ingrese la cantidad disponible: ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad))
        {
            Console.WriteLine("Cantidad inválida. Ingrese un número entero.");
            return;
        }
        Console.Write("Ingrese el precio del libro: ");
        if (!int.TryParse(Console.ReadLine(), out int precio))
        {
            Console.WriteLine("Precio inválido. Ingrese un número entero.");
            return;
        }

        var nuevoLibro = new Libro { Titulo = titulo, Cantidad = cantidad, Precio = precio };
        contexto.Libros.Add(nuevoLibro);
        contexto.SaveChanges();
        
        Console.WriteLine("Libro agregado correctamente.");
    }

    static void ActualizarLibro(ContextoLibreria contexto)
    {
        Console.Write("Ingrese el ID del libro a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido. Ingrese un número entero.");
            return;
        }

        var libro = contexto.Libros.Find(id);
        if (libro == null)
        {
            Console.WriteLine("No se encontró ningún libro con ese ID.");
            return;
        }

        Console.WriteLine($"Libro encontrado: ID: {libro.Id}, Título: {libro.Titulo}, Cantidad: {libro.Cantidad}, Precio: {libro.Precio}");
        Console.WriteLine("Ingrese los nuevos valores (deje en blanco para mantener el valor actual):");

        Console.Write("Nueva Cantidad: ");
        var nuevaCantidadInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevaCantidadInput) && int.TryParse(nuevaCantidadInput, out int nuevaCantidad))
        {
            libro.Cantidad = nuevaCantidad;
        }

        Console.Write("Nuevo Precio: ");
        var nuevoPrecioInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevoPrecioInput) && int.TryParse(nuevoPrecioInput, out int nuevoPrecio))
        {
            libro.Precio = nuevoPrecio;
        }

        contexto.SaveChanges();
        Console.WriteLine("Libro actualizado correctamente.");
    }

    static void EliminarLibro(ContextoLibreria contexto)
    {
        Console.Write("Ingrese el ID del libro a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido. Ingrese un número entero.");
            return;
        }

        var libro = contexto.Libros.Find(id);
        if (libro == null)
        {
            Console.WriteLine("No se encontró ningún libro con ese ID.");
            return;
        }

        contexto.Libros.Remove(libro);
        contexto.SaveChanges();
        
        Console.WriteLine("Libro eliminado correctamente.");
    }
}
