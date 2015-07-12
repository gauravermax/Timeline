using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLine.Entaties;

namespace TimeLine
{
    public interface IRepository
    {
        

        /// <summary>
        /// Get all the users in the system
        /// </summary>
        /// <returns>list of users</returns>
         List<User> GetAllusers();
       

        /// <summary>
        /// Get all the suscriptions
        /// </summary>
        /// <param name="userid">identity of the user</param>
        /// <returns>list of suscriptions</returns>
         List<Suscribe> GetAllSuscription(int userid);
     
        /// <summary>
        /// Get user owned suscription 
        /// </summary>
        /// <param name="userid">identity of the user </param>
        /// <returns>suscription</returns>
         Suscribe GetOwnerSuscription(int userid);
     
        /// <summary>
        /// Get the Wall of the user
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>Wall</returns>
         Wall GetWall(int userid);


        /// <summary>
        /// Gets the comments of the users
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="ShowMyWalonly">booled to show only comments on owner wall</param>
        /// <returns>list of comments</returns>
         List<Comment> GetComments(int userid,bool ShowMyWalonly = false);
    
        /// <summary>
        /// Save comments
        /// </summary>
        /// <param name="userid">identity of the user</param>
        /// <param name="scomment"></param>
        /// <returns>comments</returns>
         Comment SaveComment(int userid,string scomment);
       
        

        /// <summary>
        /// Save users
        /// </summary>
        /// <param name="name">name of the user</param>
        /// <returns>user</returns>
         User SaveUser(string name);
       


        /// <summary>
        /// Add new suscription
        /// </summary>
        /// <param name="Requestuserid">requester user id</param>
        /// <param name="Suscriptionid">suscription user id</param>
        /// <returns></returns>
         Suscribe AddSuscription(int Requestuserid, int Suscriptionid);
       
    }
}
