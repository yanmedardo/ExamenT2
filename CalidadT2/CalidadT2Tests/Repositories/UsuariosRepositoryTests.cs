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
    class UsuariosRepositoryTests
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
            var respository = new UsuarioRepository(mockContext.Object);
            var usuario = respository.Buscar("admin");

            Assert.IsNotNull(usuario);
        }

        [Test]
        public void TestBuscarCaso02()
        {
            var respository = new UsuarioRepository(mockContext.Object);
            var usuario = respository.Buscar("user5");

            Assert.IsNull(usuario);
        }
    }
}
