using System.ComponentModel.DataAnnotations;

namespace TestTaskCSCteam.Models
{
    public class BaseEntityChild<TParent> :BaseEntity
    {
        public TParent Parent { get; set; }
    }
}