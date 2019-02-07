    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestTaskCSCteam.Models
{
    public class Business : BaseEntity//MenuItem<Country, Family>
    {
        [Required]
        public string Name { get; set; }

        public ICollection<ManyToManyEntity<Country, Business>> CountryBusinesses { get; set; }

        public ICollection<Family> Families { get; set; }
    }
}
