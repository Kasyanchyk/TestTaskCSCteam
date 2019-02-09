using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class BaseEntityChild<TParent> :BaseEntity
    {
        [JsonIgnore]
        public TParent Parent { get; set; }

        [Required]
        public int ParentId { get; set; }
    }
}