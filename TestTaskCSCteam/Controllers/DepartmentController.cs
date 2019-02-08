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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return Ok(_departments.GetAllItems());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Department> Create(Department department)
        {
            if (department == null)
                return BadRequest();

            _departments.Create(department);
            return Ok(department);
        }

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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Department> Update(Department department)
        {
            if (!_departments.GetAllItems().Any(x => x.Id == department.Id))
                return NotFound();
            _departments.Update(department);
            return Ok(department);
        }
    }
}
