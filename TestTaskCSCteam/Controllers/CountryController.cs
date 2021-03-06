﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class CountryController : Controller
    {
        private IRepository<Country> _countries;

        private IRepositoryChild<Business, Country> _businesses;

        public CountryController(IRepositoryChild<Business, Country> businesses, IRepository<Country> countries)
        {
            _businesses = businesses;
            _countries = countries;
        }

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns>Array of countries.</returns>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return _countries.GetAllItems();
        }

        /// <summary>
        /// Get businesses by id country
        /// </summary>
        /// <returns>Businesses with the following country id</returns>
        /// <response code="200">Returns businesses with the following country id</response>
        /// <response code="404">If the country with the following id does not exist</response>
        [AllowAnonymous]
        [HttpGet("business/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Business>> GetBusinessesByIdCountry(int id)
        {
            if (!_countries.GetAllItems().Any(x => x.Id == id))
                return NotFound();
            var businesses = _businesses.GetItemsByParentId(id);
            return businesses.ToList();
        }

        /// <summary>
        /// Get country by id.
        /// </summary>
        /// <returns>Country with the following id.</returns>
        /// <response code="200">Returns the country with the following id.</response>
        /// <response code="404">If the country with the following id does not exist.</response
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> GetById(int id)
        {
            if (!_countries.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _countries.GetItem(id);
        }

        /// <summary>
        /// Creates a new country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>A newly created country.</returns>
        /// <response code="201">Returns the newly created country.</response>
        /// <response code="400">If the country is not valid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Country> Create(Country country)
        {
            try
            {
                if (country == null)
                    return BadRequest();
                _countries.Create(country);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Create), country);
        }

        /// <summary>
        /// Deletes the country.
        /// </summary>
        /// <param name="id">Country id.</param>
        /// <returns>Deleted country.</returns>
        /// <response code="200">Returns the deleted country.</response>
        /// <response code="404">If the country with the following id does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Country> Delete(int id)
        {
            try
            {
                var country = _countries.GetAllItems().FirstOrDefault(x => x.Id == id);
                if (country == null)
                    return NotFound();
                _countries.Delete(country);
                return country;
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates the country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>An updated country.</returns>
        /// <response code="200">Returns the updated country.</response>
        /// <response code="400">If the country is not valid.</response>
        /// <response code="404">If the country with the following id does not exist.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Country> Update(Country country)
        {
            try
            {
                if (!_countries.GetAllItems().Any(x => x.Id == country.Id))
                    return NotFound();
                _countries.Update(country);
                return country;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
