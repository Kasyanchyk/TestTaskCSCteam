using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Models
{
    public class ManyToManyEntity<TFirst,TSecond>
    {
        public int TFirstId { get; set; }
        public TFirst TFirstObj { get; set; }

        public int TSecondId { get; set; }
        public TSecond TSecondObj { get; set; }
    }
}
