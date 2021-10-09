using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalidadT2.Constantes;
using CalidadT2.Models;
using CalidadT2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    [Authorize]
    public class BibliotecaController : Controller
    {
        private IBibliotecaRepository bibliotecaRepository;
        private IAuthRepository authRepository;

        public BibliotecaController(IBibliotecaRepository bibliotecaRepository, IAuthRepository authRepository)
        {
            this.bibliotecaRepository = bibliotecaRepository;
            this.authRepository = authRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Usuario user = LoggedUser();
            var model = bibliotecaRepository.Listar(user);
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

            bibliotecaRepository.Add(biblioteca);

            TempData["SuccessMessage"] = "Se añádio el libro a su biblioteca";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MarcarComoLeyendo(int libroId)
        {
            Usuario user = LoggedUser();

            var libro = bibliotecaRepository.Buscar(libroId, user);
            bibliotecaRepository.MarcarComoLeyendo(libro);

            TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarcarComoTerminado(int libroId)
        {
            Usuario user = LoggedUser();

            var libro = bibliotecaRepository.Buscar(libroId, user);
            bibliotecaRepository.MarcarComoTerminado(libro);

            TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        private Usuario LoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = authRepository.GetUserLogged(claim);
            return user;
        }
    }
}
