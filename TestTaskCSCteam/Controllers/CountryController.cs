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
    public class CountryController : Controller
    {
        private IRepository<Country> _countries;
        private IRepository<Business> _businesses;

        public CountryController(IRepository<Country> countries, IRepository<Business> businesses)
        {
            _countries = countries;
            _businesses = businesses;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Country>> Get()
        {
            return Ok(_countries.GetAllItems());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Business>> GetById(int id)
        {
            /*var countries = _countries.GetAllItems()
                .FirstOrDefault(x => x.Id == id);

            var businesses = _businesses.GetAllItems()
                .Where(x => x.Countries == countries)
                .Select(c => { c.Countries = null; return c; })
                .ToList();

            if (!_countries.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return Ok(countries);*/
            return Ok();
        }

        /*[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Country> Create(Organization organization)
        {
            if (organization == null)
                return BadRequest();

            _organizations.Create(organization);
            return Ok(organization);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> Delete(int id)
        {
            var organization = _organizations.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (organization == null)
                return NotFound();

            _organizations.Delete(organization);
            return organization;
        }*/
    }
}
