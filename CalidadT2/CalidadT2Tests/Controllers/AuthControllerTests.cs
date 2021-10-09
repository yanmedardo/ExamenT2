using CalidadT2.Models;
using CalidadT2.Repository;
using CalidadT2.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CalidadT2Tests.Controllers
{
    class AuthControllerTests
    {
        [Test]
        public void LoginCheck()
        {
            var usuario = new Usuario();
            usuario.Password = "admin";
            usuario.Username = "admin";

            var cookMock = new Mock<ICookieAuthService>();
            var userMock = new Mock<IUsuarioRepository>();

            userMock.Setup(o => o.FindUser(usuario.Username, usuario.Password)).Returns(usuario);

            var authCont = new AuthController(userMock.Object, cookMock.Object);

            var log = authCont.Login("admin", "admin");

            Assert.IsInstanceOf<RedirectToActionResult>(log);
        }

        [Test]
        public void LoginNoCheck()
        {
            var userMock = new Mock<IUsuarioRepository>();

            var authMock = new Mock<ICookieAuthService>();

            var controller = new AuthController(userMock.Object, authMock.Object);
            var result = controller.Login("admin", "admin1");

            Assert.IsInstanceOf<ViewResult>(result);
        }

    }
}
