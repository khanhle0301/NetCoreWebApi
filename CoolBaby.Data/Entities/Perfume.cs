using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoolBaby.Infrastructure.SharedKernel;

namespace CoolBaby.Data.Entities
{
    [Table("Perfumes")]
    public class Perfume : DomainEntity<int>
    {
        public Perfume() { }

        public Perfume(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [StringLength(250)]
        public string Name
        {
            get; set;
        }
    }
}