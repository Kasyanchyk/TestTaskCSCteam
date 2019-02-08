using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTaskCSCteam.Models;
using TestTaskCSCteam.Utilities;

namespace TestTaskCSCteam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepository<User> _users;
        private IRepository<Organization> _organizations;


        public UserController(IRepository<User> users, 
            IRepository<Organization> organizations)
        {
            _users = users;
            _organizations = organizations;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_users.GetAllItems());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetById(int id)
        {
            if (!_users.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _users.GetItem(id);
        }

        [HttpGet("createTest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> CreateTestUser()
        {
            
            //_countries.Create(country1);
            //_countries.Create(country2);



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
            //_businesses.Create(business1);
            //_businesses.Create(business2);

            var country1 = new Country { Name = "usa", Code = "111", Businesses = new List<Business>() {business1,business2 } };
            var country2 = new Country { Name = "eng", Code = "222", Businesses = new List<Business>() { business1, business2 } };

            var user = new User
            {
                Name = "name1",
                Surname = "surname1",
                Address = "address1",
                Email = "mail"
            };

            var organizations = new List<Organization>()
                    {
                        new Organization{ Name="organizationName1", Code="organizationCode1", OrganizationType="organizationType1", Owner="organizationOwner1", Countries=new List<Country>(){ country1, country2 } },
                        new Organization{ Name="organizationName2", Code="organizationCode2", OrganizationType="organizationType2", Owner="organizationOwner2", Countries=new List<Country>(){ country1, country2 }},
                        new Organization{ Name="organizationName3", Code="organizationCode3", OrganizationType="organizationType3", Owner="organizationOwner3", Countries=new List<Country>(){ country1, country2 }}
                    };
            _organizations.Create(organizations[0]);
            _organizations.Create(organizations[1]);
            _organizations.Create(organizations[2]);
            _users.Create(user);

            /*var orgCountry1 = new OrganizationCountry { Country = country1, Organization = user.Organizations.ToList()[0] };
            var orgCountry2 = new OrganizationCountry { Country = country2, Organization = user.Organizations.ToList()[1] };
            var orgCountry3 = new OrganizationCountry { Country = country2, Organization = user.Organizations.ToList()[2] };

            _organizationCountries.Create(orgCountry1);
            _organizationCountries.Create(orgCountry2);
            _organizationCountries.Create(orgCountry3);


            var countryBusiness1 = new CountryBusiness { Country = country1, Business = business1 };
            var countryBusiness2 = new CountryBusiness { Country = country1, Business = business2 };
            var countryBusiness3 = new CountryBusiness { Country = country2, Business = business2 };

            _countryBusinesses.Create(countryBusiness1);
            _countryBusinesses.Create(countryBusiness2);
            _countryBusinesses.Create(countryBusiness3);*/

            return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<User> Create(User user)
    {
        if (user == null)
            return BadRequest();

        _users.Create(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<User> Delete(int id)
    {
        var user = _users.GetAllItems().FirstOrDefault(x => x.Id == id);
        if (user == null)
            return NotFound();

        _users.Delete(user);
        return user;
    }

}
}
