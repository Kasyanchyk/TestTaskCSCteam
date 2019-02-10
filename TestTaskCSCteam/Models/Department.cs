using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class Department : BaseEntityChild<Offering>
    {
        [Required]
        public string Name { get; set; }
    }
}
