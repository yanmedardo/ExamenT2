using CalidadT2.Constantes;
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
    class BibliotecaRepositoryTests
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
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("admin");
            var respository = new BibliotecaRepository(mockContext.Object);
            var biblioteca = respository.Buscar(1, usuario);

            Assert.IsNotNull(biblioteca);
        }

        [Test]
        public void TestBuscarCaso02()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("user2");
            var respository = new BibliotecaRepository(mockContext.Object);
            var biblioteca = respository.Buscar(7, usuario);

            Assert.IsNull(biblioteca);
        }

        [Test]
        public void TestListarCaso01()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("admin");
            var respository = new BibliotecaRepository(mockContext.Object);
            var bibliotecas = respository.Listar(usuario);

            Assert.AreEqual(1, bibliotecas.Count);
        }

        [Test]
        public void TestListarCaso02()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("user3");
            var respository = new BibliotecaRepository(mockContext.Object);
            var bibliotecas = respository.Listar(usuario);

            Assert.AreEqual(0, bibliotecas.Count);
        }

        [Test]
        public void TestListarCaso03()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("user1");
            var respository = new BibliotecaRepository(mockContext.Object);
            var bibliotecas = respository.Listar(usuario);

            Assert.AreEqual(2, bibliotecas.Count);
        }

        [Test]
        public void TestAddCaso01()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("user1");

            var biblioteca = new Biblioteca
            {
                LibroId = 1,
                UsuarioId = usuario.Id,
                Estado = ESTADO.POR_LEER
            };

            var respository = new BibliotecaRepository(mockContext.Object);
            var bibliotecaBd = respository.Add(biblioteca);

            Assert.IsNotNull(bibliotecaBd);
        }

        [Test]
        public void TestAddCaso02()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("user1");

            var biblioteca = new Biblioteca
            {
                LibroId = 1,
                UsuarioId = usuario.Id,
                Estado = ESTADO.POR_LEER
            };

            var respository = new BibliotecaRepository(mockContext.Object);
            var bibliotecaBd = respository.Add(biblioteca);

            Assert.AreEqual(ESTADO.POR_LEER, bibliotecaBd.Estado);
        }

        [Test]
        public void TestMarcarComoLeyendoCaso01()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("admin");
            var repository = new BibliotecaRepository(mockContext.Object);
            var biblioteca = repository.Buscar(1, usuario);
            repository.MarcarComoLeyendo(biblioteca);
            Assert.AreEqual(ESTADO.LEYENDO, biblioteca.Estado);
        }

        [Test]
        public void TestMarcarComoTerminadoCaso01()
        {
            var userRepository = new UsuarioRepository(mockContext.Object);
            var usuario = userRepository.Buscar("admin");
            var repository = new BibliotecaRepository(mockContext.Object);
            var biblioteca = repository.Buscar(1, usuario);
            repository.MarcarComoTerminado(biblioteca);
            Assert.AreEqual(ESTADO.TERMINADO, biblioteca.Estado);
        }
    }
}
