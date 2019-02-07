using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace TestTaskCSCteam.Models
{
    public class MenuItem<TPatent,TChild> : BaseEntity
    {
        public TPatent Parent { get; set; }

        public ICollection<TChild> Children { get; set; }
    }
}
