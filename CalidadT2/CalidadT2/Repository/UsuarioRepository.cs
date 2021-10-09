using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{

    public interface IUsuarioRepository
    {
        public Usuario FindUser(string username, string password);
        public Usuario UserLogued(Claim claim);
    }

    public class UsuarioRepository : IUsuarioRepository
    {

        private AppBibliotecaContext _context;

        public UsuarioRepository(AppBibliotecaContext context)
        {
            _context = context;
        }

        public Usuario FindUser(string username, string password)
        {
            var user = _context.Usuarios.FirstOrDefault(o => o.Username == username && o.Password == password);
            return user;
        }

        public Usuario UserLogued(Claim claim)
        {
            var user = _context.Usuarios.FirstOrDefault(o => o.Username == claim.Value);
            return user;
        }
    }
}
