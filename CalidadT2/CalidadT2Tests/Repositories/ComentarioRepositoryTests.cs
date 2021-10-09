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
    class ComentarioRepositoryTests
    {
        private Mock<AppBibliotecaContext> mockContext;

        [SetUp]
        public void SetUp()
        {
            mockContext = ApplicationMockContext.GetApplicationContextMock();
        }

        [Test]
        public void TestAddCaso01()
        {
            var respository = new ComentarioRepository(mockContext.Object);

            var comentario = new Comentario()
            {
                Fecha = new DateTime(2021, 08, 20, 10, 10, 0),
                Texto = "Comentario de prueba",
                LibroId = 1,
                UsuarioId = 1,
                Puntaje = 2,
            };

            var comnetarioBd = respository.AddComentario(comentario);

            Assert.IsNotNull(comnetarioBd);
        }

        [Test]
        public void TestAddCaso02()
        {
            var respository = new ComentarioRepository(mockContext.Object);

            var comentario = new Comentario()
            {
                Fecha = new DateTime(2021, 08, 20, 10, 10, 0),
                Texto = "Comentario de prueba",
                LibroId = 1,
                UsuarioId = 1,
                Puntaje = 2,
            };

            var comnetarioBd = respository.AddComentario(comentario);

            Assert.AreEqual(1, comnetarioBd.UsuarioId);
        }
    }
}
