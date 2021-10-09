using System;
using System.Linq;
using CalidadT2.Models;
using CalidadT2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    public class LibroController : Controller
    {
        private readonly AppBibliotecaContext app;
        private ILibroRepository libroRepository;
        private IComentarioRepository comentarioRepository;
        private IAuthRepository authRepository;

        public LibroController(AppBibliotecaContext app, IAuthRepository authRepository, ILibroRepository libroRepository, IComentarioRepository comentarioRepository)
        {
            this.app = app;
            this.libroRepository = libroRepository;
            this.comentarioRepository = comentarioRepository;
            this.authRepository = authRepository;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = this.libroRepository.Buscar(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddComentario(Comentario comentario)
        {
            Usuario user = LoggedUser();
            comentario.UsuarioId = user.Id;
            comentario.Fecha = DateTime.Now;
            comentarioRepository.AddComentario(comentario);

            var libro = libroRepository.Buscar(comentario.LibroId);
            libroRepository.ActualizarPuntaje(libro, comentario);

            return RedirectToAction("Details", new { id = comentario.LibroId });
        }

        private Usuario LoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = authRepository.GetUserLogged(claim);
            return user;
        }
    }
}
