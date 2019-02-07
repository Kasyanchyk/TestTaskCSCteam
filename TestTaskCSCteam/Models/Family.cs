﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Models
{
    public class Family : MenuItem<Business, Offering>
    {
        [Required]
        public string Name { get; set; }
    }
}
