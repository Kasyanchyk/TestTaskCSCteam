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

        /// <summary>
        /// Get all businesses
        /// </summary>
        /// <returns>Array of businesses.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Business>> Get()
        {
            return Ok(_businesses.GetAllItems());
        }

        /// <summary>
        /// Get families by id business
        /// </summary>
        /// <returns>Families with the following business id</returns>
        /// <response code="200">Returns families with the following business id</response>
        /// <response code="404">If the business with the following id does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Family>> GetFamiliesByIdBusiness(int id)
        {
            var families = _families.GetItemsByParentId(id);
            return Ok(families);
        }

        /// <summary>
        /// Creates a new business.
        /// </summary>
        /// <param name="business"></param>
        /// <returns>A newly created business.</returns>
        /// <response code="201">Returns the newly created business.</response>
        /// <response code="400">If the business is not valid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Business> Create(Business business)
        {
            if (business == null)
                return BadRequest();

            _businesses.Create(business);
            return CreatedAtAction(nameof(Create), business);
        }

        /// <summary>
        /// Deletes the business.
        /// </summary>
        /// <param name="id">Business id.</param>
        /// <returns>Deleted business.</returns>
        /// <response code="200">Returns the deleted business.</response>
        /// <response code="404">If the business with the following id does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Business> Delete(int id)
        {
            var business = _businesses.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (business == null)
                return NotFound();

            _businesses.Delete(business);
            return Ok(business);
        }

        /// <summary>
        /// Updates the business.
        /// </summary>
        /// <param name="business"></param>
        /// <returns>An updated business.</returns>
        /// <response code="200">Returns the updated business.</response>
        /// <response code="400">If the business is not valid.</response>
        /// <response code="404">If the business with the following id does not exist.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Business> Update(Business business)
        {
            if (!_businesses.GetAllItems().Any(x => x.Id == business.Id))
                return NotFound();
            _businesses.Update(business);
            return Ok(business);
        }
    }
}