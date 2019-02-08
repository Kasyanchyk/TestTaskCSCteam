using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestTaskCSCteam.Models
{
    public class Country : BaseEntityChild<Organization>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        //public Organization Organization { get; set; }

        public ICollection<Business> Businesses { get; set; }

    }
}
