﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
