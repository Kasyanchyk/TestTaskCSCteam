using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTaskCSCteam.Models;
using TestTaskCSCteam.Utilities;

namespace TestTaskCSCteam.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private IRepository<Organization> _organizations;

        private IRepositoryChild<Country, Organization> _countries;

        public OrganizationController(IRepositoryChild<Country, 
            Organization> countries, 
            IRepository<Organization> organizations)
        {
            _countries = countries;
            _organizations = organizations;
        }

        /// <summary>
        /// Get all organizations
        /// </summary>
        /// <returns>Array of organizations.</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<Organization>> Get()
        {
            string str = nameof(Create);
            return Ok(_organizations.GetAllItems());
        }

        /// <summary>
        /// Get countries by id organization
        /// </summary>
        /// <returns>Countries with the following organization id</returns>
        /// <response code="200">Returns countries with the following organization id</response>
        /// <response code="404">If the organization with the following id does not exist</response>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Country>> GetCountriesByIdOrganization(int id)
        {
            var countries = _countries.GetItemsByParentId(id);  
            return Ok(countries);
        }

        /// <summary>
        /// Creates a new organization.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns>A newly created organization.</returns>
        /// <response code="201">Returns the newly created organization.</response>
        /// <response code="400">If the organization is not valid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Organization> Create(Organization organization)
        {
            if (organization == null)
                return BadRequest();
            _organizations.Create(organization);
            return CreatedAtAction(nameof(Create),organization);
        }


        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="id">Organization id.</param>
        /// <returns>Deleted organization.</returns>
        /// <response code="200">Returns the deleted organization.</response>
        /// <response code="404">If the organization with the following id does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Organization> Delete(int id)
        {
            var organization = _organizations.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (organization == null)
                return NotFound();

            _organizations.Delete(organization);
            return Ok(organization);
        }

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns>An updated organization.</returns>
        /// <response code="200">Returns the updated organization.</response>
        /// <response code="400">If the organization is not valid.</response>
        /// <response code="404">If the organization with the following id does not exist.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Organization> Update(Organization organization)
        {
            if (!_organizations.GetAllItems().Any(x => x.Id == organization.Id))
                return NotFound();
            _organizations.Update(organization);
            return Ok(organization);
        }
    }
}
