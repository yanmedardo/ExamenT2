using System;
using System.Linq;
using CalidadT2.Models;
using CalidadT2.Repository;
using CalidadT2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    public class LibroController : Controller
    {
        private readonly IUsuarioRepository userRepository;
        private readonly IComentarioRepository comentaryRepository;
        private readonly ILibroRepository bookRepository;
        private readonly ICookieAuthService cookie;

        public LibroController(IUsuarioRepository user, IComentarioRepository comentario, ILibroRepository book, ICookieAuthService cookie)
        {
            this.bookRepository = book;
            this.cookie = cookie;
            this.userRepository = user;
            this.comentaryRepository = comentario;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = bookRepository.Detalle(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddComentario(Comentario comentario)
        {
            Usuario user = LoggedUser();
            comentario.UsuarioId = user.Id;
            comentario.Fecha = DateTime.Now;
            comentaryRepository.Guardar(comentario);
            bookRepository.ActualizarPuntaje(comentario);
            return RedirectToAction("Details", new { id = comentario.LibroId });
        }

        private Usuario LoggedUser()
        {
            cookie.SetHttpContext(HttpContext);
            var claim = cookie.GetClaim();
            return userRepository.UserLogued(claim);
        }
    }
}
