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
    public class FamilyControllerTests
    {
        Mock<IRepository<Family>> mock = new Mock<IRepository<Family>>();
        Mock<IRepositoryChild<Offering, Family>> mockChild = new Mock<IRepositoryChild<Offering, Family>>();

        List<Family> listItems = new List<Family>()
        {
            new Family { Id = 1, Name="Data center", Offeringes = null },
            new Family { Id = 2, Name="Cloud", Offeringes = null }
        };

        List<Family> emptyListItems = new List<Family>()
        {
        };

        List<Offering> childListItems = new List<Offering>()
        {
            new Offering{Id = 1, Name="Data storage", ParentId=1 },
            new Offering{Id = 2, Name="Data managament", ParentId=1 }
        };

        [Fact]
        public void Get_WhenCalled_ReturnAllItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Family>>(result);
            Assert.Equal(listItems.Count, result.ToList().Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnNoItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(emptyListItems.AsQueryable());
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Family>>(result);
            Assert.Equal(emptyListItems.Count, result.ToList().Count);
        }

        [Fact]
        public void GetById_ValidId_ShouldReturnValidObject()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(1, null)).Returns(listItems[0]);

            var controller = new FamilyController(mockChild.Object, mock.Object);
            var result = controller.GetById(1);

            Assert.IsAssignableFrom<ActionResult<Family>>(result);
            Assert.Equal(listItems[0].Id, result.Value.Id);
        }

        public void GetById_InvalidId_ShouldReturnNotFoundResult()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(4, null)).Returns(listItems[0]);

            var controller = new FamilyController(mockChild.Object, mock.Object);
            var result = controller.GetById(4);

            Assert.IsAssignableFrom<ActionResult<Family>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetByParentId_ValidId_ShouldReturnValidObject()
        {
            listItems[0].Offeringes = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(1, null)).Returns(childListItems.AsQueryable());

            var controller = new FamilyController(mockChild.Object, mock.Object);
            var result = controller.GetOfferingsByIdFamily(1);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Offering>>>(result);
            Assert.Equal(listItems[0].Offeringes.Count, result.Value.ToList().Count);
        }

        [Fact]
        public void GetByParentId_InvalidId_ShouldReturnNotFoundResult()
        {
            listItems[0].Offeringes = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(4, null)).Returns(childListItems.AsQueryable());

            var controller = new FamilyController(mockChild.Object, mock.Object);
            var result = controller.GetOfferingsByIdFamily(4);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Offering>>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            var fam3 = new Family { Id = 3, Name = "name3", Offeringes = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(fam3));
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Create(fam3);

            Assert.IsAssignableFrom<ActionResult<Family>>(result);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Create_NullObject_ShouldReturnBadRequest()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(null));
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Create(null);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_ValidObject_ShouldReturnUpdatedObject()
        {
            var fam3 = new Family { Id = 1, Name = "name3", Offeringes = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(fam3));
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Update(fam3);

            Assert.IsAssignableFrom<ActionResult<Family>>(result);
            Assert.Equal(fam3.Id, result.Value.Id);
        }

        [Fact]
        public void Update_NotExistObject_ShouldReturnNotFound()
        {
            var fam3 = new Family { Id = 3, Name = "name3", Offeringes = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(fam3));
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Update(fam3);

            Assert.IsAssignableFrom<ActionResult<Family>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ValidId_ShouldReturnDeletedObject()
        {
            var fam3 = new Family { Id = 1, Name = "Data center", Offeringes = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(fam3));
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Delete(1);

            Assert.IsAssignableFrom<ActionResult<Family>>(result);
            Assert.Equal(fam3.Id, result.Value.Id);
        }

        [Fact]
        public void Delete_NotExistId_ShouldReturnNotFoundResult()
        {
            var fam3 = new Family { Id = 3, Name = "name3", Offeringes = null };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(fam3));
            var controller = new FamilyController(mockChild.Object, mock.Object);

            var result = controller.Delete(3);

            Assert.IsAssignableFrom<ActionResult<Family>>(result);
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
