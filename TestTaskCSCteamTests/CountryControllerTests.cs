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
    public class CountryControllerTests
    {
        Mock<IRepository<Country>> mock = new Mock<IRepository<Country>>();
        Mock<IRepositoryChild<Business, Country>> mockChild = new Mock<IRepositoryChild<Business, Country>>();

        List<Country> listItems = new List<Country>()
        {
            new Country { Id = 1, Name = "usa", Code = "111", Businesses = null },
            new Country { Id = 2, Name = "eng", Code = "222", Businesses = null }
        };

        List<Country> emptyListItems = new List<Country>()
        {
        };

        List<Business> childListItems = new List<Business>()
        {
            new Business { Id = 1, Name = "GIS", Families = null, ParentId = 1 },
            new Business { Id = 2, Name = "CEO", Families = null, ParentId = 1 }
        };

        [Fact]
        public void Get_WhenCalled_ReturnAllItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Country>>(result);
            Assert.Equal(listItems.Count, result.ToList().Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnNoItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(emptyListItems.AsQueryable());
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Country>>(result);
            Assert.Equal(emptyListItems.Count, result.ToList().Count);
        }

        [Fact]
        public void GetById_ValidId_ShouldReturnValidObject()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(1, null)).Returns(listItems[0]);

            var controller = new CountryController(mockChild.Object, mock.Object);
            var result = controller.GetById(1);

            Assert.IsAssignableFrom<ActionResult<Country>>(result);
            Assert.Equal(listItems[0].Id, result.Value.Id);
        }

        public void GetById_InvalidId_ShouldReturnNotFoundResult()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(4, null)).Returns(listItems[0]);

            var controller = new CountryController(mockChild.Object, mock.Object);
            var result = controller.GetById(4);

            Assert.IsAssignableFrom<ActionResult<Country>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetByParentId_ValidId_ShouldReturnValidObject()
        {
            listItems[0].Businesses = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(1, null)).Returns(childListItems.AsQueryable());

            var controller = new CountryController(mockChild.Object, mock.Object);
            var result = controller.GetBusinessesByIdCountry(1);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Business>>>(result);
            Assert.Equal(listItems[0].Businesses.Count, result.Value.ToList().Count);
        }

        [Fact]
        public void GetByParentId_InvalidId_ShouldReturnNotFoundResult()
        {
            listItems[0].Businesses = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(4, null)).Returns(childListItems.AsQueryable());

            var controller = new CountryController(mockChild.Object, mock.Object);
            var result = controller.GetBusinessesByIdCountry(4);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Business>>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            var country3 = new Country { Id = 3, Name = "ua", Code = "333", Businesses = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(country3));
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Create(country3);

            Assert.IsAssignableFrom<ActionResult<Country>>(result);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Create_NullObject_ShouldReturnBadRequest()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(null));
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Create(null);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_ValidObject_ShouldReturnUpdatedObject()
        {
            var country3 = new Country { Id = 1, Name = "ua", Code = "333", Businesses = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(country3));
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Update(country3);

            Assert.IsAssignableFrom<ActionResult<Country>>(result);
            Assert.Equal(country3.Id, result.Value.Id);
        }

        [Fact]
        public void Update_NotExistObject_ShouldReturnNotFound()
        {
            var country3 = new Country { Id = 3, Name = "ua", Code = "333", Businesses = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(country3));
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Update(country3);

            Assert.IsAssignableFrom<ActionResult<Country>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ValidId_ShouldReturnDeletedObject()
        {
            var country1 = new Country { Id = 1, Name = "usa", Code = "111", Businesses = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(country1));
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Delete(1);

            Assert.IsAssignableFrom<ActionResult<Country>>(result);
            Assert.Equal(country1.Id, result.Value.Id);
        }

        [Fact]
        public void Delete_NotExistId_ShouldReturnNotFoundResult()
        {
            var country1 = new Country { Id = 3, Name = "usa", Code = "111", Businesses = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(country1));
            var controller = new CountryController(mockChild.Object, mock.Object);

            var result = controller.Delete(4);

            Assert.IsAssignableFrom<ActionResult<Country>>(result);
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
