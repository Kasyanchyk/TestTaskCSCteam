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

        private IRepositoryChild<Country, Organization> _countries;

        public OrganizationController(IRepositoryChild<Country, Organization> countries, IRepository<Organization> organizations)
        {
            _countries = countries;
            _organizations = organizations;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Country>> Get()
        {
            return Ok(_organizations.GetAllItems());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Country>> GetCountriesByIdOrganization(int id)
        {
            var countries = _countries.GetItemsByParentId(id);  
            return Ok(countries);
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
            return Ok(organization);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Organization> Update(Organization organization)
        {
            if (!_organizations.GetAllItems().Any(x => x.Id == organization.Id))
                return NotFound();
            _organizations.Update(organization);
            return Ok(organization);
        }
    }
}
