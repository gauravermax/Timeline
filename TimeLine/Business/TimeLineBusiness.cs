using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLine.Entaties;

namespace TimeLine.Business
{
    //This is the business layer of the application
    class TimeLineBusiness
    {

        IRepository _repo;

         //initialise the repository
        public TimeLineBusiness()
        {
             _repo = new Repository();
        }

        public TimeLineBusiness(IRepository repo)
        {
             _repo = repo;
        }


        /// <summary>
        /// find the user object from user name
        /// </summary>
        /// <param name="username">user name</param>
        /// <returns>User object</returns>
        private User GetUserFromName(string username)
        {
            //Get the user id from user
            //Search the Susribe list with user id and owner flag true
            //Get all the comments from the timeline id fetched and return
            User user = _repo.GetAllusers().Find(x => x.username == username);

            // List<Suscribe> sus = _repo.GetSuscription(usr.userID);

          

            return user;
        }


        /// <summary>
        /// Find the user object from user id
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>user object</returns>
        private User GetUserFromid(int userid)
        {
            //Get the user id from user
            //Search the Susribe list with user id and owner flag true
            //Get all the comments from the timeline id fetched and return
            User user = _repo.GetAllusers().Find(x => x.userID == userid);

            // List<Suscribe> sus = _repo.GetSuscription(usr.userID);

            return user;
        }

        /// <summary>
        /// Read the comments at user wall or the suscribed walls
        /// </summary>
        /// <param name="username">requeted user</param>
        /// <param name="Mywall">boolean flag true mean to return only user wall else return all the suscribed walls</param>
        /// <returns>string of comments</returns>
        public string ReadComment(string username,Boolean Mywall = false)
        {
            User user = GetUserFromName(username);

            if (user == null)
            {
                user = _repo.SaveUser(username);
            }

            List<Comment> _comments = _repo.GetComments(user.userID,Mywall);

            string str= String.Empty;
        
            foreach(Comment cmt in _comments)
            {
                str += GetUserFromid(cmt.userid).username + " said :" + cmt.Comments + " .. " + GetTimestamp(cmt.DateofComment) +" \n ";
            }
        
            return str;
        }

        /// <summary>
        /// Number of minutes while the comment was post
        /// </summary>
        /// <param name="DateofComment">Date time of the comment</param>
        /// <returns>number of minutes</returns>
        private string GetTimestamp(DateTime DateofComment)
        {
            int Day = System.DateTime.Now.Subtract(DateofComment).Days * 24 * 60;
            int hours = System.DateTime.Now.Subtract(DateofComment).Hours * 60;
            int min = System.DateTime.Now.Subtract(DateofComment).Minutes;
            int seconds = System.DateTime.Now.Subtract(DateofComment).Seconds;

            int totalmin = Day + hours + min;
            if (totalmin == 0)
            {
                 return totalmin + ":" + seconds + " seconds ago.";

            }
            else
            {
                return totalmin + ":" + seconds + " minutes ago.";

            }

           

        }

           
        /// <summary>
        /// Method to write on the wall
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="strComments">comment to write on wall</param>
        public bool WriteOnMyTimeline(string username,string strComments)
        { 
            //Get the user id
            User user = GetUserFromName(username);

            //If user not present then create new 
            if (user == null)
            {
                user = _repo.SaveUser(username);
            }
            //Get the owner suscription id

            Comment cmt = _repo.SaveComment(user.userID,strComments);
            //Get the Wall id
            //Write Comments

            return cmt == null ? false : true;
        }

        /// <summary>
        /// Suscribe a user to view other user wall
        /// </summary>
        /// <param name="username">susciber user name</param>
        /// <param name="SscribetoUser">suscribed user name</param>
        public bool SuscribeUserTimeline(string username,string SscribetoUser)
        { 
        //Get the SctibeToUser orginal Wall id
        //

            //Get the user id 
            User usrRequest = GetUserFromName(username);
            //Get the user id
            User usrSuscribeTo = GetUserFromName(SscribetoUser);

            if(usrRequest == null || usrSuscribeTo == null)
            {
                return false;

            }
            //Get the Owner Suscription id of the Suscribe to user
            //Entry new the suscription table
            Suscribe suscribe = _repo.AddSuscription(usrRequest.userID, usrSuscribeTo.userID);

            return suscribe == null ? false : true;
           

           
        }
    }
}
