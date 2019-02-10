using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class Offering : BaseEntityChild<Family>//MenuItem<Family,Department>
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Department> Departments { get; set; }
    }
}
