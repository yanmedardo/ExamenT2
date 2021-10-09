using CalidadT2.Models;
using CalidadT2.Repository;
using CalidadT2.Tests.Repositories.Mock;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalidadT2.Tests.Repositories
{
    class LibroRepositoryTests
    {
        private Mock<AppBibliotecaContext> mockContext;

        [SetUp]
        public void SetUp()
        {
            mockContext = ApplicationMockContext.GetApplicationContextMock();
        }

        [Test]
        public void TestBuscarCaso01()
        {
            var respository = new LibroRepository(mockContext.Object);
            var libro = respository.Buscar(1);

            Assert.IsNotNull(libro);
        }

        [Test]
        public void TestBuscarCaso02()
        {
            var respository = new LibroRepository(mockContext.Object);
            var libro = respository.Buscar(5);

            Assert.IsNull(libro);
        }

        [Test]
        public void TestActualizarPuntajeCaso01()
        {
            var respository = new LibroRepository(mockContext.Object);
            var libro = respository.Buscar(1);
            var comentario = new Comentario()
            {
                Fecha = new DateTime(2021, 08, 20, 10, 10, 0),
                Texto = "Comentario de prueba",
                LibroId = 1,
                UsuarioId = 1,
                Puntaje = 2,
            };

            var libroBd = respository.ActualizarPuntaje(libro, comentario);

            Assert.AreEqual(1, libroBd.Puntaje);
        }
    }
}
