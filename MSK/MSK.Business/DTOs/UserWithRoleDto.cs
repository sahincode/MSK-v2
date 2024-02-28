using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs
{
    public class UserWithRoleDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RoleViewDto> Roles { get; set; } = new List<RoleViewDto>();
    }
}
