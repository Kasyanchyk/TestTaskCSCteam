using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestTaskCSCteam.Models
{
    public class Country : BaseEntity
        //MenuItem<Organization,Business>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public ICollection<ManyToManyEntity<Organization, Country>> OrganizationCountries { get; set; }

        public ICollection<ManyToManyEntity<Country, Business>> CountryBusinesses { get; set; }
    }
}
