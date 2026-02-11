using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kingPriceApi.Entities
{
    [Table("groups")]
    public class Group : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = default!;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }
}