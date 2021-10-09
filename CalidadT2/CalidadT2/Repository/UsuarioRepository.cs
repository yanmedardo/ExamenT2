using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{

    public interface IUsuarioRepository
    {
        public Usuario IniciarSesion(string username, string password);
        public Usuario Buscar(string username);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private AppBibliotecaContext context;

        public UsuarioRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }

        public Usuario IniciarSesion(string username, string password)
        {
            var usuario = context.Usuarios.Where(o => o.Username == username && o.Password == password).FirstOrDefault();
            return usuario;
        }

        public Usuario Buscar(string username)
        {
            var usuario = context.Usuarios.Where(o => o.Username == username).FirstOrDefault();
            return usuario;
        }
    }
}
