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
    public class BusinessController : Controller
    {
        private IRepository<Business> _businesses;

        private IRepositoryChild<Family, Business> _families;

        public BusinessController(IRepositoryChild<Family, Business> families, IRepository<Business> businesses)
        {
            _families = families;
            _businesses = businesses;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Family>> Get()
        {
            return Ok(_businesses.GetAllItems());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Family>> GetFamiliesByIdBusiness(int id)
        {
            var families = _families.GetItemsByParentId(id);
            return Ok(families);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Business> Create(Business organization)
        {
            if (organization == null)
                return BadRequest();

            _businesses.Create(organization);
            return Ok(organization);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Business> Delete(int id)
        {
            var organization = _businesses.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (organization == null)
                return NotFound();

            _businesses.Delete(organization);
            return Ok(organization);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Business> Update(Business organization)
        {
            if (!_businesses.GetAllItems().Any(x => x.Id == organization.Id))
                return NotFound();
            _businesses.Update(organization);
            return Ok(organization);
        }
    }
}