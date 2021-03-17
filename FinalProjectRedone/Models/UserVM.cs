using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class UserVM
    {
        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
