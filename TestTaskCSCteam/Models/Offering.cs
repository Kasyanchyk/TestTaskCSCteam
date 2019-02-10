using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class Offering : BaseEntityChild<Family>
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Department> Departments { get; set; }
    }
}
