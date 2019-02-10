using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class Family : BaseEntityChild<Business>
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Offering> Offeringes { get; set; }
    }
}
