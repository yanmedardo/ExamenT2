using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CalidadT2.Models;
using CalidadT2.Repository;

namespace CalidadT2.Controllers
{
    public class HomeController : Controller
    {
        private ILibroRepository libroRepository;

        public HomeController(ILibroRepository libro)
        {
            libroRepository = libro;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = libroRepository.Listar();
            return View(model);
        }
    }
}
