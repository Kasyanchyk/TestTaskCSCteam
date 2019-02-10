using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
