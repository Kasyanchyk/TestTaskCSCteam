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
    public class OfferingControllerTests
    {
        Mock<IRepository<Offering>> mock = new Mock<IRepository<Offering>>();
        Mock<IRepositoryChild<Department, Offering>> mockChild = new Mock<IRepositoryChild<Department, Offering>>();

        List<Offering> listItems = new List<Offering>()
        {
            new Offering { Id = 1, Name="Data storage", Departments = null},
            new Offering { Id = 2, Name="Data managament", Departments = null}
        };

        List<Offering> emptyListItems = new List<Offering>()
        {
        };

        List<Department> childListItems = new List<Department>()
        {
            new Department { Id = 1, Name="departmentName1", ParentId = 1},
            new Department { Id = 2, Name="departmentName2", ParentId = 1}
        };

        [Fact]
        public void Get_WhenCalled_ReturnAllItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Offering>>(result);
            Assert.Equal(listItems.Count, result.ToList().Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnNoItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(emptyListItems.AsQueryable());
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Offering>>(result);
            Assert.Equal(emptyListItems.Count, result.ToList().Count);
        }

        [Fact]
        public void GetById_ValidId_ShouldReturnValidObject()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(1, null)).Returns(listItems[0]);

            var controller = new OfferingController(mockChild.Object, mock.Object);
            var result = controller.GetById(1);

            Assert.IsAssignableFrom<ActionResult<Offering>>(result);
            Assert.Equal(listItems[0].Id, result.Value.Id);
        }

        [Fact]
        public void GetById_InvalidId_ShouldReturnNotFoundResult()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(4, null)).Returns(listItems[0]);

            var controller = new OfferingController(mockChild.Object, mock.Object);
            var result = controller.GetById(4);

            Assert.IsAssignableFrom<ActionResult<Offering>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetByParentId_ValidId_ShouldReturnValidObject()
        {
            listItems[0].Departments = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(1, null)).Returns(childListItems.AsQueryable());

            var controller = new OfferingController(mockChild.Object, mock.Object);
            var result = controller.GetDepartmentsByIdOffering(1);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Department>>>(result);
            Assert.Equal(listItems[0].Departments.Count, result.Value.ToList().Count);
        }

        [Fact]
        public void GetByParentId_InvalidId_ShouldReturnNotFoundResult()
        {
            listItems[0].Departments = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(4, null)).Returns(childListItems.AsQueryable());

            var controller = new OfferingController(mockChild.Object, mock.Object);
            var result = controller.GetDepartmentsByIdOffering(4);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Department>>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            var offering3 = new Offering { Id = 3, Name = "name3", Departments = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(offering3));
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Create(offering3);

            Assert.IsAssignableFrom<ActionResult<Offering>>(result);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Create_NullObject_ShouldReturnBadRequest()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(null));
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Create(null);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_ValidObject_ShouldReturnUpdatedObject()
        {
            var offering3 = new Offering { Id = 1, Name = "name3", Departments = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(offering3));
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Update(offering3);

            Assert.IsAssignableFrom<ActionResult<Offering>>(result);
            Assert.Equal(offering3.Id, result.Value.Id);
        }

        [Fact]
        public void Update_NotExistObject_ShouldReturnNotFound()
        {
            var offering3 = new Offering { Id = 3, Name = "name3", Departments = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(offering3));
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Update(offering3);

            Assert.IsAssignableFrom<ActionResult<Offering>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ValidId_ShouldReturnDeletedObject()
        {
            var offering1 = new Offering { Id = 1, Name = "Data storage", Departments = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(offering1));
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Delete(1);

            Assert.IsAssignableFrom<ActionResult<Offering>>(result);
            Assert.Equal(offering1.Id, result.Value.Id);
        }

        [Fact]
        public void Delete_NotExistId_ShouldReturnNotFoundResult()
        {
            var offering3 = new Offering { Id = 3, Name = "Data storage", Departments = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(offering3));
            var controller = new OfferingController(mockChild.Object, mock.Object);

            var result = controller.Delete(3);

            Assert.IsAssignableFrom<ActionResult<Offering>>(result);
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
