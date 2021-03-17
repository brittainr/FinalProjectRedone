using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class ReplyVM
    {
        public int PostID { get; set; }    // This identifies the reivew being commented on
        public int Title { get; set; }
        public String ReplyText { get; set; }

    }
}
