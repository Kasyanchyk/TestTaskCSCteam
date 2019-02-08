using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Models
{
    public class Family : BaseEntityChild<Business>
    {
        [Required]
        public string Name { get; set; }

        //public Business Business { get; set; }

        public ICollection<Offering> Offeringes { get; set; }
    }
}
