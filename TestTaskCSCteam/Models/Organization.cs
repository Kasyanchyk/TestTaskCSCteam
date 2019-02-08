using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestTaskCSCteam.Models
{
    public class Organization : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string OrganizationType { get; set; }

        [Required]
        public string Owner { get; set; }

        public ICollection<Country> Countries { get; set; }
    }
}
