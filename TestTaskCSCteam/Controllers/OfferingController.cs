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
    public class OfferingController : Controller
    {
        private IRepository<Offering> _offerings;

        private IRepositoryChild<Department, Offering> _departments;

        public OfferingController(IRepositoryChild<Department, Offering> departments, IRepository<Offering> offerings)
        {
            _departments = departments;
            _offerings = offerings;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return Ok(_offerings.GetAllItems());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Department>> GetDepartmentsByIdOffering(int id)
        {
            var departments = _departments.GetItemsByParentId(id);
            return Ok(departments);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Offering> Create(Offering offering)
        {
            if (offering == null)
                return BadRequest();

            _offerings.Create(offering);
            return Ok(offering);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Offering> Delete(int id)
        {
            var offering = _offerings.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (offering == null)
                return NotFound();

            _offerings.Delete(offering);
            return Ok(offering);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Offering> Update(Offering offering)
        {
            if (!_offerings.GetAllItems().Any(x => x.Id == offering.Id))
                return NotFound();
            _offerings.Update(offering);
            return Ok(offering);
        }
    }
}
