using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoolBaby.Infrastructure.SharedKernel;

namespace CoolBaby.Data.Entities
{
    [Table("Colors")]
    public class Color : DomainEntity<int>
    {
        public Color()
        {
        }

        public Color(int id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }

        [StringLength(250)]
        public string Name
        {
            get; set;
        }

        [StringLength(250)]
        public string Code { get; set; }
    }
}