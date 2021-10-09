using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalidadT2.Constantes;
using CalidadT2.Models;
using CalidadT2.Repository;
using CalidadT2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalidadT2.Controllers
{
    [Authorize]
    public class BibliotecaController : Controller
    {
        private readonly IUsuarioRepository userRepository;
        private readonly ICookieAuthService cookie;
        private readonly IBibliotecaRepository libraryRepository;

        public BibliotecaController(IBibliotecaRepository library, IUsuarioRepository user, ICookieAuthService cookie)
        {
            this.cookie = cookie;
            this.userRepository = user;
            this.libraryRepository = library;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Usuario user = LoggedUser();
            var model = libraryRepository.Listar(user);
            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int libro)
        {
            Usuario user = LoggedUser();

            var biblioteca = new Biblioteca
            {
                LibroId = libro,
                UsuarioId = user.Id,
                Estado = ESTADO.POR_LEER
            };
            libraryRepository.Guardar(biblioteca);

            //TempData["SuccessMessage"] = "Se añádio el libro a su biblioteca";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MarcarComoLeyendo(int libroId)
        {
            Usuario user = LoggedUser();

            var libro = libraryRepository.Detalle(user, libroId);
            libraryRepository.CambiarEstado(libro, ESTADO.LEYENDO);

            // TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarcarComoTerminado(int libroId)
        {
            Usuario user = LoggedUser();

            var libro = libraryRepository.Detalle(user, libroId);
            libraryRepository.CambiarEstado(libro, ESTADO.TERMINADO);

            //TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        public Usuario LoggedUser()
        {
            cookie.SetHttpContext(HttpContext);
            var claim = cookie.GetClaim();
            return userRepository.UserLogued(claim);
        }
    }
}
