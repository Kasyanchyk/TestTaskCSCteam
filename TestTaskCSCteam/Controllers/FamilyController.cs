using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTaskCSCteam.Models;
using TestTaskCSCteam.Utilities;

namespace TestTaskCSCteam.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FamilyController : Controller
    {
        private IRepository<Family> _families;

        private IRepositoryChild<Offering, Family> _offerings;

        public FamilyController(IRepositoryChild<Offering, Family> offerings, IRepository<Family> families)
        {
            _offerings = offerings;
            _families = families;
        }

        /// <summary>
        /// Get all families
        /// </summary>
        /// <returns>Array of families.</returns>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Family> Get()
        {
            return _families.GetAllItems();
        }

        /// <summary>
        /// Get offerings by id family
        /// </summary>
        /// <returns>Offerings with the following family id</returns>
        /// <response code="200">Returns offerings with the following family id</response>
        /// <response code="404">If the family with the following id does not exist</response>
        [AllowAnonymous]
        [HttpGet("offering/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Offering>> GetOfferingsByIdFamily(int id)
        {
            if (!_families.GetAllItems().Any(x => x.Id == id))
                return NotFound();
            var offerings = _offerings.GetItemsByParentId(id);
            return offerings.ToList();
        }

        /// <summary>
        /// Get family by id.
        /// </summary>
        /// <returns>Family with the following id.</returns>
        /// <response code="200">Returns the family with the following id.</response>
        /// <response code="404">If the family with the following id does not exist.</response
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Family> GetById(int id)
        {
            if (!_families.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _families.GetItem(id);
        }

        /// <summary>
        /// Creates a new family.
        /// </summary>
        /// <param name="family"></param>
        /// <returns>A newly created family.</returns>
        /// <response code="201">Returns the newly created family.</response>
        /// <response code="400">If the family is not valid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Family> Create(Family family)
        {
            try
            {
                if (family == null)
                    return BadRequest();
                _families.Create(family);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Create), family);
        }

        /// <summary>
        /// Deletes the family.
        /// </summary>
        /// <param name="id">Family id.</param>
        /// <returns>Deleted family.</returns>
        /// <response code="200">Returns the deleted family.</response>
        /// <response code="404">If the family with the following id does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Family> Delete(int id)
        {
            var family = _families.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (family == null)
                return NotFound();

            _families.Delete(family);
            return family;
        }

        /// <summary>
        /// Updates the family.
        /// </summary>
        /// <param name="family"></param>
        /// <returns>An updated family.</returns>
        /// <response code="200">Returns the updated family.</response>
        /// <response code="400">If the family is not valid.</response>
        /// <response code="404">If the family with the following id does not exist.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Family> Update(Family family)
        {
            if (!_families.GetAllItems().Any(x => x.Id == family.Id))
                return NotFound();
            _families.Update(family);
            return family;
        }
    }
}
