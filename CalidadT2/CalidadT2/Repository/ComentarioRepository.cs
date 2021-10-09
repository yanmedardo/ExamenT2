using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{
    public interface IComentarioRepository
    {
        public Comentario AddComentario(Comentario comentario);
    }


    public class ComentarioRepository : IComentarioRepository
    {
        private AppBibliotecaContext context;

        public ComentarioRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }

        public Comentario AddComentario(Comentario comentario)
        {
            context.Comentarios.Add(comentario);
            context.SaveChanges();
            return comentario;
        }
    }
}
