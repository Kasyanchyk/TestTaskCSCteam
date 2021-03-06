﻿using System;
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
    public class OfferingController : Controller
    {
        private IRepository<Offering> _offerings;

        private IRepositoryChild<Department, Offering> _departments;

        public OfferingController(IRepositoryChild<Department, Offering> departments, IRepository<Offering> offerings)
        {
            _departments = departments;
            _offerings = offerings;
        }

        /// <summary>
        /// Get all offerings
        /// </summary>
        /// <returns>Array of offerings.</returns>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Offering> Get()
        {
            return _offerings.GetAllItems();
        }

        /// <summary>
        /// Get departments by id offering
        /// </summary>
        /// <returns>Departments with the following offering id</returns>
        /// <response code="200">Returns departments with the following offering id</response>
        /// <response code="404">If the offering with the following id does not exist</response>
        [AllowAnonymous]
        [HttpGet("department/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Department>> GetDepartmentsByIdOffering(int id)
        {
            if (!_offerings.GetAllItems().Any(x => x.Id == id))
                return NotFound();
            var departments = _departments.GetItemsByParentId(id);
            return departments.ToList();
        }

        /// <summary>
        /// Get offering by id.
        /// </summary>
        /// <returns>Offering with the following id.</returns>
        /// <response code="200">Returns the offering with the following id.</response>
        /// <response code="404">If the offering with the following id does not exist.</response
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Offering> GetById(int id)
        {
            if (!_offerings.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _offerings.GetItem(id);
        }

        /// <summary>
        /// Creates a new offering.
        /// </summary>
        /// <param name="offering"></param>
        /// <returns>A newly created offering.</returns>
        /// <response code="201">Returns the newly created offering.</response>
        /// <response code="400">If the offering is not valid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Offering> Create(Offering offering)
        {
            try
            {
                if (offering == null)
                    return BadRequest();
                _offerings.Create(offering);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Create), offering);
        }

        /// <summary>
        /// Deletes the offering.
        /// </summary>
        /// <param name="id">Offering id.</param>
        /// <returns>Deleted offering.</returns>
        /// <response code="200">Returns the deleted offering.</response>
        /// <response code="404">If the offering with the following id does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Offering> Delete(int id)
        {
            try
            {
                var offering = _offerings.GetAllItems().FirstOrDefault(x => x.Id == id);
                if (offering == null)
                    return NotFound();

                _offerings.Delete(offering);
                return offering;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates the offering.
        /// </summary>
        /// <param name="offering"></param>
        /// <returns>An updated offering.</returns>
        /// <response code="200">Returns the updated offering.</response>
        /// <response code="400">If the offering is not valid.</response>
        /// <response code="404">If the offering with the following id does not exist.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Offering> Update(Offering offering)
        {
            try
            {
                if (!_offerings.GetAllItems().Any(x => x.Id == offering.Id))
                    return NotFound();
                _offerings.Update(offering);
                return offering;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
