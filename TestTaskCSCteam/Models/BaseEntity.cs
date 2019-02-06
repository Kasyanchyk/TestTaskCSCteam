using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
