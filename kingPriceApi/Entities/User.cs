using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kingPriceApi.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("email")]
        public required string Email { get; set; }
        [Column("name")]
        public required string Name { get; set; }
        [Column("s_name")]
        public required string Sname { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}