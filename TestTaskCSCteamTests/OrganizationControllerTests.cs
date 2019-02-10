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
    public class OrganizationControllerTests
    {
        Mock<IRepository<Organization>> mock = new Mock<IRepository<Organization>>();
        Mock<IRepositoryChild<Country, Organization>> mockChild = new Mock<IRepositoryChild<Country, Organization>>();
        
        List<Organization> listItems = new List<Organization>()
        {
            new Organization{ Id=1, Name="organizationName1", Code="organizationCode1", OrganizationType="organizationType1",
                Owner ="organizationOwner1", Countries=null},
            new Organization{ Id=2, Name="organizationName2", Code="organizationCode2", OrganizationType="organizationType2",
                Owner ="organizationOwner2", Countries=null},
            new Organization{ Id=3, Name="organizationName3", Code="organizationCode3", OrganizationType="organizationType3",
                Owner ="organizationOwner3", Countries=null}
        };

        List<Organization> emptyListItems = new List<Organization>()
        {
        };

        List<Country> childListItems = new List<Country>()
        {
            new Country { Name = "usa", Code = "111", Businesses = null, ParentId=1 },
            new Country { Name = "eng", Code = "222", Businesses = null, ParentId=1 }
        };

        [Fact]
        public void Get_WhenCalled_ReturnAllItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Organization>>(result);
            Assert.Equal(listItems.Count, result.ToList().Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnNoItems()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(emptyListItems.AsQueryable());
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Get();

            Assert.IsAssignableFrom<IEnumerable<Organization>>(result);
            Assert.Equal(emptyListItems.Count, result.ToList().Count);
        }

        [Fact]
        public void GetById_ValidId_ShouldReturnValidObject()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(1, null)).Returns(listItems[0]);

            var controller = new OrganizationController(mockChild.Object, mock.Object);
            var result = controller.GetById(1);

            Assert.IsAssignableFrom<ActionResult<Organization>>(result);
            Assert.Equal(listItems[0].Id, result.Value.Id);
        }

        [Fact]
        public void GetById_InvalidId_ShouldReturnNotFoundResult()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.GetItem(4, null)).Returns(listItems[0]);

            var controller = new OrganizationController(mockChild.Object, mock.Object);
            var result = controller.GetById(4);

            Assert.IsAssignableFrom<ActionResult<Organization>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetByParentId_ValidId_ShouldReturnValidObject()
        {
            listItems[0].Countries = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(1, null)).Returns(childListItems.AsQueryable());

            var controller = new OrganizationController(mockChild.Object, mock.Object);
            var result = controller.GetCountriesByIdOrganization(1);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Country>>>(result);
            Assert.Equal(listItems[0].Countries.Count, result.Value.ToList().Count);
        }

        [Fact]
        public void GetByParentId_InvalidId_ShouldReturnNotFoundResult()
        {
            listItems[0].Countries = childListItems;
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mockChild.Setup(repo => repo.GetItemsByParentId(4, null)).Returns(childListItems.AsQueryable());

            var controller = new OrganizationController(mockChild.Object, mock.Object);
            var result = controller.GetCountriesByIdOrganization(4);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<Country>>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            var organization4 = new Organization
            {
                Id = 4,
                Name = "organizationName4",
                Code = "organizationCode4",
                OrganizationType = "organizationType4",
                Owner = "organizationOwner4",
                Countries = null
            };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(organization4));
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Create(organization4);

            Assert.IsAssignableFrom<ActionResult<Organization>>(result);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Create_NullObject_ShouldReturnBadRequest()
        {
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Create(null));
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Create(null);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_ValidObject_ShouldReturnUpdatedObject()
        {
            var organization4 = new Organization
            {
                Id = 3,
                Name = "organizationName4",
                Code = "organizationCode4",
                OrganizationType = "organizationType4",
                Owner = "organizationOwner4",
                Countries = null
            };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(organization4));
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Update(organization4);

            Assert.IsAssignableFrom<ActionResult<Organization>>(result);
            Assert.Equal(organization4.Id, result.Value.Id);
        }

        [Fact]
        public void Update_NotExistObject_ShouldReturnNotFound()
        {
            var organization4 = new Organization
            {
                Id = 4,
                Name = "organizationName4",
                Code = "organizationCode4",
                OrganizationType = "organizationType4",
                Owner = "organizationOwner4",
                Countries = null
            };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Update(organization4));
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Update(organization4);

            Assert.IsAssignableFrom<ActionResult<Organization>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ValidId_ShouldReturnDeletedObject()
        {
            var org = new Organization
            {
                Id = 1,
                Name = "organizationName1",
                Code = "organizationCode1",
                OrganizationType = "organizationType1",
                Owner = "organizationOwner1",
                Countries = null
            };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(org));
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Delete(1);

            Assert.IsAssignableFrom<ActionResult<Organization>>(result);
            Assert.Equal(org.Id, result.Value.Id);
        }

        [Fact]
        public void Delete_NotExistId_ShouldReturnNotFoundResult()
        {
            var org = new Organization
            {
                Id = 4,
                Name = "organizationName1",
                Code = "organizationCode1",
                OrganizationType = "organizationType1",
                Owner = "organizationOwner1",
                Countries = null
            };
            mock.Setup(repo => repo.GetAllItems(null)).Returns(listItems.AsQueryable());
            mock.Setup(repo => repo.Delete(org));
            var controller = new OrganizationController(mockChild.Object, mock.Object);

            var result = controller.Delete(4);

            Assert.IsAssignableFrom<ActionResult<Organization>>(result);
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
