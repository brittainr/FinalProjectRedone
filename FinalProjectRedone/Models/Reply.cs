using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class Reply
    {

        [Key]
        public int ReplyID { get; set; }
        public String ReplyText { get; set; }
        public DateTime ReplyDate { get; set; }
        public UserModel Replier { get; set; }
    }
}
