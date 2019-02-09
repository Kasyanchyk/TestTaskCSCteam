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
    public class DepartmentController : Controller
    {
        private IRepository<Department> _departments;

        public DepartmentController(IRepository<Department> departments)
        {
            _departments = departments;
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns>Array of departments.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return Ok(_departments.GetAllItems());
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department"></param>
        /// <returns>A newly created department.</returns>
        /// <response code="201">Returns the newly created department.</response>
        /// <response code="400">If the department is not valid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Department> Create(Department department)
        {
            if (department == null)
                return BadRequest();

            _departments.Create(department);
            return CreatedAtAction(nameof(Create), department);
        }

        /// <summary>
        /// Deletes the department.
        /// </summary>
        /// <param name="id">Department id.</param>
        /// <returns>Deleted department.</returns>
        /// <response code="200">Returns the deleted department.</response>
        /// <response code="404">If the department with the following id does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Department> Delete(int id)
        {
            var department = _departments.GetAllItems().FirstOrDefault(x => x.Id == id);
            if (department == null)
                return NotFound();

            _departments.Delete(department);
            return Ok(department);
        }

        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <param name="department"></param>
        /// <returns>An updated department.</returns>
        /// <response code="200">Returns the updated department.</response>
        /// <response code="400">If the department is not valid.</response>
        /// <response code="404">If the department with the following id does not exist.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Department> Update(Department department)
        {
            if (!_departments.GetAllItems().Any(x => x.Id == department.Id))
                return NotFound();
            _departments.Update(department);
            return Ok(department);
        }
    }
}
