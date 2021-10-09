using CalidadT2.Controllers;
using CalidadT2.Models;
using CalidadT2.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalidadT2Tests.Controllers
{
    class HomeControllerTests
    {

        [Test]
        public void Index()
        {
            List<Libro> Libros = new List<Libro>();
            var libroRepository = new Mock<ILibroRepository>();

            libroRepository.Setup(o => o.Listar()).Returns(Libros);

            var homeController = new HomeController(libroRepository.Object);
            var guardarCom = homeController.Index();

            Assert.IsInstanceOf<ViewResult>(guardarCom);
        }

    }
}
