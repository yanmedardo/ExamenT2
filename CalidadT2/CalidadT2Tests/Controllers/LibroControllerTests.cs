using CalidadT2.Controllers;
using CalidadT2.Models;
using CalidadT2.Repository;
using CalidadT2.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalidadT2Tests.Controllers
{
    class LibroControllerTests
    {

        [Test]
        public void Index()
        {
            List<Biblioteca> Bibliotecas = new List<Biblioteca>();
            var userMock = new Mock<IUsuarioRepository>();
            var libraryMock = new Mock<IBibliotecaRepository>();
            var bookMock = new Mock<ILibroRepository>();
            var cookMock = new Mock<ICookieAuthService>();
            var comentarioMock = new Mock<IComentarioRepository>();

            bookMock.Setup(o => o.Detalle(4)).Returns(new Libro { Id=1});

            var libroController = new LibroController(userMock.Object, comentarioMock.Object,bookMock.Object,cookMock.Object);
            var guardarCom = libroController.Details(5);

            Assert.IsInstanceOf<ViewResult>(guardarCom);
        }


        [Test]
        public void AddComentario()
        {
            List<Biblioteca> Bibliotecas = new List<Biblioteca>();
            var userMock = new Mock<IUsuarioRepository>();
            var libraryMock = new Mock<IBibliotecaRepository>();
            var bookMock = new Mock<ILibroRepository>();
            var cookMock = new Mock<ICookieAuthService>();
            var comentarioMock = new Mock<IComentarioRepository>();

            userMock.Setup(o => o.UserLogued(null)).Returns(new Usuario { Id = 20 });
            comentarioMock.Setup(o => o.Guardar(null));
            bookMock.Setup(o => o.ActualizarPuntaje(null));

            var libroController = new LibroController(userMock.Object, comentarioMock.Object, bookMock.Object, cookMock.Object);
            var guardarCom = libroController.AddComentario(new Comentario { Puntaje=2,Texto="", LibroId=5 });

            Assert.IsInstanceOf<RedirectToActionResult>(guardarCom);
        }
    }
}
