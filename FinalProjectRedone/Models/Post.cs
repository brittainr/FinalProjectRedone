using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class Post
    {
        private List<Reply> replies = new List<Reply>();
        //setting up a pimary key for story.
        public int PostId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }
        public string Topic { get; set; }
        //setting up foriegn key from user in story. entity framework automatically sets up a relationship between these two tables 
        //when a username of type user is created. it just knows its from user. its magic.
        public virtual UserModel User { get; set; }
        [StringLength(500, MinimumLength = 20)]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public List<Reply> Replies
        {
            get { return replies; }
        }

    }
}
