using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TestTaskCSCteam.Models
{
    public class Business : BaseEntityChild<Country>
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Family> Families { get; set; }
    }
}
