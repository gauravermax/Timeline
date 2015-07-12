using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLine.Entaties
{
    public class Comment
    {
        //Identity of the comments
        public int id { get; set; }
        //Identity of the user who commented
        public int userid { get; set; }
        //Comments
        public String Comments { get; set; }
        //Date and time of the comment
        public DateTime DateofComment { get; set; }
    }
}
