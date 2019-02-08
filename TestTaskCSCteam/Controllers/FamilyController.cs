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
    public class FamilyController : Controller
    {
        private IRepository<Family> _families;

        private IRepositoryChild<Offering, Family> _offerings;

        public FamilyController(IRepositoryChild<Offering, Family> offerings, IRepository<Family> families)
        {
            _offerings = offerings;
            _families = families;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Offering>> Get()
        {
            return Ok(_families.GetAllItems());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Offering>> GetOfferingsByIdFamily(int id)
        {
            var offerings = _offerings.GetItemsByParentId(id);
            return Ok(offerings);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Family> Create(Family family)
        {
            if (family == null)
                return BadRequest();

            _families.Create(family);
            return Ok(family);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Family> Delete(int id)
        {
            var family = _families.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (family == null)
                return NotFound();

            _families.Delete(family);
            return Ok(family);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Family> Update(Family family)
        {
            if (!_families.GetAllItems().Any(x => x.Id == family.Id))
                return NotFound();
            _families.Update(family);
            return Ok(family);
        }
    }
}
