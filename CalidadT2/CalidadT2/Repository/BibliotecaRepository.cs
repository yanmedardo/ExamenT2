using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{

    public interface IBibliotecaRepository
    {
        public List<Biblioteca> Listar(Usuario user);
        public Biblioteca Guardar(Biblioteca biblioteca);
        public Biblioteca Detalle(Usuario usuario, int libroId);
        public void CambiarEstado(Biblioteca biblioteca, int nuevoEstado);
    }

    public class BibliotecaRepository : IBibliotecaRepository
    {
        private AppBibliotecaContext context;

        public BibliotecaRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }

        public List<Biblioteca> Listar(Usuario user)
        {
            return context.Bibliotecas
                .Include(o => o.Libro.Autor)
                .Include(o => o.Usuario)
                .Where(o => o.UsuarioId == user.Id)
                .ToList();
        }

        public Biblioteca Guardar(Biblioteca biblioteca)
        {
            context.Bibliotecas.Add(biblioteca);
            context.SaveChanges();
            return biblioteca;
        }

        public Biblioteca Detalle(Usuario usuario, int libroId)
        {
            return context.Bibliotecas.Where(o => o.LibroId == libroId && o.UsuarioId == usuario.Id)
                .FirstOrDefault();
        }

        public void CambiarEstado(Biblioteca biblioteca, int nuevoEstado)
        {
            biblioteca.Estado = nuevoEstado;
            context.SaveChanges();
        }
    }
}
