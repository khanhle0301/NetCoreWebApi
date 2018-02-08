using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoolBaby.Infrastructure.SharedKernel;

namespace CoolBaby.Data.Entities
{
    [Table("Sizes")]
    public class Size : DomainEntity<int>
    {
        public Size()
        {
        }

        public Size(int id, string name)
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