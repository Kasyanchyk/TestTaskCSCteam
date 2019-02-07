using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}