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

        private IRepositoryChild<Business, Country> _businesses;

        public CountryController(IRepositoryChild<Business, Country> businesses, IRepository<Country> countries)
        {
            _businesses = businesses;
            _countries = countries;
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
        public ActionResult<IEnumerable<Country>> GetBusinessesByIdCountry(int id)
        {
            var businesses = _businesses.GetItemsByParentId(id);
            return Ok(businesses);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Country> Create(Country country)
        {
            if (country == null)
                return BadRequest();

            _countries.Create(country);
            return Ok(country);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> Delete(int id)
        {
            var country = _countries.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (country == null)
                return NotFound();

            _countries.Delete(country);
            return Ok(country);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> Update(Country country)
        {
            if (!_countries.GetAllItems().Any(x => x.Id == country.Id))
                return NotFound();
            _countries.Update(country);
            return Ok(country);
        }
    }
}
