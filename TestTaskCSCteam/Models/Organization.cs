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
        public string OrganiztionType { get; set; }

        [Required]
        public string Owner { get; set; }

        public User User { get; set; }

        public ICollection<ManyToManyEntity<Organization,Country>> OrganizationCountries { get; set; }
    }
}
