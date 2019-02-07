using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Models
{
    public class Family : BaseEntity//MenuItem<Business, Offering>
    {
        [Required]
        public string Name { get; set; }

        public Family Business { get; set; }

        public ICollection<Offering> Offeringes { get; set; }
    }
}
