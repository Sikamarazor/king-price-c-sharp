using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kingPriceApi.Entities
{
    [Table("permissions")]
    public class Permission : BaseEntity
    {   
        [Column("name")]
        public required string Name { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}