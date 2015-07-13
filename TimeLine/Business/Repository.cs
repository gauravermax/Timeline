using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLine.Entaties;

namespace TimeLine.Business
{
    public class Repository : IRepository
    {
        private  List<User> users;
        private  List<Wall> lstWalls;
        private  List<Suscribe> lstsuscription;
      

        //Constructor to fill some predefine data
        public Repository()
        {
            users = new List<User>();
            User _user = new User();
            _user.userID = 1;
            _user.username = "Laura";
            // User _user = new User();
            users.Add(_user);
            //Create a record in Wall

         // private  List<Comment> lstComments;
            List<Comment> lstComments = new List<Comment>();


            Comment comment = new Comment() { id = 1, Comments = "This is me laura", userid = 1,DateofComment=System.DateTime.Now.AddDays(-1) };
            lstComments.Add(comment);

            comment = new Comment() { id = 1, Comments = "This is great", userid = 1, DateofComment = System.DateTime.Now.AddDays(-2) };



            lstComments.Add(comment);


            lstWalls = new List<Wall>();
            Wall _wall = new Wall()
            {
                WallId = 1,
                UserComments = lstComments
            };
            lstWalls.Add(_wall);

            //Create a record in Suscribe
            lstsuscription = new List<Suscribe>();

            Suscribe _sus = new Suscribe()
            {
                Owner = true,
                userid = 1,
                WallId = 1
            };

            lstsuscription.Add(_sus);

        }

        /// <summary>
        /// Get all the users in the system
        /// </summary>
        /// <returns>list of users</returns>
        public List<User> GetAllusers()
        {
          return users;
           
        }

        /// <summary>
        /// Get all the suscriptions
        /// </summary>
        /// <param name="userid">identity of the user</param>
        /// <returns>list of suscriptions</returns>
        public List<Suscribe> GetAllSuscription(int userid)
        {
            List<Suscribe> _lScribe = lstsuscription.FindAll(x => x.userid == userid);

            return _lScribe;
        }

        /// <summary>
        /// Get user owned suscription 
        /// </summary>
        /// <param name="userid">identity of the user </param>
        /// <returns>suscription</returns>
        public Suscribe GetOwnerSuscription(int userid)
        {
            Suscribe _lScribe = lstsuscription.Find(x => x.userid == userid && x.Owner == true);

            return _lScribe;
        }

        /// <summary>
        /// Get the Wall of the user
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>Wall</returns>
        public Wall GetWall(int userid)
        {
            Suscribe sus = GetOwnerSuscription(userid);
            Wall wall = lstWalls.Find(x => x.WallId == sus.WallId);
            
            return wall;
        }

        /// <summary>
        /// Gets the comments of the users
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="ShowMyWalonly">booled to show only comments on owner wall</param>
        /// <returns>list of comments</returns>
        public List<Comment> GetComments(int userid,bool ShowMyWalonly = false)
        {
            List<Comment> _lstComments = new List<Comment>();
            List<Suscribe> _suslist;
            if (ShowMyWalonly)
            {
                _suslist = new List<Suscribe>();
                _suslist.Add(GetOwnerSuscription(userid));
            }
            else
            {
                 _suslist = GetAllSuscription(userid);
            }

            foreach(Suscribe sus in _suslist)
            {
                //Get the Wall id's
                List<Wall> _lstWall = lstWalls.FindAll(x => x.WallId == sus.WallId);
                foreach(Wall _wall in _lstWall)
                {
                   
                   foreach (Comment cmt in _wall.UserComments)
                    _lstComments.Add(cmt);
                }
                
            }
            //Get the Comments
                return _lstComments.OrderBy(x=>x.DateofComment).ToList();
        }

        /// <summary>
        /// Save comments
        /// </summary>
        /// <param name="userid">identity of the user</param>
        /// <param name="scomment"></param>
        /// <returns>comments</returns>
        public Comment SaveComment(int userid,string scomment)
        {
            //int newlastid = lstComments.Last().id + 1;

            Comment comment = new Comment() { id = userid, Comments = scomment, userid = userid,DateofComment=System.DateTime.Now };
            //lstComments.Add(comment);

            //find the wall

            Wall _wall = GetWall(userid);
            _wall.UserComments.Add(comment);
          //  lstWalls.Add(_wall);

            return comment;
        }

        /// <summary>
        /// Save users
        /// </summary>
        /// <param name="name">name of the user</param>
        /// <returns>user</returns>
        public User SaveUser(string name)
        {
            int newlastid = users.Last().userID + 1;
            User _user = new User() {
                userID = newlastid,
                username = name};


            users.Add(_user);

            //Create a record in Suscribe
           // suscription = new List<Suscribe>();

            Suscribe _sus = new Suscribe()
            {
                Owner = true,
                userid = newlastid,
                WallId = newlastid
            };

            lstsuscription.Add(_sus);


            //Walls = new List<Wall>();
            Wall _wall = new Wall()
            {
                WallId = newlastid,
                UserComments = new List<Comment>()
            };
            lstWalls.Add(_wall);

            return _user;


        }


        /// <summary>
        /// Add new suscription
        /// </summary>
        /// <param name="Requestuserid">requester user id</param>
        /// <param name="Suscriptionid">suscription user id</param>
        /// <returns></returns>
        public Suscribe AddSuscription(int Requestuserid, int Suscriptionid)
        {
            Suscribe ownersus = GetOwnerSuscription(Suscriptionid);
            int Wallid = ownersus.WallId;

            Suscribe NewSuscription = new Suscribe() { WallId = Wallid, userid = Requestuserid, Owner = false, ReadOnly = true };

            lstsuscription.Add(NewSuscription);
            return NewSuscription;
        }

       
    }
}
