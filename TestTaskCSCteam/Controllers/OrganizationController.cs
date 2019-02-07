using System;
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
    public class OrganizationController : Controller
    {
        private IRepository<Organization> _organizations;
        private IRepository<Country> _countries;

        public OrganizationController(IRepository<Organization> organizations, IRepository<Country> countries)
        {
            _organizations = organizations;
            _countries = countries;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Organization>> Get()
        {
            return Ok(_organizations.GetAllItems());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Country>> GetById(int id)
        {
            /*var organization = _organizations.GetAllItems()
                .FirstOrDefault(x => x.Id == id);

            var countries = _countries.GetAllItems()
                .Where(x => x.Organizations == organization)
                .Select(c => { c.Organizations = null; return c; })
                .ToList();

            if (!_organizations.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return Ok(countries);*/
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Organization> Create(Organization organization)
        {
            if (organization == null)
                return BadRequest();

            _organizations.Create(organization);
            return Ok(organization);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Organization> Delete(int id)
        {
            var organization = _organizations.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (organization == null)
                return NotFound();

            _organizations.Delete(organization);
            return organization;
        }
    }
}
