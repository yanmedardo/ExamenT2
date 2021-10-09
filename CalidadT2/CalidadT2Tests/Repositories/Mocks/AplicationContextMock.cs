using HelloWeb.Web.Db;
using HelloWeb.Web.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWeb.Tests.Repositories.Mocks
{
    public static class AplicationContextMock
    {
        public static Mock<CalidadAppContext> GetApplicationContextMock()
        {
            IQueryable<User> userData = GetUserData();

            var mockDbSetUser = new Mock<DbSet<User>>();
            mockDbSetUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockDbSetUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockDbSetUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockDbSetUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());
            mockDbSetUser.Setup(m => m.AsQueryable()).Returns(userData);


            IQueryable<Employee> employeeData = GetEmployeeData();

            var mockDbSetEmployee = new Mock<DbSet<Employee>>();
            mockDbSetEmployee.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employeeData.Provider);
            mockDbSetEmployee.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employeeData.Expression);
            mockDbSetEmployee.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employeeData.ElementType);
            mockDbSetEmployee.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employeeData.GetEnumerator());
            mockDbSetEmployee.Setup(m => m.AsQueryable()).Returns(employeeData);


            var mockContext = new Mock<CalidadAppContext>(new DbContextOptions<CalidadAppContext>());
            mockContext.Setup(c => c.Users).Returns(mockDbSetUser.Object);
            mockContext.Setup(c => c.Employees).Returns(mockDbSetEmployee.Object);

            return mockContext;
        }

        private static IQueryable<User> GetUserData()
        {
            return new List<User>
            {
                new User { Id = 1, Username = "admin", Password = "admin", CreateAt = new DateTime(2021, 08,20, 10, 10, 0), UdaptedAt = new DateTime(2021, 08,20, 10, 10, 0), EmployeeId = 1 },
                new User { Id = 2, Username = "user1", Password = "user1", CreateAt = new DateTime(2021, 08,21, 10, 10, 0), UdaptedAt = new DateTime(2021, 08,21, 10, 10, 0) },
                new User { Id = 3, Username = "user2", Password = "user2", CreateAt = new DateTime(2021, 08,22, 10, 10, 0), UdaptedAt = new DateTime(2021, 08,24, 12, 10, 0) },
                new User { Id = 4, Username = "user3", Password = "user3", CreateAt = new DateTime(2021, 08,23, 10, 10, 0), UdaptedAt = new DateTime(2021, 08,25, 10, 10, 0), DeletedAt = new DateTime(2021, 08,25, 10, 10, 0) },
                new User { Id = 5, Username = "user4", Password = "user4", CreateAt = new DateTime(2021, 08,24, 10, 10, 0), UdaptedAt = new DateTime(2021, 08,24, 10, 10, 0) },
                new User { Id = 6, Username = "admin2", Password = "admin2", CreateAt = new DateTime(2021, 08,25, 10, 10, 0), UdaptedAt = new DateTime(2021, 08,25, 15, 10, 0), DeletedAt = new DateTime(2021, 08,25, 15, 10, 0) },
            }.AsQueryable();
        }

        private static IQueryable<Employee> GetEmployeeData()
        {
            return new List<Employee>
            {
                new Employee { 
                    Id = 1, Name = "Luis", 
                    LastName = "Mendoza" ,
                    Users = GetUserData().Where(o => o.EmployeeId == 1).ToList()
                },
               
            }.AsQueryable();
        }
    }
}
