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

        public UserController(IRepository<User> users)
        {
            _users = users;
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
            var user = new User
            {
                Name = "name1", Surname = "surname1", Address = "address1", Email = "mail",
                Organizations = new List<Organization>()
                {
                    new Organization{ Name="organizationName1", Code="organizationCode1", OrganiztionType="organizationType1", Owner="organizationOwner1", Children=new List<Country>()
                    {
                        new Country{Name="countryName1",Code="countryCode1", Children=new List<Business>()
                        {
                            new Business{Name="businessName1", Children = new List<Family>()
                            {
                                new Family{Name="familyName1", Children = new List<Offering>()
                                {
                                    new Offering{Name="offeringName1", Children = new List<Department>()
                                    {
                                        new Department{Name="departmentName1"}
                                    } }
                                } }
                            } }
                        } }
                    } }
                }
            };
            _users.Create(user);
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
