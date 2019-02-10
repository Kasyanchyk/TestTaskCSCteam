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
    public class BusinessControllerTests
    {
        Mock<IRepository<Business>> mock = new Mock<IRepository<Business>>();
        Mock<IRepositoryChild<Family, Business>> mockChild = new Mock<IRepositoryChild<Family, Business>>();

        List<Business> listItems = new List<Business>()
        {
            new Business { Id = 1, Name = "GIS", Families = null },
            new Business { Id = 2, Name = "CEO", Families = null }
        };

        List<Business> emptyListItems = new List<Business>()
        {
        };

        List<Family> childListItems = new List<Family>()
        {
            new Family{ Id=1, Name="Data center", Offeringes = null, ParentId=1 },
            new Family{ Id=2, Name="Cloud", Offeringes = null, ParentId=1 }
        };

        [Fact]
        public void Get_WhenCalled_ReturnAllItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Business>>(result);
            Assert.Equal(listItems.Count, result.ToList().Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnNoItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(emptyListItems.AsQueryable());
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Business>>(result);
            Assert.Equal(emptyListItems.Count, result.ToList().Count);
        }

        [Fact]
        public void GetById_ValidId_ShouldReturnValidObject()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(1, null)).Returns(listItems[0]);

            var controller = new BusinessController(mockChild.Object, mock.Object);
            var result = controller.GetById(1);

            Assert.IsAssignableFrom<ActionResult<Business>>(result);
            Assert.Equal(listItems[0].Id, result.Value.Id);
        }

        [Fact]
        public void GetById_InvalidId_ShouldReturnNotFoundResult()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(4, null)).Returns(listItems[0]);

            var controller = new BusinessController(mockChild.Object, mock.Object);
            var result = controller.GetById(4);

            Assert.IsAssignableFrom<ActionResult<Business>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetByParentId_ValidId_ShouldReturnValidObject()
        {
            listItems[0].Families = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(1, null)).Returns(childListItems.AsQueryable());

            var controller = new BusinessController(mockChild.Object, mock.Object);
            var result = controller.GetFamiliesByIdBusiness(1);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Family>>>(result);
            Assert.Equal(listItems[0].Families.Count, result.Value.ToList().Count);
        }

        [Fact]
        public void GetByParentId_InvalidId_ShouldReturnNotFoundResult()
        {
            listItems[0].Families = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(4, null)).Returns(childListItems.AsQueryable());

            var controller = new BusinessController(mockChild.Object, mock.Object);
            var result = controller.GetFamiliesByIdBusiness(4);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Family>>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            var bus3 = new Business { Id = 3, Name = "name3", Families = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(bus3));
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Create(bus3);

            Assert.IsAssignableFrom<ActionResult<Business>>(result);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Create_NullObject_ShouldReturnBadRequest()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(null));
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Create(null);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_ValidObject_ShouldReturnUpdatedObject()
        {
            var bus3 = new Business { Id = 1, Name = "name3", Families = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(bus3));
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Update(bus3);

            Assert.IsAssignableFrom<ActionResult<Business>>(result);
            Assert.Equal(bus3.Id, result.Value.Id);
        }

        [Fact]
        public void Update_NotExistObject_ShouldReturnNotFound()
        {
            var bus3 = new Business { Id = 3, Name = "name3", Families = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(bus3));
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Update(bus3);

            Assert.IsAssignableFrom<ActionResult<Business>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ValidId_ShouldReturnDeletedObject()
        {
            var bus1 = new Business { Id = 1, Name = "GIS", Families = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(bus1));
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Delete(1);

            Assert.IsAssignableFrom<ActionResult<Business>>(result);
            Assert.Equal(bus1.Id, result.Value.Id);
        }

        [Fact]
        public void Delete_NotExistId_ShouldReturnNotFoundResult()
        {
            var bus3 = new Business { Id = 3, Name = "GIS", Families = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(bus3));
            var controller = new BusinessController(mockChild.Object, mock.Object);

            var result = controller.Delete(4);

            Assert.IsAssignableFrom<ActionResult<Business>>(result);
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
