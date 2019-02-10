using System;
using Xunit;
using Moq;
using TestTaskCSCteam.Controllers;
using TestTaskCSCteam.Utilities;
using TestTaskCSCteam.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskCSCteamTests
{
    public class DepartmentControllerTests
    {
        Mock<IRepository<Department>> mock = new Mock<IRepository<Department>>();

        List<Department> listItems = new List<Department>()
        {
            new Department{ Id=1, Name="departmentName1"},
            new Department{ Id=2, Name="departmentName2"}
        };

        List<Department> emptyListItems = new List<Department>()
        {
        };

        [Fact]
        public void Get_WhenCalled_ReturnAllItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            var controller = new DepartmentController(mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Department>>(result);
            Assert.Equal(listItems.Count, result.ToList().Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnNoItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(emptyListItems.AsQueryable());
            var controller = new DepartmentController(mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Department>>(result);
            Assert.Equal(emptyListItems.Count, result.ToList().Count);
        }

        [Fact]
        public void GetById_ValidId_ShouldReturnValidObject()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(1, null)).Returns(listItems[0]);

            var controller = new DepartmentController(mock.Object);
            var result = controller.GetById(1);

            Assert.IsAssignableFrom<ActionResult<Department>>(result);
            Assert.Equal(listItems[0].Id, result.Value.Id);
        }

        public void GetById_InvalidId_ShouldReturnNotFoundResult()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(4, null)).Returns(listItems[0]);

            var controller = new DepartmentController(mock.Object);
            var result = controller.GetById(4);

            Assert.IsAssignableFrom<ActionResult<Department>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            var department3 = new Department { Id = 3, Name = "departmentName3" };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(department3));
            var controller = new DepartmentController(mock.Object);

            var result = controller.Create(department3);

            Assert.IsAssignableFrom<ActionResult<Department>>(result);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Create_NullObject_ShouldReturnBadRequest()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(null));
            var controller = new DepartmentController(mock.Object);

            var result = controller.Create(null);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_ValidObject_ShouldReturnUpdatedObject()
        {
            var department3 = new Department { Id = 1, Name = "departmentName3" };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(department3));
            var controller = new DepartmentController(mock.Object);

            var result = controller.Update(department3);

            Assert.IsAssignableFrom<ActionResult<Department>>(result);
            Assert.Equal(department3.Id, result.Value.Id);
        }

        [Fact]
        public void Update_NotExistObject_ShouldReturnNotFound()
        {
            var department3 = new Department { Id = 3, Name = "departmentName3" };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(department3));
            var controller = new DepartmentController(mock.Object);

            var result = controller.Update(department3);

            Assert.IsAssignableFrom<ActionResult<Department>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ValidId_ShouldReturnDeletedObject()
        {
            var department1 = new Department { Id = 1, Name = "departmentName1" };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(department1));
            var controller = new DepartmentController(mock.Object);

            var result = controller.Delete(1);

            Assert.IsAssignableFrom<ActionResult<Department>>(result);
            Assert.Equal(department1.Id, result.Value.Id);
        }

        [Fact]
        public void Delete_NotExistId_ShouldReturnNotFoundResult()
        {
            var department3 = new Department { Id = 3, Name = "departmentName3" };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(department3));
            var controller = new DepartmentController(mock.Object);

            var result = controller.Delete(3);

            Assert.IsAssignableFrom<ActionResult<Department>>(result);
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
