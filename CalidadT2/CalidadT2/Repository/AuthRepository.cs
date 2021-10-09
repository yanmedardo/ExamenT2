using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{
    public interface IAuthRepository
    {
        public Usuario GetUserLogged(Claim claim);
    }

    public class AuthRepository : IAuthRepository
    {
        private AppBibliotecaContext context;
        private IUsuarioRepository usuarioRepository;

        public AuthRepository(AppBibliotecaContext context, IUsuarioRepository usuarioRepository)
        {
            this.context = context;
            this.usuarioRepository = usuarioRepository;
        }

        public Usuario GetUserLogged(Claim claim)
        {
            var usuario = usuarioRepository.Buscar(claim.Value);
            return usuario;
        }
    }
}
