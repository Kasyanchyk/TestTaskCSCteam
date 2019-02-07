using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestTaskCSCteam.Models
{
    public class Business : MenuItem<Country,Family>
    {
        [Required]
        public string Name { get; set; }
    }
}
