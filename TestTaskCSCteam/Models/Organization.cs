using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [JsonIgnore]
        public ICollection<Country> Countries { get; set; }
    }
}
