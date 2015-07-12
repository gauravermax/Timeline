using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLine.Entaties
{
    public class Suscribe
    {
        //identity of the user 
        public int userid { get; set; }
        //Identity of the Wall user is mapped to
        public int WallId { get; set; }
        //Is the user owner of the wall . As a default user is owner of his/her own wall
        public bool Owner { get; set; }
        //Readonly rights to the wall
        public bool ReadOnly { get; set; }
    }
}
