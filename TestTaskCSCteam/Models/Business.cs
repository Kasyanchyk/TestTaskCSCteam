    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestTaskCSCteam.Models
{
    public class Business : BaseEntityChild<Country>//MenuItem<Country, Family>
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Family> Families { get; set; }

        //public Country Country { get; set; }
    }
}
