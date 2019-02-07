using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Models
{
    public class Offering : BaseEntity//MenuItem<Family,Department>
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; }

        public Family Family { get; set; }
    }
}
