using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTaskCSCteam.Models;
using TestTaskCSCteam.Utilities;

namespace TestTaskCSCteam.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepository<Organization> _organizations;

        public UserController(IRepository<Organization> organizations)
        {
            _organizations = organizations;
        }

        [HttpGet("create-test-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> CreateTestUser()
        {
            var business1 = new Business
            {
                Name = "GIS",
                Families = new List<Family>()
                    {
                        new Family{Name="Data center", Offeringes = new List<Offering>()
                        {
                            new Offering{Name="Data storage", Departments = new List<Department>()
                            {
                                new Department{Name="departmentName1"}
                            } },
                            new Offering{Name="Data managament", Departments = new List<Department>()
                            {
                                new Department{Name="departmentName2"}
                            } }
                        } },
                        new Family{Name="Cloud", Offeringes = new List<Offering>()
                        {
                            new Offering{Name="Biz cloud", Departments = new List<Department>()
                            {
                                new Department{Name="departmentName3"}
                            } },
                            new Offering{Name="Cloud compute", Departments = new List<Department>()
                            {
                                new Department{Name="departmentName4"}
                            } }
                        } }
                    }
            };

            var business2 = new Business
            {
                Name = "CEO",
                Families = new List<Family>()
                    {
                        new Family{Name="Cyber", Offeringes = new List<Offering>()
                        {
                            new Offering{Name="Consulting services", Departments = new List<Department>()
                            {
                                new Department{Name="departmentName5"}
                            } }
                        } }
                    }
            };

            var country1 = new Country { Name = "usa", Code = "111", Businesses = new List<Business>() { business1 } };
            var country2 = new Country { Name = "eng", Code = "222", Businesses = new List<Business>() { business2 } };
            
            var organization1 = new Organization { Name = "organizationName1", Code = "organizationCode1",
                OrganizationType = "organizationType1", Owner = "organizationOwner1",
                Countries = new List<Country>() { country1 } };
            _organizations.Create(organization1);

            var organization2 = new Organization { Name = "organizationName2", Code = "organizationCode2",
                OrganizationType = "organizationType2", Owner = "organizationOwner2",
                Countries = new List<Country>() { country2 } };
            _organizations.Create(organization2);

            return Ok();
        }
    }
}
