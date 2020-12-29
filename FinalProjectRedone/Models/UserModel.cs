using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
    }
}
