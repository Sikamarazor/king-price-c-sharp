using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kingPriceApi.Entities
{
    [Table("group_permissions")]
    public class GroupPermission
    {
        public Guid GroupsId { get; set; }
        public Guid PermissionsId { get; set; }

        public Group Group { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}