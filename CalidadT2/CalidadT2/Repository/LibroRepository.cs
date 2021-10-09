using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{

    public interface ILibroRepository
    {
        public List<Libro> Listar();
        public Libro Detalle(int id);
        public void ActualizarPuntaje(Comentario comentario);
    }

    public class LibroRepository : ILibroRepository
    {
        private AppBibliotecaContext context;

        public LibroRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }

        public List<Libro> Listar()
        {
            return context.Libros.Include(o => o.Autor).ToList();
        }

        public Libro Detalle(int id)
        {
            return context.Libros.Include("Autor")
                .Include("Comentarios.Usuario")
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public void ActualizarPuntaje(Comentario comentario)
        {
            var libro = Detalle(comentario.LibroId);
            libro.Puntaje = (libro.Puntaje + comentario.Puntaje) / 2;
            context.SaveChanges();
        }
    }
}
