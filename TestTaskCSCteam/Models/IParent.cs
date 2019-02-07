using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Models
{
    interface IParent<TParent>
    {
        TParent Parent { get; set; }
    }
}
