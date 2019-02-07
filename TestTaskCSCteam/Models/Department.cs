using SoftServe.ITAcademy.BackendDubbingProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Models
{
    public class Department : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public Offering Offering { get; set; }
    }
}
