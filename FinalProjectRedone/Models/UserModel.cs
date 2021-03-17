using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class UserModel:IdentityUser
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; }

        public string Name { get; set; }
    }
}
