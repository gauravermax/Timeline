using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLine.Entaties
{
    public class Wall
    {
        //Primery key
        public int WallId { get; set; }
        //List of comments
        public List<Comment> UserComments { get; set; }
    }
}
